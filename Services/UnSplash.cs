using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpotlightWallpaper.Services
{
    public class UnSplash
    {
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
            People = 0,
            Wallpapers = 1
        }

        /// <summary>
        /// current unsplash image category
        /// </summary>
        public static Categories Category = Categories.Random;


        public static async Task<FileInfo> GetUnSplashImage()
        {
            if (Directory.Exists(localPath))
            {
                // build url
                string category = string.Empty;
                Category = Categories.Random;

                var rd = random.Next(0, 2);
                Category = (Categories) rd;

                switch (Category)
                {
                    case Categories.Wallpapers:
                        category = "wallpapers";
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
            try
            {
                var response = await SpotlightApi.GetBatchResponseAsync();
            }
            catch (Exception e)
            {
                throw new Exception();
            }


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