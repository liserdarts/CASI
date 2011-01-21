'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

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