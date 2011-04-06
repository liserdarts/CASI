'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class GenericEditor
    Implements IScriptPropertyEditor

    Shared ReadOnly ScriptPropertyProperty As DependencyProperty

    Shared Sub New()
        ScriptPropertyProperty = DependencyProperty.Register("ScriptProperty", GetType(ScriptProperty), GetType(GenericEditor), New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnScriptPropertyChanged)))
    End Sub
    Private Shared Sub OnScriptPropertyChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs) 
        Dim Editor As GenericEditor = d
        Editor.ScriptProperty = e.NewValue
    End Sub

    Dim LScriptProperty As ScriptProperty
    Public Property ScriptProperty() As ScriptProperty Implements IScriptPropertyEditor.ScriptProperty
        Get
            Return LScriptProperty
        End Get
        Set
            LScriptProperty = Value
            CreateEditor
        End Set
    End Property
    
    Dim LIsReadOnly As Boolean
    Public Property IsReadOnly() As Boolean Implements IScriptPropertyEditor.IsReadOnly
        Get
            Return LIsReadOnly
        End Get
        Set
            LIsReadOnly = Value
            Editor.IsReadOnly = Value
        End Set
    End Property

    Dim Editor As IScriptPropertyEditor

    Private Function FindEditorType() As Type
        For Each Type In Me.GetType.Assembly.GetTypes
            If Type.IsSubclassOf(GetType(UIElement)) And Not Type.IsAbstract Then 'It is a non abstract class class
                If Type.GetInterfaces.Contains(GetType(IScriptPropertyEditor)) Then 'It implements the correct interface
                    For Each Att In Type.GetCustomAttributes(GetType(TargetTypeAttribute), True)
                        Dim Target As TargetTypeAttribute = Att
                        If Target.Target Is ScriptProperty.GetType Then 'This is the editor we're looking for
                            Return Type
                        End If
                    Next
                End If
            End If
        Next

        Return Nothing
    End Function

    Private Function FindEditor() As IScriptPropertyEditor
        Dim Type = FindEditorType
        
        If Type IsNot Nothing Then
            Return Activator.CreateInstance(Type)
        Else
            Return New PropertyGridEditor
        End If
    End Function

    Private Sub CreateEditor()
        UxRoot.Children.Clear

        Editor = FindEditor
        Editor.ScriptProperty = ScriptProperty
        Editor.IsReadOnly = IsReadOnly
        UxRoot.Children.Add(Editor)
    End Sub
    
End Class