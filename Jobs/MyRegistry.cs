using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentScheduler;
using SpotlightWallpaper.Services;
using System.Linq;
using Topshelf.Builders;

namespace SpotlightWallpaper.Jobs
{
    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            Action someMethod = new Action(() =>
            {
                Task.Run(async () =>
                {
                    var spotlightImage = await SpotlightApi.RunSpootlightJob();
                    var bingImage = await BingApi.GetBingImage();
                    await SetWallpapers(spotlightImage, bingImage);
                });
            });
            
            Schedule(someMethod).ToRunNow().AndEvery(3).Days();
        }

        private async Task SetWallpapers(string spotlightImage, string bingImage)
        {
            if (!string.IsNullOrWhiteSpace(spotlightImage))
            {
                await Task.Delay(10000);
                await SetWall(spotlightImage);
            }
            else if (bingImage != null)
            {
                await Task.Delay(10000);
                await SetWall(bingImage);
            }
        }

        private async Task SetWall(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
                await Win32.SetWallpaper(path);
        }


    }

}