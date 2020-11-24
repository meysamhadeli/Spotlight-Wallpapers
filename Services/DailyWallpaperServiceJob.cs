using FluentScheduler;

namespace SpotlightWallpaper.Services
{
    public class DailyWallpaperServiceJob: IJob
    {
        public async void Execute()
        {
            await BingApi.GetBingImage();
            await SpotlightApi.GetSpotlightImage();
        }
    }
}