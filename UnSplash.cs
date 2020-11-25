using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SpotlightWallpaper
{
    public class UnSplash
    {
        public UnSplash()
        {
        }

        /// <summary>
        /// url to anspash
        /// </summary>
        private static string UNSPLASH_URL = "https://source.unsplash.com/";

        /// <summary>
        /// local filename 
        /// </summary>
        private static string localPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "UnSplash");

        /// <summary>
        /// for random choose only
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// current image
        /// </summary>
        private static FileInfo current = null;

        /// <summary>
        /// unsplash image categories
        /// </summary>
        public enum Categories : int
        {
            Random = -1,
            Nature = 0,
            Wallpapers = 1,
            Travel = 2,
            People = 3,
        }

        /// <summary>
        /// current unsplash image category
        /// </summary>
        public static Categories Category = Categories.Random;
        
        /// <summary>
        /// get the current image fileinfo
        /// </summary>
        /// <returns>image fileinfo</returns>
        public FileInfo Current()
        {
            return current;
        }

        public static async Task<List<FileInfo>> GetUnSplashImages()
        {
            var fileInfoes = new List<FileInfo>();
            var listUris = new List<string>();
            for (int i = 0; i < 9; i++)
            {
                if (Directory.Exists(localPath))
                {
                    // build url
                    string category = string.Empty;

                    if (Category == Categories.Random)
                    {
                        // random image category
                        Category = (Categories) random.Next(0, 5);
                    }

                    switch (Category)
                    {
                        case Categories.Travel:
                            category = "travel";
                            break;
                        case Categories.Wallpapers:
                            category = "wallpapers";
                            break;
                        case Categories.Nature:
                            category = "nature";
                            break;
                        case Categories.People:
                            category = "people";
                            break;
                    }

                    string url = string.Format(
                        "{0}/category/{1}/{2}x{3}",
                        UNSPLASH_URL,
                        category, 1920, 1080);
                    if (!string.IsNullOrWhiteSpace(url))
                        listUris.Add(url);
                }
            }

            if (listUris.Any())
            {
                foreach (var url in listUris)
                {
                    string filename = await downloadImage(new Uri(url));
                    if (File.Exists(filename))
                    {
                        current = new FileInfo(filename);
                        fileInfoes.Add(current);
                    }
                }
            }

            return fileInfoes;


            fileInfoes = null;
            return null;
        }

        public static async Task<FileInfo> GetUnSplashImage()
        {
            var listUris = new List<string>();

            if (Directory.Exists(localPath))
            {
                // build url
                string category = string.Empty;

                if (Category == Categories.Random)
                {
                    // random image category
                    Category = (Categories) random.Next(0, 5);
                }

                switch (Category)
                {
                    case Categories.Travel:
                        category = "travel";
                        break;
                    case Categories.Wallpapers:
                        category = "wallpapers";
                        break;
                    case Categories.Nature:
                        category = "nature";
                        break;
                    case Categories.People:
                        category = "people";
                        break;
                }

                string url = string.Format(
                    "{0}/category/{1}/{2}x{3}",
                    UNSPLASH_URL,
                    category, 1920, 1080);
                string filename = await downloadImage(new Uri(url));
                if (File.Exists(filename))
                {
                    current = new FileInfo(filename);
                    return current;
                }
            }

            current = null;
            return null;
        }
        #region helper methoths

        /// <summary>
        /// download the image with url from unsplash
        /// </summary>
        /// <param name="uri">image uri</param>
        /// <returns>returns true if download success</returns>
        private static async Task<string> downloadImage(Uri uri)
        {
            using (var client = new WebClient())
            {
                try
                {
                    string filename = string.Format("{0}\\{1}.jpg", localPath, Guid.NewGuid());
                    await client.DownloadFileTaskAsync(uri, filename);
                    if (File.Exists(filename))
                    {
                        return filename;
                    }

                    return null;
                }
                catch (WebException ex)
                {
                    // Log Error
                    Debug.WriteLine("download file error: [{0}]", ex.Message);
                    return null;
                }
            }
        }

        #endregion
    }
}