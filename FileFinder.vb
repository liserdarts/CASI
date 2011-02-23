'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Finds scripts in the given base folder matching the given file pattern
''' </summary>
Public Class FileFinder
    Inherits Finder

    ''' <summary>
    ''' Gets or sets the base path.
    ''' </summary>
    ''' <value>
    ''' <value>A <c>CASI.Folder</c> object that has the path path to search.</value></value>
    <ScriptProperty("4F2AA327-AFA1-4A00-AD73-033945903579")> _
    Public Property BasePath() As Folder

    ''' <summary>
    ''' Gets or sets the file pattern.
    ''' </summary>
    ''' <value>The file pattern.</value>
    ''' <remarks>This uses wildcards where * matches more then one character and ? matches 1 character</remarks>
    Public Property FilePattern As String = "*.sql"

    ''' <summary>
    ''' Searches the base path for files that match <c>FilePattern</c>.
    ''' </summary>
    ''' <returns>The relative path to all the found scripts</returns>
    Public Overrides Function GetAllScripts() As IList(Of String)
        Dim Paths = Data.Files.FindFiles(BasePath.Folder & FilePattern)

        If Not String.IsNullOrEmpty(BasePath.Folder) Then 'Remove the base path to make the path relative
            For I = 0 To Paths.Count - 1
                Paths(I) = Paths(I).Substring(BasePath.Folder.Length)
            Next
        End If
        
        Return Paths
    End Function

    ''' <summary>
    ''' Opens the specified script.
    ''' </summary>
    ''' <param name="Path">The relative path to the script to open.</param>
    ''' <returns>A <c>System.IO.Stream</c> of the contents of the script</returns>
    Public Overrides Function Open(Path As String) As IO.Stream
        Return Data.Files.OpenFile(BasePath.Folder & Path, IO.FileMode.Open)
    End Function

End Class