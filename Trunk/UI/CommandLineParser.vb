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
    ''' Initializes a new instance of the <see cref="CommandLineParser" /> class.
    ''' </summary>
    ''' <param name="Args">The arguments passed on the command line.</param>
    ''' <remarks>
    ''' Many formats are acceptable
	''' Examples: -param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'
    ''' </remarks>
    Public Sub New(Args As IEnumerable(Of String))
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
                    If Not LParameters.ContainsKey(Parameter) Then
                        Parts(0) = Remover.Replace(Parts(0), "$1")
                        LParameters.Add(Parameter, Parts(0))
                    End If
                    Parameter = Nothing
                End If
                'Else Error: no parameter waiting for a value (skipped)
            
            Case 2 'The last parameter is still waiting. With no value, set it to true.
                If Parameter IsNot Nothing Then
                    If Not LParameters.ContainsKey(Parameter) Then
                        LParameters.Add(Parameter, "true")
                    End If
                End If
                Parameter = Parts(1)
            
            Case 3 'Parameter with enclosed value
                If Parameter IsNot Nothing Then 'The last parameter is still waiting. With no value, set it to true.
                    If Not LParameters.ContainsKey(Parameter) Then
                        LParameters.Add(Parameter, "true")
                    End If
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