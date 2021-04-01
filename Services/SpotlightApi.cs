using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotlightWallpaper.CustomException;
using SpotlightWallpaper.My;
using SpotlightWallpaper.Settings;

namespace SpotlightWallpaper.Services
{
    public class SpotlightApi
    {
        public static async Task<string> GetSpotlightImage()
        {
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";
            List<string> ext = new List<string> {".jpg", ".jpeg"};

            var response = await GetBatchResponseAsync();
            var deserializeRoot = JsonConvert.DeserializeObject<Root>(response);
            if (deserializeRoot.batchrsp?.errors != null)
                throw new Exception();
            
            var images = await GetImageInfo(response);
            var files = new DirectoryInfo(patch).EnumerateFiles("*.*", SearchOption.AllDirectories)
                .Where(path => ext.Contains(Path.GetExtension(path.Name)))
                .Select(x => new FileInfo(x.FullName)).OrderByDescending(f => f.LastWriteTime).ToList();

            var iName = await WriteImage(images.Landscape.Url, files);
            if (iName == null)
                return null;
            var saveName = $"{patch}\\{iName}.jpg";
            return saveName;
        }

        /// <summary>
        /// Consumes MsnArc API to retrieve a JSON object of Windows Spotlight images.
        /// </summary>
        /// <returns>A JSON object containing another JSON object which contains image info.</returns>
        public static async Task<string> GetBatchResponseAsync()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            RegionInfo currentRegion = new RegionInfo(currentCulture.LCID);
            string region = currentRegion.TwoLetterISORegionName.ToLower();

                var locale = currentCulture.Name;
                if (locale.Length > 2 && locale.Contains("-"))
                region = locale.Split('-')[1].ToLower();
                
            string request = String.Format(
                "https://arc.msn.com/v3/Delivery/Placement?pid=338387&fmt=json&ua=WindowsShellClient"
                + "%2F0&cdm=1&pl={0}&lc={1}&ctry={2}&time={3}",
                locale,
                locale,
                region,
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")
            );

            using (var client = new HttpClient())
            {
                var responseMsg =
                    await client.GetAsync(request);
                if (responseMsg.StatusCode != HttpStatusCode.OK)
                    throw new Exception();
                return await responseMsg.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Extracts image info from nested batch response JSON object.
        /// </summary>
        /// <param name="batchResponse">Batch response JSON object, ideally from GetbatchResponseAsync().</param>
        /// <returns>A ImageInfo containing info of landscape and portrait spotlight images.</returns>
        public async static Task<ImageInfos> GetImageInfo(string batchResponse)
        {
            return await Task.Run<ImageInfos>(() =>
            {
                var infos = new ImageInfos();

                var batchParsed = JObject.Parse(batchResponse);
                var singleItem = batchParsed.SelectToken("batchrsp.items[0].item").ToString();

                var itemParsed = JObject.Parse(singleItem);
                infos = itemParsed.SelectToken("ad").ToObject<ImageInfos>();

                return infos;
            });
        }

        /// <summary>
        /// Downloads a image and write it to working directory and return its filename.
        /// </summary>
        /// <param name="imageUrl">Image URL to download.</param>
        /// <returns>The file name of the downloaded image.</returns>
        public static async Task<string> WriteImage(string imageUrl, List<FileInfo> files)
        {
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";
            string imageName = imageUrl.Split('/').Last().Split('?')[0];

            var names = files.Select(x => x.Name).ToList();
            if (names.Contains($"{imageName}.jpg"))
            {
                return null;
            }

            using (var client = new HttpClient())
            {
                var imageResponse = await client.GetAsync(imageUrl);

                using (FileStream sourceStream = new FileStream($"{patch}/{imageName}.jpg",
                    FileMode.Append, FileAccess.Write, FileShare.None,
                    bufferSize: 4096, useAsync: true))
                {
                    var content = await imageResponse.Content.ReadAsByteArrayAsync();
                    await sourceStream.WriteAsync(content, 0, content.Length);
                }

                ;
            }


            return imageName;
        }

        #region ImageInfo object

        public class ImageInfos
        {
            private ImageInfo[] _infos = new ImageInfo[2];

            [JsonProperty("image_fullscreen_001_landscape")]
            public ImageInfo Landscape
            {
                get { return _infos[0]; }
                set { _infos[0] = value; }
            }

            [JsonProperty("image_fullscreen_001_portrait")]
            public ImageInfo Portrait
            {
                get { return _infos[1]; }
                set { _infos[1] = value; }
            }
        }

        public class ImageInfo
        {
            [JsonProperty("w")] public string Width { get; set; }
            [JsonProperty("h")] public string Height { get; set; }
            [JsonProperty("fileSize")] public string Size { get; set; }
            [JsonProperty("sha256")] public string Sha256 { get; set; }
            [JsonProperty("u")] public string Url { get; set; }
        }

        #endregion
    }
}