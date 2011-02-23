'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Finds scripts stored as resources in the given assembly matching the given file pattern
''' </summary>
Public Class ResourceFinder
    Inherits Finder
    
    ''' <summary>
    ''' Gets or sets the assembly to search.
    ''' </summary>
    ''' <value>The assembly to search.</value>
    Public Property Assembly As Reflection.Assembly

    ''' <summary>
    ''' Gets or sets the file pattern.
    ''' </summary>
    ''' <value>The file pattern.</value>
    Public Property FilePattern As String = ".*"

    Private Function GetReader() As Resources.ResourceReader
        Return New Resources.ResourceReader(Assembly.GetManifestResourceStream(Assembly.GetName.Name & ".g.resources"))
    End Function

    ''' <summary>
    ''' Finds all the available scripts.
    ''' </summary>
    ''' <returns>
    ''' The relative path to all the found scripts
    ''' </returns>
    Public Overrides Function GetAllScripts() As IList(Of String)
        Dim Paths As New List(Of String)

        For Each Script As DictionaryEntry In GetReader
            If Text.RegularExpressions.Regex.IsMatch(Script.Key, FilePattern) Then
                Paths.Add(Script.Key)
            End If
        Next

        Return Paths
    End Function

    ''' <summary>
    ''' Opens the specified script.
    ''' </summary>
    ''' <param name="Path">The relative path to the script to open.</param>
    ''' <returns>
    ''' A <c>System.IO.Stream</c> of the contents of the script
    ''' </returns>
    Public Overrides Function Open(ByVal Path As String) As IO.Stream
        For Each Script As DictionaryEntry In GetReader
            If Path = Script.Key Then
                Return CType(Script.Value, IO.Stream)
            End If
        Next
        Return Nothing
    End Function
End Class
