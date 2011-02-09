'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public MustInherit Class TransactionProvider
    
    Public MustOverride Sub BeginTransaction()
    Public MustOverride Sub RollbackTransaction()
    Public MustOverride Sub CommitTransaction()

End Class