Namespace Sql
Public Class SqlFinder
    Inherits Finder

    Public Property BasePath() As String

    Public Overrides Function GetAllScripts() As IList(Of String)
        Dim Paths = Data.Files.FindFiles(BasePath & "*.sql")

        If Not String.IsNullOrEmpty(BasePath) Then 'Remove the base path to make the path relative 
            For I = 0 To Paths.Count - 1
                Paths(I) = Paths(I).Substring(BasePath.Length)
            Next
        End If

        Return Paths
    End Function

    Public Overrides Function Open(Path As String) As IO.Stream
        Return Data.Files.OpenFile(BasePath & Path, IO.FileMode.Open)
    End Function

End Class
End Namespace