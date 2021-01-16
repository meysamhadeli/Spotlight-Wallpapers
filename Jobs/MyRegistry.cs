using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentScheduler;
using SpotlightWallpaper.Services;
using System.Linq;

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
                    var spotlightImage = await SpotlightApi.GetSpotlightImage();
                    var bingImage = await BingApi.GetBingImage();
                    if (!string.IsNullOrWhiteSpace(spotlightImage))
                    {
                        await Task.Delay(10000);
                        await SetWall(spotlightImage);
                    }
                    else
                    {
                        await Task.Delay(10000);
                        await SetWall(bingImage);
                    }
                });
            });
            
            this.Schedule(someMethod).ToRunNow().AndEvery(1).Days();
        }

        private async Task SetWall(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
                await Win32.SetWallpaper(path);
        }
    }
}