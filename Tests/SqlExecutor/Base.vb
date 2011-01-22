'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Namespace SqlExecutor
Public Class Base
    Inherits TestFramework.TestCase
    
    Protected Connection As TestSqlConnection
    Protected Executor As Sql.SqlExecutor

    Public Overrides Sub Test()
        GetConnection

        Executor = New Sql.SqlExecutor
        Executor.Connection = Connection
        
        Executor.RunScript("Create Table NewTable (Col1 Int)")

        Using Cmd = Connection.Connection.CreateCommand
            Cmd.CommandText = "Select * From NewTable"
            Cmd.ExecuteScalar
        End Using
    End Sub
    
    Protected Overridable Sub GetConnection()
        Connection = GetOverride(Of Overriders.SqlDatabaseOverride).GetConnection
        Connection.Init
    End Sub
End Class
End Namespace