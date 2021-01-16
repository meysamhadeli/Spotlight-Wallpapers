using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentScheduler;
using Microsoft.Win32;
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
        private bool killProcess = false; 

        public Form1()
        {
            // get current process
            Process current = Process.GetCurrentProcess();
            // get all the processes with currnent process name
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                //Ignore the current process  
                if (process.Id != current.Id)
                {
                    killProcess = true;
                    process.Kill();
                }
            }
            
            InitializeComponent();
            if (!killProcess)
            {
                JobManager.Initialize(new MyRegistry());
            }
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
            List<FileInfo> files = new List<FileInfo>();
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

                files = directoryInfo.GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();
                if (files.Any())
                {
                    List<FileInfo> fileInfoArray = files;
                    for (int i = 0; i < checked(fileInfoArray.Count()); i = checked(i + 1))
                    {
                        FileInfo fileInfo = fileInfoArray[i];
                        try
                        {
                            Bitmap bitmap = new Bitmap(fileInfo.FullName);
                            if (bitmap.Width != 1)
                            {
                                if (bitmap.Width > 1000)
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

                    this.PictureBox1.ImageLocation = pathSmallImage;
                    

                    this.ListView1.SmallImageList = this.ImageList1;
                    this.ListView1.LargeImageList = this.ImageList1;
                }
            }
            catch (Exception e)
            {
                this.Info.Text = "No response was received from the server unsplash";
            }
        }


        private async Task getwallzUnSplash()
        {
            label4.Text = $"downloading...";
            checkApi = CheckApi.UnSplash;
            this.Info.Text = string.Empty;
            int downloadImageNum = 0;

            try
            {
                var newImageNames = await UnSplash.GetUnSplashImage();

                    if (newImageNames != null)
                    {
                        label4.Text = string.Empty;
                    }
                    else
                    {
                        this.Info.Text = "No response was received from the server unsplash";
                        label4.Text = string.Empty;
                    }
                
                await initUnSplash();
            }
            catch (Exception e)
            {
                this.Info.Text = "No response was received from the server unsplash";
                label4.Text = String.Empty;
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
            List<FileInfo> files = new List<FileInfo>();
            num = 0;
            this.Info.Text = null;
            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Spotlight";

            DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "\\Spotlight"));

            if (!Directory.Exists(patch))
            {
                Directory.CreateDirectory(patch);
            }

            files  = directoryInfo.GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();
            if (files.Any())
            {
                List<FileInfo> fileInfoArray = files;
                for (int i = 0; i < checked(fileInfoArray.Count()); i = checked(i + 1))
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
                
                this.PictureBox1.ImageLocation = pathSmallImage;

                this.ListView1.SmallImageList = this.ImageList1;
                this.ListView1.LargeImageList = this.ImageList1;
                // }
                // w.Hide();
            }
        }

        private async Task getwallzSpotlight()
        {
            label5.Text = $"downloading...";
            checkApi = CheckApi.Spotlight;
            int downloadImageNum = 0;
            this.Info.Text = string.Empty;

            try
            {
             
                    var response = await SpotlightApi.GetBatchResponseAsync();
                    var images = await SpotlightApi.GetImageInfo(response);
                    var imageName = await SpotlightApi.WriteImage(images.Landscape.Url);

                    if (imageName != null)
                    {
                        label5.Text = string.Empty;
                    }
                
                await Task.Delay(1000);
                label5.Text = String.Empty;
                await initSpotlight();
            }
            catch (Exception e)
            {
                this.Info.Text = "No response was received from the server spotlight";
                label5.Text = string.Empty;
            }
        }

        public async Task setwall(string patc)
        {
            await Win32.SetWallpaper(patc);
            this.PictureBox1.ImageLocation = patc;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            notifyIcon1.ShowBalloonTip(1000, "Spotlight Wallpapers", "App is running minimized to try." +'\n'+
                                                                     "to exit right click and select exit.", ToolTipIcon.Info);
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
            label3.Text = $"downloading...";
            checkApi = CheckApi.Bing;
            int downloadImageNum = 0;
            this.Info.Text = string.Empty;

            try
            {
                var newImageName = await GetBingImage();

                if (newImageName != null)
                {
                    label3.Text = $"downloading image bing... {downloadImageNum}";
                    await Task.Delay(1000);
                    label3.Text = string.Empty;
                }
                await Task.Delay(1000);
                label3.Text = String.Empty;
                await initBing();
            }
            catch (Exception e)
            {
                this.Info.Text = "No response was received from the server bing";
                label3.Text = string.Empty;
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

            List<FileInfo> files = new List<FileInfo>();
            List<string> ext = new List<string> {".jpg", ".jpeg"};

            string patch = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Bing";
            

            if (!Directory.Exists(patch))
            {
                Directory.CreateDirectory(patch);
            }
            

            files = new DirectoryInfo(patch).EnumerateFiles("*.*", SearchOption.AllDirectories)
                .Where(path => ext.Contains(Path.GetExtension(path.Name)))
                .Select(x => new FileInfo(x.FullName)).OrderByDescending(f => f.LastWriteTime).ToList();

            if (files.Any())
            {
                List<FileInfo> fileInfoArray = files;
                for (int i = 0; i < checked(fileInfoArray.Count()); i = checked(i + 1))
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

                this.PictureBox1.ImageLocation = pathSmallImage;

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

        private async void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // TopMost = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            await this.initBing();
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

        private void deAcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.DeleteValue("SpotlightWallpaper",false);
            notifyIcon1.ShowBalloonTip(1000, "DeActivated Start With StartUp Windows", "DeActivated", ToolTipIcon.Info);
        }
    }
}