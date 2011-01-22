'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Class MainWindow 

    Private Sub UXRun_Click(sender As Object, e As RoutedEventArgs) Handles UXRun.Click
        Dim Finder As New ResourceFinder
        Dim Recorder As New Sql.SqlRecorder
        Dim Executor As New Sql.MSSqlExecutor
        Dim Transaction As New Sql.SqlTransactionProvider

        Finder.Assembly = Me.GetType.Assembly
        Finder.FilePattern = ".*sql"

        Dim Runner As New ScriptRunner
        Runner.Finder = Finder
        Runner.Recorder = Recorder
        Runner.Executor = Executor
        Runner.Transaction = Transaction

        For Each Prop In Runner.GetPropertyObjects
            If TypeOf Prop Is Sql.MSSqlConnection
                Dim Connection As Sql.MSSqlConnection = Prop
                Connection.UserName = UxUserName.Text
                Connection.Password = UxPassword.Text
                Connection.Server = UxServer.Text
                Connection.Database = UxDatabase.Text
            End If
        Next


        Runner.Run
    End Sub

End Class