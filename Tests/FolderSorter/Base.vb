'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Namespace FolderSorter
''' <summary>
''' Make sure file paths are sorted correctly.
''' 1.2\Something.sql
''' 1.2.1\Something.sql
''' Not like this
''' 1.2.1\Something.sql
''' 1.2\Something.sql
''' </summary>
Public Class Base
    Inherits TestFramework.TestCase
    
    Public Overrides Sub Test()
        Sort("1.2\Somthing.sql", "1.2.1\Something.sql")
        Sort("1.2\Somthing2.sql", "1.2.1\Something.sql")
        Sort("1.2\Tables\SomeTable.sql", "1.2.1\Something.sql")
    End Sub

    Private Sub Sort(FirstPath As String, SecondPath As String)
        Dim Paths As New List(Of String)
        Paths.Add(SecondPath)
        Paths.Add(FirstPath)

        Dim Sorter As New ScriptHelper.FolderSorter
        Paths = Sorter.Sort(Paths)
        AssertGreater(Paths.IndexOf(FirstPath), -1)
        AssertGreater(Paths.IndexOf(SecondPath), -1)
        AssertLess(Paths.IndexOf(FirstPath), Paths.IndexOf(SecondPath))
    End Sub

End Class
End Namespace