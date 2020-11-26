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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentScheduler;
using PixabaySharp;
using PixabaySharp.Utility;
using SpotlightWallpaper.Enum;
using SpotlightWallpaper.Jobs;
using SpotlightWallpaper.Services;
using SpotlightWallpaper.Settings;
using Registry = Microsoft.Win32.Registry;

namespace SpotlightWallpaper
{
    public partial class Form1 : Form
    {
        int num = 0;
        private UnSplash unsplash = null;
        private int bingNum = 0;
        private int unSplashNum = 0;
        private string current;
        private CheckApi checkApi;

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


        private async Task initUnSplash()
        {
            checkApi = CheckApi.UnSplash;
            this.ListView1.LargeImageList = null;
            this.ImageList1.Images.Clear();
            this.ListView1.Items.Clear();
            string pathSmallImage = null;
            FileInfo[] files = null;
            unSplashNum = 0;
            this.Info.Text = null;
            try
            {
                string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\UnSplash";

                DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\UnSplash"));

                if (!Directory.Exists(patch))
                {
                    Directory.CreateDirectory(patch);
                }

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
                                    var imageName = $"{fileInfo.Name}";
                                    this.ImageList1.Images.Add(this.GetThumbnail(bitmap));
                                    this.ListView1.Items.Add(imageName, unSplashNum);
                                    unSplashNum = checked(unSplashNum + 1);
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

                    if (!(unSplashNum > 0))
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
            catch (Exception e)
            {
                this.Info.Text = "No response was received from the server";
            }
        }


        private async Task getwallzUnSplash()
        {
            checkApi = CheckApi.UnSplash;
            this.Info.Text = null;
            this.ImageList1.Images.Clear();
            this.ListView1.Items.Clear();
            this.ListView1.SmallImageList = null;
            this.ListView1.LargeImageList = null;
            unSplashNum = 0;

            List<string> ext = new List<string> {".jpg", ".jpeg"};
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\UnSplash";
            try
            {
                for (int j = 0; j < 9; j++)
                {
                    var newImageNames = await UnSplash.GetUnSplashImage();

                    var files = new DirectoryInfo(patch).EnumerateFiles("*.*", SearchOption.AllDirectories)
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
                                        this.ListView1.Items.Add(imageName, unSplashNum);
                                        unSplashNum = checked(unSplashNum + 1);
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
                        await initUnSplash();
                        
                        this.ListView1.SmallImageList = this.ImageList1;
                        this.ListView1.LargeImageList = this.ImageList1;
                    }
                }
            }
            catch (Exception e)
            {
                this.Info.Text = "No response was received from the server";
            }
        }

        void load()
        {
            for (int i = 0; i <= 500; i++)
            {
                Thread.Sleep(10);
            }
        }

        private async Task initSpotlight()
        {
            // using (WaitingForm w = new WaitingForm(load))
            // {
            //     w.ShowDialog();
                checkApi = CheckApi.Spotlight;
                this.ListView1.LargeImageList = null;
                this.ImageList1.Images.Clear();
                this.ListView1.Items.Clear();
                string pathSmallImage = null;
                FileInfo[] files = null;
                num = 0;
                this.Info.Text = null;
                string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";

                DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Spotlight"));

                if (!Directory.Exists(patch))
                {
                    Directory.CreateDirectory(patch);
                }

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
                                    var imageName = $"{fileInfo.Name}";
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
                // }
                // w.Hide();
            }
        }

        private async Task getwallzSpotlight()
        {
            checkApi = CheckApi.Spotlight;
            this.ListView1.SmallImageList = null;
            this.ListView1.LargeImageList = null;
            this.ImageList1.Images.Clear();
            this.ListView1.Items.Clear();
            this.Info.Text = null;
            FileInfo[] files = null;
            List<string> ext = new List<string> {".jpg", ".jpeg"};
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";
            string pathSmallImage = null;
            DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets"));

            DirectoryInfo df = new DirectoryInfo(string.Concat(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Spotlight"));

            var savefiles = df.GetFiles();

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
                            if (bitmap.Width == 1920 || bitmap.Width == 1900)
                            {
                                if (savefiles.Any(x => x.Name == $"{fileInfo.Name}.jpg"))
                                    continue;
                                var imageName = $"{fileInfo.Name}";
                                this.ImageList1.Images.Add(this.GetThumbnail(bitmap));
                                this.ListView1.Items.Add(imageName, num);
                                num = checked(num + 1);
                                MyProject.Computer.FileSystem.CopyFile(fileInfo.FullName,
                                    $"{patch}\\{fileInfo.Name}.jpg", true);
                                pathSmallImage = $"{patch}\\{fileInfo.Name}.jpg";
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

                this.PictureBox1.ImageLocation = pathSmallImage;


                await initSpotlight();

                this.ListView1.SmallImageList = this.ImageList1;
                this.ListView1.LargeImageList = this.ImageList1;
            }
        }

        public async Task setwall(string patc)
        {
            await Win32.SetWallpaper(patc);
            this.PictureBox1.ImageLocation = patc;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            JobManager.Initialize(new MyRegistry());
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
            string patch = null;
            if (checkApi == CheckApi.Bing)
            {
                patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing";
            }

            if (checkApi == CheckApi.Spotlight)
            {
                patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";
            }

            if (checkApi == CheckApi.UnSplash)
            {
                patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\UnSplash";
            }

            string text = this.ListView1.SelectedItems[0].Text;
            var patchWallpaper = $"{patch}\\{text}";
            this.setwall(patchWallpaper);
        }

        private void showFolder_Click(object sender, EventArgs e)
        {
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";
            Process.Start(patch);
        }


        public bool ThumbCallback()
        {
            return true;
        }


        public async Task<string> GetBingImage()
        {
            return await BingApi.GetBingImage();
        }

        private async Task getwallzBing()
        {
            checkApi = CheckApi.Bing;
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
            try
            {
                var newImageName = await GetBingImage();

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

                    if (!(num > 0))
                    {
                        this.Info.Text = "No Wallpapers were found!";
                    }

                    await initBing();

                    this.ListView1.SmallImageList = this.ImageList1;
                    this.ListView1.LargeImageList = this.ImageList1;
                }
            }
            catch (Exception e)
            {
                this.Info.Text = "No response was received from the server";
            }
        }

        private async Task initBing()
        {
            checkApi = CheckApi.Bing;
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

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TopMost = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }


        private void activeWithStartupWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("SpotlightWallpaper", Application.ExecutablePath.ToString());
            notifyIcon1.ShowBalloonTip(1000, "Start With StartUp Windows", "Activated", ToolTipIcon.Info);
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            string patch = null;

                if (checkApi == CheckApi.Bing)
                {
                    patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing";
                }

                if (checkApi == CheckApi.Spotlight)
                {
                    patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";
                }
                
                if (checkApi == CheckApi.UnSplash)
                {
                    patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\UnSplash";
                }

                string text = this.ListView1.SelectedItems[0].Text;
                var patchWallpaper = $"{patch}\\{text}";

                if (File.Exists(patchWallpaper))
                {
                    if (checkApi == CheckApi.Spotlight)
                    {
                        GC.Collect(); 
                        GC.WaitForPendingFinalizers(); 
                        File.Delete(patchWallpaper);
                        await initSpotlight();
                    }
                    else if (checkApi == CheckApi.Bing)
                    {
                        GC.Collect(); 
                        GC.WaitForPendingFinalizers(); 
                        File.Delete(patchWallpaper);
                        await initBing();
                    }
                    else
                    {
                        GC.Collect(); 
                        GC.WaitForPendingFinalizers(); 
                        File.Delete(patchWallpaper);
                        await initUnSplash();
                    }
                }
        }

        private async void unSolash_Click(object sender, EventArgs e)
        {
            await this.initUnSplash();
        }

        private async void addUnSplash_Click(object sender, EventArgs e)
        {
            await this.getwallzUnSplash();
        }
    }
}