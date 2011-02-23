'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Sorts that paths alphabetically by comparing each folder in the paths separately
''' </summary>
''' <remarks>
''' Consider this list of scripts
'''	1.2\Somthing.sql
'''	1.2.1\Something.sql
''' A straight alphabetical sort would put everything in the 1.2.1 folder before the 1.2 folder because "." comes before "\". This is probably not what was intended. The <c>CASI.FolderSorter</c> class splits on the backslash (or forward slash) and will compare just "1.2" to "1.2.1". It will determine "1.2" should be before "1.2.1" and not even look at the rest of the path. If the folders did match it would continue comparing the rest of the path in the same way.
''' </remarks>
Public Class FolderSorter
    Inherits Sorter
    Implements IComparer(Of String)

    ''' <summary>
    ''' Sorts the paths
    ''' </summary>
    ''' <param name="Paths">The paths to sort</param>
    ''' <returns>The sorted paths</returns>
    Public Overrides Function Sort(Paths As IList(Of String)) As IList(Of String)
        Dim Sorted As New List(Of String)(Paths)
        Sorted.Sort(Me)
        Return Sorted
    End Function
    
    ''' <summary>
    ''' Compares two paths and determines what order they belong in
    ''' </summary>
    ''' <param name="X">The first path to compare</param>
    ''' <param name="Y">The second path to compare</param>
    ''' <returns>A 32-bit signed integer that indicates whether the first path precedes, follows, or appears in the same position in the sort order as the value parameter.</returns>
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