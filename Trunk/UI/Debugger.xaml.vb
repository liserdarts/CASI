'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class Debugger
    Dim LBatch As ScriptBatch
    Public Property Batch() As ScriptBatch
        Get
            Return LBatch
        End Get
        Set
            LBatch = Value
            ShowScripts
        End Set
    End Property

    Private Sub ShowScripts()
        If Batch Is Nothing Then
            UxScripts.ItemsSource = Nothing
            Return
        End If

        Dim Scripts As New List(Of String)
        For Each Tem In Batch.Templates
            Dim TemScripts = Tem.Finder.GetAllScripts
            TemScripts = Tem.Sorter.Sort(TemScripts)
            Scripts.AddRange(TemScripts)
        Next

        Scripts.Reverse
        UxScripts.ItemsSource = Scripts
    End Sub

End Class