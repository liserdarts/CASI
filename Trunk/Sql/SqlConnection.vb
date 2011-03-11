'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://casi.codeplex.com/license

Namespace Sql
''' <summary>
''' The base class for all SQL connections
''' </summary>
Public MustInherit Class SqlConnection
    Inherits ScriptProperty
    
    ''' <summary>
    ''' Gets or sets the connection to the SQL database.
    ''' </summary>
    ''' <value>The connection to the SQL database.</value>
    Public Property Connection As Common.DbConnection

    ''' <summary>
    ''' Gets or sets the transaction the connection is currently in, if any.
    ''' </summary>
    ''' <value>The transaction the connection is currently in.</value>
    Public Property Transaction As Common.DbTransaction

    Public Overrides Sub Close()
        If Connection IsNot Nothing Then
            Connection.Close
            Connection = Nothing
        End If
    End Sub
End Class
End Namespace