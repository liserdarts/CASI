'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Sql
''' <summary>
''' A generic executor that will run the scripts on a database
''' </summary>
Public Class SqlExecutor
    Inherits Executor
    
    ''' <summary>
    ''' Gets or sets the connection.
    ''' </summary>
    ''' <value>The connection to the database.</value>
    <ScriptPropertyAttribute("e5eecd6f-3481-41ae-bea8-46c96ce1ea5b")> _
    Public Property Connection() As SqlConnection

    ''' <summary>
    ''' Executes the given script.
    ''' </summary>
    ''' <param name="Script">The script</param>
    Public Overrides Sub RunScript(Path As String, Script As IO.Stream)
        Using Cmd = Connection.Connection.CreateCommand
            Dim Reader As New IO.StreamReader(Script)
            Cmd.CommandText = Reader.ReadToEnd
            Cmd.Transaction = Connection.Transaction
            Cmd.ExecuteNonQuery
        End Using
    End Sub

End Class
End Namespace