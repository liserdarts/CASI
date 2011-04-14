'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class Core
    
    Public Declare Function FreeConsole Lib "kernel32.dll"() As Boolean

    Shared WithEvents Runner As ScriptRunner

    Public Shared Sub Main(Args() As String)
        CreateRunner

        Dim CommandRunner As New UI.ConsolePropertyParser(Runner)
        If CommandRunner.Parse(Args) Then 'Run the scripts now
            Runner.Run
        Else 'Display the UI
            FreeConsole
            Dim Frm As New MainWindow(Runner)
            Frm.ShowDialog    
        End If
    End Sub

    Private Shared Sub CreateRunner()
        Dim Finder As New ResourceFinder
        Dim Recorder As New Sql.SqlRecorder
        Dim Executor As New Sql.MSSqlExecutor
        Dim Transaction As New Sql.SqlTransactionProvider

        Finder.Assembly = GetType(Core).Assembly
        Finder.FilePattern = ".*sql$"
        
        Runner = New ScriptRunner
        Runner.Finder = Finder
        Runner.Recorder = Recorder
        Runner.Executor = Executor
        Runner.Transaction = Transaction
    End Sub

    Private Shared Sub Runner_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles Runner.ProgressChanged
        Static LastStageText As String
        Dim StageText As String = e.GetStageText

        If LastStageText <> StageText Then
            Console.WriteLine(StageText)
            LastStageText = StageText
        End If
    End Sub
End Class