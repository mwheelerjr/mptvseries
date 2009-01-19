#region GNU license
// MP-TVSeries - Plugin for Mediaportal
// http://www.team-mediaportal.com
// Copyright (C) 2006-2007
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
#endregion

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using MediaPortal.Util;
using MediaPortal.GUI.Library;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.InteropServices;
using aclib.Performance;

namespace WindowPlugins.GUITVSeries
{
    public class ImageAllocator
    {
        static String s_sFontName;
        static List<String> s_SeriesImageList = new List<string>();
        static List<String> s_SeasonsImageList = new List<string>();
        static List<String> s_OtherPersistentImageList = new List<string>();
        static List<String> s_OtherDiscardableImageList = new List<string>();
        static Size reqSeriesPosterSize = new Size(680 * DBOption.GetOptions(DBOption.cQualitySeriesPosters) / 100, 1000 * DBOption.GetOptions(DBOption.cQualitySeriesPosters) / 100);
        static Size reqSeriesBannerSize = new Size(758 * DBOption.GetOptions(DBOption.cQualitySeriesBanners) / 100, 140 * DBOption.GetOptions(DBOption.cQualitySeriesBanners) / 100);
        static Size reqSeasonBannerSize = new Size(400 * DBOption.GetOptions(DBOption.cQualitySeasonBanners) / 100, 578 * DBOption.GetOptions(DBOption.cQualitySeasonBanners) / 100);        
        static float reqEpisodeImagePercentage = (float)(DBOption.GetOptions(DBOption.cQualityEpisodeImages)) / 100f;

        static ImageAllocator()
        {
        }

        #region Helpers
        /// <summary>
        /// Create a banner image of the specified size, outputting the input text on it
        /// </summary>
        /// <param name="sizeImage">Size of the image to be generated</param>
        /// <param name="label">Text to be output on the image</param>
        /// <returns>a bitmap object</returns>
        private static Bitmap drawSimpleBanner(Size sizeImage, string label)
        {
            Bitmap image = new Bitmap(sizeImage.Width, sizeImage.Height);
            Graphics gph = Graphics.FromImage(image);
            //gph.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.White)), new Rectangle(0, 0, sizeImage.Width, sizeImage.Height));
            GUIFont fontList = GUIFontManager.GetFont(s_sFontName);
            Font font = new Font(fontList.FontName, 36);
            gph.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            gph.DrawString(label, font, new SolidBrush(Color.FromArgb(200, Color.White)), 5, (sizeImage.Height - font.GetHeight()) / 2);
            gph.Dispose();
            return image;
        }

        /// <summary>
        /// Takes an Image sFileName and tries to load it into MP' graphics memory
        /// If the sFileName was already in memory, it will not be reloaded (basically it caches)
        /// </summary>
        /// <param name="sFileName">The sFileName of the image to load, fails silently if it cannot be loaded</param>
        /// <returns>memory identifier</returns>
        public static string buildMemoryImageFromFile(string sFileName, System.Drawing.Size size)
        {
            try
            {
                if (String.IsNullOrEmpty(sFileName) || !System.IO.File.Exists(sFileName)) return string.Empty;
                string ident = buildIdentifier(sFileName);
                if (GUITextureManager.LoadFromMemory(null, ident, 0, size.Width, size.Height) > 0) return ident;
                else return buildMemoryImage(LoadImageFastFromFile(sFileName), ident, size, false);
            }
            catch (Exception e)
            {
                MPTVSeriesLog.Write("Unable to add to MP's Graphics memory: " + sFileName + " Error: " + e.Message);
                return string.Empty;
            }
        }

        static string buildIdentifier(string name)
        {
            return "[TVSeries:" + name + "]";
        }

        /// <summary>
        /// Takes an Image and tries to load it into MP' graphics memory
        /// If the sFileName was already in memory, it will not be reloaded (basically it caches)
        /// </summary>
        /// <param name="image">The System.Drawing.Bitmap to be loaded</param>
        /// <param name="identifier">A unique identifier for the image so it can be retrieved later on</param>
        /// <returns>memory identifier</returns>
        public static string buildMemoryImage(Image image, string identifier, System.Drawing.Size size, bool buildIdentifier)
        {
            string name = buildIdentifier ? ImageAllocator.buildIdentifier(identifier) : identifier;
            try
            {                
                // we don't have to try first, if name already exists mp will not do anything with the image
                if (size.Height > 0 && (size.Height != image.Size.Height || size.Width != image.Size.Width)) //resize
                {                    
                    image = Resize(image, size);                                        
                }
                PerfWatcher.GetNamedWatch("add to TextureManager").Start();
                GUITextureManager.LoadFromMemory(image, name, 0, size.Width, size.Height);
                PerfWatcher.GetNamedWatch("add to TextureManager").Stop();
            }
            catch (Exception)
            {
                MPTVSeriesLog.Write("Unable to add to MP's Graphics memory: " + identifier);
                return string.Empty;
            }
            return name;
        }

        public static string ExtractFullName(string identifier)
        {
            String RegExp = @"\[TVSeries:(.*)\]";
            Regex Engine = new Regex(RegExp, RegexOptions.IgnoreCase);
            Match match = Engine.Match(identifier);
            if (match.Success)
                return match.Groups[1].Value;
            else
                return identifier;            
        }

        public static void Flush(List<String> toFlush)
        {
            foreach (String sTextureName in toFlush)
            {
                Flush(sTextureName);
            }
            toFlush.Clear();
        }

        public static void Flush(string sTextureName)
        {
            GUITextureManager.ReleaseTexture(sTextureName);
        }
        #endregion

        /// <summary>
        /// Set the font name to be used to create dummy banners
        /// </summary>
        /// <param name="sFontName">Size of the image to be generated</param>
        /// <returns>nothing</returns>
        public static void SetFontName(String sFontName)
        {
            s_sFontName = sFontName;
        }

        /// <summary>
        /// Sets or gets the default Series banner size with which banners will be loaded into memory
        /// </summary>
        public static Size SetSeriesBannerSize { set { reqSeriesBannerSize = value; } get { return reqSeriesBannerSize;} }

        /// <summary>
        /// Sets or gets the default Season banner size with which banners will be loaded into memory
        /// </summary>
        public static Size SetSeasonBannerSize { set { reqSeasonBannerSize = value; } get { return reqSeasonBannerSize; } }

        /// <summary>
        /// Sets or gets the default Series poster size with which banners will be loaded into memory
        /// </summary>
        public static Size SetSeriesPosterSize { set { reqSeriesPosterSize = value; } get { return reqSeriesPosterSize; } }

        public static String GetSeriesBanner(DBSeries series)
        {
            String sFileName;
            String sTextureName;

            bool IsPoster = (DBOption.GetOptions(DBOption.cView_Series_ListFormat) == "Filmstrip" ||
                             DBOption.GetOptions(DBOption.cView_Series_ListFormat) == "ListPosters");

            if (IsPoster)
                sFileName = series.Poster;
            else           
                sFileName = series.Banner;            

            if (sFileName.Length > 0 && System.IO.File.Exists(sFileName))
            {
                if (DBOption.GetOptions(DBOption.cAltImgLoading))
                {
                    // bypass memoryimagebuilder
                    sTextureName = sFileName;
                }
                else if (IsPoster)
                    sTextureName = buildMemoryImageFromFile(sFileName, reqSeriesPosterSize);
                else
                    sTextureName = buildMemoryImageFromFile(sFileName, reqSeriesBannerSize);
            }
            else
            {                
                // no image, use text, create our own
                string ident = "series_" + series[DBSeries.cID];

                if (!IsPoster)                                
                {
                    sTextureName = buildMemoryImage(drawSimpleBanner(reqSeriesBannerSize, series[DBOnlineSeries.cPrettyName]), ident, reqSeriesBannerSize, true);
                }
                else
                    sTextureName = buildMemoryImage(drawSimpleBanner(reqSeriesPosterSize, series[DBOnlineSeries.cPrettyName]), ident, reqSeriesPosterSize, true);

            }
            if(sTextureName.Length > 0 && !s_SeriesImageList.Contains(sTextureName)) s_SeriesImageList.Add(sTextureName);            
            return sTextureName;
        }

        public static String GetSeasonBanner(DBSeason season, bool createIfNotExist)
        {
            String sFileName = season.Banner;
            String sTextureName;
            if (sFileName.Length > 0 && System.IO.File.Exists(sFileName))
            {
                if (DBOption.GetOptions(DBOption.cAltImgLoading)) 
                  sTextureName = sFileName; // bypass memoryimagebuilder
                else 
                  sTextureName = buildMemoryImageFromFile(sFileName, reqSeasonBannerSize);
            }
            else if (createIfNotExist)
            {
                // no image, use text, create our own
                string text = (season[DBSeason.cIndex] == 0) ? Translation.specials : Translation.Season + season[DBSeason.cIndex];
                string ident = season[DBSeason.cSeriesID] + "S" + season[DBSeason.cIndex];
                sTextureName = buildMemoryImage(drawSimpleBanner(reqSeasonBannerSize, text), ident, reqSeasonBannerSize, true);
            }
            else return string.Empty;

            s_SeasonsImageList.Add(sTextureName);
            return sTextureName;
        }

        public static String GetEpisodeImage(DBEpisode episode)
        {
            if (Helper.String.IsNullOrEmpty(episode.Image)) return string.Empty;
            if (episode.Image.Length > 0 && System.IO.File.Exists(episode.Image))
              return episode.Image;
            else
              return String.Empty;
        }


        public static String GetOtherImage(string sFileName, System.Drawing.Size size, bool bPersistent)
        {
            return GetOtherImage(null, sFileName, size, bPersistent);
        }

        public static String GetOtherImage(Image i, string sFileName, System.Drawing.Size size, bool bPersistent)
        {
            String sTextureName;
            if(i != null) sTextureName = buildMemoryImage(i, sFileName, size, true);
            else sTextureName = buildMemoryImageFromFile(sFileName, size);
            if (bPersistent)
            {
                if (!s_OtherPersistentImageList.Contains(sTextureName))
                    s_OtherPersistentImageList.Add(sTextureName);
            }
            else if (!s_OtherDiscardableImageList.Contains(sTextureName))
                s_OtherDiscardableImageList.Add(sTextureName);
            return sTextureName;
        }

        public static void FlushAll()
        {
            FlushOthers(true);
            FlushSeasons();
            FlushSeries();
        }
        public static void FlushSeries()
        {
            Flush(s_SeriesImageList);
        }
        public static void FlushSeasons()
        {
            Flush(s_SeasonsImageList);
        }
        public static void FlushOthers(bool bFlushPersistents)
        {
            Flush(s_OtherDiscardableImageList);
            if (bFlushPersistents)
                Flush(s_OtherPersistentImageList);
        }

        #region FastBitmapLoading From File
        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode)]
        private static extern int GdipLoadImageFromFile(string filename, out IntPtr image);
        private static Type imageType = typeof(System.Drawing.Bitmap);

        /// <summary>
        /// Loads an Image from a File by invoking GDI Plus instead of using build-in .NET methods, or falls back to Image.FromFile
        /// Can perform up to 10x faster
        /// </summary>
        /// <param name="filename">The filename to load</param>
        /// <returns>A .NET Image object</returns>
        public static Image LoadImageFastFromFile(string filename)
        {
            PerfWatcher.GetNamedWatch("Img Loading").Start();
            IntPtr image = IntPtr.Zero;
            Image i = null;
            try
            {
                // We are not using ICM at all, fudge that, this should be FAAAAAST!
                if (GdipLoadImageFromFile(filename, out image) != 0)
                {
                    MPTVSeriesLog.Write("ImageLoadFast threw an error");
                    i = Image.FromFile(filename);
                }
                else i = (Image)imageType.InvokeMember("FromGDIplus", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, new object[] { image });

            }
            catch (Exception e)
            {
                // this probably means the image is bad
                PerfWatcher.GetNamedWatch("Img Loading").Stop();
                MPTVSeriesLog.Write("ImageLoading threw an error: " + filename + " - " + e.Message);
                return null;                
            }
            PerfWatcher.GetNamedWatch("Img Loading").Stop();
            return i;
        }
        #endregion

        public static Bitmap Resize(Image img, Size size)
        {
            Bitmap bmp = null;

            // this should be tons faster by using simple nearestneighbour (about 3x in my testapp)
            // but for some reason when run in here its about 2x slower than a simple new Bitmap()
            // ???????

            //if (size.Height % 16 != 0)
            //    size.Height += 16 - size.Height % 16;
            //if (size.Width % 16 != 0)
            //    size.Width += 16 - size.Width % 16;
            //MPTVSeriesLog.Write(size.Width + "x" + size.Height);

            //PerfWatcher.GetNamedWatch("NN ImgScaling").Start();
            ////create a new Bitmap the size of the new image
            //bmp = new Bitmap(size.Width, size.Height);
            ////create a new graphic from the Bitmap
            //Graphics graphic = Graphics.FromImage((Image)bmp);
            //graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            //graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
            ////draw the newly resized image
            //PerfWatcher.GetNamedWatch("NN ImgScaling draw").Start();
            //graphic.DrawImage(img, 0, 0, size.Width, size.Height);
            //PerfWatcher.GetNamedWatch("NN ImgScaling draw").Stop();
            ////dispose and free up the resources
            //graphic.Dispose();

            //PerfWatcher.GetNamedWatch("NN ImgScaling").Stop();
            aclib.Performance.PerfWatcher.GetNamedWatch("ImgScaling").Start();
            bmp = new Bitmap(img, size);
            //bmp = (Bitmap)img.GetThumbnailImage(size.Width, size.Height, null, IntPtr.Zero);
            //bmp = (Bitmap)img;
            aclib.Performance.PerfWatcher.GetNamedWatch("ImgScaling").Stop();
            return bmp;
        }
    }
}
