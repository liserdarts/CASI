'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Console
Public Class ParseProperties
    Inherits TestFramework.TestCase
    
    Public Overrides Sub Test()
        Dim Executor As New TestExecutor
        
        Dim Runner As New ScriptRunner
        Runner.Executor = Executor
        Runner.Finder = New TestFinder
        Runner.Recorder = New TestRecoder
        Runner.Transaction = New TestTransactionProvider
        
        Dim Parser As New ConsolePropertyParser(Runner)
        AssertIsFalse(Parser.Parse(New String(){}))

        AssertIsFalse(Executor.TestProperty.Open)
        Dim Args As New List(Of String)
        Args.Add("-TestScriptProperty.TestProperty=TestValue")
        AssertIsTrue(Parser.Parse(Args))
        AssertIsEqual(Executor.TestProperty.TestProperty, "TestValue")
    End Sub
End Class
End Namespace