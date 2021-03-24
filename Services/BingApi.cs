using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;

namespace SpotlightWallpaper.Services
{
    internal class BingApi
    {
        public const string Url = "https://www.bing.com";
        
        private static RestClient _restClient;

        public BingApi()
        {
            _restClient = new RestClient(Url);
        }

        public static async Task<string> GetBingImage()
        {
            var client = new RestClient("http://www.bing.com/");
            var request = new RestRequest("HPImageArchive.aspx?format=js&idx=0&n=1&mkt=en-US", Method.GET);
            var response = await client.ExecuteAsync<dynamic>(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception();
            string imageName = (response.Data["images"][0]["title"] + ".jpg");
            string imageUrl = response.Data["images"][0]["url"];
            var imageRequest = new RestRequest(imageUrl, Method.GET);
            Byte[] imageBytes = null;

            await Task.Run(() => { imageBytes = client.DownloadData(imageRequest); });

            string ImageSavePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing\{imageName?.Replace(" ",".")}";

            List<string> ext = new List<string> {".jpg", ".jpeg"};
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing";
            
            if (!Directory.Exists(patch))
            {
                Directory.CreateDirectory(patch);
            }
            
            var files = new DirectoryInfo(patch).EnumerateFiles("*.*", SearchOption.AllDirectories)
                .Where(path => ext.Contains(Path.GetExtension(path.Name)))
                .Select(x => new FileInfo(x.FullName)).OrderByDescending(f => f.LastWriteTime).ToList();

            var names = files.Select(x => x.Name).ToList();
            if (names.Contains(imageName?.Replace(" ",".")))
            {
                return null;
            }

            try
            {
                using (FileStream sourceStream = new FileStream(ImageSavePath,
                    FileMode.Append, FileAccess.Write, FileShare.None,
                    bufferSize: 4096, useAsync: true))
                {
                    await sourceStream.WriteAsync(imageBytes,0, imageBytes.Length);
                };
            }
            catch (Exception e)
            {
                throw e;
            }
          
            return ImageSavePath;
        }

    }
}
