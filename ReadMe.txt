Copyright (c) 2011, Nicholas Avery
Licensed under the Microsoft Public License (Ms-PL)
you may not use this file except in compliance with the License.
You may obtain a copy of the license at 
http://scripthelper.codeplex.com/license

Resources
	http://geekswithblogs.net/wesm/archive/2010/03/04/database-version-control-resources.aspx
	http://sqlinstaller.codeplex.com/

ToDo
	Need a way to run a combination of things
		Some projects have sql scripts and images that are inserted into the database
		Run the sql stuff and then the image stuff (it's ok if it's not the same transaction)
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
				Can't tell if this will work anywhere or requires and install of something
	Features from competing ASI database installer
		1) a backup of the database is created and restored in an error situation, instead of operating in a transaction (we had some scripts that weren't allowed inside a transaction -- was really the driving point that led to the use of the new SQL installer)
		2) a script file can consist of multiple SQL batches (GOs are allowed)
		3) versions can be run in an order other than by name alphabetically

		The future enhancements (some may or may not become a reality):
		1) More automation (the installer would store some information in the database itself to let itself know when it was run later what version to start upgrading at)
		2) The ability to order scripts by something other than filename alphabetically (no more naming numerically)
		3) Ability to set the owner of a database to something other than the user running the installer 
		4) Ability to temporarily elevate SQL Server privileges of the user running the installer (some of our operations require sys admin privileges for installation time)
		5) Mid installation 'restore points' (sometimes it would be nice to be able to continue even at the mid-version level, just before a script that has a lot of potential to be problematic but after a lot of other long running data migration scripts)