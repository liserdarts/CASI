'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class ScriptRunner
    
    ''' <summary>
    ''' Occurs when the progress of running the scripts has changed.
    ''' </summary>
    Event ProgressChanged As EventHandler(Of ProgressChangedEventArgs)
    Protected Overridable Sub OnProgressChanged(Stage As ProgressChangedEventArgs.ProgressStages, Progress As Double)
        RaiseEvent ProgressChanged(Me, New ProgressChangedEventArgs(Stage, Progress))
    End Sub

    ''' <summary>
    ''' Runs the whole process of finding, executing and recording all the scripts.
    ''' </summary>
    Public Sub Run(Template As ScriptTemplate)
        SyncLock Me
            Try
                Initialize(Template)

                Dim Paths = GetScripts(Template)
                Paths = FilterScripts(Template, Paths)

                BeginTransaction(Template)

                Try
                    RunScripts(Template, Paths)
                    CommitTransaction(Template)
                Catch Ex As Exception
                    Template.Transaction.RollbackTransaction
                    Throw
                End Try

                'The recorder may not be affected by the transaction, and can't guarantee will have it's changes reverted on an error
                RecordScripts(Template, Paths)
            Finally
                Close(Template)
            End Try
        End SyncLock
    End Sub

    ''' <summary>
    ''' Initializes every <see cref="ScriptProperty" /> object.
    ''' </summary>
    Private Sub Initialize(Template As ScriptTemplate)
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.Initialize, 0)

        Dim Properties = Template.GetPropertyObjects
        For I As Integer = 0 To Properties.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.Initialize, I / Properties.Count)
            Console.WriteLine("Initializing " & Properties(I).GetType.FullName)
            Properties(I).Init
        Next
    End Sub

    ''' <summary>
    ''' Closes every <see cref="ScriptProperty" /> object.
    ''' </summary>
    Private Sub Close(Template As ScriptTemplate)
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.Close, 0)

        Dim Properties = Template.GetPropertyObjects
        For I As Integer = 0 To Properties.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.Close, I / Properties.Count)
            Console.WriteLine("Closing " & Properties(I).GetType.FullName)
            Properties(I).Close
        Next
    End Sub

    ''' <summary>
    ''' Gets the script paths from finder.
    ''' </summary>
    ''' <returns>The paths to all the scripts</returns>
    Private Function GetScripts(Template As ScriptTemplate) As IList(Of String)
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.GetScripts, 0)
        Return Template.Finder.GetAllScripts
    End Function

    ''' <summary>
    ''' Removes scripts that have already run and sorts them
    ''' </summary>
    ''' <param name="Paths">The paths to all the scripts</param>
    ''' <returns>The paths to all the scripts that haven't run in the order they should be run</returns>
    Private Function FilterScripts(Template As ScriptTemplate, Paths As IList(Of String)) As IList(Of String)
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.FilterScripts, 0)
        Dim NewPaths As New List(Of String)

        'Get a list of scripts that have not already run
        For I = 0 To Paths.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.FilterScripts, I / Paths.Count)

            Dim Path = Paths(I)
            If Not Template.Recorder.HasRunScript(Path) Then
                NewPaths.Add(Path)
            End If
        Next

        Return Template.Sorter.Sort(NewPaths)
    End Function

    ''' <summary>
    ''' Begins a transaction using the TransactionProvider
    ''' </summary>
    Private Sub BeginTransaction(Template As ScriptTemplate)
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.BeginTransaction, 0)
        Template.Transaction.BeginTransaction
    End Sub

    ''' <summary>
    ''' Runs the given scripts using the Executor
    ''' </summary>
    ''' <param name="Paths">The paths to all the scripts to run</param>
    Private Sub RunScripts(Template As ScriptTemplate, Paths As IList(Of String))
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.RunScripts, 0)

        For I = 0 To Paths.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.RunScripts, I / Paths.Count)

            Dim Path = Paths(I)
            Console.WriteLine("Running script " & Path)
            Using Stream = Template.Finder.Open(Path)
                Template.Executor.RunScript(Stream)
            End Using
        Next
    End Sub
    
    ''' <summary>
    ''' Commits the transaction started by the TransactionProvider
    ''' </summary>
    Private Sub CommitTransaction(Template As ScriptTemplate)
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.CommitTransaction, 0)

        Template.Transaction.CommitTransaction
    End Sub
    
    ''' <summary>
    ''' Records the given script paths as having run
    ''' </summary>
    ''' <param name="Paths">The paths to all the scripts</param>
    Private Sub RecordScripts(Template As ScriptTemplate, Paths As IList(Of String))
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.RecordScripts, 0)

        For I = 0 To Paths.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.RecordScripts, I / Paths.Count)

            Dim Path = Paths(I)
            Template.Recorder.RecordScript(Path)
        Next
    End Sub
End Class