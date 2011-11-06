'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Console
''' <summary>
''' Use the ConsolePropertyParser to build arguments for the command line
''' </summary>
Public Class BuildProperties
    Inherits ParseProperties
    
    Public Overrides Sub Test()
        CreateBatch
        Executor.TestProperty.TestProperty = "TestValue"

        Dim Parser As New ConsolePropertyParser(Batch)
        Dim Args = Parser.Build

        CreateBatch
        Parse(Args, "TestValue")
    End Sub

End Class
End Namespace