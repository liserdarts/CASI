'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Public Class TestRecoder
    Inherits Recorder
    
    Public RanScripts As New List(Of String)({"Script1"})

    Public Overrides Function HasRunScript(Path As String) As Boolean
        Return RanScripts.Contains(Path)
    End Function

    Public Overrides Sub RecordScript(Path As String)
        RanScripts.Add(Path)
    End Sub

End Class