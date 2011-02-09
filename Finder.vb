'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public MustInherit Class Finder
        
    Public MustOverride Function GetAllScripts() As IList(Of String)

    Public MustOverride Function Open(Path As String) As IO.Stream
End Class
