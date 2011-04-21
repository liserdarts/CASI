'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Console
''' <summary>
''' Test using <c>CommandLineParser</c> to build arguments for the command line
''' </summary>
Public Class BuildCommands
    Inherits ParseCommands
    
    Protected Builder As CommandLineParser

    Public Overrides Sub Test()
        Builder = New CommandLineParser
        MyBase.Test
    End Sub

    Protected Overrides Sub AddArg(Arg As String, Name As String, Value As String)
        Builder.Add(Name, Value)
        MyBase.AddArg(Arg, Name, Value)
    End Sub

    Protected Overrides Sub CheckParams()
        Args = New List(Of String)(Builder.Build.Split(" "))
        MyBase.CheckParams
    End Sub

End Class
End Namespace