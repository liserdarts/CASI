'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace SqlRecorder
Public Class RunTwice
    Inherits Base
    
    Public Overrides Sub Test()
        MyBase.Test
        
        'Make sure it won't try and create the table twice
        Dim Recorder As New Sql.SqlRecorder
        Recorder.Connection = Connection
        Recorder.HasRunScript("Anything")
    End Sub

    Protected Overrides Sub GetConnection()
        If Connection IsNot Nothing Then Return
        Connection = GetOverride(Of Overriders.SqlDatabaseOverride).GetConnection
        Connection.Init
    End Sub

End Class
End Namespace