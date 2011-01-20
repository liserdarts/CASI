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
    End Sub

End Class
End Namespace