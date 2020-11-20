using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpotlightWallpaper
{
    public class BingApi
    {
        public static Task<string> GetURL()
        {
            string InfoUrl = "http://cn.bing.com/HPImageArchive.aspx?idx=0&n=1";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(InfoUrl);
            request.Method = "GET"; request.ContentType = "text/html;charset=UTF-8";
            string xmlDoc;
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = webResponse.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    xmlDoc = reader.ReadToEnd();
                }
            }
            Regex regex = new Regex("<Url>(?<MyUrl>.*?)</Url>", RegexOptions.IgnoreCase);
            MatchCollection collection = regex.Matches(xmlDoc);
            string ImageUrl = "http://www.bing.com" + collection[0].Groups["MyUrl"].Value;
            if (true)
            {
                ImageUrl = ImageUrl.Replace("1366x768", "1920x1080");
            }
            return Task.FromResult(ImageUrl);
        }
        
        public static async Task<string> WriteImage(FileInfo[] files)
        {
            string ImageSavePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing";
            Bitmap bmpWallpaper;
            WebRequest webreq = WebRequest.Create(await GetURL());
         
            WebResponse webres = webreq.GetResponse();
            using (Stream stream = webres.GetResponseStream())
            {
                bmpWallpaper = (Bitmap)Image.FromStream(stream);
                if (!Directory.Exists(ImageSavePath))
                {
                    Directory.CreateDirectory(ImageSavePath);
                }

                var names = files.Select(x => x.Name).ToList();
                var p = "bing" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() +
                        DateTime.Now.Day.ToString() + ".jpg";
                if (names.Contains(p))
                    return null;
                bmpWallpaper.Save(ImageSavePath + "\\bing" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".jpg", ImageFormat.Jpeg);
            }
            string strSavePath = "bing" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".jpg";
            return strSavePath;
        }
        
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
            int uAction,
            int uParam,
            string lpvParam,
            int fuWinIni
        );
        public static async Task SetWallpaper(string strSavePath)
        {
            SystemParametersInfo(20, 1, strSavePath, 1);
        }
    }
}