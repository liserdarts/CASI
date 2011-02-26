'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

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