'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace SqlRecorder
''' <summary>
''' Make sure RecordScript can be called before HasRunScript
''' </summary>
Public Class RecordScriptFirst
    Inherits Base
    
    Protected Overrides Sub CreateRecorder()
        MyBase.CreateRecorder

        Recorder.RecordScript("RecordScriptFirst")
    End Sub

End Class
End Namespace