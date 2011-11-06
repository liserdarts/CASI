'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Runner
Public Class OnlyRunOnce
    Inherits Base
    
    Public Overrides Sub Test()
        CreateRunner
        
        Batch.Run
        AssertGreater(Executor.RunLog.Count, 0)
        
        Executor.RunLog.Clear
        Batch.Run
        AssertIsEqual(Executor.RunLog.Count, 0)
    End Sub
End Class
End Namespace