'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class TestExecutor
    Inherits Executor
    
    Event RunScriptEvent As EventHandler
    
    <ScriptPropertyAttribute("FD46628D-AD89-43B0-AF3B-EB0DCA8B1488")> _
    Public Property TestProperty() As TestScriptProperty
    
    Public RunLog As New List(Of String)

    Public Overrides Sub RunScript(Path As String, Script As IO.Stream)
        RaiseEvent RunScriptEvent(Me, EventArgs.Empty)

        Dim Reader As New IO.StreamReader(Script)
        RunLog.Add(Reader.ReadToEnd)
    End Sub

End Class