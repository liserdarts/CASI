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

    Dim Properties As IList(Of ScriptProperty)
    ''' <summary>
    ''' Gets all the <c>ScriptProperty</c> objects used for configuration.
    ''' </summary>
    ''' <returns>If the objects have not been created, the will be. Multiple calls will return the same objects.</returns>
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
        
        NewTemplate.Properties = GetPropertyObjects

        Return NewTemplate
    End Function
End Class