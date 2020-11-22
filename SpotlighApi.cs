﻿using System;
 using System.Drawing.Imaging;
 using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotlightWallpaper.My;

namespace SpotlightWallpaper
{
    public class SpotlighApi
    {
        /// <summary>
        /// Consumes MsnArc API to retrieve a JSON object of Windows Spotlight images.
        /// </summary>
        /// <returns>A JSON object containing another JSON object which contains image info.</returns>
        public static async Task<string> GetSpotlightResponseAsync()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            RegionInfo currentRegion = new RegionInfo(currentCulture.LCID);
            string region = currentRegion.TwoLetterISORegionName.ToLower();

            var locale = currentCulture.Name;

            int screenWidth = 1600;
            int screenHeight = 900;

            using (var client = new HttpClient())
            {
                string request = String.Format(
                    "https://arc.msn.com/v3/Delivery/Cache?pid=338387&fmt=json&ua=WindowsShellClient"
                    + "%2F0&pl={0}&lc={1}&ctry={2}&time={3}",
                    locale,
                    locale,
                    region,
                    DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                );
                var responseMsg = await client.GetAsync(request);
                return await responseMsg.Content.ReadAsStringAsync();   
            }
        }
        

        /// <summary>
        /// Extracts image info from nested batch response JSON object.
        /// </summary>
        /// <param name="batchResponse">Batch response JSON object, ideally from GetbatchResponseAsync().</param>
        /// <returns>A ImageInfo containing info of landscape and portrait spotlight images.</returns>
        public static Task<ImageInfos> GetImageInfo(string batchResponse)
        {
            var batchParsed = JObject.Parse(batchResponse);
            var singleItem = batchParsed.SelectToken("batchrsp.items[0].item").ToString();

            var itemParsed = JObject.Parse(singleItem);
            return Task.FromResult(itemParsed.SelectToken("ad").ToObject<ImageInfos>());
        }

        /// <summary>
        /// Downloads a image and write it to working directory and return its filename.
        /// </summary>
        /// <param name="imageUrl">Image URL to download.</param>
        /// <returns>The file name of the downloaded image.</returns>
        public static async Task<string> WriteImage(string imageUrl, FileInfo[] files)
        {
            string imageName = imageUrl.Split('/').Last().Split('?')[0];
            using (var client = new HttpClient())
            {
                var imageResponse = await client.GetAsync(imageUrl);
                var res = await imageResponse.Content.ReadAsByteArrayAsync();
                
                string ImageSavePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";

                if (!Directory.Exists(ImageSavePath))
                {
                    Directory.CreateDirectory(ImageSavePath);
                }

                var names = files.Select(x => x.Name).ToList();
                if (!names.Contains($"spotlight{imageName}.jpg"))
                {
                    File.WriteAllBytes(ImageSavePath + "\\spotlight" + imageName + ".jpg", res); // Requires System.IO
                    string strSavePath = "spotlight" + imageName + ".jpg";
                    return strSavePath;
                }
            }

            return null;
        }

        #region ImageInfo object

        public class ImageInfos
        {
            private ImageInfo[] _infos = new ImageInfo[2];
            
            [JsonProperty("image_fullscreen_001_landscape")]
            public ImageInfo Landscape { get { return _infos[0]; } set { _infos[0] = value; } }
            [JsonProperty("image_fullscreen_001_portrait")]
            public ImageInfo Portrait { get { return _infos[1]; } set { _infos[1] = value; } }
        }

        public class ImageInfo
        {
            [JsonProperty("w")]
            public string Width { get; set; }
            [JsonProperty("h")]
            public string Height { get; set; }
            [JsonProperty("fileSize")]
            public string Size { get; set; }
            [JsonProperty("sha256")]
            public string Sha256 { get; set; }
            [JsonProperty("u")]
            public string Url { get; set; }
        }
        #endregion
    }
}