Namespace Data
Public Class SqlDatabase
    Public Interface ISqlDatabase
        Function GetConnection(ConnectionString As String) As Common.DbConnection
    End Interface

    Shared Sub New()
        Manager = New SqlDatabaseManager
    End Sub
    
    Public Shared Manager As ISqlDatabase

    Public Shared Function GetConnection(ConnectionString As String) As Common.DbConnection
        Return Manager.GetConnection(ConnectionString)
    End Function

End Class
End Namespace