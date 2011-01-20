Public MustInherit Class Executor
    Public Sub RunScript(Script As String)
        Dim Stream As New IO.MemoryStream(Text.Encoding.UTF8.GetBytes(Script))    
        RunScript(Stream)
    End Sub

    Public MustOverride Sub RunScript(Script As IO.Stream)
End Class