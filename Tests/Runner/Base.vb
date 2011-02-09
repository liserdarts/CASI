'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Runner
Public Class Base
    Inherits TestFramework.TestCase

    Protected WithEvents Runner As ScriptRunner
    Protected WithEvents Finder As TestFinder
    Protected WithEvents Transaction As TestTransactionProvider
    Protected WithEvents Executor As TestExecutor
    Protected WithEvents Recorder As TestRecoder
    
    Public Overrides Sub Test()
        CreateRunner
        Run
    End Sub

    Protected Overridable Sub CreateRunner()
        Runner = New ScriptRunner
        
        Finder = New TestFinder
        Recorder = New TestRecoder
        Transaction = New TestTransactionProvider
        Executor = New TestExecutor
        Runner.Finder = Finder
        Runner.Recorder = Recorder
        Runner.Transaction = Transaction
        Runner.Executor = Executor
    End Sub

    Protected Overridable Sub Run()
        Runner.Run
    End Sub

    Protected Overridable Sub Transaction_BeginTransactionEvent(sender As Object, e As EventArgs) Handles Transaction.BeginTransactionEvent
    End Sub
    Protected Overridable Sub Transaction_RollbackTransactionEvent(sender As Object, e As EventArgs) Handles Transaction.RollbackTransactionEvent
    End Sub
    Protected Overridable Sub Transaction_CommitTransactionEvent(sender As Object, e As EventArgs) Handles Transaction.CommitTransactionEvent
    End Sub
    Protected Overridable Sub Executor_RunScriptEvent(sender As Object, e As EventArgs) Handles Executor.RunScriptEvent
    End Sub

End Class
End Namespace