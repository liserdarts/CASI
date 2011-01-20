Public Class ResourceFinder
    Inherits Finder
    
    Public Assembly As Reflection.Assembly
    Public FilePattern As String = ".*"

    Private Function GetReader() As Resources.ResourceReader
        Return New Resources.ResourceReader(Assembly.GetManifestResourceStream(Assembly.GetName.Name & ".g.resources"))
    End Function

    Public Overrides Function GetAllScripts() As IList(Of String)
        Dim Paths As New List(Of String)

        For Each Script As DictionaryEntry In GetReader
            If Text.RegularExpressions.Regex.IsMatch(Script.Key, FilePattern) Then
                Paths.Add(Script.Key)
            End If
        Next

        Paths.Sort
        Return Paths
    End Function

    Public Overrides Function Open(Path As String) As IO.Stream
        For Each Script As DictionaryEntry In GetReader
            If Path = Script.Key Then
                Return CType(Script.Value, IO.Stream)
            End If
        Next
        Return Nothing
    End Function
End Class
