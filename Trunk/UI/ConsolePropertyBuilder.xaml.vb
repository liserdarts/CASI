'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class ConsolePropertyBuilder

    Public Shadows Property DataContext() As ScriptTemplate
        Get
            Return MyBase.DataContext
        End Get
        Set
            MyBase.DataContext = Value
            UpdateText
        End Set
    End Property
    
    Private Sub UxText_GotFocus(sender As Object, e As RoutedEventArgs) Handles UxText.GotFocus
        UpdateText
    End Sub

    Private Sub UpdateText()
        If DataContext Is Nothing Then
            UxText.Text = ""
            Return
        End If

        Dim Builder As New ConsolePropertyParser(DataContext)
        UxTexT.Text = Builder.Build
    End Sub
End Class