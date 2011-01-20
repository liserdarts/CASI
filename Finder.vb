Public MustInherit Class Finder
        
    Public MustOverride Function GetAllScripts() As IList(Of String)

    Public MustOverride Function Open(Path As String) As IO.Stream
End Class
