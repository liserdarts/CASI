Namespace ResourceFinder
Public Class Base
    Inherits TestFramework.TestCase
    
    Protected Finder As ScriptHelper.ResourceFinder

    Public Overrides Sub Test()
        CreateFinder

        Dim Paths = Finder.GetAllScripts
        AssertGreater(Paths.Count, 0)

        Dim LastPath As String = ""
        For Each Path In Paths
            AssertGreater(Path, LastPath)
            LastPath = Path

            Using Script = Finder.Open(Path)
                AssertNotNothing(Script)
            End Using
        Next
    End Sub

    Protected Overridable Sub CreateFinder()
        Finder = New ScriptHelper.ResourceFinder
        Finder.Assembly = Me.GetType.Assembly
    End Sub

End Class
End Namespace