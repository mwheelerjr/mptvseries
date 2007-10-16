using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowPlugins.GUITVSeries.Local_Parsing_Classes {
    public partial class ManualParseDialog : Form {
        private FileInfo videoFile;
        private static Hashtable episodeCache;

        private const string SERIES_HELP_MESSAGE = "For an unlisted series, type series name here...";

        // launches the dialog with no default information specified
        public ManualParseDialog() : this (string.Empty) {
        }

        static ManualParseDialog() {
            episodeCache = new Hashtable();
        }

        // launches the ManualParseDialog with the specified filename loaded in the file textfield        
        public ManualParseDialog(string filename) {
            InitializeComponent();

            videoFile = null;
 
            setFile(filename);

            // populate the series dropdown listed with series that currently exist in the DB
            List<DBOnlineSeries> seriesList = DBOnlineSeries.getAllSeries();
            foreach (DBOnlineSeries currSeries in seriesList) {
                this.seriesComboBox.Items.Add(currSeries);
            }
            
            // set the default help info for the series combo box
            seriesComboBox.Text = SERIES_HELP_MESSAGE;
            seriesComboBox.ForeColor = Color.Gray;
        }

        // checks the validity of the provided file, if ok, store it's info and update screen
        private void setFile(string filename) {
            // check if we have anythign passed in
            if (filename != string.Empty) {
                videoFile = new FileInfo(filename);

                // file exists, update screen and quit
                if (videoFile.Exists) {
                    fileTextBox.Text = videoFile.Name;
                    return;
                }
            }

            // we couldn't create a valid file object pointing to an existing file
            videoFile = null;
            fileTextBox.Text = "";
            return;
        }

        // retrieves a list of all episodes info for a given series. if we already pulled
        // it once, it will use to cacheing to prevent multiple pulls of the same data
        private List<DBOnlineEpisode> getEpisodes(DBOnlineSeries series) {
            if (series == null)
                return null;
            
            // try to load from cache. i.e. check if we already loaded before, and if
            // so don't connect to the online DB again
            List<DBOnlineEpisode> episodeList;
            episodeList = (List<DBOnlineEpisode>) episodeCache[series];

            // if we dont have the episode list, pull it down
            if (episodeList == null) {
                // try to grab the season id, and exit if we fail. 
                int selectedSeriesID;
                bool haveValidSeriesID = int.TryParse(series[DBOnlineSeries.cID], out selectedSeriesID);
                if (!haveValidSeriesID) {
                    episodeComboBox.Enabled = false;
                    return new List<DBOnlineEpisode>();
                }

                // retrieve and store the rsults
                GetEpisodes episodeGrabber = new GetEpisodes(selectedSeriesID);
                episodeList = episodeGrabber.Results;
                episodeCache.Add(series, episodeList);
            }

            return episodeList;
        }

        // commits the episode to the database (as well as the season and series if neccisary)
        private bool commitEpisode() {
            DBOnlineEpisode onlineEp = (DBOnlineEpisode) episodeComboBox.SelectedItem;
            DBOnlineSeries onlineSeries = (DBOnlineSeries) seriesComboBox.SelectedItem;

            if (onlineEp == null || onlineSeries == null)
                return false;

            int seriesID = (int) onlineSeries[DBOnlineSeries.cID];
            int seasonNum = (int) onlineEp[DBOnlineEpisode.cSeasonIndex];
            int episodeNum = (int) onlineEp[DBOnlineEpisode.cEpisodeIndex]; 

            // build the local series
            DBSeries localSeries = DBSeries.Get(onlineSeries[DBOnlineSeries.cID]);
            if (localSeries == null) {
                localSeries = new DBSeries((string)onlineSeries[DBOnlineSeries.cPrettyName]);
                localSeries[DBSeries.cID] = onlineSeries[DBOnlineSeries.cID];
            }
            localSeries.Commit();
            onlineSeries[DBOnlineSeries.cHasLocalFiles] = 1;
            onlineSeries.Commit();            
            
            // make sure the season is in the DB
            DBSeason season = new DBSeason(seriesID, seasonNum);
            season[DBSeason.cHasLocalFilesTemp] = true;
            season[DBSeason.cHasEpisodes] = true;
            season.Commit();

            // construct and add the episode (i am not sure how much of this is required...
            // would be nice if a DBOnlineEpisode object could just create a DBEpisode object...
            DBEpisode episode = new DBEpisode(this.videoFile.FullName);
            episode[DBEpisode.cImportProcessed] = 1;
            episode[DBEpisode.cSeriesID] = seriesID;
            episode[DBEpisode.cSeasonIndex] = seasonNum;
            episode[DBEpisode.cEpisodeIndex] = episodeNum; 
            episode.onlineEpisode[DBOnlineEpisode.cEpisodeIndex] = episodeNum;
            episode.onlineEpisode[DBOnlineEpisode.cSeasonIndex] = seasonNum;
            episode[DBOnlineEpisode.cID] = onlineEp[DBOnlineEpisode.cID];
            if (episode[DBOnlineEpisode.cEpisodeName].ToString().Length == 0)
                episode[DBOnlineEpisode.cEpisodeName] = onlineEp[DBOnlineEpisode.cEpisodeName];
            episode.Commit();
            onlineEp.Commit();

            // update detailed online data for new stuff
            OnlineParsing onlineParser = new OnlineParsing((Feedback.Interface) this.Owner);
            onlineParser.UpdateSeries(true);
            onlineParser.UpdateBanners(true);
            onlineParser.UpdateEpisodes(true);
            
            return true;
        }
 
        // attempts to populate the season combo box based on the selection in the series combo
        private void populateSeasonList() {
           // grab series and episode info
            DBOnlineSeries selectedSeries = (DBOnlineSeries)seriesComboBox.SelectedItem;
            List<DBOnlineEpisode> episodeList = this.getEpisodes(selectedSeries);

            // clear the currently selected season
            seasonComboBox.Items.Clear();
            seasonComboBox.Text = "";
            seasonComboBox.SelectedItem = null;

            // if no series is found or we have no episodes, nothing to populate, quit
            if (selectedSeries == null || episodeList.Count == 0) {
                seasonComboBox.Enabled = false;
                return;
            }
            
            // loop through the episode list and add the seasons to the combo box.
            seasonComboBox.Items.Clear();
            foreach (DBOnlineEpisode currEpisode in episodeList) {
                int seasonNum = (int) currEpisode[DBOnlineEpisode.cSeasonIndex];
                if (!seasonComboBox.Items.Contains(seasonNum))
                    seasonComboBox.Items.Add(seasonNum);
            }
            
            // if we had values, enable the box
            if (seasonComboBox.Items.Count > 0)
                seasonComboBox.Enabled = true;
            else
                seasonComboBox.Enabled = false;
        }

        // attempts to populate the episode combo box based on the values in the series
        // combo and the season combo box. if either has an invalid value, just exit.
        private void populateEpisodeList() {
            int selectedSeason;

            // grab the season and series selected
            DBOnlineSeries selectedSeries = (DBOnlineSeries) seriesComboBox.SelectedItem;
            bool seasonIsValidNum = int.TryParse(seasonComboBox.Text, out selectedSeason);

            // if we dont have a valid season and series, exit
            if (!seasonIsValidNum || selectedSeries == null) {
                episodeComboBox.Text = "";
                episodeComboBox.SelectedItem = null;
                episodeComboBox.Enabled = false;
                return;
            }

            // if the series the combobox is returning doesn't match up with the text it is
            // displaying, fail. for some reason, with a text edit of one additonal char, a 
            // combo box still returns the last selected item, even though the user manually 
            // modified the selected value... a bug in .NET?
            if (selectedSeries[DBOnlineSeries.cPrettyName] != seriesComboBox.Text) {
                episodeComboBox.Text = "";
                episodeComboBox.SelectedItem = null;
                episodeComboBox.Enabled = false;
                return;
            }

            // update the entries in the GUI controls
            episodeComboBox.Enabled = true;
            episodeComboBox.Items.Clear();
            episodeComboBox.SelectedItem = null;
            episodeComboBox.Text = "";

            // grab the episodes for the given series and season from the online DB
            List<DBOnlineEpisode> episodeList = this.getEpisodes(selectedSeries);
            foreach (DBOnlineEpisode currEpisode in episodeList) {
                if ((int)currEpisode[DBOnlineEpisode.cSeasonIndex] == selectedSeason)
                    episodeComboBox.Items.Add(currEpisode);
            }
        }

        // checks the existing value of the series combobox and updates the help message 
        // accordingly. intended to be triggered by focus changes for the combo box
        private void updateSeriesHelpMessage() {
            // when the series combo gets focus, clear the help message as needed
            if (seriesComboBox.Text == SERIES_HELP_MESSAGE) {
                seriesComboBox.Text = "";
                seriesComboBox.SelectedItem = null;
                seriesComboBox.ForeColor = Color.Black;
                return;
            }

            // if nothing is in the seriesbox, fill in the help info
            if (seriesComboBox.Text == "") {
                seriesComboBox.Text = SERIES_HELP_MESSAGE;
                seriesComboBox.SelectedItem = null;
                seriesComboBox.ForeColor = Color.Gray;
                return;
            }
        }

        // checks if the value in the series combo box is a custom entry. if so, looks
        // up the series and loads it into memory
        private void checkForNewSeries() {
            if (seriesComboBox.Text == SERIES_HELP_MESSAGE)
                return;
            
            DBOnlineSeries selectedSeries = (DBOnlineSeries)seriesComboBox.SelectedItem;
            if (seriesComboBox.Text.Length != 0 && seriesComboBox.SelectedItem == null) {
                OnlineParsing parser = new OnlineParsing((Feedback.Interface) this.Owner);
                DBOnlineSeries newSeries = parser.SearchForSeries(seriesComboBox.Text);

                // if a series was able to be parsed from the custom text, 
                // set it as the selected series
                if (newSeries != null) {
                    // check if we already have this series in the list
                    bool alreadyExists = false;
                    foreach (DBOnlineSeries currSeries in seriesComboBox.Items) {
                        if (currSeries[DBOnlineSeries.cID] == newSeries[DBOnlineSeries.cID]) {
                            seriesComboBox.SelectedItem = currSeries;
                            alreadyExists = true;
                            break;
                        }
                    }
                    
                    // if we didnt find it, add it
                    if (!alreadyExists) {
                        seriesComboBox.Items.Add(newSeries);
                        seriesComboBox.SelectedItem = newSeries;
                    }
                }
            }
        }

        // when an episode is selected, validate the data in the combo boxes and if
        // everything looks ok, enable the OK button
        private void updateOKButton() {
            DBOnlineEpisode onlineEp = (DBOnlineEpisode)episodeComboBox.SelectedItem;
            DBOnlineSeries onlineSeries = (DBOnlineSeries)seriesComboBox.SelectedItem;

            if (onlineEp == null || onlineSeries == null)
                okButton.Enabled = false;
            else
                okButton.Enabled = true;
        }

        // action performed when the "Browse..." button is clicked. Launches file select dialog.
        private void selectFileButton_click(object sender, EventArgs e) {
            // if we already have a file loaded, init the dialog with that file selected
            if (videoFile != null) {
                openFileDialog.FileName = videoFile.Name;
                openFileDialog.InitialDirectory = videoFile.DirectoryName;
            }

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
                setFile(openFileDialog.FileName);
        }

        private void seriesComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            populateSeasonList();
            populateEpisodeList();
            updateOKButton();
        }

        private void seriesComboBox_TextChanged(object sender, EventArgs e) {
            populateSeasonList();
            populateEpisodeList();
            updateOKButton();
        }


        private void seriesComboBox_LostFocus(object sender, EventArgs e) {
            updateSeriesHelpMessage();
            checkForNewSeries();
        }

        private void seriesComboBox_GotFocus(object sender, EventArgs e) {
            updateSeriesHelpMessage();
        }

        private void seasonComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            populateEpisodeList();
            updateOKButton();
        }

        private void episodeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            updateOKButton();
        }
        
        private void okButton_Click(object sender, EventArgs e) {
            commitEpisode();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}