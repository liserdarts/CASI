'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class Core
    
    Public Declare Function FreeConsole Lib "kernel32.dll"() As Boolean

    Shared WithEvents Batch As ScriptBatch

    Public Shared Sub Main(Args() As String)
        CreateBatch

        Dim CommandRunner As New ConsolePropertyParser(Batch)
        If CommandRunner.Parse(Args) Then 'Run the scripts now
            Batch.Run
        Else 'Display the UI
            FreeConsole
            Dim Frm As New MainWindow(Batch)
            Frm.ShowDialog    
        End If
    End Sub

    Private Shared Sub CreateBatch()
        Dim Finder As New ResourceFinder
        Dim Recorder As New Sql.SqlRecorder
        Dim Executor As New Sql.MSSqlExecutor
        Dim Transaction As New Sql.SqlTransactionProvider

        Finder.Assembly = GetType(Core).Assembly
        Finder.FilePattern = ".*sql$"
        
        Dim Template As New ScriptTemplate
        Template.Finder = Finder
        Template.Recorder = Recorder
        Template.Executor = Executor
        Template.Transaction = Transaction

        Batch = New Scriptbatch
        Batch.AddTemplate(Template)
    End Sub

    Private Shared Sub Runner_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles Batch.ProgressChanged
        Static LastStageText As String
        Dim StageText As String = e.GetStageText

        If LastStageText <> StageText Then
            Console.WriteLine(StageText)
            LastStageText = StageText
        End If
    End Sub
End Class