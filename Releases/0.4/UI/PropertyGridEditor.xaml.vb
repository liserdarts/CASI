'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class PropertyGridEditor
    Implements IScriptPropertyEditor

    Dim LScriptProperty As ScriptProperty
    Public Property ScriptProperty() As ScriptProperty Implements IScriptPropertyEditor.ScriptProperty
        Get
            Return LScriptProperty
        End Get
        Set
            LScriptProperty = Value
            UxProperties.SelectedObject = Value
        End Set
    End Property

    Public Property IsReadOnly() As Boolean Implements IScriptPropertyEditor.IsReadOnly
        Get
            Return UxProperties.IsEnabled
        End Get
        Set
            UxProperties.IsEnabled = Not Value
        End Set
    End Property
    
End Class