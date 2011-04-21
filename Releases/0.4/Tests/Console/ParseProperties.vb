'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Console
''' <summary>
''' Use the ParseProperties class to set properties on ScriptProperty objects
''' </summary>
Public Class ParseProperties
    Inherits TestFramework.TestCase
    
    Protected Executor As TestExecutor
    Protected Runner As ScriptRunner

    Public Overrides Sub Test()
        CreateRunner
        Parse("-TestScriptProperty.TestProperty=TestValue", "TestValue")
    End Sub

    Protected Overridable Sub CreateRunner()
        Executor = New TestExecutor
        
        Runner = New ScriptRunner
        Runner.Executor = Executor
        Runner.Finder = New TestFinder
        Runner.Recorder = New TestRecoder
        Runner.Transaction = New TestTransactionProvider

        Runner.GetPropertyObjects
    End Sub

    Protected Sub Parse(Args As String, Value As String)
        Dim Parser As New ConsolePropertyParser(Runner)
        AssertIsFalse(Parser.Parse(New String(){}))

        AssertIsTrue(Parser.Parse(Args.Split(" ")))
        AssertIsEqual(Executor.TestProperty.TestProperty, value)
    End Sub
End Class
End Namespace