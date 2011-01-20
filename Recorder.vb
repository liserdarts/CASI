Public MustInherit Class Recorder
    
    Public MustOverride Function HasRunScript(Path As String) As Boolean

    Public MustOverride Sub RecordScript(Path As String)

End Class
