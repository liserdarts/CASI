Namespace SqlFinder
Public Class Base
    Inherits TestFramework.TestCase
    
    Protected Finder As New Sql.SqlFinder

    Public Overrides Sub Test()
        CreateFinder

        Dim Scripts = Finder.GetAllScripts
        AssertGreater(Scripts.Count, 0)

        For Each Path In Scripts
            AssertNotNothing(Finder.Open(Path))
        Next
    End Sub

    Protected Overridable Sub CreateFinder()
        Finder = New Sql.SqlFinder
    End Sub

End Class
End Namespace