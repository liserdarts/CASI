'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Runner
Public Class ExecutionException
    Inherits Base
    
    'Must rollback the transaction and not record any script when there is an exception

    Dim RolledBack As Boolean

    Public Overrides Sub Test()
        MyBase.Test
        AssertIsTrue(RolledBack)
        AssertIsEqual(Recorder.RanScripts.Count, 0)
    End Sub

    Protected Overrides Sub CreateRunner()
        MyBase.CreateRunner
        Recorder.RanScripts.Clear
    End Sub
    
    Protected Overrides Sub Run()
        Try
            MyBase.Run
            'Should always get a test exception
            Assert(False)
        Catch Ex As TestException
        End Try
    End Sub

    Protected Overrides Sub Transaction_RollbackTransactionEvent(sender As Object, e As EventArgs)
        RolledBack = True
    End Sub

    Protected Overrides Sub Transaction_CommitTransactionEvent(sender As Object, e As EventArgs)
        AssertIsTrue(False)
    End Sub

    Protected Overrides Sub Executor_RunScriptEvent(sender As Object, e As EventArgs)
        'Wait for the second script to make sure the first wasn't recorded.
        
        Static ScriptCount As Integer
        ScriptCount = ScriptCount + 1
        If ScriptCount > 1 Then
            Throw New TestException
        End If
    End Sub

    Protected Class TestException
        Inherits Exception

        Public Sub New()
            MyBase.New("Simulating a problem with a script")
        End Sub
    End Class
End Class
End Namespace