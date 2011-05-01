Public Class CommandLine

    Public Sub New(Template As ScriptTemplate)
        ' This call is required by the designer.
        InitializeComponent()
        
        UxCommand.DataContext = Template
    End Sub

    Private Sub UxClose_Click(sender As Object, e As RoutedEventArgs) Handles UxClose.Click
        Close
    End Sub

End Class