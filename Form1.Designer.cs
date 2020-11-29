using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SpotlightWallpaper.My;
using SpotlightWallpaper.My.Resources;

namespace SpotlightWallpaper
{
	public partial class Form1
    {
	    /// <summary>
	    /// Required designer variable.
	    /// </summary>
	    private IContainer components = null;

	    /// <summary>
	    /// Clean up any resources being used.
	    /// </summary>
	    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	    protected override void Dispose(bool disposing)
	    {
		    if (disposing && (components != null))
		    {
			    components.Dispose();
		    }

		    base.Dispose(disposing);
	    }

	    #region Windows Form Designer generated code

	    /// <summary>
	    /// Required method for Designer support - do not modify
	    /// the contents of this method with the code editor.
	    /// </summary>
	    private void InitializeComponent()
	    {
		    this.components = new System.ComponentModel.Container();
		    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
		    this.checkNow = new System.Windows.Forms.Button();
		    this.ListView1 = new System.Windows.Forms.ListView();
		    this.optionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		    this.setWallpaper = new System.Windows.Forms.ToolStripMenuItem();
		    this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		    this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		    this.PictureBox1 = new System.Windows.Forms.PictureBox();
		    this.Label1 = new System.Windows.Forms.Label();
		    this.Label2 = new System.Windows.Forms.Label();
		    this.btnAbout = new System.Windows.Forms.Button();
		    this.button1 = new System.Windows.Forms.Button();
		    this.TotalImage = new System.Windows.Forms.Label();
		    this.Info = new System.Windows.Forms.Label();
		    this.button2 = new System.Windows.Forms.Button();
		    this.button3 = new System.Windows.Forms.Button();
		    this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		    this.activeWithStartupWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		    this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		    this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		    this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
		    this.button4 = new System.Windows.Forms.Button();
		    this.button5 = new System.Windows.Forms.Button();
		    this.optionsMenu.SuspendLayout();
		    ((System.ComponentModel.ISupportInitialize) (this.PictureBox1)).BeginInit();
		    this.contextMenuStrip1.SuspendLayout();
		    this.SuspendLayout();
		    // 
		    // checkNow
		    // 
		    this.checkNow.Location = new System.Drawing.Point(18, 93);
		    this.checkNow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		    this.checkNow.Name = "checkNow";
		    this.checkNow.Size = new System.Drawing.Size(175, 37);
		    this.checkNow.TabIndex = 1;
		    this.checkNow.Text = "Spotlight";
		    this.checkNow.UseVisualStyleBackColor = true;
		    this.checkNow.Click += new System.EventHandler(this.checkNow_Click);
		    // 
		    // ListView1
		    // 
		    this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		    this.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		    this.ListView1.ContextMenuStrip = this.optionsMenu;
		    this.ListView1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
		    this.ListView1.Location = new System.Drawing.Point(255, 93);
		    this.ListView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		    this.ListView1.Name = "ListView1";
		    this.ListView1.Size = new System.Drawing.Size(857, 560);
		    this.ListView1.TabIndex = 3;
		    this.ListView1.UseCompatibleStateImageBehavior = false;
		    // 
		    // optionsMenu
		    // 
		    this.optionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.setWallpaper, this.deleteToolStripMenuItem});
		    this.optionsMenu.Name = "optionsMenu";
		    this.optionsMenu.Size = new System.Drawing.Size(163, 48);
		    // 
		    // setWallpaper
		    // 
		    this.setWallpaper.Name = "setWallpaper";
		    this.setWallpaper.Size = new System.Drawing.Size(162, 22);
		    this.setWallpaper.Text = "Set As Wallpaper";
		    this.setWallpaper.Click += new System.EventHandler(this.setWallpaper_Click);
		    // 
		    // deleteToolStripMenuItem
		    // 
		    this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		    this.deleteToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
		    this.deleteToolStripMenuItem.Text = "Delete";
		    this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
		    // 
		    // ImageList1
		    // 
		    this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
		    this.ImageList1.ImageSize = new System.Drawing.Size(160, 90);
		    this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		    // 
		    // PictureBox1
		    // 
		    this.PictureBox1.Location = new System.Drawing.Point(20, 265);
		    this.PictureBox1.Name = "PictureBox1";
		    this.PictureBox1.Size = new System.Drawing.Size(230, 161);
		    this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		    this.PictureBox1.TabIndex = 6;
		    this.PictureBox1.TabStop = false;
		    // 
		    // Label1
		    // 
		    this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		    this.Label1.Location = new System.Drawing.Point(18, 240);
		    this.Label1.Name = "Label1";
		    this.Label1.Size = new System.Drawing.Size(230, 22);
		    this.Label1.TabIndex = 7;
		    this.Label1.Text = "Current Wallpaper";
		    this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		    // 
		    // Label2
		    // 
		    this.Label2.AutoSize = true;
		    this.Label2.ForeColor = System.Drawing.Color.IndianRed;
		    this.Label2.Location = new System.Drawing.Point(272, 53);
		    this.Label2.Name = "Label2";
		    this.Label2.Size = new System.Drawing.Size(0, 21);
		    this.Label2.TabIndex = 8;
		    // 
		    // btnAbout
		    // 
		    this.btnAbout.Location = new System.Drawing.Point(20, 441);
		    this.btnAbout.Name = "btnAbout";
		    this.btnAbout.Size = new System.Drawing.Size(230, 33);
		    this.btnAbout.TabIndex = 9;
		    this.btnAbout.Text = "About";
		    this.btnAbout.UseVisualStyleBackColor = true;
		    this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
		    // 
		    // button1
		    // 
		    this.button1.Location = new System.Drawing.Point(18, 180);
		    this.button1.Name = "button1";
		    this.button1.Size = new System.Drawing.Size(175, 36);
		    this.button1.TabIndex = 10;
		    this.button1.Text = "Bing";
		    this.button1.UseVisualStyleBackColor = true;
		    this.button1.Click += new System.EventHandler(this.button1_Click);
		    // 
		    // TotalImage
		    // 
		    this.TotalImage.ForeColor = System.Drawing.SystemColors.MenuHighlight;
		    this.TotalImage.Location = new System.Drawing.Point(255, 30);
		    this.TotalImage.Name = "TotalImage";
		    this.TotalImage.Size = new System.Drawing.Size(305, 23);
		    this.TotalImage.TabIndex = 11;
		    // 
		    // Info
		    // 
		    this.Info.ForeColor = System.Drawing.Color.Red;
		    this.Info.Location = new System.Drawing.Point(255, 65);
		    this.Info.Name = "Info";
		    this.Info.Size = new System.Drawing.Size(305, 23);
		    this.Info.TabIndex = 12;
		    // 
		    // button2
		    // 
		    this.button2.Location = new System.Drawing.Point(199, 180);
		    this.button2.Name = "button2";
		    this.button2.Size = new System.Drawing.Size(49, 36);
		    this.button2.TabIndex = 13;
		    this.button2.Text = "1+";
		    this.button2.UseVisualStyleBackColor = true;
		    this.button2.Click += new System.EventHandler(this.button2_Click);
		    // 
		    // button3
		    // 
		    this.button3.Location = new System.Drawing.Point(199, 93);
		    this.button3.Name = "button3";
		    this.button3.Size = new System.Drawing.Size(49, 37);
		    this.button3.TabIndex = 14;
		    this.button3.Text = "10+";
		    this.button3.UseVisualStyleBackColor = true;
		    this.button3.Click += new System.EventHandler(this.button3_Click);
		    // 
		    // contextMenuStrip1
		    // 
		    this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.activeWithStartupWindowsToolStripMenuItem, this.showToolStripMenuItem, this.exitToolStripMenuItem});
		    this.contextMenuStrip1.Name = "contextMenuStrip1";
		    this.contextMenuStrip1.Size = new System.Drawing.Size(227, 70);
		    // 
		    // activeWithStartupWindowsToolStripMenuItem
		    // 
		    this.activeWithStartupWindowsToolStripMenuItem.Name = "activeWithStartupWindowsToolStripMenuItem";
		    this.activeWithStartupWindowsToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
		    this.activeWithStartupWindowsToolStripMenuItem.Text = "Active with Startup Windows";
		    this.activeWithStartupWindowsToolStripMenuItem.Click += new System.EventHandler(this.activeWithStartupWindowsToolStripMenuItem_Click);
		    // 
		    // showToolStripMenuItem
		    // 
		    this.showToolStripMenuItem.Name = "showToolStripMenuItem";
		    this.showToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
		    this.showToolStripMenuItem.Text = "Show";
		    this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
		    // 
		    // exitToolStripMenuItem
		    // 
		    this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
		    this.exitToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
		    this.exitToolStripMenuItem.Text = "Exit";
		    this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
		    // 
		    // notifyIcon1
		    // 
		    this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
		    this.notifyIcon1.Icon = ((System.Drawing.Icon) (resources.GetObject("notifyIcon1.Icon")));
		    this.notifyIcon1.Text = "Settings";
		    this.notifyIcon1.Visible = true;
		    this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
		    // 
		    // button4
		    // 
		    this.button4.Location = new System.Drawing.Point(18, 139);
		    this.button4.Name = "button4";
		    this.button4.Size = new System.Drawing.Size(175, 35);
		    this.button4.TabIndex = 15;
		    this.button4.Text = "UnSplash";
		    this.button4.UseVisualStyleBackColor = true;
		    this.button4.Click += new System.EventHandler(this.unSolash_Click);
		    // 
		    // button5
		    // 
		    this.button5.Location = new System.Drawing.Point(199, 139);
		    this.button5.Name = "button5";
		    this.button5.Size = new System.Drawing.Size(49, 35);
		    this.button5.TabIndex = 16;
		    this.button5.Text = "10+";
		    this.button5.UseVisualStyleBackColor = true;
		    this.button5.Click += new System.EventHandler(this.addUnSplash_Click);
		    // 
		    // Form1
		    // 
		    this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
		    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		    this.ClientSize = new System.Drawing.Size(1117, 657);
		    this.Controls.Add(this.button5);
		    this.Controls.Add(this.button4);
		    this.Controls.Add(this.button3);
		    this.Controls.Add(this.button2);
		    this.Controls.Add(this.Info);
		    this.Controls.Add(this.TotalImage);
		    this.Controls.Add(this.button1);
		    this.Controls.Add(this.btnAbout);
		    this.Controls.Add(this.Label2);
		    this.Controls.Add(this.Label1);
		    this.Controls.Add(this.PictureBox1);
		    this.Controls.Add(this.ListView1);
		    this.Controls.Add(this.checkNow);
		    this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
		    this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
		    this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		    this.Name = "Form1";
		    this.Text = "Windows Spotlight Wallpaper";
		    this.Load += new System.EventHandler(this.Form1_Load);
		    this.Move += new System.EventHandler(this.Form1_Move);
		    this.optionsMenu.ResumeLayout(false);
		    ((System.ComponentModel.ISupportInitialize) (this.PictureBox1)).EndInit();
		    this.contextMenuStrip1.ResumeLayout(false);
		    this.ResumeLayout(false);
		    this.PerformLayout();
	    }

	    private System.Windows.Forms.Button button5;

	    private System.Windows.Forms.Button button4;

	    private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;

	    private System.Windows.Forms.ToolStripMenuItem activeWithStartupWindowsToolStripMenuItem;

	    private System.Windows.Forms.NotifyIcon notifyIcon1;

	    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

	    private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;

	    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

	    private System.Windows.Forms.Button button3;

	    private System.Windows.Forms.Button button2;

	    private System.Windows.Forms.Label Info;

	    private System.Windows.Forms.Label TotalImage;

	    private System.Windows.Forms.Button button1;

	    private const int SETDESKWALLPAPER = 20;

	    private const int UPDATEINIFILE = 1;

	    private  Button btnAbout;

	    private  Button checkNow;

	    private ImageList ImageList1;

	    private  Label Label1;

	    private  Label Label2;

	    private ListView ListView1;

	    private System.Windows.Forms.ContextMenuStrip optionsMenu;

	    private  PictureBox PictureBox1;

	    private ToolStripMenuItem setWallpaper;

	    #endregion
	   
    }
}