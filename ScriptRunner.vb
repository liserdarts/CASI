Public Class ScriptRunner
    
    Public Finder As Finder
    Public Recorder As Recorder
    Public Transaction As TransactionProvider
    Public Executor As Executor

    Public Sub Run()
        Dim Scripts = Finder.GetAllScripts
        Dim NewScripts As New List(Of String)

        'Get a list of scripts that have not already run
        For Each Script In Scripts
            If Not Recorder.HasRunScript(Script) Then
                NewScripts.Add(Script)
            End If
        Next

        Transaction.BeginTransaction

        Try
            For Each Path In NewScripts
                Console.WriteLine("Running script " & Path)
                Using Stream = Finder.Open(Path)
                    Executor.RunScript(Stream)
                End Using
            Next

            Transaction.CommitTransaction
        Catch Ex As Exception
            Transaction.RollbackTransaction
            Console.WriteLine(Ex.ToString)
            Throw
        End Try

        'Record all the scripts that ran
        'The recorder may not be affected by the transaction, and can't guarantee will have it's changes reverted on an error
        For Each Script In NewScripts
            Recorder.RecordScript(Script)
        Next

    End Sub
End Class