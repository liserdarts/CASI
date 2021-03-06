﻿'Copyright (c) 2011-2015, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Sql
''' <summary>
''' Executes scripts on an Microsoft SQL database
''' </summary>
Public Class MSSqlExecutor
    Inherits Executor

    ''' <summary>
    ''' Gets or sets the connection.
    ''' </summary>
    ''' <value>The connection to the database.</value>
    <ScriptPropertyAttribute("e5eecd6f-3481-41ae-bea8-46c96ce1ea5b")> _
    Public Property Connection() As MSSqlConnection

    ''' <summary>
    ''' Gets or sets the timeout.
    ''' </summary>
    ''' <value>The timeout.</value>
    Public Property CommandTimeout As TimeSpan = TimeSpan.FromSeconds(60)

    ''' <summary>
    ''' Executes the given script.
    ''' </summary>
    ''' <param name="Script">The script</param>
    Public Overrides Sub RunScript(Path As String, Script As IO.Stream)
        'ToDo: Allow GOs in the script
        Using Cmd = Connection.Connection.CreateCommand
            Cmd.CommandTimeout = Math.Round(CommandTimeout.TotalSeconds)

            Dim Reader As New IO.StreamReader(Script)
            Cmd.CommandText = Reader.ReadToEnd
            Cmd.Transaction = Connection.Transaction
            Cmd.ExecuteNonQuery
        End Using
    End Sub

End Class
End Namespace