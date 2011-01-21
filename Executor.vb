'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Public MustInherit Class Executor
    Public Sub RunScript(Script As String)
        Dim Stream As New IO.MemoryStream(Text.Encoding.UTF8.GetBytes(Script))    
        RunScript(Stream)
    End Sub

    Public MustOverride Sub RunScript(Script As IO.Stream)
End Class