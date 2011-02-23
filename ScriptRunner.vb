'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class ScriptRunner
    
    Public Property Finder As Finder
    Public Property Recorder As Recorder
    Public Property Sorter As Sorter = New FolderSorter
    Public Property Transaction As TransactionProvider
    Public Property Executor As Executor

    ''' <summary>
    ''' Occurs when the progress of running the scripts has changed.
    ''' </summary>
    Event ProgressChanged As EventHandler(Of ProgressChangedEventArgs)
    Protected Overridable Sub OnProgressChanged(Stage As ProgressChangedEventArgs.ProgressStages, Progress As Double)
        RaiseEvent ProgressChanged(Me, New ProgressChangedEventArgs(Stage, Progress))
    End Sub
    
    Dim Properties As IList(Of ScriptProperty)
    ''' <summary>
    ''' Gets all the <c>ScriptProperty</c> objects used for configuration.
    ''' </summary>
    ''' <returns>If the objects have not been created, the will be. Multiple calls will return the same objects.</returns>
    Public Function GetPropertyObjects() As IList(Of ScriptProperty)
        If Properties Is Nothing Then
            Dim Objects As New List(Of Object)
            Objects.Add(Finder)
            Objects.Add(Recorder)
            Objects.Add(Transaction)
            Objects.Add(Executor)

            Properties = (New ScriptPropertyCreator).GetProperties(Objects)
        End If
        Return Properties
    End Function

    ''' <summary>
    ''' Runs the whole process of finding, running and recording all the scripts.
    ''' </summary>
    Public Sub Run()
        Initialize

        Dim Paths = FilterScripts(GetScripts)

        BeginTransaction

        Try
            RunScripts(Paths)
            CommitTransaction
        Catch Ex As Exception
            Transaction.RollbackTransaction
            Throw
        End Try

        'The recorder may not be affected by the transaction, and can't guarantee will have it's changes reverted on an error
        RecordScripts(Paths)
    End Sub

    ''' <summary>
    ''' Initializes every <c>ScriptProperty</c> object.
    ''' </summary>
    Private Sub Initialize()
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.Initialize, 0)

        Dim Properties = GetPropertyObjects
        For I As Integer = 0 To Properties.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.Initialize, I / Properties.Count)
            Console.WriteLine("Initializing " & Properties(I).GetType.FullName)
            Properties(I).Init
        Next
    End Sub

    ''' <summary>
    ''' Gets the script paths from finder.
    ''' </summary>
    ''' <returns>The paths to all the scripts</returns>
    Private Function GetScripts() As IList(Of String)
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.GetScripts, 0)
        Return Finder.GetAllScripts
    End Function

    ''' <summary>
    ''' Removes scripts that have already run and sorts them
    ''' </summary>
    ''' <param name="Paths">The paths to all the scripts</param>
    ''' <returns>The paths to all the scripts that haven't run in the order they should be run</returns>
    Private Function FilterScripts(Paths As IList(Of String)) As IList(Of String)
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.FilterScripts, 0)
        Dim NewPaths As New List(Of String)

        'Get a list of scripts that have not already run
        For I = 0 To Paths.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.FilterScripts, I / Paths.Count)

            Dim Path = Paths(I)
            If Not Recorder.HasRunScript(Path) Then
                NewPaths.Add(Path)
            End If
        Next

        Return Sorter.Sort(NewPaths)
    End Function

    ''' <summary>
    ''' Begins a transaction using the TransactionProvider
    ''' </summary>
    Private Sub BeginTransaction()
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.BeginTransaction, 0)
        Transaction.BeginTransaction
    End Sub

    ''' <summary>
    ''' Runs the given scripts using the Executor
    ''' </summary>
    ''' <param name="Paths">The paths to all the scripts to run</param>
    Private Sub RunScripts(Paths As IList(Of String))
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.RunScripts, 0)

        For I = 0 To Paths.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.RunScripts, I / Paths.Count)

            Dim Path = Paths(I)
            Console.WriteLine("Running script " & Path)
            Using Stream = Finder.Open(Path)
                Executor.RunScript(Stream)
            End Using
        Next
    End Sub
    
    ''' <summary>
    ''' Commits the transaction started by the TransactionProvider
    ''' </summary>
    Private Sub CommitTransaction()
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.CommitTransaction, 0)

        Transaction.CommitTransaction
    End Sub
    
    ''' <summary>
    ''' Records the given script paths as having run
    ''' </summary>
    ''' <param name="Paths">The paths to all the scripts</param>
    Private Sub RecordScripts(Paths As IList(Of String))
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.RecordScripts, 0)

        For I = 0 To Paths.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.RecordScripts, I / Paths.Count)

            Dim Path = Paths(I)
            Recorder.RecordScript(Path)
        Next
    End Sub
End Class