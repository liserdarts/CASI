'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Namespace SqlTransactionProvider
Public MustInherit Class Base
    Inherits TestFramework.TestCase

    Protected Connection As Common.DbConnection
    Protected Transaction As Sql.SqlTransactionProvider
    Protected Executor As Sql.SqlExecutor

    Public Overrides Sub Test()
        GetConnection
        
        Transaction = New Sql.SqlTransactionProvider
        Executor = New Sql.SqlExecutor
        Executor.Transaction = Transaction
        Executor.Transaction.Connection = Connection
        
        Executor.RunScript("Create Table NewTable (Col1 Int)")

        Transaction.BeginTransaction
        Executor.RunScript("Insert Into NewTable (Col1) Values (1)")

        EndTransaction
        
        Dim Count As Integer
        Using Cmd = Connection.CreateCommand
            Cmd.CommandText = "Select Count(*) From NewTable"
            Count = Cmd.ExecuteScalar
        End Using

        ValidateRows(Count)
    End Sub
    
    Protected Overridable Sub GetConnection()
        Connection = GetOverride(Of Overriders.SqlDatabaseOverride).GetConnection
    End Sub

    Protected MustOverride Sub EndTransaction()
    Protected MustOverride Sub ValidateRows(RowCount As Integer)

End Class
End Namespace