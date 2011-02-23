'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Data
''' <summary>
''' Contains functions for finding and opening files
''' </summary>
Public Class Files

    ''' <summary>
    ''' An interface defining these provided functions
    ''' </summary>
    Public Interface IFileManager
        Function OpenFile(Path As String, Mode As IO.FileMode) As IO.Stream
        Function FindFiles(Pattern As String) As List(Of String)
    End Interface
    
    Shared Sub New()
        Manager = New FileManager
    End Sub
    
    ''' <summary>
    ''' The object that contains the actual implementation of these functions
    ''' </summary>
    Public Shared Manager As IFileManager
    
    ''' <summary>
    ''' Opens a file.
    ''' </summary>
    Public Shared Function OpenFile(Path As String, Mode As IO.FileMode) As IO.Stream
        Return Manager.OpenFile(Path, Mode)
    End Function
    
    ''' <summary>
    ''' Finds files using a wildcard pattern
    ''' </summary>
    ''' <param name="Pattern">The pattern. This can include a folder path to look in</param>
    ''' <returns>The path to all the found files</returns>
    Public Shared Function FindFiles(Pattern As String) As List(Of String)
        Return Manager.FindFiles(Pattern)
    End Function
    
End Class
End Namespace