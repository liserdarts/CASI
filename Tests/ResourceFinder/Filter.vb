Namespace ResourceFinder
Public Class Filter
    Inherits Base
    
    Public Overrides Sub Test()
        MyBase.Test

        Dim Paths = Finder.GetAllScripts
        AssertGreater(Paths.Count, 0)

        For Each Path In Paths
            AssertIsTrue(Path.EndsWith(".sql"))
        Next
    End Sub

    Protected Overrides Sub CreateFinder()
        MyBase.CreateFinder
        Finder.FilePattern = ".*sql"
    End Sub

End Class
End Namespace