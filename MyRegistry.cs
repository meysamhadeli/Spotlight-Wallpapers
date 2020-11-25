using System;
using System.Threading.Tasks;
using FluentScheduler;

namespace SpotlightWallpaper
{
   
        public class MyRegistry : Registry
        {
            public MyRegistry()
            {
                Action someMethod = new Action(() =>
                {
                     Task.Run(async () =>
                    {
                        await SpotlightApi.GetSpotlightImage();
                        await BingApi.GetBingImage();
                    });

                });

                this.Schedule(someMethod).ToRunNow().AndEvery(1).Days();
            }
        }
    
}