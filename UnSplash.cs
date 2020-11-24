using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
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
        private const string UNSPLASH_URL = "https://source.unsplash.com/";

        /// <summary>
        /// local filename 
        /// </summary>
        private string localPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "UnSplash");

        /// <summary>
        /// for random choose only
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// current image
        /// </summary>
        private FileInfo current = null;

        /// <summary>
        /// unsplash image categories
        /// </summary>
        public enum Categories : int
        {
            Random = -1,
            Buildings = 0,
            Food = 1,
            Nature = 2,
            People = 3,
            Technology = 4,
            Objects = 5
        }

        /// <summary>
        /// default image size
        /// </summary>
        public Size ImageSize = new Size(1280, 1024);

        /// <summary>
        /// current unsplash image category
        /// </summary>
        public Categories Category = Categories.Objects;

        /// <summary>
        /// constructor
        /// </summary>
        // public Unsplash()
        // {
        //     // if (!Directory.Exists(this.localPath))
        //     // {
        //     //     Helper.Directory.CreateDirectory(new DirectoryInfo(this.localPath));
        //     // }
        // }

        /// <summary>
        /// get the current image fileinfo
        /// </summary>
        /// <returns>image fileinfo</returns>
        public FileInfo Current()
        {
            return this.current;
        }
        
        public async Task<FileInfo> Next()
        {
            if (Directory.Exists(this.localPath))
            {
                // build url
                string category = string.Empty;

                if (this.Category == Categories.Random)
                {
                    // random image category
                    this.Category = (Categories)this.random.Next(0, 5);
                }

                switch (this.Category)
                {
                    case Categories.Buildings: category = "buildings"; break;
                    case Categories.Food: category = "food"; break;
                    case Categories.Nature: category = "nature"; break;
                    case Categories.People: category = "people"; break;
                    case Categories.Technology: category = "technology"; break;
                    case Categories.Objects: category = "objects"; break;
                }

                string url = string.Format(
                    "{0}/category/{1}/{2}x{3}",
                    UNSPLASH_URL,
                    category,
                    1920,
                    1080);

                string filename = await this.downloadImage(new Uri(url));
                if (File.Exists(filename))
                {
                    this.current = new FileInfo(filename);
                    return this.current;
                }
            }
            this.current = null;
            return null;
        }
        
        #region helper methoths
        /// <summary>
        /// download the image with url from unsplash
        /// </summary>
        /// <param name="uri">image uri</param>
        /// <returns>returns true if download success</returns>
        private async Task<string> downloadImage(Uri uri)
        {
            using (var client = new WebClient())
            {
                try
                {
                    string filename = string.Format("{0}\\{1}.jpg", this.localPath, Guid.NewGuid());
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