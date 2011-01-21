'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Namespace Runner
Public Class RunInOrder
    Inherits BAse

    'Scripts should always run in the order the finder returns them

    Public Overrides Sub Test()
        MyBase.Test

        AssertGreater(Executor.RunLog.Count, 1)

        Dim LastIndex As Integer
        For Each Script In Executor.RunLog
            AssertGreater(Finder.Scripts.IndexOf(Script), LastIndex - 1)
            LastIndex = Finder.Scripts.IndexOf(Script)
        Next
    End Sub

End Class
End Namespace