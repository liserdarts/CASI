'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Class MainWindow 
    
    Dim Finder As New ResourceFinder
    Dim Recorder As New Sql.SqlRecorder
    Dim Executor As New Sql.MSSqlExecutor
    Dim Transaction As New Sql.SqlTransactionProvider
    Dim Runner As New ScriptRunner
    
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Finder.Assembly = Me.GetType.Assembly
        Finder.FilePattern = ".*sql"
        
        Runner = New ScriptRunner
        Runner.Finder = Finder
        Runner.Recorder = Recorder
        Runner.Executor = Executor
        Runner.Transaction = Transaction

        UxPropertyObjects.ItemsSource = Runner.GetPropertyObjects
    End Sub

    Private Sub UXRun_Click(sender As Object, e As RoutedEventArgs) Handles UXRun.Click
        Runner.Run
    End Sub

End Class