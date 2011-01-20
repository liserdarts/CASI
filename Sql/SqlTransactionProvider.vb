Namespace Sql
Public Class SqlTransactionProvider
    Inherits TransactionProvider

    Public Property Connection() As Common.DbConnection
    Public Transaction As Common.DbTransaction

    Public Overrides Sub BeginTransaction()
        If Transaction IsNot Nothing Then
            Throw New Exception("A transaction is already open. Nested transactions are not supported.")
        End If
        Transaction = Connection.BeginTransaction
    End Sub

    Public Overrides Sub CommitTransaction()
        Transaction.Commit
        Transaction = Nothing
    End Sub

    Public Overrides Sub RollbackTransaction()
        Transaction.Rollback
        Transaction = Nothing
    End Sub

End Class
End Namespace