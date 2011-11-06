'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class Progress

    Dim WithEvents LBatch As ScriptBatch
    Public Property Batch() As ScriptBatch
        Get
            Return LBatch
        End Get
        Set
            LBatch = Value
        End Set
    End Property

    Private Sub Batch_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles LBatch.ProgressChanged
        If Not Dispatcher.CheckAccess Then
            Dispatcher.BeginInvoke(New EventHandler(Of ProgressChangedEventArgs)(AddressOf Batch_ProgressChanged), sender, e)
            Return
        End If

        Select Case e.Stage
        Case ProgressChangedEventArgs.ProgressStages.Initialize
            UxProgress.IsIndeterminate = False
        Case ProgressChangedEventArgs.ProgressStages.GetScripts
            UxProgress.IsIndeterminate = True
        Case ProgressChangedEventArgs.ProgressStages.FilterScripts
            UxProgress.IsIndeterminate = False
        Case ProgressChangedEventArgs.ProgressStages.BeginTransaction
            UxProgress.IsIndeterminate = True
        Case ProgressChangedEventArgs.ProgressStages.RunScripts
            UxProgress.IsIndeterminate = False
        Case ProgressChangedEventArgs.ProgressStages.CommitTransaction
            UxProgress.IsIndeterminate = True
        Case ProgressChangedEventArgs.ProgressStages.RecordScripts
            UxProgress.IsIndeterminate = False
        Case ProgressChangedEventArgs.ProgressStages.Close
            UxProgress.IsIndeterminate = False
        Case Else
            UxProgress.IsIndeterminate = True
        End Select

        UxProgress.Value = e.Progress * 100
        UxStage.Text = e.GetStageText
    End Sub
End Class