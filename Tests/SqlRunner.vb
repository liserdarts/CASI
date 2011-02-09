'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class SqlRunner
    Inherits TestFramework.TestCase
    
    'ToDo: Create a TestSqlExecutor that uses a SQL server CE database

    Protected WithEvents Runner As ScriptRunner
    Protected WithEvents Finder As CASI.FileFinder
    Protected WithEvents Transaction As Sql.SqlTransactionProvider
    Protected WithEvents Executor As Executor
    Protected WithEvents Recorder As Sql.SqlRecorder
    
    Public Overrides Sub Test()
        CreateRunner
        SetPropertyObjects
        Run
    End Sub

    Protected Overridable Sub CreateRunner()
        Runner = New ScriptRunner
        
        Finder = New CASI.FileFinder
        Transaction = New Sql.SqlTransactionProvider
        Executor = New TestSqlExecutor
        Recorder = New Sql.SqlRecorder

        Runner.Finder = Finder
        Runner.Recorder = Recorder
        Runner.Transaction = Transaction
        Runner.Executor = Executor
    End Sub

    Protected Overridable Sub SetPropertyObjects()
        Runner.GetPropertyObjects
    End Sub

    Protected Overridable Sub Run()
        Runner.Run
    End Sub

End Class
