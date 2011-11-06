'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Imports System.Text.RegularExpressions

''' <summary>
''' Parses a command line and gets the passed parameters
''' </summary>
Public Class CommandLineParser

    ''' <summary>
    ''' Clears all the parsed parameters.
    ''' </summary>
    Public Sub Clear()
        LParameters.Clear
    End Sub
    
    ''' <summary>
    ''' Adds a parameter.
    ''' </summary>
    ''' <param name="Param">The name of the parameter.</param>
    ''' <param name="Value">The value of the parameter.</param>
    ''' <returns><c>True</c> if the parameter was added, otherwise <c>False</c>.</returns>
    ''' <remarks>The parameter will not be added if a parameter with the same name already exists.</remarks>
    Public Function Add(Param As String, Value As String) As Boolean
        If Not LParameters.ContainsKey(Param) Then
            LParameters.Add(Param, Value)
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Parses arguments from the command line.
    ''' </summary>
    ''' <param name="Args">The arguments passed on the command line.</param>
    ''' <remarks>
    ''' Many formats are acceptable
	''' Examples: -param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'
    ''' </remarks>
    Public Sub Parse(Args As IEnumerable(Of String))
        Dim Spliter As Regex = New Regex("^-{1,2}|^/|=|:", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
        Dim Remover As Regex = New Regex("^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
        Dim Parameter As String = Nothing
        Dim Parts As String()
        
        For Each Txt As String In Args
            'Look for new parameters (-,/ or --) and a possible enclosed value (=,:)
            Parts = Spliter.Split(Txt, 3)
            Select Case Parts.Length
            Case 1 'Found a value (for the last parameter found (space separator))
                If Parameter IsNot Nothing Then
                    Parts(0) = Remover.Replace(Parts(0), "$1")
                    Add(Parameter, Parts(0))
                    Parameter = Nothing
                End If
                'Else Error: no parameter waiting for a value (skipped)
            
            Case 2 'Found a new parameter
                If Parameter IsNot Nothing Then 'The last parameter is still waiting. With no value, set it to true.
                    Add(Parameter, "true")
                End If
                Parameter = Parts(1)
            
            Case 3 'Found a parameter with enclosed value
                If Parameter IsNot Nothing Then 'The last parameter is still waiting. With no value, set it to true.
                    Add(Parameter, "true")
                End If
                Parameter = Parts(1)
                
                If Not LParameters.ContainsKey(Parameter) Then 'Remove possible enclosing characters (",')
                    Parts(2) = Remover.Replace(Parts(2), "$1")
                    LParameters.Add(Parameter, Parts(2))
                End If
                Parameter = Nothing
            End Select
        Next
        
        If Parameter IsNot Nothing Then 'A parameter is still waiting
            If Not LParameters.ContainsKey(Parameter) Then
                LParameters.Add(Parameter, "true")
            End If
        End If
    End Sub
    
    ''' <summary>
    ''' Builds arguments that can be used on the command line.
    ''' </summary>
    ''' <returns>A string that can be parsed to recreated the parameters.</returns>
    Public Function Build() As String
        Dim Builder As New Text.StringBuilder

        For Each Param In LParameters
            Builder.Append("-")
            Builder.Append(Param.Key)
            Builder.Append("=""")
            Builder.Append(Param.Value)
            Builder.Append(""" ")
        Next

        Return Builder.ToString
    End Function

    Dim LParameters As New Dictionary(Of String, String)
    ''' <summary>
    ''' Gets all the parameters parsed into a name value dictionary.
    ''' </summary>
    Public ReadOnly Property Parameters() As IDictionary(Of String, String)
        Get
            Return New Dictionary(Of String, String)(LParameters)
        End Get
    End Property

    ''' <summary>
    ''' Gets a value indicating whether this <see cref="CommandLineParser" /> is contains the parameter.
    ''' </summary>
    ''' <value><c>True</c> if the parameter is present; otherwise, <c>False</c>.</value>
    Public ReadOnly Property Contains(Param As String) As Boolean
        Get
            Return LParameters.ContainsKey(Param)
        End Get
    End Property

    ''' <summary>
    ''' Gets the value of the given parameter.
    ''' </summary>
    Public Default ReadOnly Property Item(Param As String) As String
        Get
            If LParameters.ContainsKey(Param) Then
                Return LParameters(Param)
            Else
                Return Nothing
            End If
        End Get
    End Property
    
    ''' <summary>
    ''' Gets if a parameter is set to <code>True</code>, otherwise <code>False</code>.
    ''' </summary>
    '''<remarks>If the parameter is not present it is considered <code>False</code></remarks>
    Public ReadOnly Property ItemBool(ByVal Param As String) As Boolean
        Get
            If LParameters.ContainsKey(Param) Then
                Return LParameters(Param).ToLower = "true"
            Else
                Return False
            End If
        End Get
    End Property
End Class