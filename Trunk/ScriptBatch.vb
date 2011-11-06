'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Combines a <see cref="ScriptTemplate" /> with a <see cref="ScriptRunner" />
''' </summary>
Public Class ScriptBatch

    ''' <summary>
    ''' Occurs when the progress of running the scripts has changed.
    ''' </summary>
    Event ProgressChanged As EventHandler(Of ProgressChangedEventArgs)
    Protected Overridable Sub OnProgressChanged(Stage As ProgressChangedEventArgs.ProgressStages, Progress As Double)
        RaiseEvent ProgressChanged(Me, New ProgressChangedEventArgs(Stage, Progress))
    End Sub
    
    Dim WithEvents LRunner As New ScriptRunner
    Public ReadOnly Property Runner() As ScriptRunner
        Get
            Return LRunner
        End Get
    End Property
    Private Sub LRunner_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles LRunner.ProgressChanged
        OnProgressChanged(e.Stage, e.Progress)
    End Sub

    ''' <summary>
    ''' Adds a template to this batch
    ''' </summary>
    Public Sub AddTemplate(Templates As ScriptTemplate)
        If Properties IsNot Nothing Then
            Throw New InvalidOperationException("Templates can't be added after the property objects are created.")
        End If
        LTemplates.Add(Templates)
    End Sub

    Dim LTemplates As New List(Of ScriptTemplate)
    Public ReadOnly Property Templates() As IList(Of ScriptTemplate)
        Get
            Return New List(Of ScriptTemplate)(LTemplates)
        End Get
    End Property

    Dim Properties As IList(Of ScriptProperty)
    ''' <summary>
    ''' Gets all the <c>ScriptProperty</c> objects used for configuration.
    ''' </summary>
    ''' <returns>If the objects have not been created, the will be. Multiple calls will return the same objects.</returns>
    Public Function GetPropertyObjects() As IList(Of ScriptProperty)
        If Properties Is Nothing Then
            Dim Objects As New List(Of Object)
            For Each Template In Templates
                Objects.Add(Template.Finder)
                Objects.Add(Template.Recorder)
                Objects.Add(Template.Transaction)
                Objects.Add(Template.Executor)
            Next

            Properties = (New ScriptPropertyCreator).GetProperties(Objects)
        End If
        Return New List(Of ScriptProperty)(Properties)
    End Function

    Public Sub Run()
        SyncLock Me
            Try
                Initialize

                For Each Template In Templates
                    Runner.Run(Template)
                Next
            Finally
                Close
            End Try
        End SyncLock
    End Sub

    ''' <summary>
    ''' Initializes every <see cref="ScriptProperty" /> object.
    ''' </summary>
    Private Sub Initialize()
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.Initialize, 0)

        Dim Properties = GetPropertyObjects
        For I As Integer = 0 To Properties.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.Initialize, I / Properties.Count)
            Console.WriteLine("Initializing " & Properties(I).GetType.FullName)
            Properties(I).Init
        Next
    End Sub
        ''' <summary>
    ''' Closes every <see cref="ScriptProperty" /> object.
    ''' </summary>
    Private Sub Close()
        OnProgressChanged(ProgressChangedEventArgs.ProgressStages.Close, 0)

        Dim Properties = GetPropertyObjects
        For I As Integer = 0 To Properties.Count - 1
            OnProgressChanged(ProgressChangedEventArgs.ProgressStages.Close, I / Properties.Count)
            Console.WriteLine("Closing " & Properties(I).GetType.FullName)
            Properties(I).Close
        Next
    End Sub
End Class