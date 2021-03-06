﻿'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Base class to sort scripts into the order they are meant to be run
''' </summary>
Public MustInherit Class Sorter
    
    ''' <summary>
    ''' When overridden in a derived class, sorts the paths
    ''' </summary>
    ''' <param name="Paths">The paths to sort</param>
    ''' <returns>The sorted paths</returns>
    Public MustOverride Function Sort(Paths As IList(Of String)) As IList(Of String)

End Class