Namespace SqlFinder
Public Class OnlySqlFiles
    Inherits Base
    
    Public Overrides Sub Test()
        CreateFinder

        Dim Scripts = Finder.GetAllScripts
        AssertGreater(Scripts.Count, 0)

        For Each Script In Scripts
            AssertIsTrue(Script.ToLower.EndsWith(".sql"))
        Next
    End Sub

End Class
End Namespace