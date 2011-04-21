'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class TestScriptProperty
    Inherits ScriptProperty
    
    Public Property TestProperty() As String
    Public Property Open() As Boolean

    Public Overrides Sub Init()
        Open = True
    End Sub
    
    Public Overrides Sub Close()
        Open = False
    End Sub

End Class