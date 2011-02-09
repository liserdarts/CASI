'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

''' <summary>
''' Executes scripts on a SQL Server CE database
''' </summary>
''' <remarks></remarks>
Public Class TestSqlExecutor
    Inherits Sql.SqlExecutor
    
    <ScriptPropertyAttribute("e5eecd6f-3481-41ae-bea8-46c96ce1ea5b")> _
    Public Shadows Property Connection() As TestSqlConnection
        Get
            Return MyBase.Connection
        End Get
        Set
            Value = Value
        End Set
    End Property

End Class