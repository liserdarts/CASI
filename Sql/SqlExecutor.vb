'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

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