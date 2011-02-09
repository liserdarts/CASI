'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Runner
Public Class Transaction
    Inherits Base
    
    'Scripts should always be run inside of a transaction

    Public Overrides Sub Test()
        CreateRunner
        Runner.Run
        AssertGreater(Executor.RunLog.Count, 0)
    End Sub

    Protected InTransaction As Boolean

    Protected Overrides Sub Transaction_BeginTransactionEvent(sender As Object, e As EventArgs)
        InTransaction = True
    End Sub

    Protected Overrides Sub Transaction_RollbackTransactionEvent(sender As Object, e As EventArgs)
        InTransaction = False
    End Sub

    Protected Overrides Sub Transaction_CommitTransactionEvent(sender As Object, e As EventArgs)
        InTransaction = False
    End Sub

    Protected Overrides Sub Executor_RunScriptEvent(sender As Object, e As EventArgs)
        AssertIsTrue(InTransaction)
    End Sub

End Class
End Namespace