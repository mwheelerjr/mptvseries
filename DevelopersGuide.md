# Introduction #

This document will walk you through setting up your development environment to contribute to the MP-TVSeries project. If you follow this guide, you should be able to retrieve the latest version of the source code, make bug fixes or enhancements, and submit your changes. This guide details a development environment utilizing Visual Studio 2008 and Tortoise SVN.

## Prerequisites ##
As mentioned above, you will at a minimum need two tools to work with the MP-TVSeries source code. Once you get going though you might want to consider grabbing a program to browse the MP-TVSeries database. Any app that can read a SQLite3 database will do e.g. SQLite2009 Pro Enterprise Manager

### Visual Studio 2008 / Visual Studio 2010 ###
If you do not already have it installed, you will need to install Visual C# before you proceed with the rest of this guide. Visual C# is a subset of Visual Studio, and it is the only portion of Visual Studio you will need. Throughout this document though, these terms will be used interchangeably. Don't get confused, all you need is Visual C#. A walk through for the Visual Studio installation is outside the scope of this guide, but you should not need anything beyond a standard install of the Visual Studio. The Express edition of Visual Studio 2008 is available from Microsoft, free of charge at the following URL, again, you will only need the Visual C# component:

http://www.microsoft.com/express/download/

### TortoiseSVN ###
We will be using TortoiseSVN for retrieving the latest source code and changes from the Subversion repositories for both MediaPortal and MP-TVSeries. There are many other Subversion clients you could use, TortoiseSVN just happens to be our favorite. You will need TortoiseSVN installed before proceeding with this guide. You can get the latest version of TortoiseSVN from the following URL:

http://tortoisesvn.net/downloads

### DirectX End-User Runtimes ###
MediaPortal uses the DirectX runtime, so if you have not already installed MediaPortal or the DirectX End-User Runtimes you will need to install it before you can successfully build MediaPortal. At the time of writing the latest build is August 2009 which you can download from the following URL:

http://www.microsoft.com/downloads/details.aspx?familyid=04AC064B-00D1-474E-B7B1-442D8712D553&displaylang=en

Alternatively you may prefer to install the DirectX Software Development Kit (August 2009) which you can download from:

http://www.microsoft.com/downloads/details.aspx?familyid=B66E14B8-8505-4B17-BF80-EDB2DF5ABAD4&displaylang=en

### Microsoft .Net Framework 3.5 ###
MP-TVSeries and MediaPortal target the Microsoft .Net Framework 3.5, If you have already installed MediaPortal 1.0.1 or above then you will most likely already have this pre-requisite. The .Net Framework 3.5 is available to download from the following URL:

http://www.microsoft.com/downloads/details.aspx?FamilyId=333325FD-AE52-4E35-B531-508D977D32A6&displaylang=en


## Folder Structure ##
So now that you have your tools setup, you will want to pull down the latest source code. Before you do that though, you will have to decide where you want your projects to be stored. The default location for Visual Studio Projects is "My Documents\Visual Studio 2008\Projects". Any location will do if you would rather not work from there. To give you a little visibility, this is the folder structure I am recommending:

![http://mptvseries.googlecode.com/svn/wiki/Images/FolderStructure.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/FolderStructure.jpg)

So go ahead and create your folder structure. Create the PluginDev folder and the MediaPortal, Common-MP-TV3 and MP-TVSeries folders below that. These are all you need to do manually, everything else will automatically be pulled down by TortoiseSVN.

# Getting the MediaPortal Source Code #
## Checking Out ##

Now that you have your folders created, we need to pull down the latest code for the TVSeries and MediaPortal projects. MediaPortal is a dependency of MP-TVSeries, so let's start with that. Right click the MediaPortal folder you created previously and click "SVN Checkout". If you do not see this you most likely either do not have TortoiseSVN installed or it is not setup properly.

![http://mptvseries.googlecode.com/svn/wiki/Images/SVNCheckout.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/SVNCheckout.jpg)

The only thing you need to change on this popup is the "URL of Repository" field. Copy and paste the first URL listed below into the "URL of Repository" field, then click OK.

```
https://mediaportal.svn.sourceforge.net/svnroot/mediaportal/trunk/mediaportal
```

MediaPortal references a project "PowerScheduler.Interfaces" outside of its folder structure, this being "Common-MP-TVE3". Right-click on the Common-MP-TVE3 folder and do a SVN Checkout from the source below:
```
https://mediaportal.svn.sourceforge.net/svnroot/mediaportal/trunk/Common-MP-TVE3
```

## Getting the MP-TVSeries Source Code ##

Alright so now that you have retrieved the source code for MediaPortal, you will need to get the source code for MP-TVSeries. The MP-TVSeries source code is also hosted in a subversion repository so the procedure is virtually identical. So just right click the MP-TVSeries folder you created previously, and click "SVN Checkout. The "URL of Repository" field should be populated with the following URL:
```
http://mptvseries.googlecode.com/svn/trunk/
```
Click OK and wait for the checkout to complete. This should be significantly faster than the MediaPortal checkout.

# Setting up your Visual Studio Solution #
## Introduction ##

So now that you have the source code for MP-TVSeries and MediaPortal, you need to setup a work environment by creating a Visual Studio Solution. There is one C# Project that makes up the MP-TVSeries application and there are over a dozen that make up the MediaPortal application. We will be loading the MP-TVSeries project and all MediaPortal projects.

You might be wondering why a solution file for MP-TVSeries is not committed to Subversion. If you work on any other MediaPortal plug-ins you could add them to the same Visual Studio Solution you are using here for MP-TVSeries. For example it would make sense to have the MovingPictures project loaded in the same Solution as MP-TVSeries if you occasionally do a few bug fixes for that project. This setup may not be the desired setup for everyone though, different people will be working on different projects, so only the project files for MP-TVSeries are committed to Subversion, and everyone is free to setup their Solution file as they see fit.

## Creating the Solution ##

Creating your own solution is pretty easy. In Windows Explorer browse to the MP-TVSeries folder that you recently pulled the source code into and go to the "MP-TVSeries" sub folder. You will see the MP-TVSeries project file (with the extension .csproj). Double click the file to launch it in Visual Studio 2008. After opening the project, click the Save All button. This will prompt you to save as a solution:

![http://mptvseries.googlecode.com/svn/wiki/Images/SaveAsSoln.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/SaveAsSoln.jpg)

**Don't click OK on the save dialog yet though!** We want to change the location of the Solution. By default it will be placed in the same directory as the MP-TVSeries project, but we will be using multiple projects in the solution. So go back up to the PluginDev directory you created earlier and save your solution there. You probably will want to change the name to something more generic as well (like PluginDev.sln).

## Adding Additional Projects ##

Now that you have created your solution you are almost done. All that is left is to add the remaining projects to the solution. Currently your solution only contains the  MP-TVSeries project, but you will also need the MediaPortal projects that MP-TVSeries depends on. If you expand the "References" node of the existing MP-TVSeries project in the Solution Explorer you can see all the dependencies of the project. Some of these are .NET assemblies but a few are MediaPortal assemblies. You can satisfy these dependencies by adding the corresponding projects to the solution. The easiest way to accomplish this is to add the entire MediaPortal solution to a new folder. Since there is a lot of projects as part of MediaPortal its best to first create a new solution folder (MediaPortal) to contain the different MediaPortal projects:

![http://mptvseries.googlecode.com/svn/wiki/Images/AddNewSolnFolder.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/AddNewSolnFolder.jpg)

Next, we want to add the MediaPortal.sln to the newly created folder above. To add the MediaPortal solution, select the new solution folder that you just created and select "Add Existing Project" from the right click menu. In the "Add Existing Project" dialog browse up the MediaPortal root directory. To load the MediaPortal solution, you first need to change the "Files of Type" dropdown box to "Solution Files (.sln).
Once you have selected the MediaPortal.sln file to load you should see a solution explorer window similar to this:

![http://mptvseries.googlecode.com/svn/wiki/Images/FinalSolnExplorer.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/FinalSolnExplorer.jpg)

Once you have these projects added to your environment your References for the MP-TVSeries project should be free of warning icons, like the image below. If you see any warnings, this probably means additional dependencies have been added since the time of this writing. Don't panic, just browse through the MediaPortal folder structure and find the project Visual Studio is complaining about. You might also want to double check the references for the other MediaPortal projects. Again, at the time of this writing, the projects listed above are all that is required, but additional dependencies may have been added to any of these projects over the last few months.

![http://mptvseries.googlecode.com/svn/wiki/Images/References.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/References.jpg)

# API Key #
For your plugin to successfully import data from the online database, you will need to register for an **APIKey**. Simply visit http://thetvdb.com, and request this from one of the site Admins.

Once you have been given an APIKey you will need to enter this into the plugins APIKey resource file. In the Solution explorer, expand **Online Parsing Classes** and double click on the file **APIKey.resx**. Enter your APIKey in the **Value** field.

# Testing Your Setup #
## Building and Running ##

Well believe it or not you are about done, your environment should be ready to go for development purposes. The key test here is to make sure everything compiles properly, so right click "MP-TVSeries" in the Solution Explorer and select "Build". This should take a bit of time, depending on how powerful of a computer you have. Something like one to two minutes is not uncommon because of all the MediaPortal code that needs to be compiled.

If everything compiled correctly, then next you will want to test that it works in MediaPortal.

_Note: If you want to build the entire solution ie. All of the MediaPortal projects as well, be sure to edit the Post Build step for the MediaPortal Project. Currently it references $(SolutionDir) as its first parameter, in this case its not correct. You need to change this to reflect your MediaPortal solution directory._

## Testing in MediaPortal ##

If you want to test changes you have made in an actual MediaPortal install, you need to make sure that MP-TVSeries.dll is copied to your working windows\plugins directory and that all 3rd Party Assemblies are copied to MediaPortals binary directory. This can usually be done by modifying the Post-Build step of the MP-TVseries project. By default the Post Build step will copy to your MediaPortal development environment created above.

## Debugging in MediaPortal ##

To debug MP-TVSeries in MediaPortal you will need to configure the Debug properties for the project. Right Click on the MediaPortal project and select Properties. Now, select the Debug tab and enter the location to MediaPortal.exe in the "Start external program" field:

![http://mptvseries.googlecode.com/svn/wiki/Images/DebugSettings.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/DebugSettings.jpg)

Similarly, you can enter in the path to Configuration.exe if you want to debug MP-TVSeries configuration.

Before pressing F5 to debug, you will probably also want to set the MediaPortal project as the default. Do this by Right Clicking on the MP-TVSeries project and selecting "Set as Startup project".

Should should now be able to debug MediaPortal and TVSeries in the same solution.

## Problems? ##

If you get any errors with the build process, this most likely has to do with your References in one of your projects. In other words a project is most likely missing a dependency. In this case you will want to go through each project checking for any References flagged with a warning icon. If you find anything flagged, odds are it's a MediaPortal project. Track it down and add it to your solution. At the time of this writing though, you should not have any issues, if you followed all the steps and have the correct folder structure all referenced projects should be automatically found.

# Submitting Changes #
## Submitting a Patch ##

Most new developers on the project will be using this method first. Subversion gives you the ability to take all the changes you have made on your local system to the MP-TVSeries source code and group it into one file called a patch. It's not difficult to do, in Windows Explorer, right click the MP-TVSeries folder (the root folder, the one that links to the trunk), then click TortoiseSVN->Create Patch. On the following pop-up dialog you will have the ability to review all the changes that will be included in the patch. Once satisfied, click OK and your patch will be created. Give the patch a meaningful name and upload it to the projects Issue Tracker. One of the regular developers will review it and respond. Thanks for helping out!

## Committing Changes to Subversion ##

If you start making regular contributions to the project then you'll probably be added as a Project Member. This means you can commit changes directly to Subversion via TortoiseSVN. I am not going to cover this process here but it is pretty simple. Just be sure that you fill out a meaningful log message. The ability to skim through the past log messages in Subversion is very important, and if you make a commit without logging a message you will get yelled at. Also try to commit groups of files all at once. If you have made changes to five files all for one bug fix or enhancement, then these five files should all be committed together. Don't check them in one at a time individually, this just makes it more difficult to track changes in the version history.

## Problems with Visual Studio 2010 and x64 OS ##

If you have an issue with Visual Studio 2010 and are running x64 OS, similar to below

Error 1 Could not load file or assembly '[file:///MEDIAPORTAL/SVN/TVSeries/External/Core.dll](file:///MEDIAPORTAL/SVN/TVSeries/External/Core.dll)' or one of its dependencies. An attempt was made to load a program with an incorrect format. Line 656, position 5. D:\MEDIAPORTAL\SVN\TVSeries\MP-TVSeries\Configuration\GUIConfiguration.resx 656 5 MP-TVSeries

see this from [Visual Studio Blog](http://blogs.msdn.com/b/visualstudio/archive/2010/06/19/resgen-exe-error-an-attempt-was-made-to-load-a-program-with-an-incorrect-format.aspx) for a solution. Workaround 2 has been confirmed to work but you can use whichever.