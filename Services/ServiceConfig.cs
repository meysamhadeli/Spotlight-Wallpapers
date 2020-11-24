
using System.Threading.Tasks;
using Topshelf;

namespace SpotlightWallpaper.Services
{
    public static class ServiceConfig
    {
        public static async Task Run()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<DailyWallpaperService>(s =>
                {
                    s.ConstructUsing(_ => new DailyWallpaperService());
                    s.WhenStarted(p => p.Start());
                    s.WhenStopped(p => p.Stop());
                });

                configure.SetServiceName("Daily wallpaper changer");
                configure.SetDisplayName("Daily wallpaper changer");
            });
        }
    }
}