'Copyright (c) 2011-2013, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class Progress

    Dim LastProgress As ProgressChangedEventArgs
    Dim StartTime As Date
    Dim WithEvents Timer As System.Threading.Timer

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
        
        If LastProgress Is Nothing OrElse LastProgress.Stage <> e.Stage Then
            StartTime = Date.Now
        End If
        LastProgress = e
        
        If Timer Is Nothing Then
            Timer = New System.Threading.Timer(AddressOf UpdateTime)
        End If
        If UxProgress.IsIndeterminate Then
            Timer.Change(0, System.Threading.Timeout.Infinite)
        Else
            Timer.Change(0, 1000)
        End If
    End Sub

    Private Sub UpdateTime()
        Try
            Dim TimeSpent = Date.Now - StartTime
            
            If TimeSpent.TotalSeconds < 2 Or LastProgress.Progress = 0 Then
                SetText(LastProgress.GetStageText)
                Return
            End If

            Dim TimeLeft = TimeSpan.FromTicks(TimeSpent.Ticks * (1 / LastProgress.Progress) * (1 - LastProgress.Progress))
            Dim Text = "{0} ({1} Second(s) Remaining)"

            SetText(String.Format(Text, LastProgress.GetStageText, Math.Round(TimeLeft.TotalSeconds)))
        Catch Ex As Exception
        End Try
    End Sub

    Private Sub SetText(Text As String)
        If Not Dispatcher.CheckAccess Then
            Dispatcher.Invoke(New Action(Of String)(AddressOf SetText), Text)
            Return
        End If
        UxStage.Text = Text
    End Sub
End Class