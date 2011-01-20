Namespace SqlFinder
Public Class RelativePath
    Inherits Base
    
    Public Overrides Sub Test()
        MyBase.Test

        Dim Scripts = Finder.GetAllScripts
        AssertGreater(Scripts.Count, 0)

        For Each Script In Scripts
            AssertIsFalse(Script.ToLower.Contains("sample"))
        Next
    End Sub

    Protected Overrides Sub CreateFinder()
        MyBase.CreateFinder
        Finder.BasePath = "Sample"
    End Sub

End Class
End Namespace