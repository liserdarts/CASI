﻿'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Public Class ScriptRunner
    
    Public Finder As Finder
    Public Recorder As Recorder
    Public Sorter As Sorter = New FolderSorter
    Public Transaction As TransactionProvider
    Public Executor As Executor
    
    Dim Properties As IList(Of ScriptProperty)
    Public Function GetPropertyObjects() As IList(Of ScriptProperty)
        If Properties Is Nothing Then
            Dim Objects As New List(Of Object)
            Objects.Add(Finder)
            Objects.Add(Recorder)
            Objects.Add(Transaction)
            Objects.Add(Executor)

            Properties = (New ScriptPropertyCreator).GetProperties(Objects)
        End If
        Return Properties
    End Function

    Public Sub Run()
        For Each Prop In GetPropertyObjects
            Prop.Init
        Next

        Dim Scripts = Finder.GetAllScripts
        Dim NewScripts As New List(Of String)

        'Get a list of scripts that have not already run
        For Each Script In Scripts
            If Not Recorder.HasRunScript(Script) Then
                NewScripts.Add(Script)
            End If
        Next

        NewScripts = Sorter.Sort(NewScripts)

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
            Throw
        End Try

        'Record all the scripts that ran
        'The recorder may not be affected by the transaction, and can't guarantee will have it's changes reverted on an error
        For Each Script In NewScripts
            Recorder.RecordScript(Script)
        Next

    End Sub
End Class