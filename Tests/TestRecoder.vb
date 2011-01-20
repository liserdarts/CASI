Public Class TestRecoder
    Inherits Recorder
    
    Public RanScripts As New List(Of String)({"Script1"})

    Public Overrides Function HasRunScript(Path As String) As Boolean
        Return RanScripts.Contains(Path)
    End Function

    Public Overrides Sub RecordScript(Path As String)
        RanScripts.Add(Path)
    End Sub

End Class