'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Namespace Sql
Public Class SqlTransactionProvider
    Inherits TransactionProvider

    <ScriptPropertyAttribute("e5eecd6f-3481-41ae-bea8-46c96ce1ea5b")> _
    Public Property Connection() As SqlConnection
    
    Public Overrides Sub BeginTransaction()
        If Connection.Transaction IsNot Nothing Then
            Throw New Exception("A transaction is already open. Nested transactions are not supported.")
        End If
        Connection.Transaction = Connection.Connection.BeginTransaction
    End Sub

    Public Overrides Sub CommitTransaction()
        Connection.Transaction.Commit
        Connection.Transaction = Nothing
    End Sub

    Public Overrides Sub RollbackTransaction()
        Connection.Transaction.Rollback
        Connection.Transaction = Nothing
    End Sub

End Class
End Namespace