Copyright (c) 2011-2013, Nicholas Avery
Licensed under the Microsoft Public License (Ms-PL)
you may not use this file except in compliance with the License.
You may obtain a copy of the license at 
http://casi.codeplex.com/license

Resources
	http://geekswithblogs.net/wesm/archive/2010/03/04/database-version-control-resources.aspx
	http://sqlinstaller.codeplex.com/

ToDo
	Some form of branching
		A way to show (and record?) when running a branch that may not be compatible with the trunk or other branches
		If branch scripts are run the trunk code may not work until the branch is reintegrated. Users should be warned.
	Make sure recording will work before actually running scripts
		Don't want to get into a state where we don't know what scripts have run
	Replace tokens in scripts
	Able to batch scripts with GO in sql
		SQL Management Objects will parse this out
			Tracked it down to Microsoft.SqlServer.BatchParserClient
				Microsoft.SqlServer.Management.Common.ExecuteBatch.GetStatements
				Can't tell if this will work anywhere or requires an install of something