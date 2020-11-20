using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using SpotlightWallpaper.My;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace SpotlightWallpaper
{
	[DesignerGenerated]
	public sealed class AboutBox1 : Form
	{
		private static List<WeakReference> __ENCList;

		[AccessedThroughProperty("TableLayoutPanel")]
		private System.Windows.Forms.TableLayoutPanel _TableLayoutPanel;

		[AccessedThroughProperty("LogoPictureBox")]
		private PictureBox _LogoPictureBox;

		[AccessedThroughProperty("LabelProductName")]
		private Label _LabelProductName;

		[AccessedThroughProperty("LabelVersion")]
		private Label _LabelVersion;

		[AccessedThroughProperty("LabelCompanyName")]
		private Label _LabelCompanyName;

		[AccessedThroughProperty("TextBoxDescription")]
		private TextBox _TextBoxDescription;

		[AccessedThroughProperty("OKButton")]
		private Button _OKButton;

		[AccessedThroughProperty("LabelCopyright")]
		private Label _LabelCopyright;

		private IContainer components;

		internal Label LabelCompanyName
		{
			[DebuggerNonUserCode]
			get
			{
				return this._LabelCompanyName;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._LabelCompanyName = value;
			}
		}

		internal Label LabelCopyright
		{
			[DebuggerNonUserCode]
			get
			{
				return this._LabelCopyright;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._LabelCopyright = value;
			}
		}

		internal Label LabelProductName
		{
			[DebuggerNonUserCode]
			get
			{
				return this._LabelProductName;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._LabelProductName = value;
			}
		}

		internal Label LabelVersion
		{
			[DebuggerNonUserCode]
			get
			{
				return this._LabelVersion;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._LabelVersion = value;
			}
		}

		internal PictureBox LogoPictureBox
		{
			[DebuggerNonUserCode]
			get
			{
				return this._LogoPictureBox;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._LogoPictureBox = value;
			}
		}

		internal Button OKButton
		{
			[DebuggerNonUserCode]
			get
			{
				return this._OKButton;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				AboutBox1 aboutBox1 = this;
				EventHandler eventHandler = new EventHandler(aboutBox1.OKButton_Click);
				if (this._OKButton != null)
				{
					this._OKButton.Click -= eventHandler;
				}
				this._OKButton = value;
				if (this._OKButton != null)
				{
					this._OKButton.Click += eventHandler;
				}
			}
		}

		internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel
		{
			[DebuggerNonUserCode]
			get
			{
				return this._TableLayoutPanel;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._TableLayoutPanel = value;
			}
		}

		internal TextBox TextBoxDescription
		{
			[DebuggerNonUserCode]
			get
			{
				return this._TextBoxDescription;
			}
			[DebuggerNonUserCode]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._TextBoxDescription = value;
			}
		}

		[DebuggerNonUserCode]
		static AboutBox1()
		{
			AboutBox1.__ENCList = new List<WeakReference>();
		}

		[DebuggerNonUserCode]
		public AboutBox1()
		{
			AboutBox1 aboutBox1 = this;
			base.Load += new EventHandler(aboutBox1.AboutBox1_Load);
			AboutBox1.__ENCAddToList(this);
			this.InitializeComponent();
		}

		[DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			lock (AboutBox1.__ENCList)
			{
				if (AboutBox1.__ENCList.Count == AboutBox1.__ENCList.Capacity)
				{
					int item = 0;
					int count = checked(AboutBox1.__ENCList.Count - 1);
					for (int i = 0; i <= count; i = checked(i + 1))
					{
						if (AboutBox1.__ENCList[i].IsAlive)
						{
							if (i != item)
							{
								AboutBox1.__ENCList[item] = AboutBox1.__ENCList[i];
							}
							item = checked(item + 1);
						}
					}
					AboutBox1.__ENCList.RemoveRange(item, checked(AboutBox1.__ENCList.Count - item));
					AboutBox1.__ENCList.Capacity = AboutBox1.__ENCList.Count;
				}
				AboutBox1.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void AboutBox1_Load(object sender, EventArgs e)
		{
			string str;
			str = (Operators.CompareString(MyProject.Application.Info.Title, "", false) == 0 ? Path.GetFileNameWithoutExtension(MyProject.Application.Info.AssemblyName) : MyProject.Application.Info.Title);
			this.Text = string.Format("About {0}", str);
			this.LabelProductName.Text = MyProject.Application.Info.ProductName;
			this.LabelVersion.Text = string.Format("Version {0}", MyProject.Application.Info.Version.ToString());
			this.LabelCopyright.Text = MyProject.Application.Info.Copyright;
			this.LabelCompanyName.Text = MyProject.Application.Info.CompanyName;
			this.TextBoxDescription.Text = MyProject.Application.Info.Description;
		}

		[DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if ((!disposing || this.components == null ? false : true))
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(AboutBox1));
			this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.LogoPictureBox = new PictureBox();
			this.LabelProductName = new Label();
			this.LabelVersion = new Label();
			this.LabelCopyright = new Label();
			this.LabelCompanyName = new Label();
			this.TextBoxDescription = new TextBox();
			this.OKButton = new Button();
			this.TableLayoutPanel.SuspendLayout();
			((ISupportInitialize)this.LogoPictureBox).BeginInit();
			this.SuspendLayout();
			this.TableLayoutPanel.ColumnCount = 2;
			this.TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33f));
			this.TableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67f));
			this.TableLayoutPanel.Controls.Add(this.LogoPictureBox, 0, 0);
			this.TableLayoutPanel.Controls.Add(this.LabelProductName, 1, 0);
			this.TableLayoutPanel.Controls.Add(this.LabelVersion, 1, 1);
			this.TableLayoutPanel.Controls.Add(this.LabelCopyright, 1, 2);
			this.TableLayoutPanel.Controls.Add(this.LabelCompanyName, 1, 3);
			this.TableLayoutPanel.Controls.Add(this.TextBoxDescription, 1, 4);
			this.TableLayoutPanel.Controls.Add(this.OKButton, 1, 5);
			this.TableLayoutPanel.Dock = DockStyle.Fill;
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel = this.TableLayoutPanel;
			Point point = new Point(9, 9);
			tableLayoutPanel.Location = point;
			this.TableLayoutPanel.Name = "TableLayoutPanel";
			this.TableLayoutPanel.RowCount = 6;
			this.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel1 = this.TableLayoutPanel;
			System.Drawing.Size size = new System.Drawing.Size(396, 258);
			tableLayoutPanel1.Size = size;
			this.TableLayoutPanel.TabIndex = 0;
			this.LogoPictureBox.Dock = DockStyle.Fill;
			this.LogoPictureBox.Image = (Image)componentResourceManager.GetObject("LogoPictureBox.Image");
			PictureBox logoPictureBox = this.LogoPictureBox;
			point = new Point(3, 3);
			logoPictureBox.Location = point;
			this.LogoPictureBox.Name = "LogoPictureBox";
			this.TableLayoutPanel.SetRowSpan(this.LogoPictureBox, 6);
			PictureBox pictureBox = this.LogoPictureBox;
			size = new System.Drawing.Size(124, 252);
			pictureBox.Size = size;
			this.LogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.LogoPictureBox.TabIndex = 0;
			this.LogoPictureBox.TabStop = false;
			this.LabelProductName.Dock = DockStyle.Fill;
			Label labelProductName = this.LabelProductName;
			point = new Point(136, 0);
			labelProductName.Location = point;
			Label label = this.LabelProductName;
			System.Windows.Forms.Padding padding = new System.Windows.Forms.Padding(6, 0, 3, 0);
			label.Margin = padding;
			Label labelProductName1 = this.LabelProductName;
			size = new System.Drawing.Size(0, 17);
			labelProductName1.MaximumSize = size;
			this.LabelProductName.Name = "LabelProductName";
			Label label1 = this.LabelProductName;
			size = new System.Drawing.Size(257, 17);
			label1.Size = size;
			this.LabelProductName.TabIndex = 0;
			this.LabelProductName.Text = "Product Name";
			this.LabelProductName.TextAlign = ContentAlignment.MiddleLeft;
			this.LabelVersion.Dock = DockStyle.Fill;
			Label labelVersion = this.LabelVersion;
			point = new Point(136, 25);
			labelVersion.Location = point;
			Label labelVersion1 = this.LabelVersion;
			padding = new System.Windows.Forms.Padding(6, 0, 3, 0);
			labelVersion1.Margin = padding;
			Label labelVersion2 = this.LabelVersion;
			size = new System.Drawing.Size(0, 17);
			labelVersion2.MaximumSize = size;
			this.LabelVersion.Name = "LabelVersion";
			Label label2 = this.LabelVersion;
			size = new System.Drawing.Size(257, 17);
			label2.Size = size;
			this.LabelVersion.TabIndex = 0;
			this.LabelVersion.Text = "Version";
			this.LabelVersion.TextAlign = ContentAlignment.MiddleLeft;
			this.LabelCopyright.Dock = DockStyle.Fill;
			Label labelCopyright = this.LabelCopyright;
			point = new Point(136, 50);
			labelCopyright.Location = point;
			Label labelCopyright1 = this.LabelCopyright;
			padding = new System.Windows.Forms.Padding(6, 0, 3, 0);
			labelCopyright1.Margin = padding;
			Label labelCopyright2 = this.LabelCopyright;
			size = new System.Drawing.Size(0, 17);
			labelCopyright2.MaximumSize = size;
			this.LabelCopyright.Name = "LabelCopyright";
			Label labelCopyright3 = this.LabelCopyright;
			size = new System.Drawing.Size(257, 17);
			labelCopyright3.Size = size;
			this.LabelCopyright.TabIndex = 0;
			this.LabelCopyright.Text = "Copyright";
			this.LabelCopyright.TextAlign = ContentAlignment.MiddleLeft;
			this.LabelCompanyName.Dock = DockStyle.Fill;
			Label labelCompanyName = this.LabelCompanyName;
			point = new Point(136, 75);
			labelCompanyName.Location = point;
			Label labelCompanyName1 = this.LabelCompanyName;
			padding = new System.Windows.Forms.Padding(6, 0, 3, 0);
			labelCompanyName1.Margin = padding;
			Label labelCompanyName2 = this.LabelCompanyName;
			size = new System.Drawing.Size(0, 17);
			labelCompanyName2.MaximumSize = size;
			this.LabelCompanyName.Name = "LabelCompanyName";
			Label labelCompanyName3 = this.LabelCompanyName;
			size = new System.Drawing.Size(257, 17);
			labelCompanyName3.Size = size;
			this.LabelCompanyName.TabIndex = 0;
			this.LabelCompanyName.Text = "Company Name";
			this.LabelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
			this.TextBoxDescription.Dock = DockStyle.Fill;
			TextBox textBoxDescription = this.TextBoxDescription;
			point = new Point(136, 103);
			textBoxDescription.Location = point;
			TextBox textBox = this.TextBoxDescription;
			padding = new System.Windows.Forms.Padding(6, 3, 3, 3);
			textBox.Margin = padding;
			this.TextBoxDescription.Multiline = true;
			this.TextBoxDescription.Name = "TextBoxDescription";
			this.TextBoxDescription.ReadOnly = true;
			this.TextBoxDescription.ScrollBars = ScrollBars.Both;
			TextBox textBoxDescription1 = this.TextBoxDescription;
			size = new System.Drawing.Size(257, 123);
			textBoxDescription1.Size = size;
			this.TextBoxDescription.TabIndex = 0;
			this.TextBoxDescription.TabStop = false;
			this.TextBoxDescription.Text = componentResourceManager.GetString("TextBoxDescription.Text");
			this.OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Button oKButton = this.OKButton;
			point = new Point(318, 232);
			oKButton.Location = point;
			this.OKButton.Name = "OKButton";
			Button button = this.OKButton;
			size = new System.Drawing.Size(75, 23);
			button.Size = size;
			this.OKButton.TabIndex = 0;
			this.OKButton.Text = "&OK";
			this.AutoScaleDimensions = new SizeF(6f, 13f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.OKButton;
			size = new System.Drawing.Size(414, 276);
			this.ClientSize = size;
			this.Controls.Add(this.TableLayoutPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutBox1";
			padding = new System.Windows.Forms.Padding(9);
			this.Padding = padding;
			this.ShowInTaskbar = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "AboutBox1";
			this.TableLayoutPanel.ResumeLayout(false);
			this.TableLayoutPanel.PerformLayout();
			((ISupportInitialize)this.LogoPictureBox).EndInit();
			this.ResumeLayout(false);
		}

		private void OKButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}