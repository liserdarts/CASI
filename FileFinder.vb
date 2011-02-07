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
        
        Paths.Sort(New Sorter)
        
        Return Paths
    End Function

    Public Overrides Function Open(Path As String) As IO.Stream
        Return Data.Files.OpenFile(BasePath.Folder & Path, IO.FileMode.Open)
    End Function

    Private Class Sorter
        Implements IComparer(Of String)
        
        Public Function Compare(X As String, Y As String) As Integer Implements IComparer(Of String).Compare
            Dim XAray = X.Split("\"c, "/"c)
            Dim YAray = Y.Split("\"c, "/"c)
            
            For I As Integer = 0 To Math.Min(XAray.Length, YAray.Length) - 1
                Dim Diff = XAray(I).CompareTo(YAray(I))
                If Diff <> 0 Then
                    Return Diff
                End If
            Next

            Return XAray.Length.CompareTo(YAray.Length)
        End Function

    End Class

End Class