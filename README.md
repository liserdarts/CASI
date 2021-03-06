# CASI - Continuous Automated Script Integrator

## Project Description

Easily run large and continuously changing batches of scripts.

CASI is a tool to make instillations easier. If you've ever maintained software with an sql database that is installed on many different sites you are probably familiar with the problems associated with making changes to the database. Continuous integration, branching, Q‎A, and building release sets are all difficult because you have to keep track of what changes have been applied to what copies of the database. CASI along with a few development guidelines will cut though these problems and will help streamline your development and install process.

This is a very new project. I have plans for some more features and support for more database systems. If you're interested in this project, please let me know. Nothing will motivate me more then some community engagement.

## Database Version Management

I've noticed on several occasions developers do not treat databases like they do code. It seams that when most projects get started an entire team works on the same database and then when it comes time to install for testing, demos, production, or whatever it's done with a backup and restore. It always starts innocent enough but soon enough the database gets around and as development continues nobody can keep track of the changes made to the development database. When other copies need updated nobody is prepared and changes are applied manually, usually by memory.

I've seen teams react a few different ways to this problem. Some teams make sure to document every change to the database in release notes, which are applied by a trained installer. Sometimes teams try to keep a few copies of their database, one for development, one empty for new installs, and maybe one for automated tests. This method inevitably leads to them coming out of sync with some changes in one but not the others. Some teams just avoid database changes at almost any cost. I've even seen a team create a database with sequentially named tables and columns, more then needed, just so none would be added later. If you're doing these things take a step back and take a good look. There is a better way.

The solution lies in treating you database like code. We already have many source control tools we can use. How can you treat a database as code? Just write out sql scripts to create each table, foreign key, view, stored procedure, and every other object you have in your database. If you haven't done this for your database yet it can be a chore, but it saves so much trouble down the line. Once the sql scripts exist it is possible to create a tool to run them for us. Since nothing is ever finished and the database will continue changing it should be possible to build a tool that will not just create a new database but apply any new changes to them as well. That's what CASI is.

## How it works

CASI is very customizable but the process generally goes like this. Given information on where to find scripts and what database to connect to CASI follows this process

1. Get a list of all the available scripts in the order they are meant to be run
2. Read the ScriptUpdates table and remove any scripts that are already recorded there
3. Begin a database transaction
4. Run each script 1 at a time
5. Commit the transaction
6. Record each script path in the ScriptUpdates table

Recording every script run in the database has a big advantage. Other tools similar to CASI exist but I have not yet found one with this advantage.

* Team members getting latest from your source code repository will get the latest scripts. All they will need to do is run the CASI tool and it will run and apply changes from any of the new scripts. No worrying about what scripts are new or even if there are any new scripts. This makes the process very easy to automate.
* Team members working on a branch will be able to add any scripts to that branch. Then when the branch is merged back into the trunk those scripts will come with it and all developers will get them.
* Creating a release is very easy, there is no work to do at all. No need to choose which scripts belong to which version.
* Continuous integration is easy. QA can get a nightly build and run CASI to use the new code without any help.
