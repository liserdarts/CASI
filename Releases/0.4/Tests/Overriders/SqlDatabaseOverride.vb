'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Overriders
Public Class SqlDatabaseOverride
    Inherits TestFramework.Override

    Protected Overrides Sub StartOverrideInternal()
    End Sub
    Protected Overrides Sub StopOverrideInternal()
    End Sub
    
    Protected Overrides Sub EnterTransactionInternal()
    End Sub
    Protected Overrides Sub RollbackTransactionInternal()
        For Each Connection In Connections
            Connection.DeleteDatabase
        Next
        Connections.Clear
    End Sub

    Dim Connections As New List(Of TestSqlConnection)
    Public Function GetConnection() As TestSqlConnection
        Dim Connection As New TestSqlConnection
        Connections.Add(Connection)
        Return Connection
    End Function

End Class
End Namespace