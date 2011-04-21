'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Sql
''' <summary>
''' Provides a connection of an Microsoft SQL database
''' </summary>
''' <remarks>
''' If the database doesn't exist it will be created
''' </remarks>
Public Class MSSqlConnection
    Inherits SqlConnection
    
    ''' <summary>
    ''' Gets or sets the server name.
    ''' </summary>
    ''' <value>The server name.</value>
    Public Property Server As String
    ''' <summary>
    ''' Gets or sets the database name.
    ''' </summary>
    ''' <value>The database name.</value>
    Public Property Database As String
    
    ''' <summary>
    ''' Gets or sets the username to connection with.
    ''' </summary>
    ''' <value>The username.</value>
    Public Property UserName As String
    ''' <summary>
    ''' Gets or sets the password.
    ''' </summary>
    ''' <value>The password.</value>
    Public Property Password As String
    
    Public Overrides Sub Init()
        CreateDatabase
        Connection = GetConnection(Database)
    End Sub

    Private Sub CreateDatabase()
        Using Connection = GetConnection("Master")
            Using Cmd = Connection.CreateCommand
                Cmd.CommandText = "Select Count(*) From sysdatabases Where name ='" & Database & "'"
                Dim Exist = Convert.ToInt32(Cmd.ExecuteScalar) > 0
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