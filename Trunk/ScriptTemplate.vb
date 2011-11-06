'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Contains properties used to set how scripts are run.
''' </summary>
Public Class ScriptTemplate
    Implements ICloneable

    Public Property Finder As Finder
    Public Property Recorder As Recorder
    Public Property Sorter As Sorter = New FolderSorter
    Public Property Transaction As TransactionProvider
    Public Property Executor As Executor

    Private Function CloneObj() As Object Implements ICloneable.Clone
        Return Clone
    End Function
    Public Function Clone() As ScriptTemplate
        Dim NewTemplate As New ScriptTemplate
        NewTemplate.Finder = Finder
        NewTemplate.Recorder = Recorder
        NewTemplate.Sorter = Sorter
        NewTemplate.Transaction = Transaction
        NewTemplate.Executor = Executor

        Return NewTemplate
    End Function
End Class