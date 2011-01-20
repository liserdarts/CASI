Public Class TestExecutor
    Inherits Executor
    
    Event RunScriptEvent As EventHandler

    Public RunLog As New List(Of String)

    Public Overrides Sub RunScript(Script As IO.Stream)
        RaiseEvent RunScriptEvent(Me, EventArgs.Empty)

        Dim Reader As New IO.StreamReader(Script)
        RunLog.Add(Reader.ReadToEnd)
    End Sub

End Class