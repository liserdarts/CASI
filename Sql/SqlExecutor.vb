Namespace Sql
Public Class SqlExecutor
    Inherits Executor

    Public Transaction As SqlTransactionProvider

    Public Overrides Sub RunScript(Script As IO.Stream)
        Using Cmd = Transaction.Connection.CreateCommand
            Dim Reader As New IO.StreamReader(Script)
            Cmd.CommandText = Reader.ReadToEnd
            Cmd.Transaction = Transaction.Transaction
            Cmd.ExecuteNonQuery
        End Using
    End Sub

End Class
End Namespace