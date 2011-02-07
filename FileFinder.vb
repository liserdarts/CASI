'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Public Class FileFinder
    Inherits Finder

    <ScriptProperty("4F2AA327-AFA1-4A00-AD73-033945903579")> _
    Public Property BasePath() As Folder

    Public Property FilePattern As String = "*.sql"

    Public Overrides Function GetAllScripts() As IList(Of String)
        Dim Paths = Data.Files.FindFiles(BasePath.Folder & FilePattern)

        If Not String.IsNullOrEmpty(BasePath.Folder) Then 'Remove the base path to make the path relative
            For I = 0 To Paths.Count - 1
                Paths(I) = Paths(I).Substring(BasePath.Folder.Length)
            Next
        End If
        
        Return Paths
    End Function

    Public Overrides Function Open(Path As String) As IO.Stream
        Return Data.Files.OpenFile(BasePath.Folder & Path, IO.FileMode.Open)
    End Function

End Class