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
    Protected Batch As ScriptBatch

    Public Overrides Sub Test()
        CreateBatch
        Parse("-TestScriptProperty.TestProperty=TestValue", "TestValue")
    End Sub

    Protected Overridable Sub CreateBatch()
        Executor = New TestExecutor
        
        Dim Template As New ScriptTemplate
        Template = New ScriptTemplate
        Template.Executor = Executor
        Template.Finder = New TestFinder
        Template.Recorder = New TestRecoder
        Template.Transaction = New TestTransactionProvider

        Batch = New ScriptBatch
        Batch .AddTemplate(Template)
        Batch.GetPropertyObjects
    End Sub

    Protected Sub Parse(Args As String, Value As String)
        Dim Parser As New ConsolePropertyParser(Batch)
        AssertIsFalse(Parser.Parse(New String(){}))

        AssertIsTrue(Parser.Parse(Args.Split(" ")))
        AssertIsEqual(Executor.TestProperty.TestProperty, value)
    End Sub
End Class
End Namespace