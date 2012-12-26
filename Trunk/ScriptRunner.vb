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
            Dim Paths = GetScripts(Template)
            Paths = FilterScripts(Template, Paths)

            BeginTransaction(Template)

            Try
                RunScripts(Template, Paths)
                CommitTransaction(Template)
            Catch Ex As Exception
                Try
                    Template.Transaction.RollbackTransaction
                Catch Ex2 As Exception
                    Console.WriteLine(Ex2.ToString)
                End Try
                Throw
            End Try

            'The recorder may not be affected by the transaction, and can't guarantee will have it's changes reverted on an error
            RecordScripts(Template, Paths)
        End SyncLock
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
                Template.Executor.RunScript(Path, Stream)
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