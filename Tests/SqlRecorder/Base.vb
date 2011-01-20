Namespace SqlRecorder
Public Class Base
    Inherits TestFramework.TestCase

    Protected Connection As Common.DbConnection
    
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
        Connection = Data.SqlDatabase.GetConnection("")
    End Sub

End Class
End Namespace