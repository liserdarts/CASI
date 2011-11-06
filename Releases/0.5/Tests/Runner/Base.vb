'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Runner
Public Class Base
    Inherits TestFramework.TestCase

    Protected WithEvents Batch As ScriptBatch
    Protected WithEvents Template As ScriptTemplate
    Protected WithEvents Finder As TestFinder
    Protected WithEvents Transaction As TestTransactionProvider
    Protected WithEvents Executor As TestExecutor
    Protected WithEvents Recorder As TestRecoder
    
    Public Overrides Sub Test()
        CreateRunner
        Run
    End Sub

    Protected Overridable Sub CreateRunner()
        Finder = New TestFinder
        Recorder = New TestRecoder
        Transaction = New TestTransactionProvider
        Executor = New TestExecutor

        Template = New ScriptTemplate
        Template.Finder = Finder
        Template.Recorder = Recorder
        Template.Transaction = Transaction
        Template.Executor = Executor
        
        Batch = New ScriptBatch
        Batch.AddTemplate(Template)
    End Sub

    Protected Overridable Sub Run()
        Try
            Batch.Run
        Finally
            AssertIsFalse(Executor.TestProperty.Open)
        End Try
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