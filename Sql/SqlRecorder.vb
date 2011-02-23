'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Sql
''' <summary>
''' Records the scripts in an SQL database
''' </summary>
''' <remarks>
''' The table where the scripts are recorded is create with this
''' Create Table [{0}]
'''	(ID BigInt Identity (1, 1) Not Null,
'''	ScriptPath national character varying(255) Not Null,
''' InstallDate DateTime Not Null)
''' </remarks>
Public Class SqlRecorder
    Inherits Recorder
    
    ''' <summary>
    ''' Gets or sets the connection.
    ''' </summary>
    ''' <value>The connection to the database.</value>
    <ScriptPropertyAttribute("e5eecd6f-3481-41ae-bea8-46c96ce1ea5b")> _
    Public Property Connection() As SqlConnection

    ''' <summary>
    ''' Gets or sets the name of the table to record the scripts in.
    ''' </summary>
    ''' <value>The name of the table.</value>
    ''' <remarks>If the table doesn't exist it will be created</remarks>
    Public Property TableName() As String = "ScriptUpdates"

    ''' <summary>
    ''' Determines whether [has run script] [the specified path].
    ''' </summary>
    ''' <param name="Path">The path to the script.</param>
    ''' <returns>
    ''' <c>True</c> if the script has been run; otherwise, <c>False</c>.
    ''' </returns>
    Public Overrides Function HasRunScript(Path As String) As Boolean
        CreateTable
        Using Cmd = Connection.Connection.CreateCommand
            Cmd.CommandText = String.Format("Select * From [{0}] Where ScriptPath = @Path", TableName)
            
            Dim PathParam = Cmd.CreateParameter
            PathParam.ParameterName = "Path"
            PathParam.Value = Path
            Cmd.Parameters.Add(PathParam)

            Using Reader = Cmd.ExecuteReader
                Return Reader.Read
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Records the script as having run.
    ''' </summary>
    ''' <param name="Path">The path to the script.</param>
    Public Overrides Sub RecordScript(Path As String)
        Using Cmd = Connection.Connection.CreateCommand
            Cmd.CommandText = _
                "Insert Into [{0}] " &
                "(ScriptPath, InstallDate) " &
                "Values " &
                "(@Path, @Date)"
            Cmd.CommandText = String.Format(Cmd.CommandText, TableName)

            Dim PathParam = Cmd.CreateParameter
            PathParam.ParameterName = "Path"
            PathParam.Value = Path
            Cmd.Parameters.Add(PathParam)

            Dim DateParam = Cmd.CreateParameter
            DateParam.ParameterName = "Date"
            DateParam.Value = Date.Now
            Cmd.Parameters.Add(DateParam)

            Cmd.ExecuteNonQuery
        End Using
    End Sub

    Private Sub CreateTable()
        Static CreatedTable As Boolean
        If CreatedTable Then Return
        
        Using Cmd = Connection.Connection.CreateCommand
            Cmd.CommandText = String.Format("Select * from INFORMATION_SCHEMA.TABLES Where TABLE_NAME = '{0}'", TableName)
            Using Reader = Cmd.ExecuteReader
                If Reader.Read Then 'The table already exists
                    CreatedTable = True
                    Return
                End If
            End Using
        End Using

        Using Cmd = Connection.Connection.CreateCommand
            Cmd.CommandText = _
                "Create Table [{0}] " &
                "(ID BigInt Identity (1, 1) Not Null, " &
                "ScriptPath national character varying(255) Not Null, " &
                "InstallDate DateTime Not Null)"
            Cmd.CommandText = String.Format(Cmd.CommandText, TableName)

            Cmd.ExecuteNonQuery
        End Using

        CreatedTable = True
    End Sub

End Class
End Namespace