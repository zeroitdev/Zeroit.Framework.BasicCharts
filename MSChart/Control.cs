// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Control.cs" company="Zeroit Dev Technologies">
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
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace Zeroit.Framework.BasicCharts
{

    #region Control
    /// <summary>
    /// Class MSChartExtensionZoomDialog.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    internal partial class MSChartExtensionZoomDialog : Form
    {
        /// <summary>
        /// The PTR chart area
        /// </summary>
        private ChartArea ptrChartArea;
        /// <summary>
        /// The PTR x axis
        /// </summary>
        private Axis ptrXAxis, ptrYAxis;
        /// <summary>
        /// Initializes a new instance of the <see cref="MSChartExtensionZoomDialog"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public MSChartExtensionZoomDialog(ChartArea sender)
        {
            InitializeComponent();
            ptrChartArea = sender;
            cbAxisType.SelectedIndex = 0;
            cbAxisType_SelectedIndexChanged(this, null);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbAxisType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbAxisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbAxisType.SelectedIndex)
            {
                case 0: //Primary Axis
                    ptrXAxis = ptrChartArea.AxisX;
                    ptrYAxis = ptrChartArea.AxisY;
                    break;

                case 1:  //Secondary Axis
                    ptrXAxis = ptrChartArea.AxisX2;
                    ptrYAxis = ptrChartArea.AxisY2;
                    break;
            }
            txtXLimit.Text = string.Format("[{0} to {1}]",
                FormatDouble(ptrXAxis.Minimum),
                FormatDouble(ptrXAxis.Maximum));

            txtYLimit.Text = string.Format("[{0} to {1}]",
                FormatDouble(ptrYAxis.Minimum),
                FormatDouble(ptrYAxis.Maximum));

            txtXMin.Text = ptrXAxis.ScaleView.ViewMinimum.ToString();
            txtXMax.Text = ptrXAxis.ScaleView.ViewMaximum.ToString();
            txtYMin.Text = ptrYAxis.ScaleView.ViewMinimum.ToString();
            txtYMax.Text = ptrYAxis.ScaleView.ViewMaximum.ToString();
        }

        /// <summary>
        /// Handles the Click event of the btOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btOK_Click(object sender, EventArgs e)
        {
            bool inputValid = true;

            //Sanity Check
            if (!ValidateInput(txtXMin)) { inputValid = false; }
            if (!ValidateInput(txtXMax)) { inputValid = false; }
            if (!ValidateInput(txtYMin)) { inputValid = false; }
            if (!ValidateInput(txtYMax)) { inputValid = false; }
            if (!inputValid)
            {
                MessageBox.Show("Invalid input values!", this.Text,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Limit Check
            double xStart = Convert.ToDouble(txtXMin.Text);
            double xEnd = Convert.ToDouble(txtXMax.Text);
            double yStart = Convert.ToDouble(txtYMin.Text);
            double yEnd = Convert.ToDouble(txtYMax.Text);

            //Perform ZOOM
            double XMin = ptrXAxis.ValueToPixelPosition(xStart);
            double XMax = ptrXAxis.ValueToPixelPosition(xEnd);
            double YMin = ptrYAxis.ValueToPixelPosition(yStart);
            double YMax = ptrYAxis.ValueToPixelPosition(yEnd);

            ptrXAxis.ScaleView.Zoom(xStart, xEnd);
            ptrYAxis.ScaleView.Zoom(yStart, yEnd);

            //Swtich to next axis
            ptrXAxis = (ptrXAxis == ptrChartArea.AxisX) ? ptrChartArea.AxisX2 : ptrChartArea.AxisX;
            ptrYAxis = (ptrYAxis == ptrChartArea.AxisY) ? ptrChartArea.AxisY2 : ptrChartArea.AxisY;
            ptrXAxis.ScaleView.Zoom(ptrXAxis.PixelPositionToValue(XMin), ptrXAxis.PixelPositionToValue(XMax));
            ptrYAxis.ScaleView.Zoom(ptrYAxis.PixelPositionToValue(YMin), ptrYAxis.PixelPositionToValue(YMax));

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Validates the input.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool ValidateInput(TextBox sender)
        {
            double result;
            bool valid = double.TryParse(sender.Text, out result);
            sender.BackColor = valid ? Color.FromKnownColor(KnownColor.Window) : Color.FromArgb(255, 192, 192);
            return valid;
        }

        /// <summary>
        /// Formats the double.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>System.String.</returns>
        private string FormatDouble(double number)
        {
            double numberRange = Math.Abs(number);
            if (numberRange < 0)
                return number.ToString("0.0000");
            else if (numberRange < 10)
                return number.ToString("0.0000");
            else if (numberRange < 100)
                return number.ToString("0.00");
            else if (numberRange < 1000)
                return number.ToString("0.0");
            else
                return number.ToString("0");
        }
    }
    #endregion

    #region Designer Generated Code

    partial class MSChartExtensionZoomDialog
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
            this.txtXMin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYMin = new System.Windows.Forms.TextBox();
            this.txtYMax = new System.Windows.Forms.TextBox();
            this.txtXMax = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtXLimit = new System.Windows.Forms.Label();
            this.txtYLimit = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.cbAxisType = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtXMin
            // 
            this.txtXMin.Location = new System.Drawing.Point(31, 62);
            this.txtXMin.Name = "txtXMin";
            this.txtXMin.Size = new System.Drawing.Size(69, 20);
            this.txtXMin.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y";
            // 
            // txtYMin
            // 
            this.txtYMin.Location = new System.Drawing.Point(31, 88);
            this.txtYMin.Name = "txtYMin";
            this.txtYMin.Size = new System.Drawing.Size(69, 20);
            this.txtYMin.TabIndex = 3;
            // 
            // txtYMax
            // 
            this.txtYMax.Location = new System.Drawing.Point(106, 88);
            this.txtYMax.Name = "txtYMax";
            this.txtYMax.Size = new System.Drawing.Size(69, 20);
            this.txtYMax.TabIndex = 4;
            // 
            // txtXMax
            // 
            this.txtXMax.Location = new System.Drawing.Point(106, 62);
            this.txtXMax.Name = "txtXMax";
            this.txtXMax.Size = new System.Drawing.Size(69, 20);
            this.txtXMax.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Min";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(125, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Max";
            // 
            // txtXLimit
            // 
            this.txtXLimit.AutoSize = true;
            this.txtXLimit.Location = new System.Drawing.Point(181, 65);
            this.txtXLimit.Name = "txtXLimit";
            this.txtXLimit.Size = new System.Drawing.Size(68, 13);
            this.txtXLimit.TabIndex = 8;
            this.txtXLimit.Text = "[XAxis Limits]";
            // 
            // txtYLimit
            // 
            this.txtYLimit.AutoSize = true;
            this.txtYLimit.Location = new System.Drawing.Point(181, 91);
            this.txtYLimit.Name = "txtYLimit";
            this.txtYLimit.Size = new System.Drawing.Size(71, 13);
            this.txtYLimit.TabIndex = 9;
            this.txtYLimit.Text = "[YAxis Limits ]";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 38);
            this.panel1.TabIndex = 5;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(239, 8);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.Location = new System.Drawing.Point(158, 8);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // cbAxisType
            // 
            this.cbAxisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAxisType.FormattingEnabled = true;
            this.cbAxisType.Items.AddRange(new object[] {
            "Primary Axis",
            "Secondary Axis"});
            this.cbAxisType.Location = new System.Drawing.Point(14, 12);
            this.cbAxisType.Name = "cbAxisType";
            this.cbAxisType.Size = new System.Drawing.Size(161, 21);
            this.cbAxisType.TabIndex = 0;
            this.cbAxisType.SelectedIndexChanged += new System.EventHandler(this.cbAxisType_SelectedIndexChanged);
            // 
            // MSChartExtensionZoomDialog
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(323, 161);
            this.Controls.Add(this.cbAxisType);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtYLimit);
            this.Controls.Add(this.txtXLimit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtYMax);
            this.Controls.Add(this.txtXMax);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYMin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtXMin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MSChartExtensionZoomDialog";
            this.Text = "Zoom Settings...";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>
        /// The text x minimum
        /// </summary>
        private TextBox txtXMin;
        /// <summary>
        /// The label1
        /// </summary>
        private Label label1;
        /// <summary>
        /// The label2
        /// </summary>
        private Label label2;
        /// <summary>
        /// The text y minimum
        /// </summary>
        private TextBox txtYMin;
        /// <summary>
        /// The text y maximum
        /// </summary>
        private TextBox txtYMax;
        /// <summary>
        /// The text x maximum
        /// </summary>
        private TextBox txtXMax;
        /// <summary>
        /// The label3
        /// </summary>
        private Label label3;
        /// <summary>
        /// The label4
        /// </summary>
        private Label label4;
        /// <summary>
        /// The text x limit
        /// </summary>
        private Label txtXLimit;
        /// <summary>
        /// The text y limit
        /// </summary>
        private Label txtYLimit;
        /// <summary>
        /// The panel1
        /// </summary>
        private Panel panel1;
        /// <summary>
        /// The bt cancel
        /// </summary>
        private Button btCancel;
        /// <summary>
        /// The bt ok
        /// </summary>
        private Button btOK;
        /// <summary>
        /// The cb axis type
        /// </summary>
        private ComboBox cbAxisType;
    }

    #endregion

}
