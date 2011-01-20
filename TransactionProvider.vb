Public MustInherit Class TransactionProvider
    
    Public MustOverride Sub BeginTransaction()
    Public MustOverride Sub RollbackTransaction()
    Public MustOverride Sub CommitTransaction()

End Class