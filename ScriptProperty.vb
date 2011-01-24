'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

<AttributeUsage(AttributeTargets.Property, AllowMultiple := False)> _
Public Class ScriptPropertyAttribute
    Inherits Attribute
    
    Public Sub New(UniqueID As String)
        LUniqueID = New Guid(UniqueID)
    End Sub

    Dim LUniqueID As Guid
    Public ReadOnly Property UniqueID() As Guid
        Get
            Return LUniqueID
        End Get
    End Property
End Class

Public MustInherit Class ScriptProperty
    
    Public ReadOnly Property PropertyName() As String
        Get
            Return Me.GetType.Name
        End Get
    End Property

    ''' <summary>
    ''' When overridden in a derived class prepares the object for use
    ''' </summary>
    Public MustOverride Sub Init()
End Class