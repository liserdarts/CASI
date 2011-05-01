'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class SqlRunner
    Inherits TestFramework.TestCase
    
    'ToDo: Create a TestSqlExecutor that uses a SQL server CE database

    Protected WithEvents Batch As ScriptBatch
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
        Batch = New ScriptBatch
        
        Finder = New CASI.FileFinder
        Transaction = New Sql.SqlTransactionProvider
        Executor = New TestSqlExecutor
        Recorder = New Sql.SqlRecorder

        Batch.Template.Finder = Finder
        Batch.Template.Recorder = Recorder
        Batch.Template.Transaction = Transaction
        Batch.Template.Executor = Executor
    End Sub

    Protected Overridable Sub SetPropertyObjects()
        Batch.Template.GetPropertyObjects
    End Sub

    Protected Overridable Sub Run()
        Batch.Run
    End Sub

End Class
