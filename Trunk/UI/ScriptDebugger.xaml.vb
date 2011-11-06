'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class ScriptDebugger

    Shared ReadOnly BatchProperty As DependencyProperty

    Shared Sub New()
        BatchProperty = DependencyProperty.Register("Batch", GetType(ScriptBatch), GetType(ScriptDebugger), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnBatchChanged)))
    End Sub
    Private Shared Sub OnBatchChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs) 
        Dim Debugger As ScriptDebugger = d
        Debugger.Batch = e.NewValue
    End Sub

    Public Shadows Property DataContext() As String
        Get
            Return MyBase.DataContext
        End Get
        Set
            MyBase.DataContext = MyBase.DataContext
            ShowScript
        End Set
    End Property
    
    Dim WithEvents LBatch As ScriptBatch
    Public Property Batch() As ScriptBatch
        Get
            Return LBatch
        End Get
        Set
            LBatch = Value
            ShowScript
        End Set
    End Property

    
    Private Sub ShowScript()
        UxRecord.IsEnabled = False
        UxRun.IsEnabled = False
        If Batch Is Nothing Or String.IsNullOrEmpty(DataContext) Then Return

        'Can't call Batch.Template.Recorder.HasRunScript without initializing
        UxRecord.IsEnabled = True 'Not Batch.Template.Recorder.HasRunScript(DataContext)
        UxRun.IsEnabled = True
    End Sub

    Dim ScriptTemplate As ScriptTemplate

    Private Sub UxRecord_Click(sender As Object, e As RoutedEventArgs) Handles UxRecord.Click
        Throw New NotImplementedException
        'ScriptTemplate = Batch.Template.Clone

        'ScriptTemplate.Finder = New DummyFinder(DataContext, Batch.Template.Finder)
        'ScriptTemplate.Recorder = New DummyRecorder(Batch.Template.Recorder)
        'ScriptTemplate.Executor = New DummyExecutor
        
        'Run
    End Sub

    Private Sub UxRun_Click(sender As Object, e As RoutedEventArgs) Handles UxRun.Click
        Throw New NotImplementedException
        'ScriptTemplate = Batch.Template.Clone

        'ScriptTemplate.Finder = New DummyFinder(DataContext, Batch.Template.Finder)
        'ScriptTemplate.Recorder = New DummyRecorder(Batch.Template.Recorder)
        
        'Run
    End Sub

    Private Sub Run()
        If Dispatcher.CheckAccess Then
            Dim Thread As New System.Threading.Thread(AddressOf Run)
            Thread.IsBackground = True
            Thread.Name = "DebugRunner"
            Thread.Start
            Return
        End If

        Try
            Batch.Runner.Run(ScriptTemplate)
            Console.WriteLine("Done")
        Catch Ex As Exception
            Console.WriteLine(Ex.ToString)
            Console.WriteLine("Failed")
        End Try

        Dispatcher.Invoke(New Action(AddressOf Finish))
    End Sub
    Private Sub Finish()
        ShowScript
    End Sub

    Private Class DummyFinder
        Inherits Finder
        
        Public Sub New(Path As String, InnerFinder As Finder)
            Me.Path = Path
            Me.InnerFinder = InnerFinder
        End Sub

        Dim Path As String
        Dim InnerFinder As Finder

        Public Overrides Function GetAllScripts() As IList(Of String)
            Return New String(){Path}
        End Function

        Public Overrides Function Open(Path As String) As IO.Stream
            Return InnerFinder.Open(Path)
        End Function
    End Class

    Private Class DummyRecorder
        Inherits Recorder
        
        Public Sub New(InnerRecorder As Recorder)
            Me.InnerRecorder = InnerRecorder
        End Sub

        Dim InnerRecorder As Recorder

        Public Overrides Function HasRunScript(Path As String) As Boolean
            Return False
        End Function

        Public Overrides Sub RecordScript(Path As String)
            If Not InnerRecorder.HasRunScript(Path) Then
                InnerRecorder.RecordScript(Path)
            End If
        End Sub
    End Class

    Private Class DummyExecutor
        Inherits Executor
        
        Public Overrides Overloads Sub RunScript(Path As String, Script As IO.Stream)
            Console.WriteLine("Not Executing")
            'Do nothing
        End Sub
    End Class
End Class