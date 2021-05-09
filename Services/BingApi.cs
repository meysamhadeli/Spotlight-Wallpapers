using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace SpotlightWallpaper.Services
{
    internal class BingApi
    {
        private const string Url = "https://www.bing.com";
        
        public static async Task<string> GetBingImage()
        {
            var (saveImages, response) = await SaveImages();

            foreach (var image in response.Data.Images)
            {
                
                if (CheckExistImage(image, saveImages, out var ImageSavePath)) return null;

                try
                {
                    await DownloadImage(image, ImageSavePath);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return saveImages.LastOrDefault();
        }

        private static async Task<(List<string> saveImages, IRestResponse<HPImageArchive> response)> SaveImages()
        {
            var client = new RestClient(Url);
            var request = new RestRequest("HPImageArchive.aspx", Method.GET);
            List<string> saveImages = new List<string>();
            request.AddParameter("format", "js");
            request.AddParameter("mkt", "en-US");
            request.AddParameter("n", 8);

            var response = await client.ExecuteAsync<HPImageArchive>(request);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception();
            return (saveImages, response);
        }

        private static bool CheckExistImage(HPImageArchiveImage image, List<string> saveImages, out string ImageSavePath)
        {
            ImageSavePath =
                $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing\{image.Title?.Replace(" ", ".")}.jpeg";
            saveImages.Add(ImageSavePath);
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
            if (names.Contains(image.Title?.Replace(" ", ".")))
            {
                return true;
            }

            return false;
        }

        private static async Task DownloadImage(HPImageArchiveImage image, string ImageSavePath)
        {
            var httpClient = new HttpClient();

            var res = await httpClient.GetAsync(Url + image.Url);

            var stream = await res.Content.ReadAsStreamAsync();

            var result = Image.FromStream(stream);

            result.Save(ImageSavePath, ImageFormat.Jpeg);
        }
        
        internal class HPImageArchive
        {
            [DeserializeAs(Name = "images")] public List<HPImageArchiveImage> Images { get; set; }
        }


        internal class HPImageArchiveImage

        {
            [DeserializeAs(Name = "url")] public string Url { get; set; }
            [DeserializeAs(Name = "title")] public string Title { get; set; }
            [DeserializeAs(Name = "copyright")] public string Copyright { get; set; }
            [DeserializeAs(Name = "hsh")] public string Hash { get; set; }
        }
    }
}