Namespace SqlExecutor
Public Class Base
    Inherits TestFramework.TestCase
    
    Protected Connection As Common.DbConnection
    Protected Executor As Sql.SqlExecutor

    Public Overrides Sub Test()
        GetConnection

        Executor = New Sql.SqlExecutor
        Executor.Transaction = New Sql.SqlTransactionProvider
        Executor.Transaction.Connection = Connection
        
        Executor.RunScript("Create Table NewTable (Col1 Int)")

        Using Cmd = Connection.CreateCommand
            Cmd.CommandText = "Select * From NewTable"
            Cmd.ExecuteScalar
        End Using
    End Sub
    
    Protected Overridable Sub GetConnection()
        Connection = Data.SqlDatabase.GetConnection("")
    End Sub
End Class
End Namespace