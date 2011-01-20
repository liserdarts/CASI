Namespace Data
Public Class SqlDatabaseManager
    Implements SqlDatabase.ISqlDatabase
    
    Public Function GetConnection(ConnectionString As String) As Common.DbConnection Implements SqlDatabase.ISqlDatabase.GetConnection
        Throw New NotImplementedException
        'ToDo: Implement this    
    End Function

End Class
End Namespace