'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Sql
''' <summary>
''' Uses a SQL database to begin commit and rollback transactions
''' </summary>
Public Class SqlTransactionProvider
    Inherits TransactionProvider
    
    ''' <summary>
    ''' Gets or sets the connection.
    ''' </summary>
    ''' <value>The connection to the database.</value>
    <ScriptPropertyAttribute("e5eecd6f-3481-41ae-bea8-46c96ce1ea5b")> _
    Public Property Connection() As SqlConnection
    
    ''' <summary>
    ''' Begins a transaction.
    ''' </summary>
    Public Overrides Sub BeginTransaction()
        If Connection.Transaction IsNot Nothing Then
            Throw New Exception("A transaction is already open. Nested transactions are not supported.")
        End If
        Connection.Transaction = Connection.Connection.BeginTransaction
    End Sub

    ''' <summary>
    ''' Commits the transaction.
    ''' </summary>
    Public Overrides Sub CommitTransaction()
        Connection.Transaction.Commit
        Connection.Transaction = Nothing
    End Sub

    ''' <summary>
    ''' Rolls the transaction back.
    ''' </summary>
    Public Overrides Sub RollbackTransaction()
        Connection.Transaction.Rollback
        Connection.Transaction = Nothing
    End Sub

End Class
End Namespace