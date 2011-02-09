'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Sorts scripts folder by folder
''' </summary>
Public Class FolderSorter
    Inherits Sorter
    Implements IComparer(Of String)

    Public Overrides Function Sort(Paths As IList(Of String)) As IList(Of String)
        Dim Sorted As New List(Of String)(Paths)
        Sorted.Sort(Me)
        Return Sorted
    End Function
    
    Public Function Compare(X As String, Y As String) As Integer Implements IComparer(Of String).Compare
        Dim XAray = X.Split("\"c, "/"c)
        Dim YAray = Y.Split("\"c, "/"c)
            
        For I As Integer = 0 To Math.Min(XAray.Length, YAray.Length) - 1
            Dim Diff = XAray(I).CompareTo(YAray(I))
            If Diff <> 0 Then
                Return Diff
            End If
        Next

        Return XAray.Length.CompareTo(YAray.Length)
    End Function

End Class