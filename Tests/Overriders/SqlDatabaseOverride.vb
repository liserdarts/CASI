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
            If Connection.Value.State = ConnectionState.Open Then
                Connection.Value.Close
            End If
            IO.File.Delete(Connection.Key)
        Next
        Connections.Clear
    End Sub

    Dim Connections As New Dictionary(Of String, SqlServerCe.SqlCeConnection)
    Public Function GetConnection() As Common.DbConnection
        Dim Path = IO.Path.GetTempFileName
        If IO.File.Exists(Path) Then
            IO.File.Delete(Path)
        End If

        Dim ConnectionString = String.Format("Max Database Size = 4000; Data Source = {0}", Path)
            
        Dim Engine As New SqlServerCe.SqlCeEngine(ConnectionString)
        Engine.CreateDatabase
        
        Dim Connection As SqlServerCe.SqlCeConnection
        Connection = New SqlServerCe.SqlCeConnection(ConnectionString)
        Connection.Open

        Connections.Add(Path, Connection)

        Return Connection
    End Function

End Class
End Namespace