// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-11-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="AboutDialog.cs" company="Zeroit Dev Technologies">
//    This program is for creating a Bar Chart control.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Zeroit.Framework.BasicCharts
{

    #region AboutDialog

    #region Control
    public partial class AboutDialog : Form
    {
        /// <summary>
        /// Gets or sets the website link.
        /// </summary>
        /// <value>The website link.</value>
        public string WebsiteLink { get; set; }
        /// <summary>
        /// Gets or sets the facebook link.
        /// </summary>
        /// <value>The facebook link.</value>
        public string FacebookLink { get; set; }
        /// <summary>
        /// Gets or sets the git hub link.
        /// </summary>
        /// <value>The git hub link.</value>
        public string GitHubLink { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutDialog"/> class.
        /// </summary>
        /// <param name="appName">Name of the application.</param>
        public AboutDialog(string appName)
        {
            InitializeComponent();
            Text += " " + appName;
            lbVersion.Text = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            //AssemblyName[] assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            //foreach (AssemblyName assembly in assemblyNames)
            //{
            //    TbComponents.Rows.Add(new string[] { assembly.Name, assembly.Version.ToString() });
            //}
            //TbComponents.Sort(TbComponents.Columns[0], ListSortDirection.Ascending);
        }

        /// <summary>
        /// Icons the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void IconClick(object sender, EventArgs e)
        {
            if (sender == bloggerIcon) Process.Start(WebsiteLink);
            else if (sender == facebookIcon) Process.Start(FacebookLink);
            else if (sender == githubIcon) Process.Start(GitHubLink);
        }

        /// <summary>
        /// Handles the Shown event of the AboutDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AboutDialog_Shown(object sender, EventArgs e)
        {
            bloggerIcon.Visible = !string.IsNullOrEmpty(WebsiteLink);
            facebookIcon.Visible = !string.IsNullOrEmpty(FacebookLink);
        }
    }
    #endregion

    #region Designer Generated Code

    /// <summary>
    /// Class AboutDialog.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    partial class AboutDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bloggerIcon = new System.Windows.Forms.PictureBox();
            this.facebookIcon = new System.Windows.Forms.PictureBox();
            this.githubIcon = new System.Windows.Forms.PictureBox();
            this.btOK = new System.Windows.Forms.Button();
            this.lbVersion = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bloggerIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.facebookIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.githubIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(64)))), ((int)(((byte)(80)))));
            this.flowLayoutPanel1.Controls.Add(this.bloggerIcon);
            this.flowLayoutPanel1.Controls.Add(this.facebookIcon);
            this.flowLayoutPanel1.Controls.Add(this.githubIcon);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(353, 235);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(78, 26);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // bloggerIcon
            // 
            this.bloggerIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bloggerIcon.Image = Properties.Resources.CAELogoSmall;
            this.bloggerIcon.Location = new System.Drawing.Point(3, 3);
            this.bloggerIcon.Name = "bloggerIcon";
            this.bloggerIcon.Size = new System.Drawing.Size(20, 20);
            this.bloggerIcon.TabIndex = 0;
            this.bloggerIcon.TabStop = false;
            this.bloggerIcon.Click += new System.EventHandler(this.IconClick);
            // 
            // facebookIcon
            // 
            this.facebookIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.facebookIcon.Image = Properties.Resources.FacebookIcon;
            this.facebookIcon.Location = new System.Drawing.Point(29, 3);
            this.facebookIcon.Name = "facebookIcon";
            this.facebookIcon.Size = new System.Drawing.Size(20, 20);
            this.facebookIcon.TabIndex = 1;
            this.facebookIcon.TabStop = false;
            this.facebookIcon.Click += new System.EventHandler(this.IconClick);
            // 
            // githubIcon
            // 
            this.githubIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.githubIcon.Image = Properties.Resources.GithubIcon;
            this.githubIcon.Location = new System.Drawing.Point(55, 3);
            this.githubIcon.Name = "githubIcon";
            this.githubIcon.Size = new System.Drawing.Size(20, 20);
            this.githubIcon.TabIndex = 2;
            this.githubIcon.TabStop = false;
            this.githubIcon.Click += new System.EventHandler(this.IconClick);
            // 
            // btOK
            // 
            this.btOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(353, 268);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(78, 23);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // lbVersion
            // 
            this.lbVersion.BackColor = System.Drawing.Color.Black;
            this.lbVersion.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(222)))), ((int)(((byte)(234)))));
            this.lbVersion.Location = new System.Drawing.Point(326, 29);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(94, 19);
            this.lbVersion.TabIndex = 2;
            this.lbVersion.Text = "Vx.x.x.x";
            this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AboutDialog
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Properties.Resources.AboutDlgBackground;
            this.ClientSize = new System.Drawing.Size(442, 298);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Shown += new System.EventHandler(this.AboutDialog_Shown);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bloggerIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.facebookIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.githubIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>
        /// The flow layout panel1
        /// </summary>
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        /// <summary>
        /// The blogger icon
        /// </summary>
        private System.Windows.Forms.PictureBox bloggerIcon;
        /// <summary>
        /// The facebook icon
        /// </summary>
        private System.Windows.Forms.PictureBox facebookIcon;
        /// <summary>
        /// The bt ok
        /// </summary>
        private System.Windows.Forms.Button btOK;
        /// <summary>
        /// The lb version
        /// </summary>
        private System.Windows.Forms.Label lbVersion;
        /// <summary>
        /// The github icon
        /// </summary>
        private PictureBox githubIcon;
    }

    #endregion

    #endregion

}
