'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Creates a SQL Server CE database in a temp folder
''' </summary>
Public Class TestSqlConnection
    Inherits Sql.SqlConnection

    Dim Path As String
    
    Public Overrides Sub Init()
        If Connection IsNot Nothing Then Throw New Exception("This connection was already initialized")

        Path = IO.Path.GetTempFileName
        If IO.File.Exists(Path) Then
            IO.File.Delete(Path)
        End If

        Dim ConnectionString = String.Format("Max Database Size = 4000; Data Source = {0}", Path)
        
        Dim Engine As New SqlServerCe.SqlCeEngine(ConnectionString.ToString)
        Engine.CreateDatabase
        
        Dim CeConnection As SqlServerCe.SqlCeConnection
        CeConnection = New SqlServerCe.SqlCeConnection(ConnectionString.ToString)
        CeConnection.Open

        Connection = CeConnection
    End Sub

    Public Sub DeleteDatabase()
        If Connection.State = ConnectionState.Open Then
            Connection.Close
        End If
        If IO.File.Exists(Path) Then
            IO.File.Delete(Path)
        End If
    End Sub
End Class
