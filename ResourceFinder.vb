'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class ResourceFinder
    Inherits Finder
    
    Public Assembly As Reflection.Assembly
    Public FilePattern As String = ".*"

    Private Function GetReader() As Resources.ResourceReader
        Return New Resources.ResourceReader(Assembly.GetManifestResourceStream(Assembly.GetName.Name & ".g.resources"))
    End Function

    Public Overrides Function GetAllScripts() As IList(Of String)
        Dim Paths As New List(Of String)

        For Each Script As DictionaryEntry In GetReader
            If Text.RegularExpressions.Regex.IsMatch(Script.Key, FilePattern) Then
                Paths.Add(Script.Key)
            End If
        Next

        Return Paths
    End Function

    Public Overrides Function Open(Path As String) As IO.Stream
        For Each Script As DictionaryEntry In GetReader
            If Path = Script.Key Then
                Return CType(Script.Value, IO.Stream)
            End If
        Next
        Return Nothing
    End Function
End Class
