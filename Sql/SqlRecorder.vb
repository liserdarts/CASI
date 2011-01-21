'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Namespace Sql
Public Class SqlRecorder
    Inherits Recorder

    Public Property Connection() As Common.DbConnection
    Public Property TableName() As String = "ScriptUpdates"

    Public Overrides Function HasRunScript(Path As String) As Boolean
        CreateTable
        Using Cmd = Connection.CreateCommand
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

    Public Overrides Sub RecordScript(Path As String)
        Using Cmd = Connection.CreateCommand
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
        
        Using Cmd = Connection.CreateCommand
            Cmd.CommandText = String.Format("Select * from INFORMATION_SCHEMA.TABLES Where TABLE_NAME = '{0}'", TableName)
            Using Reader = Cmd.ExecuteReader
                If Reader.Read Then 'The table already exists
                    CreatedTable = True
                    Return
                End If
            End Using
        End Using

        Using Cmd = Connection.CreateCommand
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