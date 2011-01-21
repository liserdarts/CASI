'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

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