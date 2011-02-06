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
        For Each Child In FindChildren(Of PropertyEditorLibrary.PropertyEditor)(UxPropertyObjects)
            Child.IsEnabled = False
        Next
        UXRun.IsEnabled = False
        UxResults.Visibility = Windows.Visibility.Visible

        Dim Thread As New System.Threading.Thread(AddressOf Run)
        Thread.IsBackground = True
        Thread.Name = "ScriptRunner"
        Thread.Start
    End Sub

    Private Sub Run()
        Console.SetOut(New TextBoxWriter(UxLog))
        Runner.Run
        Console.WriteLine("Done")

        Dispatcher.Invoke(New Action(AddressOf Finish))
    End Sub
    Private Sub Finish()
        UxProgress.Visibility = Windows.Visibility.Collapsed
    End Sub

    Private Function FindChildren(Of T As DependencyObject)(Parent As FrameworkElement) As List(Of T)
        Dim Children As New List(Of T)
        
        For I As Integer = 0 To VisualTreeHelper.GetChildrenCount(Parent) - 1
            Dim Cur As DependencyObject
            Cur = VisualTreeHelper.GetChild(Parent, I)
            
            If TypeOf Cur Is T Then
                Children.Add(Cur)
            Else
                Children.AddRange(FindChildren(Of T)(Cur))
            End If
        Next
        
        Return Children
    End Function

End Class

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
        If Not Txt.Dispatcher.CheckAccess Then
            Txt.Dispatcher.BeginInvoke(New Action(Of Char)(AddressOf Write), Value)
            Return
        End If
        Txt.AppendText(Value)
    End Sub

End Class