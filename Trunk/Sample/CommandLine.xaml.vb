Public Class CommandLine

    Public Sub New(Runner As ScriptRunner)
        ' This call is required by the designer.
        InitializeComponent()
        
        UxCommand.DataContext = Runner
    End Sub

    Private Sub UxClose_Click(sender As Object, e As RoutedEventArgs) Handles UxClose.Click
        Close
    End Sub

End Class