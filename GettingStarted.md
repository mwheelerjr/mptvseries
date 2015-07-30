
# Introduction #
This guide is intended to guide you through your first experience with the MP-TVSeries plugin. It will take you through the most common steps of the configuration before you can start browsing your collection in MediaPortal. It does not cover any advanced configurations such as custom Regular Expressions.

# Configuration #
Before you launch MediaPortal, the plugin needs to be configured. This is handled from with MediaPortal's configuration utility.

  * Launch MediaPortal Configuration from your desktop shortcut if not already open
  * Select **Plugins** from the tree list.
  * Right Click on the **MP-TVSeries** icon.
> ![http://mptvseries.googlecode.com/svn/wiki/Images/MediaPortalPluginConfigurationMenu.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/MediaPortalPluginConfigurationMenu.jpg)
  * Select **Configuration** from the menu.

## Import Paths ##
The configuration will first take you to the **Import** tab.
  * You now need to enter in the locations where your TVSeries collections are.
  * Click on the **Path** box in the the Import Paths Grid.
  * Enter in the path to your TV Shows in the text box provided, or click on the **Browse** button to select a location:
> ![http://mptvseries.googlecode.com/svn/wiki/Images/PluginAddImportPath.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/PluginAddImportPath.jpg)

In this example I have chosen a Folder on my local hard drive.
  * Press **OK** to save
  * Ensure that the **Enabled** checkbox is checked for each path you want added.


## Parsing ##
Once you have setup your import paths, check that your episodes were picked up by the plugins parsing engine. This is illustrated in the following image:
> ![http://mptvseries.googlecode.com/svn/wiki/Images/ParsingFailure.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ParsingFailure.jpg)

If the plugin fails to parse any of your episodes they will be highlighted and moved to the top of the list. As you can see in this image, the parser failed to recognize one of the episodes **Alias\Season 1\Alias.avi**. The parser requires at a minimum three items in the path for it to successfully download the necessary information from the online database, these are:
  1. Series Name
  1. Season Number
  1. Episode Number

We can see that _Alias.avi_ does not contain a **Season No.** and an **Episode No**. This can easily be modified by editing the file to include these items e.g. _Alias s01e01.avi_. Here is an illustration of how you can store your Media and be confident that they will parse correctly:
> ![http://mptvseries.googlecode.com/svn/wiki/Images/BestPracticesPathStructure.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/BestPracticesPathStructure.jpg)

Note: there are many ways in which you can organize your media, this is just one example. To find out more and receive help you can visit the [Parsing Expressions](http://forum.team-mediaportal.com/my-tvseries-162/expressions-rules-requests-21978/) topic in the TVSeries forums.

  * Click on the **Online Data** tab, to configure online settings
## Online Data Settings ##
The Online Data tab allows you configure the plugins behaviour when communicating to the [TheTVDB.com](http://www.thetvdb.com). Here is a list of recommended settings (not mandatory) that you may wish to change before continuing:

  * Add in your unique Account Identifier into the Account ID text field, this allows you to download/upload your favourite series and series/episode ratings. The Account Identifier can be found in your users [Account](http://thetvdb.com/?tab=userinfo) page.
> ![http://mptvseries.googlecode.com/svn/wiki/Images/theTVDBAccountIdentifier.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/theTVDBAccountIdentifier.jpg)

If you are not a registered user online then you can sign-up [here](http://thetvdb.com/?tab=register).

  * Set the **Language** for the data online (english is the most up to date)
  * To speed the initial import process and avoid prompts for each series, its recommended to enable _Automatically choose Series when an exact match is found online_
  * For most users, enable _Automatically choose Aired when multiple orders are found online_. If you have got a collection of episodes that rely on different episode orders e.g. DVD Order then dont select this option. The DVD Order is sometimes not complete for some series online, so be sure that your series is entered correctly online when choosing DVD Order when prompted during import.
  * Enable _Download episode information for whole series_ if you like to see information on every episode for your series even if its not local on disk. This option only downloads the extra information, another setting is used to view all data.
  * Click on the **General Settings** tab, to configure general plugin settings

## General Settings ##
Use General Settings to configure behaviour of the plugin when browsing your collection in MediaPortal. Here is a list of recommended settings (not mandatory) that you may wish to change before continuing:

  * Select your **Language** from the dropdown box, this is a localization setting for all plugin data displayed to the user in MediaPortal. It does not translate the data retrieved online.
  * Enable _Popup Rate dialog after episode is watched_ if you like to contribute to the online database and store your episode ratings immediately after watching an episode. If this option is not enabled you also have the choice of manually invoking the rate dialog.
  * Click on the **Import** tab, to get ready to import data from online.

## Import ##
The Import Tab is used to initially download the series/season/episode data from [TheTVDB.com](http://www.thetvdb.com)
  * In this example, we will import 'Alias' (named correctly) & 'Star Trek TNG' (named incorrectly)

> ![http://mptvseries.googlecode.com/svn/wiki/Images/ImportDemoFiles.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ImportDemoFiles.jpg)

### Initial Configuration ###
  * Click on the Import Tab
  * Add Import Paths, in this example 'D:\TV'
  * Click the Parsing Check
  * Click the Start Import Wizard button

> ![http://mptvseries.googlecode.com/svn/wiki/Images/ImportStartWizard.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ImportStartWizard.jpg)

  * **Step 1 of 4 - Review and Change the local File Information**
You may make changes to the results below, and/or add files. Click Next

> ![http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep1.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep1.jpg)

  * **Step 2 of 4 - Identifying Series**
In the  below example, Alias was Identified, Star Trek TNG was not, as the filenames Star Trek TNG 1x1.avi needs be Star Trek The Next Generation 1x1.avi to gain a automatic match. However we can manually choose the correct series

> ![http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep2.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep2.jpg)

  * Type the Series name, click Search
  * If a match does not appear in the menu you can type in a new search string and search again.

> ![http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep2-1CustomSearch.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep2-1CustomSearch.jpg)

  * After your shows are all identified correctly, click the next button:
TIP: you can click _Show only Series requiring manual Selection_ to hide all the correctly identified shows so you can focus on those which are an issue

> ![http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep2-2Approve.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep2-2Approve.jpg)

  * **Step 3 of 4 - Identifying Episodes**
In this screen, you can check the identitifed episodes match those online episodes, if you click _Show Only Episodes requiring manual selection_ this is a faster way to see what needs help. The Main reason you might see episodes needing manual selection, is if the episode numbers in your files do not match what is stored at thetvdb.com

> ![http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep3.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep3.jpg)

> ![http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep3-2ManualSelection.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep3-2ManualSelection.jpg)

For each series, You may select a prefered ordering option from this screen. Its recommended in most cases to choose **Aired** unless you know for sure that the **DVD** order is correct online and matches your collection. For a more detailed description of these options, see the [FAQ](http://code.google.com/p/mptvseries/wiki/FAQ#Episode_Ordering_-_Online_Matching)

> ![http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep3-1Ordering.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep3-1Ordering.jpg)

  * **Step 4 of 4 - Download**
The importer will download the information for each series & episode you have correctly identified in the previous steps

The Importing is complete when a Tick is beside each process and the Log window reports **Completed**. The initial import process can take anywhere between a couple of minutes to an hour, it highly depends on the number of series and episodes in your collection.
> ![http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep4Finish.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ImportStep4Finish.jpg)

  * Click on the **Views/Filters** tab, to configure the available views.

## View/Filters ##
Use Views/Filters to better categorize your collection when browsing in MediaPortal. Here is a list of recommended settings (not mandatory) that you may wish to change before continuing:

  * Click on the **Templates** button, browse the list of default templates not already in the list that may suit you and click **OK**
  * If you would like to manually configure a **View** that contains only the series you like, then click the **Add** button.
  * Enter in a name for your view and then click the **Add/Remove series** button:
> ![http://mptvseries.googlecode.com/svn/wiki/Images/ViewsAddRemoveSeries.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/ViewsAddRemoveSeries.jpg)
  * Press **OK** to close the Add/Remove series dialog
  * Press **OK** to close the View Configuration dialog and commit the view and series selection to the database.
  * Repeat these steps for any other custom views you may wish to create, these will be presented to you in a **Views** menu from with-in MediaPortal.
  * If you would like to **Lock** a particular view or multiple views select the view from the list and enable _Prompt for Pin Code when entering view_. This will allow you to lock down the view unless they know your PinCode. This is useful for protecting younger family members where the content is unsuitable e.g. Series with a Content Rating of TV-14 or TV-MA.
  * Click the **Pin Code** button to enter in a 4-Digit Pin Code to protect your views.

Thats It! You have now completely gone through all the basic configuration parameters of the plugin, sure there is much more to learn but this should get you going for now. Exit the plugin configuration (all Information is saved in the database as soon as you change something in configuration)
  * Press **OK** to close and save MediaPortals Configuration.

# Mediaportal #
Now that we have configured the plugin, lets take a tour of the plugin from with-in MediaPortal.

  * Open MediaPortal, you are presented with the Home screen and a list of Plugins to choose from.
  * Select My TVSeries to enter the plugin. At first you will be presented with a listing of all your Series (View: All):
> ![http://mptvseries.googlecode.com/svn/wiki/Images/PluginSeriesListView.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/PluginSeriesListView.jpg)

The series list can be presented to you in a different number of layouts e.g. List Posters (as shown above), List Banners, Wide Banners thumbs and Filmstrip. The look of the interface is controlled by the skin, in this example I have chosen the skin [StreamedMP](http://code.google.com/p/streamedmp/downloads/list).

## Navigating ##
Take a minute and examine the image above to get familiar with some of the terms used. You can interact with the plugin using the Remote / Keyboard to select different series or enter a series. When you enter a series, you will be presented with list of seasons / episodes available:
> ![http://mptvseries.googlecode.com/svn/wiki/Images/PluginEpisodeView.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/PluginEpisodeView.jpg)

  * To play an episode, select **OK/ENTER** on the Remote / Keyboard and the episode will automatically play.

## Context Menu ##
With-in each view heirachy (groups, series, seasons, episodes), you have access to a number of options / actions. Some are these are context sensitive e.g. Rate an Episode. To access these options you need to invoke the menu.

  * Select an item on the facade e.g. a series or an episode
  * Press **Info** or press **F9** on the remote to invoke the menu
  * Browse the list of possibilities presented, the list generated depends on what was selected.
  * Try selecting a series, invoke the menu. Now repeat when selecting an episode.
> ![http://mptvseries.googlecode.com/svn/wiki/Images/PluginSeriesMenu.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/PluginSeriesMenu.jpg)

Here is a list of some of the items that you can do through the menu system:
  * **Series**:
    * Rate the series
    * Mark all Episodes as Watched / Unwatched
    * Add all Episodes of selected series to a Playlist
    * Choose a different Fanart for the background
    * Cycle through and change the Artwork
  * **Episode**
    * Rate the episode
    * Toggle the watched status of the episode
    * Add the episode to a playlist

## Views System ##
You can access the view menu to filter the content of your collection. The Views menu can be accessed from anywhere.

  * Press **Info** or press **F9** on the remote to invoke the menu
  * Select **Change View...**
  * Select a view from the list
> ![http://mptvseries.googlecode.com/svn/wiki/Images/PluginChangeViewMenu.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/PluginChangeViewMenu.jpg)

When you have selected a view, the facade will filter accordingly. If you select a **Group** view, the series will be organized into groups e.g. By Network, By Actor, By Genre

Some views are protected using a 4-Digit Pin Code, if the view is protected then you will be prompted to enter a pin code before the series facade is displayed:
> ![http://mptvseries.googlecode.com/svn/wiki/Images/PluginPinCodeDialog.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/PluginPinCodeDialog.jpg)

If the Pin Code entered is in-correct, you will be prompted in the dialog. Press **Left** or **Delete** on the keyboard to clear the pin code and try again.

## Playlists ##
Playlists are a special type of view, when you play from a playlist the next episode immediately plays and so on. You also have options to **Repeat** or **Shuffle** the list.
> ![http://mptvseries.googlecode.com/svn/wiki/Images/PluginPlaylistView.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/PluginPlaylistView.jpg)

### Create ###
To Create a playlist, follow these directions:
  * Select an episode from the list
  * Press **Info** or press **F9** on the remote to invoke the menu
  * Select **Add to Playlist**
  * Repeat for all episodes you want in the playlist, if you want to add all episodes of a series/season in 1 step then you can **Add to playlist** at the series/season level.

### View ###
To View the current playlist you first need to access the Hidden menu, follow these directions:
  * If you are in List or Banners layout, navigate **Left** or away from the facade
  * If you are in Filmstrip layout, navigate **Up**
> ![http://mptvseries.googlecode.com/svn/wiki/Images/PluginHiddenMenu.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/PluginHiddenMenu.jpg)
  * Select **Current Playlist**
  * To open a saved playlist, select **My Playlists**

### Control ###
There are several actions you can do when in playlist view such as:
  * Move an item up or down (navigate right on the facade, and select the arrow)
  * Delete an item (navigate right on the facade, and select the cross)
  * Invoke the Hidden Menu (navigate left on the facade)
    * Shuffle items
    * Auto Play playlists when loaded (not to be confused with created)
    * Clear all items
    * Save a playlist to disk
    * Load a playlist from disk
    * Skip forward / next when playing an item (while playing an item you can click **Back** on the remote or **ESC** on the keyboard to enter the facade)
> ![http://mptvseries.googlecode.com/svn/wiki/Images/PluginPlaylistHiddenMenu.jpg](http://mptvseries.googlecode.com/svn/wiki/Images/PluginPlaylistHiddenMenu.jpg)