'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace SqlRecorder
Public Class Base
    Inherits TestFramework.TestCase

    Protected Connection As TestSqlConnection
    Protected Recorder As Sql.SqlRecorder
    
    Public Overrides Sub Test()
        GetConnection
        CreateRecorder

        AssertIsFalse(Recorder.HasRunScript("Script1"))
        AssertIsFalse(Recorder.HasRunScript("Script2"))

        Recorder.RecordScript("Script1")

        AssertIsTrue(Recorder.HasRunScript("Script1"))
        AssertIsFalse(Recorder.HasRunScript("Script2"))
    End Sub
    
    Protected Overridable Sub GetConnection()
        Connection = GetOverride(Of Overriders.SqlDatabaseOverride).GetConnection
        Connection.Init
    End Sub

    
    Protected Overridable Sub CreateRecorder()
        Recorder = New Sql.SqlRecorder
        Recorder.Connection = Connection
    End Sub

End Class
End Namespace