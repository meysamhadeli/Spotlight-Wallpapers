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
                        await BingApi.GetBingImage();
                        await SpotlightApi.GetSpotlightImage();
                    });

                });

                this.Schedule(someMethod).ToRunNow().AndEvery(1).Days();
            }
        }
    
}