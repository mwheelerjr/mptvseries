# Introduction #

The purpose of this document is to help you understand how exactly MP-TVSeries interacts with the skin you will be/are creating. In this document you're going to find a general overview of how the plug-in is structured, the components that you, as a skinner should be interested in, and a list of available fields that you can use.

For skinning MP-TVSeries, you are welcome to use whatever skin you like as a base, but we recommend you start with the Blue3wide skin that is installed with the release. It is written by the same people that write the plug-in, so if you are having trouble getting something to work right, refer to this skin.

If you are new to skinning, please check out the [Skin Designer's Guide](http://www.team-mediaportal.com/manual/MediaPortal1_Development/SkinArchitecture) on the MediaPortal wiki.

# Table of Contents #



# Skinner's Change Log #
## v3.3.0 ##
  * Added new skin button to enable filters e.g. Unwatched Episodes.
| **Control Type** | **ID** | **Description** |
|:-----------------|:-------|:----------------|
| Button           | 10     | Displays menu of available filters |

  * Views menu will now show a lock icon to indicate if a view is parental controlled, the image loaded is **media\lock.png**

## v3.2.0 ##
  * Added extra loading parameter support to jump to a particular Series or Season. Can also specify an episode which will filter out the list of episodes, typically you would just specify a series and a season as user might want to play the next episode. Below are some examples:

```
<hyperlinkParameter>seriesid:79488</hyperlinkParameter>
```
```
<hyperlinkParameter>seriesid:79488|seasonidx:2</hyperlinkParameter>
```
```
<hyperlinkParameter>seriesid:79488|seasonidx:2|episodeidx:6</hyperlinkParameter>
```

## v3.1.0 ##
  * Now that there is a MPEI Extension installer you can also check if an update is available using a Skin Button e.g.
```
<control>
  <description>MPEIUPDATE:5e2777c3_966c_407f_b5a9_e51205b70b3e</description>
  <type>button</type>
  <id>99000</id>
  <label>#MPEI.Translation.UpdateAvailable.Label</label>
  <visible>string.equals(#mpei.5e2777c3_966c_407f_b5a9_e51205b70b3e.haveupdate, true)</visible>
</control>
```
  * Extension GUID is: **5e2777c3\_966c\_407f\_b5a9\_e51205b70b3e**

## v3.0.0 ##
  * Added Coverflow support, to enable skin support from skin you need to add the coverflow facade control and also enable it in TVSeries.SkinSettings.xml:
```
<layouts>
    <group List="true" SmallIcons="false" />
    <series ListPosters="true" ListBanners="true" Filmstrip="true" WideBanners="true" Coverflow="true" />
    <season List="true" Filmstrip="true" Coverflow="true" />
    <episode List="true" />
</layouts>
```
Coverflow is supported in Series and Season view and will load the Poster image into each card. If you want to control the quality of the image added, set the corresponding GraphicsQuality setting:
```
<graphicsquality import="true">
    <seriesbanners>60</seriesbanners>
    <seriesposters>20</seriesposters>
    <seriescoverflow>40</seriescoverflow>    
    <seasonbanners>75</seasonbanners>
    <seasoncoverflow>90</seasoncoverflow>
    <episodethumbs>90</episodethumbs>
</graphicsquality>
```
## v2.7 - v2.8 ##
**New Skin Settings**

The plugin can now load simple settings by looking in a new **defines** node. This version introduces a few settings to control the visibility of fanart per layout (list, icons, filmstrip) and per view level (series, seasons, episodes):
```
<defines>
    <property key="fanart.seriesview">true</property>
    <property key="fanart.seasonview">true</property>
    <property key="fanart.episodeview">true</property>
    <property key="fanart.listlayout">true</property>
    <property key="fanart.iconslayout">true</property>
    <property key="fanart.filmstriplayout">true</property>
    <property key="fanart.coverflowlayout">true</property>
</defines>
```
## v2.6 - v2.7 ##
**New Skin Properties**| **Propterty** | **Description** |
|:--------------|:----------------|
| #TVSeries.Current.Fanart | The Filename of the current fanart loaded, can be used in texture property elsewhere e.g. Actors GUI |
| #itemcount    | Item count of facade objects, this is for skins re-using this property from other plugins |

  * New GUI to display Actors for Series:
| **IDs** | **Description** |
|:--------|:----------------|
| 9816    | Actors GUI Window ID |
| 2       | Layout GUI Button |
| 3       | Refresh GUI Button |

  * The following skin properties for Actors GUI can be used:
| **Propterty** | **Description** |
|:--------------|:----------------|
| #TVSeries.Actor.Name | Name Actor      |
| #TVSeries.Actor.Role | Role the plays  |
| #TVSeries.Actor.Image | Image of Actor  |
| #TVSeries.Actor.SeriesID | The tvdb SeriesID for series |

  * You can also use the following standard properties in Actors GUI
| **Propterty** | **Description** |
|:--------------|:----------------|
| #selectedthumb | Image of Actor, including default image when no image is available |

  * Note: if no Actor image is displayed then the plugin loads defaultActor.png in skins media folder.

## v2.5 - v2.6 ##
  * A **New** Image (indicating recently added episodes or Unwatched episodes) overlayed on Series WideBanners / Posters Filmstrip, requires a new image in the skins media directory: **tvseries\_newlabel.png**. The position of the image can be controlled from TVSeries.SkinSettings.xml with the following code:
```
<thumbstamp>
	<widebanners>
		<posx>668</posx>
		<posy>-5</posy>
	</widebanners>
	<posters>
		<posx>580</posx>
		<posy>-5</posy>
	</posters>
</thumbstamp>
```

  * Skin can now define the images loaded in the Video OSD from TVSeries.SkinSettings.xml:
```
<videoosdimages import="true">
	<episode use="true" />
	<series use="true" />		
	<season use="true" />		
	<custom use="false"><![CDATA[skin\StreamedMP\Media\custom.png]]></custom>
</videoosdimages>
```

  * Skin can define images to be loaded into skin properties when playback is started, on stopped the image property is cleared. These properties can be defined in the TVSeries.SkinSettings.xml:
```
<videoplayimages import="true">
	<property>
		<name>TVSeries.Play.ClearArt</name>
		<value><![CDATA[thumbs\ClearArt\<Series.ID>.png]]></value>
	</property>
	<!-- You can define multiple properties -->
</videoplayimages>
```

## v2.3 - v2.5 ##
**New Skin Properties:**| **ID** | **Description** |
|:-------|:----------------|
|#TVSeries.LastOnlineUpdate|Push's the Last Date\Time an Online Update occurred |
|#TVSeries.WatchedCount|Push's the Watched Count of episodes in Episode View|
|#TVSeries.UnWatchedCount|Push's the UnWatched Count of episodes in Episode View|

  * All Translations are now pushed to the skin using the following property syntax:
**#TVSeries.Translation.$(FieldName).Label**

where **$(FieldName)** corresponds to the field name in the translation files e.g:

  * #TVSeries.Translation.**Episodes**.Label
  * #TVSeries.Translation.**MediaInfo**.Label
  * #TVSeries.Translation.**Cast**.Label
  * #TVSeries.Translation.**Actors**.Label
  * #TVSeries.Translation.**FirstAired**.Label
  * #TVSeries.Translation.**Certification**.Label
  * #TVSeries.Translation.**AirsDay**.Label

See Debug log output for a full listing.

  * Translations can also be applied to Formatting Rules in TVSeries.SkinSettings.xml using the functions Trans($(FieldName)) e.g.

`<Enabled>1<Format><Episode.InfoPanelLine1Key><FormatAs>Trans(Rating)`

This will Translate the property #TVSeries.Episode.InfoPanelLine1Key by looking up the translation for "Rating".

## v2.2 - v2.3 ##
  * Series Posters and Series Banners are now exposed to the skin as two separate properties:

| **Property** | **Description** |
|:-------------|:----------------|
|#TVSeries.SeriesPoster|Push's the current selected series Poster to the skin |
|#TVSeries.SeriesBanner|Push's the current selected series Wide Banner to the skin|

  * PlayList View now has a new Episode Count property:
| **Property** | **Description** |
|:-------------|:----------------|
|#TVSeries.Playlist.Count|Push's the current number of items in the playlist to skin |

  * Two new skin dialogs for User Ratings and Parental Control input:

| **Skin ID** | **Filename** | **Description** |
|:------------|:-------------|:----------------|
|9814         |TVSeries.RatingDialog.xml |Displays a rating dialog to the user|
|9815         |TVSeries.PinCodeDialog.xml|Displays a pin-code entry dialog to the user|

  * Dummy Label controls **1235** and **1236** have been removed, skinner should use **facadeview.list** and **!facadeview.list** instead.

  * Season Artwork property has been renamed from #TVSeries.SeasonBanner to #TVSeries.SeasonPoster

  * Added new filtered episode count property #TVSeries.FilteredEpisodeCount. This accurately reports the number of items in Episode View

  * The Colors used on the facade have changed. Watched Color is now represented by **playedColor**, non-local files use **remoteColor** and the default unwatched color uses **textColor**.

# Skin Files #
The plugin requires several skin files if you wish to expose all functionality of the plugin. Not all skin files are mandatory but its recommended that all are included with Skin releases. The table below describes each skin file and controls used:

## Main ##
This is the main skin file of the plugin. It must exist otherwise a skin error dialog will be shown to the user.

### Skin File ###
| **ID** | **Filename** |
|:-------|:-------------|
| 9811   | TVSeries.xml |

### Controls ###
This table illustrates the skin controls used by the main plugin

| **Control Type** | **ID** | **Description** |
|:-----------------|:-------|:----------------|
| Button           | 2      | Displays list of available database views |
| Button           | 3      | Displays list of available layouts set by skin |
| Button           | 4      | Displays list of options that change the behaviour of plugin |
| Button           | 5      | Runs a Local scan of the import paths and a Full update from online |
| Button           | 9      | Displays a list of saved playlists for load |
| Facade           | 50     | The main facade control used by all views |
| Animation        | 51     | An Import Animation that gets displayed when the plugin is working |
| Image            | 66     | Displays a single image of all logos that were evaluated by the [Logo Rules](http://code.google.com/p/mptvseries/wiki/SkinDesignersGuide#Logo_Rules) |
| Image            | 67     | Displays a thumbnail for selected episode |
| Image            | 524    | Fanart Background #1|
| Image            | 525    | Fanart Background #2, used for Fanart Fading Transitions|

### Dummy Controls ###
Dummy Controls are used so the plugin can force changes in the skin. This is done by changing the visible property. Since these are just dummy controls, they can be defined off screen.

| **ID** | **Description** |
|:-------|:----------------|
| 1232   | Visible when fanart is loaded for selected series|
| 1237   | Visible when Series view is loaded |
| 1238   | Visible when Season view is loaded |
| 1239   | Visible when Episode view is loaded |
| 1240   | Visible when Group view is loaded |
| 1241   | Visible when Fanart has colour properties defined |
| 1242   | Visible when Series Posters are used e.g. List Poster layout |
| 1243   | Visible when the selected item is Watched, applied to all views except groups |
| 1244   | Visible when the selected item is available, applied to all views except groups |


## Fanart Chooser Window ##
This is the skin file that the plugin uses to show a list of Fanart that the user has downloaded and available online to download.

### Skin File ###
| **ID** | **Filename** |
|:-------|:-------------|
| 9812   | TVSeries.FanArt.xml |

### Controls ###
This table illustrates the skin controls used by the fanart chooser:

| **Control Type** | **ID** | **Description** |
|:-----------------|:-------|:----------------|
| Button           | 2      | Displays list of available layouts |
| Label            | 11     | Localized static label for Resolution property |
| Button           | 12     | Displays list of Resolutions to filter the Fanart by |
| ToggleButton     | 13     | Toggles Random Fanart mode |
| Label            | 14     | Localized static label for Disabled property |
| Label            | 15     | Localized static label for Default property |

## Playlist ##
This is the skin file that the plugin uses to show the current playlist loaded.

### Skin File ###
| **ID** | **Filename** |
|:-------|:-------------|
| 9813   | TVSeries.Playlist.xml |

### Controls ###
This table illustrates the skin controls used by the Playlist window:

| **Control Type** | **ID** | **Description** |
|:-----------------|:-------|:----------------|
| Button           | 2      | Displays list of available layouts |
| Button           | 9      | Displays list of saved playlists to load |
| Button           | 20     | Shuffles the items in the current playlist |
| Button           | 21     | Prompts to save the current playlist to disk |
| Button           | 23     | Plays the current playlist |
| Button           | 24     | Skips forwards to the next item in the playlist |
| Button           | 25     | Skips backward to the next item in the playlist |
| ToggleButton     | 30     | Toggles Playlist repeat behaviour when playlist has finished playing|
| ToggleButton     | 30     | Toggles Playlist Auto Play when playlist is loaded|
| Facade           | 50     | Main facade for the playlist window |

## Rating Dialog ##
This is the skin file that the plugin uses to show a Rate Dialog for episodes and series.

### Skin File ###
| **ID** | **Filename** |
|:-------|:-------------|
| 9814   | TVSeries.RatingDialog.xml |

### Controls ###
This table illustrates the skin controls used by the Rating dialog:

| **Control Type** | **ID** | **Description** |
|:-----------------|:-------|:----------------|
| Label            | 7      | Displays the current select rating value |
| ToggleButton     | 9      | Displays list of saved playlists to load |
| ToggleButton     | 100    | Active when Rate Stars equals One |
| ToggleButton     | 101    | Active when Rate Stars equals Two |
| ToggleButton     | 102    | Active when Rate Stars equals Three |
| ToggleButton     | 103    | Active when Rate Stars equals Four |
| ToggleButton     | 104    | Active when Rate Stars equals Five |
| ToggleButton     | 105    | Active when Rate Stars equals Six |
| ToggleButton     | 106    | Active when Rate Stars equals Seven |
| ToggleButton     | 107    | Active when Rate Stars equals Eight |
| ToggleButton     | 108    | Active when Rate Stars equals Nine |
| ToggleButton     | 109    | Active when Rate Stars equals Ten |

## Pin Code Dialog ##
This is the skin file that the plugin uses to show a Pin Code Dialog for Parental Controls.

### Skin File ###
| **ID** | **Filename** |
|:-------|:-------------|
| 9815   | TVSeries.PinCodeDialog.xml |

### Controls ###
This table illustrates the skin controls used by the Pin Code dialog:

| **Control Type** | **ID** | **Description** |
|:-----------------|:-------|:----------------|
| Label            | 6      | Displays feedback to the user if the Pin Code entered in incorrect |
| Image            | 100    | Visible when 1st pin is entered |
| Image            | 101    | Visible when 2nd pin is entered |
| Image            | 102    | Visible when 3rd pin is entered |
| Image            | 103    | Visible when 4th pin is entered |

# Skin Properties #
Each skin has its own unique set of properties that can be used, see the table below for more information:

## Main Plugin ##
| **Property** | **Description** |
|:-------------|:----------------|
|#TVSeries.Title  | The Title of the Series or Episode  |
|#TVSeries.Subtitle  | The Sub-Title of the Series or Episode |
|#TVSeries.Description  | The Description of the Series or Episode |
|#TVSeries.SimpleCurrentView  | The name of the current view |
|#TVSeries.CurrentView  | The name of the current view including hierarchy structure |
|#TVSeries.SeriesPoster  | The Filename of the current Series Poster |
|#TVSeries.SeriesBanner  | The Filename of the current Series Wide Banner |
|#TVSeries.SeasonPoster  | The Filename of the current Season Poster |
|#TVSeries.EpisodeImage  | The Filename of the current Episode Thumbnail |
|#TVSeries.Logos  | The Filename of the dynamically created Logos |
|#TVSeries.Fanart.1  | The 1st Fanart used in the Fanart Fading Logic |
|#TVSeries.Fanart.2  | The 2nd Fanart used in the Fanart Fading Logic |
|#TVSeries.SeriesCount  | The current number of Series displayed in the Facade |
|#TVSeries.GroupCount  | The current number of Groups displayed in the Facade |
|#TVSeries.FilteredEpisodeCount | This accurately reports the number of items in Episode View |

**Note:** Title, Sub-Title and Description properties can be overridden by the user in the MP-TVSeries configuration under the **Layout** tab.

The TVSeries plugin also exposes all Database fields, simply prefix the property with the current level (Series,Season or Episode) e.g:

  * #TVSeries.Series.EpisodeCount
  * #TVSeries.Season.EpisodeCount
  * #TVSeries.Episode.VideoFrameRate

For a full list of fields available see the **Details** tab in configuration. It should be noted that using these fields takes away any flexibility for Power Users being able to easily customize the skin, in this case there is a powerful feature called [Formatting Rules](http://code.google.com/p/mptvseries/wiki/SkinDesignersGuide#Formatting_Rules) available to skinners.

## Fanart Chooser ##
| **Property** | **Description** |
|:-------------|:----------------|
|#TVSeries.FanArt.Colors.LightAccent  | The Light Accent color of the current Fanart |
|#TVSeries.FanArt.Colors.DarkAccent  | The Dark Accent color of the current Fanart |
|#TVSeries.FanArt.Colors.Neutral Midtone  | The Neutral Accent color of the current Fanart |
|#TVSeries.FanArt.SelectedFanartResolution  | The Resolution of the current Fanart |
|#TVSeries.FanArt.SelectedFanartInfo  | Detailed description of the current Fanart  |
|#TVSeries.FanArt.SelectedPreview  | The file name to the FullSize image of the current Fanart |
|#TVSeries.FanArt.LoadingStatus  | The current loading status of Fanart thumbnails from online  |
|#TVSeries.FanArt.DownloadingStatus  | The current downloading status of all fanart set to download |
|#TVSeries.FanArt.PageTitle  | The name of the Fanart Chooser window |
|#TVSeries.FanArt.SelectedFanartColors  | The Colors set for the current Fanart |
|#TVSeries.FanArt.SelectedFanartIsChosen  | True if the current Fanart is the default fanart |
|#TVSeries.FanArt.SelectedFanartIsDisabled  | True if the current Fanart is disabled |


# View Hierarchy #
The plugin consists of several hierarchical views, these being Groups, Series, Seasons and Episodes. Each view has a dummy label control associated with it to determine which one is currently visible on the screen. By using the visible condition of the label you can change your skin layout accordingly.

| **Control ID** | **Description** |
|:---------------|:----------------|
| 1237           | skin control is visible when **series** view is visible |
| 1238           | skin control is visible when **season** view is visible |
| 1239           | skin control is visible when **episode** view is visible |
| 1240           | skin control is visible when **group** view is visible |

# Layout Types #
The plugin exposes all layouts provided by MediaPortal, these being List, Small Thumbs, Large Thumbs and Filmstrip. Some of these layout types are not available in all views.

## Series View Layouts ##
The plugin allows several layout types in series view:

  * List with Posters
  * List with Banners
  * Wide Banner Thumbs
  * Filmstrip

The plugin loads the appropriate image into graphical view e.g. In Wide Banners, the series wide banner is loaded as the default icon.

Since there are two List views, the plugin needs to distinguish between the two when a users selects one. This is done with a dummy label control with ID: 1242. We set this dummy label visible property to **True** when we select List with Posters and False otherwise. Here is a table illustrating their use:

| **Layout** | **Facade Type** | **Default Icon** | **Series Poster Dummy Label** |
|:-----------|:----------------|:-----------------|:------------------------------|
| List Banners | facadeview.list | N/A              | false                         |
| List Posters | facadeview.list | N/A              | true                          |
| Wide Banners | facadeview.largeicons | Series Banners   | N/A                           |
| Filmstrip  | facadeview.filmstrip | Series Posters   | N/A                           |

Here is an example case of wanting to push a label to the skin when the user is in List Poster Layout:

```
<control>
  <type>label</type>
  <id>0</id>
  <posX>10</posX>
  <posY>10</posY>
  <label>this is a test</label>
  <visible>facadeview.list+Control.IsVisible(1242)</visible>
</control>
```

of course this only works if the skin has a definition for the dummy label control, typically this will set off screen:
```
<control>
  <type>label</type>
  <description>Dummy Label for Series Posters</description>
  <id>1242</id>
  <posX>-50</posX>
  <posY>-50</posY>
  <label></label>
  <visible>false</visible>
</control>
```

## Season View Layouts ##
The Season view exposes two layouts:

  * List
  * Filmstrip

The Filmstrip layout will load the **Series Posters** into the default icon.

## Episode View Layouts ##
The Episode view currently only exposes the **List** facade.

## Group View Layouts ##
The Group view exposes two layouts:

  * List
  * Small Icons

The default icon loaded into the Small Icon facade is the first logo defined for the currently selected item. This depends on Logo Rules defined, see Logo Rules for more details.

## Available Layouts ##
The skin can control what layouts it supports and consequently what the available layouts there are to choose from with-in the plugin. This is defined in a special skin file called **TVSeries.SkinSettings.xml**.

The layouts the skin supports are defined like so:
```
<layouts>
  <group List="true" SmallIcons="true"/>
  <series ListPosters="true" ListBanners="true" Filmstrip="true" WideBanners="true"/>
  <season List="true" Filmstrip="true"/>
  <episode List="true"/>
</layouts>
```
If your skin does not support a particular layout in a view, then this should be reflected in the Skin Settings file.

## Layout Content ##
The **List** layout in views, allow you to set the three item labels on the facade. The plugin reads the Skin Settings file to determine what to display. This is illustrated in the below example:
```
<views import="true" AppendlmageToList="false">		
  <group layout="List"></group>
  <series layout="ListPosters">			
    <item1></item1>
    <item2><![CDATA[<Series.Pretty_Name>]]></item2>			
    <item3></item3>
  </series>
  <season layout="List">	
    <item1></item1>
    <item2><![CDATA[Season <Season.SeasonIndex>]]></item2>		
    <item3></item3>
  </season>
  <episode>	
    <item1><![CDATA[<Episode.FirstAired>]]></item1>
    <item2><![CDATA[<Episode.EpisodeIndex>: <Episode.LocalEpisodeName>]]></item2>			
    <item3></item3>
  </episode>
</views>
```

The item labels using angled brackets are database fields in the plugin, a list of useful fields can be found here. These fields should also be wrapped in a CDATA section so that the XML document can be read correctly by the XML parser. You are not limited to database fields, you can also type in any string or even create a Formatting Rule and use that when you need to format the data e.g. Episode Counts

# Formatting Rules #
The Skin Settings file also allows you to create advanced properties not available in the database called Formatting Rules. These are basically expressions that the plugin will evaluate when loading the skin. A formatting rules consists of two main parts, the property name and how it should be formatted. Here is an example:

```
<formatting import="true">
  <![CDATA[<Enabled>1<Format><Episode.RatingPath><FormatAs>TVSeries\starEval(10*Round(<Episode.Rating>*2)/2).png
  <Enabled>1<Format><Episode.InfoPanelLine1Key><FormatAs>
  <Enabled>1<Format><Episode.InfoPanelLine1Value><FormatAs>(MY RATING: <Episode.myRating>)
  <Enabled>1<Format><Episode.InfoPanelLine2Key><FormatAs>FILE SIZE
  <Enabled>1<Format><Episode.InfoPanelLine2Value><FormatAs><Episode.FileSize>
  <Enabled>1<Format><Episode.InfoPanelLine4Key><FormatAs>DIRECTOR
  <Enabled>1<Format><Episode.InfoPanelLine4Value><FormatAs><Episode.Director>
  <Enabled>1<Format><Episode.InfoPanelLine5Key><FormatAs>WRITER
  <Enabled>1<Format><Episode.InfoPanelLine5Value><FormatAs><Episode.Writer>
  <Enabled>1<Format><Episode.InfoPanelLine6Key><FormatAs>GUEST STARS
  <Enabled>1<Format><Episode.InfoPanelLine6Value><FormatAs><Episode.GuestStars>
  <Enabled>1<Format><Episode.SeasonLabel><FormatAs>Season <Episode.SeasonIndex>
  <Enabled>1<Format><Season.SeasonLabel><FormatAs>Season <Season.SeasonIndex>
  <Enabled>1<Format><Series.InfoPanelLine1Key><FormatAs>RATING
  <Enabled>1<Format><Series.InfoPanelLine1Value><FormatAs>(MY RATING: <Series.myRating>)
  <Enabled>1<Format><Series.InfoPanelLine2Key><FormatAs>FIRST AIRED
  <Enabled>1<Format><Series.InfoPanelLine2Value><FormatAs><Series.FirstAired> (<Series.Status>)
  <Enabled>1<Format><Series.InfoPanelLine3Key><FormatAs>GENRE
  <Enabled>1<Format><Series.InfoPanelLine3Value><FormatAs><Series.Genre>
  <Enabled>1<Format><Series.InfoPanelLine4Key><FormatAs>RUNTIME
  <Enabled>1<Format><Series.InfoPanelLine4Value><FormatAs><Series.Runtime> minutes
  <Enabled>1<Format><Series.InfoPanelLine5Key><FormatAs>NETWORK
  <Enabled>1<Format><Series.InfoPanelLine5Value><FormatAs><Series.Network>
  <Enabled>1<Format><Series.InfoPanelLine6Key><FormatAs>ACTORS
  <Enabled>1<Format><Series.InfoPanelLine6Value><FormatAs><Series.Actors>
  <Enabled>1<Format>x () fps<FormatAs>N / A
  <Enabled>1<Format>0 bytes<FormatAs>N / A
  <Enabled>1<Format>(MY RATING: )<FormatAs> 
  <Enabled>1<Format>2channels <FormatAs>2.0 
  <Enabled>1<Format>6channels <FormatAs>5.1  ]]>
</formatting>
```

Formatting Rules can also make use of the built-in Math engine, allowing you to have a lot more control on how the data is displayed e.g. Lets say you want to display the number of episodes that are watched for a series, you can create a rule which subtracts the Unwatched episodes from the Total Episodes like so:
```
<Enabled>1<Format><Series.EpisodesWatched><FormatAs><Series.EpisodeCount>-<Series.EpisodesUnWatched>
```

This can then be used in your skin as standard property using: #TVSeries.Series.EpisodesWatched.

Formatting Rules can also be configured in the plugins configuration and then copied into the Skin Settings xml (set import="false" to see logos tab in configuration settings).

# Logo Rules #
Logo Rules allow you to create a Single Image on the skin which consists of multiple logos, the logos get added to the image (from Left to Right) in the order they were successfully evaluated. If the image does not exist, or the rule does not evaluate based on the conditions set then the image is not added. The advantage of this is that you dont need to worry about any gaps generated, also it only requires a single Image on the skin. Here is an example set of logo rules:
```
<logos import="true">
  <![CDATA[skin\Blue3wide\Media\TVSeries\Logos\<Series.Network>.png;-;;-;=;-;;-;AND;-;;-;=;-;;-;AND;-;;-;=;-;;-;
  skin\Blue3wide\Media\TVSeries\Logos\HD 720P_2.png;-;<Episode.videoWidth>;-;=;-;1280;-;AND;-;<Episode.videoHeight>;-;=;-;720;-;AND;-;;-;=;-;;-;
  skin\Blue3wide\Media\TVSeries\Logos\Full HD 1080.png;-;<Episode.videoWidth>;-;=;-;1920;-;AND;-;<Episode.videoHeight>;-;=;-;1080;-;AND;-;;-;=;-;;-;
  skin\Blue3wide\Media\TVSeries\Logos\<Episode.VideoCodec>.png;-;;-;=;-;;-;AND;-;;-;=;-;;-;AND;-;;-;=;-;;-;
  skin\Blue3wide\Media\TVSeries\Logos\<Episode.AudioCodec>.png;-;;-;=;-;;-;AND;-;;-;=;-;;-;AND;-;;-;=;-;;-;
  skin\Blue3wide\Media\TVSeries\Logos\Widescreen White.png;-;<Episode.VideoAspectRatio>;-;>;-;1.7;-;AND;-;<Episode.VideoAspectRatio>;-;<;-;2.5;-;AND;-;;-;=;-;;-;
  skin\Blue3wide\Media\TVSeries\Logos\Fullscreen.png;-;<Episode.VideoAspectRatio>;-;>;-;1.3;-;AND;-;<Episode.VideoAspectRatio>;-;<;-;1.5;-;AND;-;;-;=;-;;-;
  skin\Blue3wide\Media\TVSeries\Logos\<Series.Genre>.png;-;;-;=;-;;-;AND;-;;-;=;-;;-;AND;-;;-;=;-;;-;]]>
</logos>
```
Logo Rules can be easily configured in the plugins configuration and then copied into the Skin Settings xml (set import="false" to see logos tab in configuration settings).