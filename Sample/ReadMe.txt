Setting up a script runner
	A script runner is the core of the process. It needs 4 replaceable pieces that do the actual work. The script runner is responsible for putting them together. The 4 pieces are a Finder, a Recorder, a TransactionProvider, and a Executor.

	Finder
		The Finder is responsible for returning a list of all the scripts that are available to run. It should return the list in the order the scripts are meant to be run. Later the finder will be responsible for opening the scripts and return a Stream.

	Recorder
		The Recorder is responsible for looking up if a script has been run before, based on its path. Later when the scripts are actually run it will be responsible for recording that.

	TransactionProvider
		The TransactionProvider is responsible for making sure all the scripts are executed in some form of a transaction. There are different strategies for accomplishing this but in the end it must be able to commit and rollback the transaction.

	Executor
		The executor is responsible for actually running the scripts

SqlRecorder
	The SqlRecorder records what scripts have been run using a table in an SQL database. If the table does not exist it will be created. The name of the table can be changed with the TableName property.

Including Script Files
	There is more then one way to include script files. Each has its own advantages and disadvantages. The more straight forward way is to include all the script files alongside the exe and other assemblies. This is simple to do and can make debugging easy if there is a problem with any of the scripts. An alternative method is what's used by this sample project. Instead the scripts are included as resources of the ScriptHelper.Sample.exe. This makes debugging more difficult because you can't easily read the scripts and you can't change them without compiling again. However it allows the scripts to be more portable.

	Scripts As A Resource
		Script files in the ScriptHelper.Sample project are compiled as a resource into the exe. This removed the need to copy all the script files with the exe were ever you want to run it. For example if the scripts were part of a unit test project, whatever test framework you are using will probably not copy the script files for you and as a result your code will not be able to find them. Additionally any other project that wants to reference your assembly will not have to worry about copying the scripts because they will already be included.

		How It Works
			Normally to add a file as a resource you have to change its build action using the properties. This would work but is not necessary because of a custom action in the project file.

			<Target Name="BeforeBuild">
			  <ItemGroup>
				<Resource Include="@(Content)" />
			  </ItemGroup>
			</Target>

			This can be added to any project. It will make sure that every file included as content in the project will be added as a resource to the exe or dll. Files that are not part of the project but are included in the project's folder or a sub folder will not be included.