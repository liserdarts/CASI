Public Class ProgressChangedEventArgs
    Inherits EventArgs

    Public Enum ProgressStages
        Initialize = 1
        GetScripts = 2
        FilterScripts = 3
        BeginTransaction = 4
        RunScripts = 5
        CommitTransaction = 6
        RecordScripts = 7
    End Enum

    Public Sub New(Stage As ProgressStages, Progress As Double)
        Me.Stage = Stage
        Me.Progress = Progress
    End Sub
    
    Public Property Stage As ProgressStages
    Public Property Progress As Double
End Class