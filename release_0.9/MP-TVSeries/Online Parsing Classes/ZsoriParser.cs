using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;

namespace WindowPlugins.GUITVSeries
{
    class ZsoriParser
    {
        static public XmlNodeList GetMirrors(String sServer)
        {
            return Generic(sServer + "/GetMirrors.php");
        }

        static public XmlNodeList GetSeries(String sSeriesName)
        {
            return Generic(DBOnlineMirror.Interface + "/GetSeries.php?seriesname=" + sSeriesName.Replace(' ', '+'));
        }

        static public XmlNodeList GetEpisodes(int nSeriesID, long nGetEpisodesTimeStamp)
        {
            return Generic(DBOnlineMirror.Interface + "/GetEpisodes.php?seriesid=" + nSeriesID + "&lasttime=" + nGetEpisodesTimeStamp);
        }

        static public XmlNodeList GetEpisodes(int nSeriesID, int nSeasonIndex, int nEpisodeIndex)
        {
            return Generic(DBOnlineMirror.Interface + "/GetEpisodes.php?seriesid=" + nSeriesID + "&season=" + nSeasonIndex + "&episode=" + nEpisodeIndex);
        }

        static public XmlNodeList UpdateSeries(String sSeriesIDs, long nUpdateSeriesTimeStamp)
        {
            return Generic(DBOnlineMirror.Interface + "/SeriesUpdates.php?lasttime=" + nUpdateSeriesTimeStamp + "&idlist=" + sSeriesIDs);
        }

        static public XmlNodeList UpdateEpisodes(String sEpisodesIDs, long nUpdateEpisodesTimeStamp)
        {
            return Generic(DBOnlineMirror.Interface + "/EpisodeUpdates.php?lasttime=" + nUpdateEpisodesTimeStamp + "&idlist=" + sEpisodesIDs);
        }

        static public XmlNodeList GetBanners(int nSeriesID, long nUpdateBannersTimeStamp)
        {
            return Generic(DBOnlineMirror.Interface + "/GetBanners.php?seriesid=" + nSeriesID + "&lasttime=" + nUpdateBannersTimeStamp);
        }

        static private XmlNodeList Generic(String sUrl)
        {
            MPTVSeriesLog.Write("Retrieving Data from: ", sUrl, MPTVSeriesLog.LogLevel.Debug);
            WebClient client = new WebClient();
            Stream data = null;
            try
            {
                data = client.OpenRead(sUrl);
            }
            catch (Exception e)
            {
                // can't connect, timeout, etc
                MPTVSeriesLog.Write("Can't connect to " + sUrl + " : " + e.Message);
            }
            if (data != null)
            {
                StreamReader reader = new StreamReader(data);
                String sXmlData = reader.ReadToEnd().Replace('\0', ' ');
                data.Close();
                reader.Close();
                MPTVSeriesLog.Write("*************************************", MPTVSeriesLog.LogLevel.Debug);
                MPTVSeriesLog.Write(sXmlData, MPTVSeriesLog.LogLevel.Debug, false);
                MPTVSeriesLog.Write("*************************************", MPTVSeriesLog.LogLevel.Debug);
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(sXmlData);
                    // skip xml node
                    XmlNode root = doc.FirstChild.NextSibling;
                    if (root.Name == "Items")
                    {
                        return root.ChildNodes;
                    }
                }
                catch (XmlException e)
                {
                    // bummer
                    MPTVSeriesLog.Write("Xml parsing of " + sUrl + " failed (line " + e.LineNumber + " - " + e.Message + ")");
                }
            }
            return null;
        }

    }
}
