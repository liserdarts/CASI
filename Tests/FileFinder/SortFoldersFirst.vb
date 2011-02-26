'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Namespace FileFinder
''' <summary>
''' Make sure file paths are sorted correctly.
''' 1.2\Something.sql
''' 1.2.1\Something.sql
''' Not like this
''' 1.2.1\Something.sql
''' 1.2\Something.sql
''' </summary>
Public Class SortFoldersFirst
    Inherits Base

    Public Overrides Sub Test()
        '"Create" some files
        Data.Files.OpenFile("1.2.1\Something.sql", IO.FileMode.Create).Close
        Data.Files.OpenFile("1.2\Something.sql", IO.FileMode.Create).Close
        
        CreateFinder
        
        Dim Scripts = Finder.GetAllScripts
        AssertLess(Scripts.IndexOf("1.2\Something.sql"), Scripts.IndexOf("1.2.1\Something.sql"))
    End Sub

End Class
End Namespace