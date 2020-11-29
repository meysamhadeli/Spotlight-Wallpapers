using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SpotlightWallpaper
{
    public class Win32
    {
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;
        
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
            int uAction,
            int uParam,
            string lpvParam,
            int fuWinIni
        );

        public static async Task SetWallpaper(string strSavePath)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, strSavePath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

        }
    }
}