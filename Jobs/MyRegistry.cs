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
                    var bingResult = await BingApi.RunBingJob(); 
                    await SpotlightApi.RunSpootlightJob(bingResult);
                });
            });
            
            Schedule(someMethod).ToRunNow().AndEvery(6).Hours();
        }
        
        public async static Task SetWall(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
                await Win32.SetWallpaper(path);
        }


    }

}