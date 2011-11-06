'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Public Class ScriptPropertyCreator

    Private Class ScriptPropertyKey
        Public Property UniqueID As Guid
        
        Public Property Properties As New List(Of ObjectProperty)
        Public Property Types As New List(Of Type)
        Public Property State As ScriptProperty
    End Class
    Private Class ObjectProperty
        Public Obj As Object
        Public Prop As Reflection.PropertyInfo
    End Class
    
    ''' <summary>
    ''' Finds and creates the script properties the given objects need
    ''' </summary>
    ''' <param name="Objects">The objects the have properties that need set</param>
    ''' <returns>A list of script property objects that the given objects need</returns>
    Public Function GetProperties(Objects As IList(Of Object)) As IList(Of ScriptProperty)
        Dim States = GetTypes(Objects)
        ResolveTypes(States)
        CreateState(States)
        AssignProperties(States)
        
        Dim StateObjects As New List(Of ScriptProperty)
        For Each State In States
            StateObjects.Add(State.Value.State)
        Next

        Return StateObjects
    End Function

    ''' <summary>
    ''' Gets the type of property objects the given objects need
    ''' </summary>
    ''' <param name="Objects">The objects to search</param>
    ''' <returns>The properties and types that need created</returns>
    Private Function GetTypes(Objects As IList(Of Object)) As Dictionary(Of Guid, ScriptPropertyKey)
        Dim Properties As New Dictionary(Of Guid, ScriptPropertyKey)

        For Each Obj In Objects
            For Each Prop In Obj.GetType.GetProperties
                If Prop.CanWrite Then
                    If Prop.PropertyType.IsSubclassOf(GetType(ScriptProperty)) Then
                        Dim Atts = Prop.GetCustomAttributes(GetType(ScriptPropertyAttribute), False)
                        Dim Att As ScriptPropertyAttribute = Nothing
                        If Atts.Count > 0 Then
                            Att = Atts(0)
                        End If

                        If Att IsNot Nothing Then 'Use this property
                            Dim ScriptProperty As ScriptPropertyKey
                            If Properties.ContainsKey(Att.UniqueID) Then
                                ScriptProperty = Properties(Att.UniqueID)
                            Else
                                ScriptProperty = New ScriptPropertyKey
                                ScriptProperty.UniqueID = Att.UniqueID
                                Properties.Add(Att.UniqueID, ScriptProperty)
                            End If
                            
                            If Not ScriptProperty.Types.Contains(Prop.PropertyType) Then 'Include this property type
                                ScriptProperty.Types.Add(Prop.PropertyType)
                            End If

                            Dim ObjectProp As New ObjectProperty
                            ObjectProp.Obj = Obj
                            ObjectProp.Prop = Prop
                            ScriptProperty.Properties.Add(ObjectProp)
                        End If
                    End If
                End If
            Next
        Next
        
        Return Properties
    End Function
    
    ''' <summary>
    ''' Finds the types required for each property
    ''' </summary>
    Private Sub ResolveTypes(Properties As Dictionary(Of Guid, ScriptPropertyKey))
        For Each Prop In Properties.Values
            Do Until Prop.Types.Count = 1
                Dim First As Type
                Dim Second As Type

                First = Prop.Types(0)
                Second = Prop.Types(1)

                If First Is Second Then 'Either can be removed
                    Prop.Types.RemoveAt(0)
                Else
                    If First.IsSubclassOf(Second) Then 'The first is more specific, remove the second
                        Prop.Types.RemoveAt(1)
                    ElseIf Second.IsSubclassOf(First) Then 'The second is more specific, remove the first
                        Prop.Types.RemoveAt(0)
                    Else 'There is no acceptable answer that can satisfy both types
                        Throw New Exception(String.Format("Neither {0} or {1} inherits from the other, no type exists that will work for the {2} property.", First, Second, Prop.UniqueID))
                    End If
                End If
            Loop

            If Prop.Types(0).IsAbstract Then
                Throw New Exception(String.Format("{0} is the most specific type found for the {1} property, but it is abstract", Prop.Types(0), Prop.UniqueID))
            End If
        Next
    End Sub

    ''' <summary>
    ''' Creates the property objects
    ''' </summary>
    ''' <param name="Properties">The property objects that need created</param>
    Private Sub CreateState(Properties As Dictionary(Of Guid, ScriptPropertyKey))
        For Each Prop In Properties.Values
            Prop.State = Activator.CreateInstance(Prop.Types(0))
        Next
    End Sub

    ''' <summary>
    ''' Assign the newly created property objects back to the properties that need them
    ''' </summary>
    ''' <param name="Properties">The properties that need assigned</param>
    Private Sub AssignProperties(Properties As Dictionary(Of Guid, ScriptPropertyKey))
        For Each State In Properties.Values
            For Each Prop In State.Properties
                Prop.Prop.SetValue(Prop.Obj, State.State, New Object(){})
            Next
        Next
    End Sub

End Class