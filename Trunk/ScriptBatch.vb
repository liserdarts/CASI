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

    Dim LTemplate As New ScriptTemplate
    Public ReadOnly Property Template() As ScriptTemplate
        Get
            Return LTemplate
        End Get
    End Property

    Dim WithEvents LRunner As New ScriptRunner
    Public ReadOnly Property Runner() As ScriptRunner
        Get
            Return LRunner
        End Get
    End Property

    Public Sub Run()
        Runner.Run(Template)
    End Sub

    Private Sub LRunner_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles LRunner.ProgressChanged
        RaiseEvent ProgressChanged(Me, e)
    End Sub
End Class