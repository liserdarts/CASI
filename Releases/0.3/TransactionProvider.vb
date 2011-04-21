'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Base class to begin commit and rollback transactions
''' </summary>
Public MustInherit Class TransactionProvider
    
    ''' <summary>
    ''' When overridden in a derived class, begins a transaction.
    ''' </summary>
    Public MustOverride Sub BeginTransaction()
    
    ''' <summary>
    ''' When overridden in a derived class, rolls the transaction back.
    ''' </summary>
    Public MustOverride Sub RollbackTransaction()
    
    ''' <summary>
    ''' When overridden in a derived class, commits the transaction.
    ''' </summary>
    Public MustOverride Sub CommitTransaction()

End Class