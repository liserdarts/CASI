''' <summary>
''' Provides data for the <c>CASI.ScriptRunner.ProgressChanged</c> event.
''' </summary>
Public Class ProgressChangedEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Specifies a stage of a <c>CASI.ScriptRunner</c> object.
    ''' </summary>
    Public Enum ProgressStages
        Initialize = 1
        GetScripts = 2
        FilterScripts = 3
        BeginTransaction = 4
        RunScripts = 5
        CommitTransaction = 6
        RecordScripts = 7
        Close = 8
    End Enum

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ProgressChangedEventArgs" /> class.
    ''' </summary>
    ''' <param name="Stage">The stage.</param>
    ''' <param name="Progress">The progress.</param>
    Public Sub New(Stage As ProgressStages, Progress As Double)
        Me.Stage = Stage
        Me.Progress = Progress
    End Sub
    
    ''' <summary>
    ''' Gets or sets the stage.
    ''' </summary>
    Public Property Stage As ProgressStages

    ''' <summary>
    ''' Gets or sets the progress percentage.
    ''' </summary>
    ''' <value>The progress as a decimal between 0 and 1.</value>
    Public Property Progress As Double
End Class