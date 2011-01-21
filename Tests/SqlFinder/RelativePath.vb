'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

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