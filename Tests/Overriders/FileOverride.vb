Namespace Overriders
Public Class FileOverride
    Inherits TestFramework.Override
    Implements Data.Files.IFileManager
    
    Protected Overrides Sub StartOverrideInternal()
        Data.Files.Manager = Me
    End Sub
    Protected Overrides Sub StopOverrideInternal()
        Data.Files.Manager = Nothing
    End Sub
    
    Protected Overrides Sub EnterTransactionInternal()
        Files = New Dictionary(Of String, ReusableStream)
        
        Dim Resources As New List(Of String)
        Resources.Add("SampleNotes.txt")
        Resources.Add("SampleScript1.sql")
        Resources.Add("SampleScript2.sql")
        
        For Each Resource In Resources
            Using File = Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream("ScriptHelper.Tests." & Resource)
                Using NewStream As New ReusableStream
                    Dim Buffer(256) As Byte
                    Dim Length As Integer
                    Do Until File.Position = File.Length
                        Length = File.Read(Buffer, 0, 256)
                        NewStream.Write(Buffer, 0, Length)
                    Loop
                    Files.Add(Resource, NewStream)
                End Using
            End Using
        Next
        
        Files.Add(Me.GetType.Assembly.Location, Nothing)
    End Sub
    Protected Overrides Sub RollbackTransactionInternal()
        For Each File In Files
            If File.Value IsNot Nothing Then
                If Not File.Value.Closed Then 'The test should not pass
                    Link.Fail(String.Format("File {0} was not closed.", File.Key), New StackTrace(True))
                End If
            End If
        Next
        Files = Nothing
    End Sub

    Dim Files As Dictionary(Of String, ReusableStream)
    
    Public Function OpenFile(Path As String, Mode As IO.FileMode) As IO.Stream Implements Data.Files.IFileManager.OpenFile
        'Create or empty the file, or throw an exception if the mode will not work
        Select Case Mode
        Case IO.FileMode.Append, IO.FileMode.Truncate
            Throw New NotSupportedException("Append and Truncate are not supported")
        Case IO.FileMode.Create
            If Files.ContainsKey(Path) Then
                Files(Path) = New IO.MemoryStream
            End If
        Case IO.FileMode.CreateNew
            If Files.ContainsKey(Path) Then
                Throw New IO.IOException(String.Format("The file '{0}' already exists", Path))
            End If
        Case IO.FileMode.Open
            If Files.ContainsKey(Path) = False Then
                Throw New IO.IOException(String.Format("The file '{0}' does not exists", Path))
            End If
        End Select
        
        If Files.ContainsKey(Path) = False Then
            Files.Add(Path, New ReusableStream)
        End If
        
        Dim File As ReusableStream
        File = Files(Path)
        If File.Closed = False Then
            Throw New Exception(String.Format("File {0} is already open.", Path))
        End If
        Return File
    End Function
    
    Public Function FindFiles(Pattern As String) As List(Of String) Implements Data.Files.IFileManager.FindFiles
        Dim Results As New List(Of String)
        For Each File In Files.Keys
            If File Like Pattern Then
                Results.Add(File)
            End If
        Next
        
        Return Results
    End Function
    
    Private Class ReusableStream
        Inherits IO.MemoryStream
        
        Public Overrides Sub Close()
            Seek(0, IO.SeekOrigin.Begin)
            Closed = True
        End Sub
        
        Dim LClosed As Boolean = True
        Public Property Closed() As Boolean
            Get
                Return LClosed
            End Get
            Set
                LClosed = Value
            End Set
        End Property
    End Class

End Class
End Namespace
