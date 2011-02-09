'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Data
Public Class FileManager
    Implements Files.IFileManager
    
    Public Function OpenFile(Path As String, Mode As IO.FileMode) As IO.Stream Implements Files.IFileManager.OpenFile
        Return IO.File.Open(Path, Mode)
    End Function
    
    Public Function FindFiles(Pattern As String) As List(Of String) Implements Files.IFileManager.FindFiles
        Dim Drive As String = IO.Path.GetPathRoot(Pattern)
        If String.IsNullOrEmpty(Drive) Then Return New List(Of String)
        
        Dim Dir As New IO.DirectoryInfo(Drive)
        Return FindFiles(Dir, Pattern.Substring(Drive.Length))
    End Function
    
    Private Function FindFiles(Base As IO.DirectoryInfo, Pattern As String) As List(Of String)
        Dim Results As New List(Of String)
        Dim NextPart As String
        NextPart = Pattern.Split(IO.Path.DirectorySeparatorChar, IO.Path.DirectorySeparatorChar)(0)
        
        If NextPart = Pattern Then 'There are no more folders
            For Each File In Base.GetFiles(Pattern)
                Results.Add(File.FullName)
            Next
            
        Else 'Step one more folder
            Pattern = Pattern.Substring(NextPart.Length + 1)
            
            For Each Folder In Base.GetDirectories(NextPart)
                Results.AddRange(FindFiles(Folder, Pattern))
            Next
        End If
        
        Return Results
    End Function
    
End Class
End Namespace