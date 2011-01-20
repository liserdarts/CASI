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