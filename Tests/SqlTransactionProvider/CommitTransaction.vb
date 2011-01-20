Namespace SqlTransactionProvider
Public Class CommitTransaction
    Inherits Base

    Protected Overrides Sub EndTransaction()
        Transaction.CommitTransaction
    End Sub

    Protected Overrides Sub ValidateRows(Count As Integer)
        AssertIsEqual(Count, 1)
    End Sub

End Class
End Namespace