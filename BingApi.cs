using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RestSharp;


namespace SpotlightWallpaper
{
    public class BingApi
    {
        
        public static async Task<string> GetBingImage()
        {
                var client = new RestClient("http://www.bing.com/");
                var request = new RestRequest("HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US", Method.GET);
                var response = await client.ExecuteAsync<dynamic>(request);
                string imageUrl = response.Data["images"][0]["url"];
                var shs = response.Data["images"][0]["hsh"];
                var imageRequest = new RestRequest(imageUrl, Method.GET);
                var imageBytes = client.DownloadData(imageRequest);
                string ImageSavePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing\{shs}.jpg";
                string exPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing";
                
                List<string> ext = new List<string> {".jpg", ".jpeg"};
                string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing";
                var files = new DirectoryInfo(patch).EnumerateFiles("*.*", SearchOption.AllDirectories)
                    .Where(path => ext.Contains(Path.GetExtension(path.Name)))
                    .Select(x => new FileInfo(x.FullName)).ToArray();

                if (!Directory.Exists(exPath))
                {
                    Directory.CreateDirectory(exPath);
                }

                var names = files.Select(x => x.Name).ToList();
                if (names.Contains($"{shs}.jpg"))
                {
                    return null;
                }
                
                File.WriteAllBytes(ImageSavePath, imageBytes);
                return ImageSavePath;
        }
        
    }
}