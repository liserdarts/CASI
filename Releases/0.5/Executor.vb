'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Base class to execute scripts.
''' </summary>
Public MustInherit Class Executor

    ''' <summary>
    ''' Executes the given script.
    ''' </summary>
    ''' <param name="Script">The script</param>
    ''' <remarks>Converts the String into an <c>System.IO.MemoryStream</c> using UTF8 encoding</remarks>
    Public Sub RunScript(Path As String,Script As String)
        Dim Stream As New IO.MemoryStream(Text.Encoding.UTF8.GetBytes(Script))
        RunScript(Path, Stream)
    End Sub

    ''' <summary>
    ''' When overridden in a derived class, executes the given script.
    ''' </summary>
    ''' <param name="Script">The script</param>
    Public MustOverride Sub RunScript(Path As String, Script As IO.Stream)
End Class