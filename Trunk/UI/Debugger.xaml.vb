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

        Dim Scripts = Batch.Template.Finder.GetAllScripts
        Scripts = Batch.Template.Sorter.Sort(Scripts)
        UxScripts.ItemsSource = Scripts.Reverse
    End Sub

End Class