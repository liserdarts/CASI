'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class GenericDesigner

    Public Property ItemsSource() As IEnumerable(Of ScriptProperty)
        Get
            Return UxPropertyObjects.ItemsSource
        End Get
        Set
            UxPropertyObjects.ItemsSource = Value
        End Set
    End Property

    Dim LIsReadOnly As Boolean
    Public Property IsReadOnly() As Boolean
        Get
            Return LIsReadOnly
        End Get
        Set
            LIsReadOnly = Value
            For Each Child In FindChildren(UxPropertyObjects)
                Child.IsReadOnly = Value
            Next
        End Set
    End Property
    
    Private Function FindChildren(Parent As FrameworkElement) As List(Of IScriptPropertyEditor)
        Dim Children As New List(Of IScriptPropertyEditor)
        
        For I As Integer = 0 To VisualTreeHelper.GetChildrenCount(Parent) - 1
            Dim Cur As DependencyObject
            Cur = VisualTreeHelper.GetChild(Parent, I)
            
            If TypeOf Cur Is IScriptPropertyEditor Then
                Children.Add(Cur)
            Else
                Children.AddRange(FindChildren(Cur))
            End If
        Next
        
        Return Children
    End Function
End Class