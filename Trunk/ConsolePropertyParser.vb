'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Parses arguments from the command line and sets properties on the <see cref="ScriptProperty" /> objects from a <see cref="ScriptRunner" />.
''' </summary>
Public Class ConsolePropertyParser
    
    Dim WithEvents Template As ScriptTemplate

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ConsolePropertyParser" /> class.
    ''' </summary>
    ''' <param name="Template">The <see cref="ScriptTemplate" /> to use.</param>
    Public Sub New(Template As ScriptTemplate)
        Me.Template = Template
    End Sub

    ''' <summary>
    ''' Parses command line arguments and sets <see cref="ScriptProperty" /> objects from the <see cref="ScriptRunner" />.
    ''' </summary>
    ''' <param name="Args">The arguments from the command line</param>
    ''' <returns><c>True</c> if any properties were set, otherwise <c>False</c>.</returns>
    ''' <remarks></remarks>
    Public Function Parse(Args As IEnumerable(Of String)) As Boolean
        Dim CommandLine As New CommandLineParser
        Dim PropertySet As Boolean

        CommandLine.Parse(Args)
        
        For Each Obj In Template.GetPropertyObjects
            For Each Prop In Obj.GetType.GetProperties
                If Prop.CanWrite And Prop.GetIndexParameters.Length = 0 Then
                    PropertySet = PropertySet Or SetProperty(CommandLine, Prop, Obj)
                End If
            Next
        Next
        
        Return PropertySet
    End Function

    Private Function SetProperty(CommandLine As CommandLineParser, Prop As Reflection.PropertyInfo, Obj As Object) As Boolean
        Dim PropertySet As Boolean

        Dim FullName = String.Format("{0}.{1}", Prop.DeclaringType.Name, Prop.Name)
        If CommandLine.Contains(FullName) Then
            Prop.SetValue(Obj, CommandLine(FullName), Nothing)
            PropertySet = True
        End If
        
        If Prop.DeclaringType.Name <> Obj.GetType.Name Then
            FullName = String.Format("{0}.{1}", Obj.GetType.Name, Prop.Name)
            If CommandLine.Contains(FullName) Then
                Prop.SetValue(Obj, CommandLine(FullName), Nothing)
                PropertySet = True
            End If
        End If

        Return PropertySet
    End Function

    ''' <summary>
    ''' Builds arguments that can be used on the command line.
    ''' </summary>
    ''' <returns>A string that can be parsed to set the properties again.</returns>
    Public Function Build() As String
        Dim Builder As New CommandLineParser
        
        For Each Obj In Template.GetPropertyObjects
            For Each Prop In Obj.GetType.GetProperties
                If Prop.CanRead And Prop.CanWrite And Prop.GetIndexParameters.Length = 0 And Prop.PropertyType Is GetType(String) Then
                    Builder.Add(String.Format("{0}.{1}", Prop.DeclaringType.Name, Prop.Name), Prop.GetValue(Obj, Nothing))
                End If
            Next
        Next

        Return Builder.Build
    End Function

End Class