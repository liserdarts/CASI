Public Class TextBoxWriter
    Inherits IO.TextWriter
    
    Dim Txt As Primitives.TextBoxBase
    Public Sub New(Txt As Primitives.TextBoxBase)
        Me.Txt = Txt
    End Sub

    Public Overrides ReadOnly Property Encoding() As Text.Encoding
        Get
            Return Text.Encoding.Default
        End Get
    End Property

    Public Overrides Sub Write(Value As Char)
        If Not Txt.Dispatcher.CheckAccess Then 'Run on the UI thread
            Txt.Dispatcher.BeginInvoke(New Action(Of Char)(AddressOf Write), Value)
            Return
        End If
        Txt.AppendText(Value)
        Txt.ScrollToEnd
    End Sub

End Class