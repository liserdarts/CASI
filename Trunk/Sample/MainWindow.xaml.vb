'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Class MainWindow 
    
    Dim Finder As New ResourceFinder
    Dim Recorder As New Sql.SqlRecorder
    Dim Executor As New Sql.MSSqlExecutor
    Dim Transaction As New Sql.SqlTransactionProvider
    Dim WithEvents Runner As New ScriptRunner
    
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Finder.Assembly = Me.GetType.Assembly
        Finder.FilePattern = ".*sql$"
        
        Runner = New ScriptRunner
        Runner.Finder = Finder
        Runner.Recorder = Recorder
        Runner.Executor = Executor
        Runner.Transaction = Transaction

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
    Private Sub Runner_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles Runner.ProgressChanged
        Static LastStageText As String
        Dim StageText As String = e.GetStageText

        If LastStageText <> StageText Then
            Console.WriteLine(StageText)
            LastStageText = StageText
        End If
    End Sub
End Class