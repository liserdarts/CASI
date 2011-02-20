'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Sql
Public Class MSSqlConnection
    Inherits SqlConnection
    
    Public Property Server As String
    Public Property Database As String
    
    Public Property UserName As String
    Public Property Password As String
    
    Public Overrides Sub Init()
        CreateDatabase
        Connection = GetConnection(Database)
    End Sub

    Private Sub CreateDatabase()
        Using Connection = GetConnection("Master")
            Using Cmd = Connection.CreateCommand
                Cmd.CommandText = "Select Count(*) From sysdatabases Where name ='" & Database & "'"
                Dim Exist = Convert.ToInt32(Cmd.ExecuteScalar(Cmd)) > 0
                If Exist Then Return
            End Using

            Using Cmd = Connection.CreateCommand
                Cmd.CommandText = "Create Database [" & Database & "]"
                Cmd.ExecuteNonQuery
            End Using
        End Using
    End Sub

    Private Function GetConnection(DatabaseName As String) As SqlClient.SqlConnection
        Dim ConnectionString As New SqlClient.SqlConnectionStringBuilder
        ConnectionString.Add("uid", UserName)
        ConnectionString.Add("pwd", Password)
        ConnectionString.Add("data source", Server)
        ConnectionString.Add("initial catalog", DatabaseName)
        
        Dim Connection As New SqlClient.SqlConnection(ConnectionString.ToString)
        Connection.Open

        Return Connection
    End Function

End Class
End Namespace