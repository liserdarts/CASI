'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

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