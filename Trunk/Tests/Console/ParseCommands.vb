﻿'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Console
''' <summary>
''' Test using <c>CommandLineParser</c> to parse arguments from the command line
''' </summary>
Public Class ParseCommands
    Inherits TestFramework.TestCase
    
    Protected Args As List(Of String)
    Protected Parameters As Dictionary(Of String, String)

    Public Overrides Sub Test()
        Args = New List(Of String)
        Parameters = New Dictionary(Of String, String)

        AddArg("-param1=value1", "param1", "value1")
        AddArg("--param2=value2", "param2", "value2")
        AddArg("/param3=value3", "param3", "value3")

        AddArg("-param4:value4", "param4", "value4")
        AddArg("-param5 value5", "param5", "value5")

        AddArg("-param6=""value6-/=""", "param6", "value6-/=")
        AddArg("-param7=""value7-/=""", "param7", "value7-/=")

        AddArg("-FalseParam=false", "FalseParam", "false")
        AddArg("-FalseParam2=false", "FalseParam2", "false")
        AddArg("-TrueParam", "TrueParam", "true")

        CheckParams
    End Sub

    Protected Overridable Sub AddArg(Arg As String, Name As String, Value As String)
        Args.AddRange(Arg.Split(" "))
        Parameters.Add(Name, Value)
    End Sub

    Protected Overridable Sub CheckParams()
        Dim Parser As New CommandLineParser
        Parser.Parse(Args)
        
        AssertGreater(Parameters.Count, 0)
        AssertIsEqual(Parameters.Count, Parser.Parameters.Count)
        For Each Param In Parameters
            AssertIsTrue(Parser.Parameters.ContainsKey(Param.Key), Param.Key)
            AssertIsEqual(Parser.Parameters(Param.Key), Param.Value)
            AssertIsEqual(Parser(Param.Key), Param.Value)
            AssertIsEqual(Parser.ItemBool(Param.Key), Param.Value.ToLower = "true")
        Next

        AssertIsNothing(Parser("Doesn'tExist"))
    End Sub

End Class
End Namespace