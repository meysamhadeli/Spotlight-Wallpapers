using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.MyServices;
using SpotlightWallpaper.My;
using SpotlightWallpaper.My.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotlightWallpaper
{
    public partial class Form1 : Form
    {
        int num = 0;
        private int bingNum = 0;
        private string current;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MyProject.Forms.AboutBox1.ShowDialog();
        }

        private async void checkNow_Click(object sender, EventArgs e)
        {
            await initSpotlight();
        }


        private async Task initSpotlight()
        {
            this.ListView1.SmallImageList = null;
            this.ListView1.LargeImageList = null;
            this.ImageList1.Images.Clear();
            this.ListView1.Items.Clear();
            string pathSmallImage = null;
            FileInfo[] files = null;
            num = 0;
            this.Info.Text = null;
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";

            DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets"));

            files = directoryInfo.GetFiles();
            if (files.Length > 0)
            {
                FileInfo[] fileInfoArray = files;
                for (int i = 0; i < checked((int) fileInfoArray.Length); i = checked(i + 1))
                {
                    FileInfo fileInfo = fileInfoArray[i];
                    try
                    {
                        Bitmap bitmap = new Bitmap(fileInfo.FullName);
                        if (bitmap.Width != 1)
                        {
                            if (bitmap.Width == 1920)
                            {
                                var imageName = $"{fileInfo.Name}.jpg";
                                this.ImageList1.Images.Add(this.GetThumbnail(bitmap));
                                this.ListView1.Items.Add(imageName, num);
                                num = checked(num + 1);
                                pathSmallImage = $"{patch}\\{imageName}";
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        ProjectData.SetProjectError(exception);
                        Console.WriteLine(exception.Message);
                        ProjectData.ClearProjectError();
                    }
                }

                if (!(num > 0))
                {
                    this.Info.Text = "No Wallpapers were found!";
                }
                else
                {
                    this.PictureBox1.ImageLocation = pathSmallImage;
                }

                this.ListView1.SmallImageList = this.ImageList1;
                this.ListView1.LargeImageList = this.ImageList1;
            }
        }

        private async Task getwallzSpotlight()
        {
            this.ListView1.SmallImageList = null;
            this.ListView1.LargeImageList = null;
            this.ImageList1.Images.Clear();
            this.ListView1.Items.Clear();
            this.Info.Text = null;
            FileInfo[] files = null;
            List<string> ext = new List<string> {".jpg", ".jpeg"};
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";

            DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets"));

            files = directoryInfo.GetFiles();

            if (files.Length > 0)
            {
                FileInfo[] fileInfoArray = files;
                for (int i = 0; i < checked((int) fileInfoArray.Length); i = checked(i + 1))
                {
                    FileInfo fileInfo = fileInfoArray[i];
                    try
                    {
                        Bitmap bitmap = new Bitmap(fileInfo.FullName);
                        if (bitmap.Width != 1)
                        {
                            if (bitmap.Width == 1920)
                            {
                                if (files.Any(x => x.Name == $"{fileInfo.Name}"))
                                    continue;
                                var imageName = $"{fileInfo.Name}";
                                this.ImageList1.Images.Add(this.GetThumbnail(bitmap));
                                this.ListView1.Items.Add(imageName, num);
                                num = checked(num + 1);
                                MyProject.Computer.FileSystem.CopyFile(fileInfo.FullName,
                                    $"{patch}\\{fileInfo.Name}.jpg", true);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        ProjectData.SetProjectError(exception);
                        Console.WriteLine(exception.Message);
                        ProjectData.ClearProjectError();
                    }
                }

                await initSpotlight();
                
                this.ListView1.SmallImageList = this.ImageList1;
                this.ListView1.LargeImageList = this.ImageList1;
            }
        }

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        public void setwall(string patc)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, patc, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            this.PictureBox1.ImageLocation = patc;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await this.initSpotlight();
        }

        private Image GetThumbnail(Bitmap image)
        {
            Form1 form1 = this;
            Image.GetThumbnailImageAbort getThumbnailImageAbort = new Image.GetThumbnailImageAbort(form1.ThumbCallback);
            double width = (double) ((double) image.Width / (double) image.Height);
            int num = 160;
            int num1 = 90;
            if (width > 1)
            {
                num = 160;
                num1 = checked((int) Math.Round(100 / width));
            }
            else if (width < 1)
            {
                num = checked((int) Math.Round(100 / width));
                num1 = 90;
            }

            return image.GetThumbnailImage(num, num1, getThumbnailImageAbort, new IntPtr());
        }

        private void setWallpaper_Click(object sender, EventArgs e)
        {
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";
            string text = this.ListView1.SelectedItems[0].Text;
            var patchWallpaper = $"{patch}\\{text}";
            this.setwall(patchWallpaper);
        }

        private void showFolder_Click(object sender, EventArgs e)
        {
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";
            Process.Start(patch);
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public bool ThumbCallback()
        {
            return true;
        }

        public async Task<string> GetSpotlightImage(FileInfo[] files)
        {
            try
            {
                var response = await SpotlighApi.GetSpotlightResponseAsync().ConfigureAwait(false);
                var images = await SpotlighApi.GetImageInfo(response).ConfigureAwait(false);
                return await SpotlighApi.WriteImage(images.Landscape.Url, files).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.Info.Text = "Server spotlight is filterd use DNS";
                return null;
            }
        }

        public async Task<string> GetBingImage(FileInfo[] files)
        {
            return await BingApi.WriteImage(files);
        }

        private async Task getwallzBing()
        {
            this.Info.Text = null;
            this.ImageList1.Images.Clear();
            this.ListView1.Items.Clear();
            this.ListView1.SmallImageList = null;
            this.ListView1.LargeImageList = null;
            bingNum = 0;

            FileInfo[] files = null;
            List<string> ext = new List<string> {".jpg", ".jpeg"};

            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing";
            files = new DirectoryInfo(patch).EnumerateFiles("*.*", SearchOption.AllDirectories)
                .Where(path => ext.Contains(Path.GetExtension(path.Name)))
                .Select(x => new FileInfo(x.FullName)).ToArray();

            var newImageName = await GetBingImage(files);

            files = new DirectoryInfo(patch).EnumerateFiles("*.*", SearchOption.AllDirectories)
                .Where(path => ext.Contains(Path.GetExtension(path.Name)))
                .Select(x => new FileInfo(x.FullName)).ToArray();

            if (files.Length > 0)
            {
                FileInfo[] fileInfoArray = files;
                for (int i = 0; i < checked((int) fileInfoArray.Length); i = checked(i + 1))
                {
                    FileInfo fileInfo = fileInfoArray[i];
                    try
                    {
                        Bitmap bitmap = new Bitmap(fileInfo.FullName);
                        if (bitmap.Width != 1)
                        {
                            if (bitmap.Width == 1920)
                            {
                                if (files.Any(x => x.Name == $"{fileInfo.Name}"))
                                    continue;
                                var imageName = $"{fileInfo.Name}";
                                this.ImageList1.Images.Add(this.GetThumbnail(bitmap));
                                this.ListView1.Items.Add(imageName, bingNum);
                                bingNum = checked(bingNum + 1);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        ProjectData.SetProjectError(exception);
                        Console.WriteLine(exception.Message);
                        ProjectData.ClearProjectError();
                    }
                }

                await initBing();
                
                this.ListView1.SmallImageList = this.ImageList1;
                this.ListView1.LargeImageList = this.ImageList1;
            }
        }

        private async Task initBing()
        {
            this.Info.Text = null;
            this.ImageList1.Images.Clear();
            this.ListView1.Items.Clear();
            this.ListView1.SmallImageList = null;
            this.ListView1.LargeImageList = null;
            string pathSmallImage = null;
            bingNum = 0;

            FileInfo[] files = null;
            List<string> ext = new List<string> {".jpg", ".jpeg"};

            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing";
            
            files = new DirectoryInfo(patch).EnumerateFiles("*.*", SearchOption.AllDirectories)
                .Where(path => ext.Contains(Path.GetExtension(path.Name)))
                .Select(x => new FileInfo(x.FullName)).ToArray();

            if (files.Length > 0)
            {
                FileInfo[] fileInfoArray = files;
                for (int i = 0; i < checked((int) fileInfoArray.Length); i = checked(i + 1))
                {
                    FileInfo fileInfo = fileInfoArray[i];
                    try
                    {
                        Bitmap bitmap = new Bitmap(fileInfo.FullName);
                        if (bitmap.Width != 1)
                        {
                            if (bitmap.Width == 1920)
                            {
                                var imageName = $"{fileInfo.Name}";
                                this.ImageList1.Images.Add(this.GetThumbnail(bitmap));
                                this.ListView1.Items.Add(imageName, bingNum);
                                bingNum = checked(bingNum + 1);
                                pathSmallImage = $"{patch}\\{fileInfo.Name}";
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        ProjectData.SetProjectError(exception);
                        Console.WriteLine(exception.Message);
                        ProjectData.ClearProjectError();
                    }
                }
                if (!(num > 0))
                {
                    this.Info.Text = "No Wallpapers were found!";
                }
                else
                {
                    this.PictureBox1.ImageLocation = pathSmallImage;
                }
                
                this.ListView1.SmallImageList = this.ImageList1;
                this.ListView1.LargeImageList = this.ImageList1;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await initBing();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await getwallzBing();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await getwallzSpotlight();
        }
    }
}