'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Data
Public Class Files

    Public Interface IFileManager
        Function OpenFile(Path As String, Mode As IO.FileMode) As IO.Stream
        Function FindFiles(Pattern As String) As List(Of String)
    End Interface
    
    Shared Sub New()
        Manager = New FileManager
    End Sub
    
    Public Shared Manager As IFileManager
    
    Public Shared Function OpenFile(Path As String, Mode As IO.FileMode) As IO.Stream
        Return Manager.OpenFile(Path, Mode)
    End Function
    
    Public Shared Function FindFiles(Pattern As String) As List(Of String)
        Return Manager.FindFiles(Pattern)
    End Function
    
End Class
End Namespace