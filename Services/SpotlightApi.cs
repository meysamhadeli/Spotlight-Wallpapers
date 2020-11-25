using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SpotlightWallpaper.My;
using SpotlightWallpaper.Settings;

namespace SpotlightWallpaper.Services
{
    public class SpotlightApi
    {
        public static async Task GetSpotlightImage()
        {
            List<string> ext = new List<string> {".jpg", ".jpeg"};
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";
            string pathSmallImage = null;
            DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets"));

            DirectoryInfo df = new DirectoryInfo(string.Concat(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Spotlight"));

            var files = directoryInfo.GetFiles();
            var savefiles = df.GetFiles();
            var reciveNewWallpapers = new List<string>();
            if (files.Length > 0)
            {
                foreach (var fileInfo in files)
                {
                    Bitmap bitmap = new Bitmap(fileInfo.FullName);
                    if (bitmap.Width != 1)
                    {
                        if (bitmap.Width == 1920 || bitmap.Width == 1900)
                        {
                            var info = fileInfo;
                            if (savefiles.Any(x => x.Name == $"{info.Name}.jpg"))
                                continue;
                            MyProject.Computer.FileSystem.CopyFile(fileInfo.FullName,
                                $"{patch}\\{fileInfo.Name}.jpg", true);
                            
                            reciveNewWallpapers.Add($"{patch}\\{fileInfo.Name}.jpg");
                        }
                    }
                }
            }

            if (reciveNewWallpapers.Any())
            {
                await Win32.SetWallpaper(reciveNewWallpapers.LastOrDefault());
            }
        }
    }
}