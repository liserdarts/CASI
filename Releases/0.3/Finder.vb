'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Base class to find and opens scripts.
''' </summary>
Public MustInherit Class Finder
        
    ''' <summary>
    ''' When overridden in a derived class, finds all the available scripts.
    ''' </summary>
    ''' <returns>The relative path to all the found scripts</returns>
    Public MustOverride Function GetAllScripts() As IList(Of String)
    
    ''' <summary>
    ''' When overridden in a derived class, opens the specified script.
    ''' </summary>
    ''' <param name="Path">The relative path to the script to open.</param>
    ''' <returns>A <c>System.IO.Stream</c> of the contents of the script</returns>
    Public MustOverride Function Open(Path As String) As IO.Stream
End Class
