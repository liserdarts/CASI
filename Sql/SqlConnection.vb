'Copyright (c) 2011, Nicholas Avery
'Licensed under the Microsoft Public License (Ms-PL)
'you may not use this file except in compliance with the License.
'You may obtain a copy of the license at 
'http://scripthelper.codeplex.com/license

Namespace Sql
Public MustInherit Class SqlConnection
    Inherits ScriptProperty
    
    ''' <summary>
    ''' The connection to the sql database
    ''' </summary>
    Public Connection As Common.DbConnection

    ''' <summary>
    ''' The transaction the connection is currently in, if any
    ''' </summary>
    Public Transaction As Common.DbTransaction
    
End Class
End Namespace