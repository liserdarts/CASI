Class MainWindow 

    Private Sub UXRun_Click(sender As Object, e As RoutedEventArgs) Handles UXRun.Click
        Dim Finder As New ResourceFinder
        Dim Recorder As New Sql.SqlRecorder
        Dim Executor As New Sql.SqlExecutor
        Executor.Transaction = New Sql.SqlTransactionProvider

        Finder.Assembly = Me.GetType.Assembly
        Finder.FilePattern = ".*sql"

        Dim Runner As New ScriptRunner
        Runner.Finder = Finder
        Runner.Recorder = Recorder
        Runner.Executor = Executor
        Runner.Transaction = Executor.Transaction

        Using Connection = GetConnection(UxDatabase.Text)
            Recorder.Connection = Connection
            Executor.Transaction.Connection = Connection
            Runner.Run
        End Using
    End Sub

    Private Sub CreateDatabase(DatabaseName As String)
        Using Connection = GetConnection("Master")
            Using Cmd = Connection.CreateCommand
                Cmd.CommandText = "Select Count(*) From sysdatabases Where name ='" & DatabaseName & "'"
                Dim Exist = Convert.ToInt32(Cmd.ExecuteScalar(Cmd)) > 0
                If Exist Then Return
            End Using

            Using Cmd = Connection.CreateCommand
                Cmd.CommandText = "Create Database [" & DatabaseName & "]"
                Cmd.ExecuteNonQuery
            End Using
        End Using
    End Sub

    Private Function GetConnection(DatabaseName As String) As SqlClient.SqlConnection
        Dim ConnectionString As New SqlClient.SqlConnectionStringBuilder
        ConnectionString.Add("uid", UxUserName.Text)
        ConnectionString.Add("pwd", UxPassword.Text)
        ConnectionString.Add("timeout", "3600000")
        ConnectionString.Add("data source", DatabaseName)
        ConnectionString.Add("initial catalog", UxServer.Text)
        
        Dim Connection As New SqlClient.SqlConnection(ConnectionString.ToString)
        Connection.Open

        Return Connection
    End Function
End Class