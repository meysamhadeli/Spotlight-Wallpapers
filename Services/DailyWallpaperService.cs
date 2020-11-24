using FluentScheduler;
using Microsoft.VisualBasic.Logging;

namespace SpotlightWallpaper.Services
{
    public class DailyWallpaperService
    {
        public void Start()
        {
            var registry = new Registry();
            registry.Schedule<DailyWallpaperServiceJob>().ToRunNow().AndEvery(1).Days();
            JobManager.Initialize(registry);
            JobManager.Start();
        }

        public void Stop()
        {
            JobManager.StopAndBlock();
            JobManager.RemoveAllJobs();
        }
    }
}