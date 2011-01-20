Public Class TestFinder
    Inherits Finder

    Public Scripts As New List(Of String)({"Script1", "Script2", "Script3"})

    Public Overrides Function GetAllScripts() As IList(Of String)
        Return Scripts
    End Function

    Public Overrides Function Open(Path As String) As IO.Stream
        Return New IO.MemoryStream(Text.Encoding.UTF8.GetBytes(Path))
    End Function

End Class