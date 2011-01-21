'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

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