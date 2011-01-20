Namespace Runner
Public Class OnlyRunOnce
    Inherits Base
    
    Public Overrides Sub Test()
        CreateRunner
        
        Runner.Run
        AssertGreater(Executor.RunLog.Count, 0)
        
        Executor.RunLog.Clear
        Runner.Run
        AssertIsEqual(Executor.RunLog.Count, 0)
    End Sub
End Class
End Namespace