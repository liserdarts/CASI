Namespace Overriders
Public Class SqlDatabaseOverride
    Inherits TestFramework.Override
    Implements Data.SqlDatabase.ISqlDatabase

    Protected Overrides Sub StartOverrideInternal()
        Data.SqlDatabase.Manager = Me
    End Sub
    Protected Overrides Sub StopOverrideInternal()
        Data.SqlDatabase.Manager = Nothing
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
    Public Function GetConnection(ConnectionString As String) As Common.DbConnection Implements Data.SqlDatabase.ISqlDatabase.GetConnection
        Dim Path = IO.Path.GetTempFileName
        If IO.File.Exists(Path) Then
            IO.File.Delete(Path)
        End If

        ConnectionString = String.Format("Max Database Size = 4000; Data Source = {0}", Path)
            
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