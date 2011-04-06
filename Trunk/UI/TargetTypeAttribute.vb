'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

<AttributeUsage(AttributeTargets.Class)> _
Public Class TargetTypeAttribute
    Inherits Attribute

    Public Property Target() As Type
End Class