'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Namespace Sql
Public Class MSSqlExecutor
    Inherits Executor

    <ScriptPropertyAttribute("e5eecd6f-3481-41ae-bea8-46c96ce1ea5b")> _
    Public Property Connection() As MSSqlConnection

    Public Overrides Sub RunScript(Script As IO.Stream)
        'ToDo: Allow GOs in the script
        Using Cmd = Connection.Connection.CreateCommand
            Dim Reader As New IO.StreamReader(Script)
            Cmd.CommandText = Reader.ReadToEnd
            Cmd.Transaction = Connection.Transaction
            Cmd.ExecuteNonQuery
        End Using
    End Sub

End Class
End Namespace