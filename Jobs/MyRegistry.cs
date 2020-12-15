using System;
using System.Threading.Tasks;
using FluentScheduler;
using SpotlightWallpaper.Services;

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
                    await BingApi.GetBingImage(spotlightImage);
                });
            });

            this.Schedule(someMethod).ToRunNow().AndEvery(1).Days();
        }
    }
}