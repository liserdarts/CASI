'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Namespace SqlRecorder
Public Class Base
    Inherits TestFramework.TestCase

    Protected Connection As TestSqlConnection
    
    Public Overrides Sub Test()
        GetConnection

        Dim Recorder As New Sql.SqlRecorder
        Recorder.Connection = Connection

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

End Class
End Namespace