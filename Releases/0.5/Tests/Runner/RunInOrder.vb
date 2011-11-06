'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Runner
''' <summary>
''' Scripts should always run in the order the sorter sorts them
''' </summary>
Public Class RunInOrder
    Inherits Base

    Public Overrides Sub Test()
        MyBase.Test

        AssertGreater(Executor.RunLog.Count, 1)

        Dim Sorted = (Template.Sorter.Sort(Finder.Scripts))
        Dim LastIndex As Integer
        For Each Script In Executor.RunLog
            If Executor.RunLog.Contains(Script) Then
                AssertGreater(Executor.RunLog.IndexOf(Script), LastIndex - 1)
                LastIndex = Sorted.IndexOf(Script)
            End If
        Next
    End Sub

    
    Protected Overrides Sub CreateRunner()
        MyBase.CreateRunner
        
        Finder.Scripts.Reverse
    End Sub

End Class
End Namespace