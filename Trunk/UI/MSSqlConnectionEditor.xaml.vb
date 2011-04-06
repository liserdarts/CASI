'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class MSSqlConnectionEditor
    
    Dim WithEvents LConnection As Sql.MSSqlConnection
    Public Property Connection() As Sql.MSSqlConnection
        Get
            Return LConnection
        End Get
        Set
            LConnection = Value
            UpdateEditor
        End Set
    End Property

    Public Property IsReadOnly() As Boolean
        Get
            Return UxServer.IsReadOnly
        End Get
        Set
            UxServer.IsReadOnly = Value
            UxDatabase.IsReadOnly = Value
            UxUsername.IsReadOnly = Value
            UxPassword.IsEnabled = Not Value
        End Set
    End Property

    Private Sub UpdateEditor()
        If Connection Is Nothing Then Return

        UxServer.Text = Connection.Server
        UxDatabase.Text = Connection.Database
        UxUsername.Text = Connection.UserName
        UxPassword.Password = Connection.Password
    End Sub

    Private Sub UxServer_TextChanged(sender As System.Object, e As TextChangedEventArgs) Handles UxServer.TextChanged
        If Connection Is Nothing Then Return
        Connection.Server = UxServer.Text
    End Sub
    
    Private Sub UxDatabase_TextChanged(sender As Object, e As TextChangedEventArgs) Handles UxDatabase.TextChanged
        If Connection Is Nothing Then Return
        Connection.Database = UxDatabase.Text
    End Sub

    Private Sub UxUsername_TextChanged(sender As Object, e As TextChangedEventArgs) Handles UxUsername.TextChanged
        If Connection Is Nothing Then Return
        Connection.UserName = UxUsername.Text
    End Sub

    Private Sub UxPassword_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles UxPassword.PasswordChanged
        If Connection Is Nothing Then Return
        Connection.Password = UxPassword.Password
    End Sub
End Class