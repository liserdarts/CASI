Public Class TestTransactionProvider
    Inherits TransactionProvider
    
    Event BeginTransactionEvent As EventHandler
    Event RollbackTransactionEvent As EventHandler
    Event CommitTransactionEvent As EventHandler
    
    Public Overrides Sub BeginTransaction()
        RaiseEvent BeginTransactionEvent(Me, EventArgs.Empty)
    End Sub
    
    Public Overrides Sub RollbackTransaction()
        RaiseEvent RollbackTransactionEvent(Me, EventArgs.Empty)
    End Sub

    Public Overrides Sub CommitTransaction()
        RaiseEvent CommitTransactionEvent(Me, EventArgs.Empty)
    End Sub

End Class