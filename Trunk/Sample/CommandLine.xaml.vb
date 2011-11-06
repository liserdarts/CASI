Public Class CommandLine

    Public Sub New(Batch As ScriptBatch)
        ' This call is required by the designer.
        InitializeComponent()
        
        UxCommand.DataContext = Batch
    End Sub

    Private Sub UxClose_Click(sender As Object, e As RoutedEventArgs) Handles UxClose.Click
        Close
    End Sub

End Class