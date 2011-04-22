'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Class MainWindow

    Dim Runner As New ScriptRunner
    
    Public Sub New(Runner As ScriptRunner)
        InitializeComponent
        Me.Runner = Runner
    End Sub
    
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        UxPropertyObjects.ItemsSource = Runner.GetPropertyObjects
        UxProgress.Runner = Runner
    End Sub

    Private Sub UXRun_Click(sender As Object, e As RoutedEventArgs) Handles UXRun.Click
        UxPropertyObjects.IsReadOnly = True
        UXRun.IsEnabled = False
        UxResults.Visibility = Windows.Visibility.Visible

        Dim Thread As New System.Threading.Thread(AddressOf Run)
        Thread.IsBackground = True
        Thread.Name = "ScriptRunner"
        Thread.Start
    End Sub

    Private Sub Run()
        Console.SetOut(New UI.TextBoxWriter(UxLog))
        Try
            Runner.Run
            Console.WriteLine("Done")
        Catch Ex As Exception
            Console.WriteLine(Ex.ToString)
            Console.WriteLine("Failed")
        End Try

        Dispatcher.Invoke(New Action(AddressOf Finish))
    End Sub
    Private Sub Finish()
        UxProgress.Visibility = Windows.Visibility.Collapsed
    End Sub

    Private Sub UxBuildCommandLine_Click(sender As Object, e As RoutedEventArgs) Handles UxBuildCommandLine.Click
        Dim Command As New CommandLine(Runner)
        Command.ShowDialog
    End Sub
End Class