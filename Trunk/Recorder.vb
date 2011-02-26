'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Base class to record that scripts have run and looks up if they have run.
''' </summary>
Public MustInherit Class Recorder
    
    ''' <summary>
    ''' When overridden in a derived class, determines whether [has run script] [the specified path].
    ''' </summary>
    ''' <param name="Path">The path to the script.</param>
    ''' <returns>
    ''' <c>True</c> if the script has been run; otherwise, <c>False</c>.
    ''' </returns>
    Public MustOverride Function HasRunScript(Path As String) As Boolean

    ''' <summary>
    ''' When overridden in a derived class, records the script as having run.
    ''' </summary>
    ''' <param name="Path">The path to the script.</param>
    Public MustOverride Sub RecordScript(Path As String)

End Class
