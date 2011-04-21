'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

<TargetType(Target := GetType(Folder))> _
Public Class FolderEditor
    Implements IScriptPropertyEditor
    
    Dim WithEvents Folder As Folder
    Public Property ScriptProperty() As ScriptProperty Implements IScriptPropertyEditor.ScriptProperty
        Get
            Return Folder
        End Get
        Set
            Folder = Value
            UpdateEditor
        End Set
    End Property

    Public Property IsReadOnly() As Boolean Implements IScriptPropertyEditor.IsReadOnly
        Get
            Return Not UxFolder.IsEnabled
        End Get
        Set
            UxFolder.IsEnabled = Not Value
        End Set
    End Property
    
    Private Sub UpdateEditor()
        If ScriptProperty Is Nothing Then Return

        UxFolder.Directory = Folder.Folder
    End Sub

    Private Sub UxFolder_LostFocus(sender As System.Object, e As RoutedEventArgs) Handles UxFolder.LostFocus
        If ScriptProperty Is Nothing Then Return
        Folder.Folder = UxFolder.Directory
    End Sub
End Class