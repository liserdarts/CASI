Namespace SqlTransactionProvider
Public Class RollbackTransaction
    Inherits Base
    
    Protected Overrides Sub EndTransaction()
        Transaction.RollbackTransaction
    End Sub

    Protected Overrides Sub ValidateRows(Count As Integer)
        AssertIsEqual(Count, 0)
    End Sub

End Class
End Namespace