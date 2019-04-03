// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-04-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="MetroPieChart.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Zeroit.Framework.BasicCharts.Metro
{
    /// <summary>
    /// A class collection for rendering a metro-style pie chart.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [DefaultEvent("Click")]
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(ZeroitMetroPieChart), "ZeroitMetroPieChart.bmp")]
    [Designer(typeof(ZeroitMetroPieChartDesigner))]
	public class ZeroitMetroPieChart : Control
	{
        #region Private Fields

        /// <summary>
        /// The segments
        /// </summary>
        private ZeroitMetroPieChartSegmentCollection _Segments = new ZeroitMetroPieChartSegmentCollection();

        /// <summary>
        /// The radius
        /// </summary>
        private int _radius = 200;

        /// <summary>
        /// The effect size
        /// </summary>
        private int _EffectSize = 10;

        /// <summary>
        /// The show effect
        /// </summary>
        private bool _ShowEffect = true;

        /// <summary>
        /// The drawsegmentborders
        /// </summary>
        private bool _drawsegmentborders = false;

        /// <summary>
        /// The use dynamic border colors
        /// </summary>
        private bool _UseDynamicBorderColors = false;

        /// <summary>
        /// The segment border size
        /// </summary>
        private int _SegmentBorderSize = 2;

        /// <summary>
        /// The style
        /// </summary>
        private ZeroitMetroPieChart.TrackerStyle _Style = ZeroitMetroPieChart.TrackerStyle.Normal;

        /// <summary>
        /// The show segment names
        /// </summary>
        private bool _ShowSegmentNames = false;

        /// <summary>
        /// The segment names per row
        /// </summary>
        private int _SegmentNamesPerRow = 2;

        /// <summary>
        /// The use dynamic fill colors
        /// </summary>
        private bool _UseDynamicFillColors = false;

        /// <summary>
        /// The fill color alpha
        /// </summary>
        private int _FillColorAlpha = 82;

        /// <summary>
        /// The show donut effect
        /// </summary>
        private bool _showDonutEffect = false;

        /// <summary>
        /// The donut effect size
        /// </summary>
        private int _DonutEffectSize = 100;

        /// <summary>
        /// The seperate segments
        /// </summary>
        private bool _SeperateSegments = false;

        /// <summary>
        /// The draw border
        /// </summary>
        private bool _DrawBorder = false;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor = Color.FromArgb(18, 173, 196);

        /// <summary>
        /// The abstract color
        /// </summary>
        Color abstractColor = Color.FromArgb(18, 173, 196);

        /// <summary>
        /// The abstract border width
        /// </summary>
        float abstractBorderWidth = 2f;

        /// <summary>
        /// The unknown border color
        /// </summary>
        Color[] unknownBorderColor = new Color[] { Color.Black, Color.Black };

        /// <summary>
        /// The unknown f ill color
        /// </summary>
        Color[] unknownFIllColor = new Color[] { Color.Black, Color.Black };

        /// <summary>
        /// The border size
        /// </summary>
        private int _BorderSize = 2;

        /// <summary>
        /// The shade offset
        /// </summary>
        private int shadeOffset = 40;

        #endregion

        #region Public Properties 

        /// <summary>
        /// Gets or sets the shade offset.
        /// </summary>
        /// <value>The shade offset.</value>
        public int ShadeOffset
	    {
	        get { return shadeOffset; }
	        set
	        {
                shadeOffset = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the color of the abstract.
        /// </summary>
        /// <value>The color of the abstract.</value>
        public Color AbstractColor
	    {
	        get { return abstractColor; }
	        set
	        {
                abstractColor = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the width of the abstract border.
        /// </summary>
        /// <value>The width of the abstract border.</value>
        public float AbstractBorderWidth
	    {
	        get { return abstractBorderWidth; }
	        set
	        {
                abstractBorderWidth = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the color of the unknown border.
        /// </summary>
        /// <value>The color of the unknown border.</value>
        public Color[] UnknownBorderColor
	    {
	        get { return unknownBorderColor; }
	        set
	        {
                unknownBorderColor = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the color of the unknown fill.
        /// </summary>
        /// <value>The color of the unknown fill.</value>
        public Color[] UnknownFillColor
	    {
	        get { return unknownFIllColor; }
	        set
	        {
	            unknownFIllColor = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Sets the color of the border.")]
        public Color BorderColor
        {
            get
            {
                return this._BorderColor;
            }
            set
            {
                //if (this._BorderColor != value)
                //{
                //    this._BorderColor = value;
                //    this.Invalidate();
                //}

                _BorderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the size of the border.
        /// </summary>
        /// <value>The size of the border.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(2)]
        [Description("Sets the size of the border.")]
        public int BorderSize
        {
            get
            {
                return this._BorderSize;
            }
            set
            {
                if (value != this._BorderSize)
                {
                    this._BorderSize = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the donut effect.
        /// </summary>
        /// <value>The size of the donut effect.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(100)]
        [Description("Sets the size of the donut effect.")]
        public int DonutEffectSize
        {
            get
            {
                return this._DonutEffectSize;
            }
            set
            {
                if (value != this._DonutEffectSize)
                {
                    this._DonutEffectSize = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw the border.
        /// </summary>
        /// <value><c>true</c> if draw border; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Gibt an, ob das PieChart umrandet werden soll.")]
        public bool DrawBorder
        {
            get
            {
                return this._DrawBorder;
            }
            set
            {
                if (this._DrawBorder != value)
                {
                    this._DrawBorder = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw segment borders.
        /// </summary>
        /// <value><c>true</c> if draw segment borders; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to draw segment borders.")]
        public bool DrawSegmentBorders
        {
            get
            {
                return this._drawsegmentborders;
            }
            set
            {
                if (value != this._drawsegmentborders)
                {
                    this._drawsegmentborders = value;
                    if (!this._drawsegmentborders)
                    {
                        this._SeperateSegments = false;
                    }
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the dynamic color offset.
        /// </summary>
        /// <value>The dynamic color offset.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(82)]
        [Description("Sets the dynamic color offset.")]
        public int DynamicColorOffset
        {
            get
            {
                return this._FillColorAlpha;
            }
            set
            {
                if (value != this._FillColorAlpha)
                {
                    this._FillColorAlpha = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the effect.
        /// </summary>
        /// <value>The size of the effect.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(10)]
        [Description("Sets the size of the effect.")]
        public int EffectSize
        {
            get
            {
                return this._EffectSize;
            }
            set
            {
                if (value != this._EffectSize)
                {
                    this._EffectSize = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(200)]
        [Description("Sets the radius.")]
        public int Radius
        {
            get
            {
                return this._radius;
            }
            set
            {
                if (value != this._radius)
                {
                    this._radius = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the border segment.
        /// </summary>
        /// <value>The size of the border segment.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(2)]
        [Description("Sets the size of the border segment.")]
        public int SegmentBorderSize
        {
            get
            {
                return this._SegmentBorderSize;
            }
            set
            {
                if (value != this._SegmentBorderSize)
                {
                    this._SegmentBorderSize = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets the segment names per row.
        /// </summary>
        /// <value>The segment names per row.</value>
        [Browsable(true)]
        [Category("Behavoir")]
        [DefaultValue(2)]
        [Description("Gets the segment names per row.")]
        public int SegmentNamesPerRow
        {
            get
            {
                return this._SegmentNamesPerRow;
            }
        }

        /// <summary>
        /// Gets the segments.
        /// </summary>
        /// <value>The segments.</value>
        [Browsable(true)]
        [Category("Data")]
        [Description("Gets the segments.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ZeroitMetroPieChartSegmentCollection Segments
        {
            get
            {
                return _Segments;
            }
            set
            {
                _Segments = value;
                Invalidate();
            }

        }

        /// <summary>
        /// Gets or sets a value indicating whether to seperate segments.
        /// </summary>
        /// <value><c>true</c> if seperate segments; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to seperate segments.")]
        public bool SeperateSegments
        {
            get
            {
                return this._SeperateSegments;
            }
            set
            {
                if (value != this._SeperateSegments)
                {
                    this._SeperateSegments = value;
                    this._drawsegmentborders = true;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show donut effect.
        /// </summary>
        /// <value><c>true</c> if show donut effect; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to show donut effect.")]
        public bool ShowDonutEffect
        {
            get
            {
                return this._showDonutEffect;
            }
            set
            {
                if (value != this._showDonutEffect)
                {
                    this._showDonutEffect = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show effect.
        /// </summary>
        /// <value><c>true</c> if show effect; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to show effect.")]
        public bool ShowEffect
        {
            get
            {
                return this._ShowEffect;
            }
            set
            {
                if (value != this._ShowEffect)
                {
                    this._ShowEffect = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show segment names.
        /// </summary>
        /// <value><c>true</c> if show segment names; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Behavoir")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to show segment names.")]
        public bool ShowSegmentNames
        {
            get
            {
                return this._ShowSegmentNames;
            }
            set
            {
                if (value != this._ShowSegmentNames)
                {
                    this._ShowSegmentNames = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(1)]
        [Description("Sets the style.")]
        public ZeroitMetroPieChart.TrackerStyle Style
        {
            get
            {
                return this._Style;
            }
            set
            {
                if (this._Style != value)
                {
                    this._Style = value;
                    if (this._Style == ZeroitMetroPieChart.TrackerStyle.Abstract)
                    {
                        this._DrawBorder = true;
                        this._BorderColor = Color.FromArgb(18, 173, 196);
                    }
                    else if (this._Style == ZeroitMetroPieChart.TrackerStyle.Normal)
                    {
                        this._DrawBorder = false;
                    }
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use dynamic border colors.
        /// </summary>
        /// <value><c>true</c> if use dynamic border colors; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to use dynamic border colors.")]
        public bool UseDynamicBorderColors
        {
            get
            {
                return this._UseDynamicBorderColors;
            }
            set
            {
                if (value != this._UseDynamicBorderColors)
                {
                    this._UseDynamicBorderColors = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use dynamic fill colors.
        /// </summary>
        /// <value><c>true</c> if use dynamic fill colors; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to use dynamic fill colors.")]
        public bool UseDynamicFillColors
        {
            get
            {
                return this._UseDynamicFillColors;
            }
            set
            {
                if (value != this._UseDynamicFillColors)
                {
                    this._UseDynamicFillColors = value;
                    this.Invalidate();
                }
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMetroPieChart" /> class.
        /// </summary>
        public ZeroitMetroPieChart()
		{
		    this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            this.Size = new System.Drawing.Size(215, 280);
			this.UpdateStyles();
			ZeroitMetroPieChart metroPieChart = this;
			this._Segments.ItemAdded += new EventHandler<ZeroitMetroPieChartSegmentCollectionEventArgs>(metroPieChart.Paths_Added);
			ZeroitMetroPieChart metroPieChart1 = this;
			this._Segments.ItemRemoving += new EventHandler<ZeroitMetroPieChartSegmentCollectionEventArgs>(metroPieChart1.Paths_Removing);
		}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnBackColorChanged(EventArgs e)
		{
			this.Invalidate();
			base.OnBackColorChanged(e);
		}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
		{
			float value = 0f;
			Rectangle rectangle;
			Graphics graphics = e.Graphics;

            if (AllowTransparency)
            {
                MakeTransparent(this, graphics);
            }

            Pen pen = new Pen(Color.FromArgb(50, 255, 255, 255), (float)this._EffectSize);
			Rectangle rectangle1 = new Rectangle(checked((int)Math.Round((double)this.Width / 2 - (double)this._radius / 2 - 1)) + BorderSize, 2 + BorderSize, this._radius - (2 * BorderSize), this._radius - (2 * BorderSize));
			Rectangle rectangle2 = new Rectangle(checked((int)Math.Round((double)this.Width / 2 - (double)this._radius / 2 + (double)this._EffectSize / 2 - 0.25)), checked((int)Math.Round((double)this._EffectSize / 2 + 2)), checked(checked(this._radius - this._EffectSize) - 3), checked(checked(this._radius - this._EffectSize) + 0));
			Rectangle rectangle3 = new Rectangle(checked((int)Math.Round((double)this.Width / 2 - (double)this._radius / 2 + (double)this._DonutEffectSize / 2 + 0)), checked((int)Math.Round((double)this._DonutEffectSize / 2 + 2)), checked(checked(0 + this._radius) - this._DonutEffectSize), checked(checked(0 + this._radius) - this._DonutEffectSize));
			Rectangle rectangle4 = new Rectangle(checked((int)Math.Round((double)this.Width / 2 - (double)this._radius / 2 + 5 - 1)), 7, checked(this._radius - 10), checked(this._radius - 10));
			int num = 0;
			System.Drawing.Font font = new System.Drawing.Font(this.Font.FontFamily, 10f, FontStyle.Regular, GraphicsUnit.Pixel);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			int count = checked(this.Segments.Count - 1);
			for (int i = 0; i <= count; i = checked(i + 1))
			{
				value += (float)this.Segments[i].Value;
			}
			float single = 0f;
			if (this.Style != ZeroitMetroPieChart.TrackerStyle.Abstract)
			{
				int count1 = checked(this.Segments.Count - 1);
				for (int j = 0; j <= count1; j = checked(j + 1))
				{
					float value1 = (float)this.Segments[j].Value / value * 360f;
					using (SolidBrush solidBrush = new SolidBrush(this.Segments[j].FillColor))
					{
						if (!this.Segments[j].UseFillStyle)
						{
							graphics.FillPie(solidBrush, rectangle1, single, value1);
						}
						else
						{
							graphics.FillPie(new HatchBrush(this.Segments[j].FillStyle, this.Segments[j].BorderColor, this.Segments[j].FillColor), rectangle1, single, value1);
						}
					}
					single += value1;
				}
			}
			else
			{
			    
                graphics.DrawEllipse(new Pen(abstractColor, abstractBorderWidth), rectangle1);
				int num1 = checked(this.Segments.Count - 1);
				for (int k = 0; k <= num1; k = checked(k + 1))
				{
					float single1 = (float)this.Segments[k].Value / value * 360f;
					using (SolidBrush solidBrush1 = new SolidBrush(this.Segments[k].FillColor))
					{
						if (!this.UseDynamicFillColors)
						{
							graphics.FillPie(solidBrush1, rectangle4, single, single1);
						}
						else
						{
							graphics.FillPie(new SolidBrush(this.Segments[k].BorderColor), rectangle4, single, single1);
							graphics.FillPie(new SolidBrush(Color.FromArgb(this._FillColorAlpha, 38, 15, 0)), rectangle4, single, single1);
						}
					}
					single += single1;
				}
			}
			if (this.DrawSegmentBorders)
			{
				single = 0f;
				if (this.Style != ZeroitMetroPieChart.TrackerStyle.Abstract)
				{
					int count2 = checked(this.Segments.Count - 1);
					for (int l = 0; l <= count2; l = checked(l + 1))
					{
						float value2 = (float)this.Segments[l].Value / value * 360f;
						using (SolidBrush solidBrush2 = new SolidBrush(this.Segments[l].FillColor))
						{
							if (this.SeperateSegments)
							{
								graphics.DrawPie(new Pen(this.BackColor, 3f), rectangle1, single, value2);
							}
							else if (!this.UseDynamicBorderColors)
							{
								graphics.DrawPie(new Pen((Color)this.Segments[l].BorderColor, (float)this.SegmentBorderSize), rectangle1, single, value2);
							}
							else
							{
							    
								Color color = this.SetShade(solidBrush2.Color, shadeOffset);
								graphics.DrawPie(new Pen(color, (float)this.SegmentBorderSize), rectangle1, single, value2);
							}
							solidBrush2.Dispose();
						}
						single += value2;
					}
				}
				else
				{
					int num2 = checked(this.Segments.Count - 1);
					for (int m = 0; m <= num2; m = checked(m + 1))
					{
						float single2 = (float)this.Segments[m].Value / value * 360f;
						using (SolidBrush solidBrush3 = new SolidBrush(this.Segments[m].FillColor))
						{
							if (!this.UseDynamicBorderColors)
							{
								graphics.DrawPie(new Pen((Color)this.Segments[m].BorderColor, (float)this.SegmentBorderSize), rectangle4, single, single2);
							}
							else
							{
								Color color1 = this.SetShade(solidBrush3.Color, 40);
								graphics.DrawPie(new Pen(color1, (float)this.SegmentBorderSize), rectangle4, single, single2);
							}
							solidBrush3.Dispose();
						}
						single += single2;
					}
				}
			}
			if (this.ShowEffect)
			{
				graphics.DrawEllipse(pen, rectangle2);
			}
			if (this.ShowDonutEffect)
			{
				graphics.FillEllipse(new SolidBrush(this.BackColor), rectangle3);
			}
			if (this.DrawBorder)
			{
				graphics.DrawEllipse(new Pen(this._BorderColor, (float)this.BorderSize), rectangle1);
			}
			if (this.ShowSegmentNames)
			{
				int num3 = 0;
				int count3 = checked(this.Segments.Count - 1);
				for (int n = 0; n <= count3; n = checked(n + 1))
				{
					using (SolidBrush solidBrush4 = new SolidBrush(this.Segments[n].FillColor))
					{
					    

					    if (num != 0)
						{
							Pen black = new Pen(unknownBorderColor[0]);
							rectangle = new Rectangle(checked((int)Math.Round((double)this.Width / 2)), checked(checked(checked(this._radius + 2) + 10) + num3), 10, 10);
							graphics.DrawRectangle(black, rectangle);
							if (!(this.Style == ZeroitMetroPieChart.TrackerStyle.Abstract & this.UseDynamicFillColors))
							{
								rectangle = new Rectangle(checked((int)Math.Round((double)this.Width / 2)), checked(checked(checked(this._radius + 2) + 10) + num3), 10, 10);
								graphics.FillRectangle(solidBrush4, rectangle);
							}
							else
							{
								SolidBrush solidBrush5 = new SolidBrush(this.Segments[n].BorderColor);
								rectangle = new Rectangle(checked((int)Math.Round((double)this.Width / 2)), checked(checked(checked(this._radius + 2) + 10) + num3), 10, 10);
								graphics.FillRectangle(solidBrush5, rectangle);
								SolidBrush solidBrush6 = new SolidBrush(Color.FromArgb(this._FillColorAlpha, 38, 15, 0));
								rectangle = new Rectangle(checked((int)Math.Round((double)this.Width / 2)), checked(checked(checked(this._radius + 2) + 10) + num3), 10, 10);
								graphics.FillRectangle(solidBrush6, rectangle);
							}
							string name = this.Segments[n].Name;
							Brush brush = new SolidBrush(unknownFIllColor[0]);
							rectangle = new Rectangle(checked((int)Math.Round((double)this.Width / 2 + 13)), checked(checked(checked(this._radius + 2) + 8) + num3), 80, 15);
							graphics.DrawString(name, font, brush, rectangle);
							num3 = checked(num3 + 20);
							num = 0;
						}
						else
						{
							Pen black1 = new Pen(unknownBorderColor[1]);
							rectangle = new Rectangle(2, checked(checked(checked(this._radius + 2) + 10) + num3), 10, 10);
							graphics.DrawRectangle(black1, rectangle);
							if (!(this.Style == ZeroitMetroPieChart.TrackerStyle.Abstract & this.UseDynamicFillColors))
							{
								rectangle = new Rectangle(2, checked(checked(checked(this._radius + 2) + 10) + num3), 10, 10);
								graphics.FillRectangle(solidBrush4, rectangle);
							}
							else
							{
								SolidBrush solidBrush7 = new SolidBrush(this.Segments[n].BorderColor);
								rectangle = new Rectangle(2, checked(checked(checked(this._radius + 2) + 10) + num3), 10, 10);
								graphics.FillRectangle(solidBrush7, rectangle);
								SolidBrush solidBrush8 = new SolidBrush(Color.FromArgb(this._FillColorAlpha, 38, 15, 0));
								rectangle = new Rectangle(2, checked(checked(checked(this._radius + 2) + 10) + num3), 10, 10);
								graphics.FillRectangle(solidBrush8, rectangle);
							}
							string str = this.Segments[n].Name;
							Brush brush1 = new SolidBrush(unknownFIllColor[1]);
							rectangle = new Rectangle(15, checked(checked(checked(this._radius + 2) + 8) + num3), 80, 15);
							graphics.DrawString(str, font, brush1, rectangle);
							num = 1;
						}
					}
				}
			}
			base.OnPaint(e);
		}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            int proportion = (Width + Height) / 2;

            if (ShowSegmentNames)
            {
                proportion = (Width + Height) / 3;
                Radius = proportion;
            }
            else
            {
                
                Radius = proportion - 10;
            }

            if (proportion < 20)
            {
                proportion = 20;
                Radius = proportion;
            }

            

            Invalidate();
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Path control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Path_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			ZeroitMetroPieChartSegment metroPieChartSegment = (ZeroitMetroPieChartSegment)sender;
			if (Operators.CompareString(e.PropertyName, "FillColor", false) == 0)
			{
			}
			this.Invalidate();
		}

        /// <summary>
        /// Handles the Added event of the Paths control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ZeroitMetroPieChartSegmentCollectionEventArgs"/> instance containing the event data.</param>
        private void Paths_Added(object sender, ZeroitMetroPieChartSegmentCollectionEventArgs e)
		{
			if (e.Item != null)
			{
				ZeroitMetroPieChart metroPieChart = this;
				e.Item.PropertyChanged += new PropertyChangedEventHandler(metroPieChart.Path_PropertyChanged);
			}
			ZeroitMetroPieChart.SegmentAddedEventHandler segmentAddedEventHandler = ZeroitMetroPieChart.SegmentAdded;
			if (segmentAddedEventHandler != null)
			{
				segmentAddedEventHandler(this, new ZeroitMetroPieChartSegmentCollectionEventArgs(e.Item));
			}
			this.Invalidate();
		}

        /// <summary>
        /// Handles the Removing event of the Paths control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ZeroitMetroPieChartSegmentCollectionEventArgs"/> instance containing the event data.</param>
        private void Paths_Removing(object sender, ZeroitMetroPieChartSegmentCollectionEventArgs e)
		{
			if (e.Item != null)
			{
				ZeroitMetroPieChart metroPieChart = this;
				e.Item.PropertyChanged -= new PropertyChangedEventHandler(metroPieChart.Path_PropertyChanged);
			}
		}

        /// <summary>
        /// Sets the shade.
        /// </summary>
        /// <param name="InputColor">Color of the input.</param>
        /// <param name="Offset">The offset.</param>
        /// <returns>Color.</returns>
        private Color SetShade(Color InputColor, int Offset)
		{
			int r = checked(InputColor.R + Offset);
			int g = checked(InputColor.G + Offset);
			int b = checked(InputColor.B + Offset);
			if (r < 0)
			{
				r = checked(r * -1);
			}
			if (g < 0)
			{
				g = checked(g * -1);
			}
			if (b < 0)
			{
				b = checked(b * -1);
			}
			Color color = Color.FromArgb(Math.Min(255, r), Math.Min(255, g), Math.Min(255, b));
			return color;
		}

        /// <summary>
        /// Occurs when [segment added].
        /// </summary>
        public static event ZeroitMetroPieChart.SegmentAddedEventHandler SegmentAdded;

        /// <summary>
        /// Delegate SegmentAddedEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ZeroitMetroPieChartSegmentCollectionEventArgs"/> instance containing the event data.</param>
        public delegate void SegmentAddedEventHandler(object sender, ZeroitMetroPieChartSegmentCollectionEventArgs e);

        /// <summary>
        /// Enum TrackerStyle
        /// </summary>
        public enum TrackerStyle
		{
            /// <summary>
            /// The abstract
            /// </summary>
            Abstract,
            /// <summary>
            /// The normal
            /// </summary>
            Normal
        }





        #region Include in Private Field

        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        public bool AllowTransparency
        {
            get { return allowTransparency; }
            set
            {
                allowTransparency = value;

                Invalidate();
            }
        }

        #endregion



        #region Include in Paint

        //-----------------------------Include in Paint--------------------------//
        //
        // if(AllowTransparency)
        //  {
        //    MakeTransparent(this,g);
        //  }
        //
        //-----------------------------Include in Paint--------------------------//

        private static void MakeTransparent(Control control, Graphics g)
        {
            var parent = control.Parent;
            if (parent == null) return;
            var bounds = control.Bounds;
            var siblings = parent.Controls;
            int index = siblings.IndexOf(control);
            Bitmap behind = null;
            for (int i = siblings.Count - 1; i > index; i--)
            {
                var c = siblings[i];
                if (!c.Bounds.IntersectsWith(bounds)) continue;
                if (behind == null)
                    behind = new Bitmap(control.Parent.ClientSize.Width, control.Parent.ClientSize.Height);
                c.DrawToBitmap(behind, c.Bounds);
            }
            if (behind == null) return;
            g.DrawImage(behind, control.ClientRectangle, bounds, GraphicsUnit.Pixel);
            behind.Dispose();
        }

        #endregion



    }

    #region Old Smart Tag
    /// <summary>
    /// Class MetroPieChartActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class MetroPieChartActionList : DesignerActionList
    {
        /// <summary>
        /// The pc
        /// </summary>
        private ZeroitMetroPieChart _pc;

        /// <summary>
        /// The designer action SVC
        /// </summary>
        private DesignerActionUIService designerActionSvc;

        /// <summary>
        /// Gets or sets the size of the donut effect.
        /// </summary>
        /// <value>The size of the donut effect.</value>
        public int DonutEffectSize
        {
            get
            {
                return this._pc.DonutEffectSize;
            }
            set
            {
                this._pc.DonutEffectSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the dynamic color offset.
        /// </summary>
        /// <value>The dynamic color offset.</value>
        public int DynamicColorOffset
        {
            get
            {
                return this._pc.DynamicColorOffset;
            }
            set
            {
                this._pc.DynamicColorOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the effect.
        /// </summary>
        /// <value>The size of the effect.</value>
        public int EffectSize
        {
            get
            {
                return this._pc.EffectSize;
            }
            set
            {
                this._pc.EffectSize = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [seperate segments].
        /// </summary>
        /// <value><c>true</c> if [seperate segments]; otherwise, <c>false</c>.</value>
        public bool SeperateSegments
        {
            get
            {
                return this._pc.SeperateSegments;
            }
            set
            {
                this._pc.SeperateSegments = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show donut effect].
        /// </summary>
        /// <value><c>true</c> if [show donut effect]; otherwise, <c>false</c>.</value>
        public bool ShowDonutEffect
        {
            get
            {
                return this._pc.ShowDonutEffect;
            }
            set
            {
                this._pc.ShowDonutEffect = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show effect].
        /// </summary>
        /// <value><c>true</c> if [show effect]; otherwise, <c>false</c>.</value>
        public bool ShowEffect
        {
            get
            {
                return this._pc.ShowEffect;
            }
            set
            {
                this._pc.ShowEffect = value;
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public ZeroitMetroPieChart.TrackerStyle Style
        {
            get
            {
                return this._pc.Style;
            }
            set
            {
                this._pc.Style = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use dynamic border colors].
        /// </summary>
        /// <value><c>true</c> if [use dynamic border colors]; otherwise, <c>false</c>.</value>
        public bool UseDynamicBorderColors
        {
            get
            {
                return this._pc.UseDynamicBorderColors;
            }
            set
            {
                this._pc.UseDynamicBorderColors = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use dynamic fill colors].
        /// </summary>
        /// <value><c>true</c> if [use dynamic fill colors]; otherwise, <c>false</c>.</value>
        public bool UseDynamicFillColors
        {
            get
            {
                return this._pc.UseDynamicFillColors;
            }
            set
            {
                this._pc.UseDynamicFillColors = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroPieChartActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public MetroPieChartActionList(IComponent component) : base(component)
        {
            this.designerActionSvc = null;
            this._pc = (ZeroitMetroPieChart)component;
            this.designerActionSvc = (DesignerActionUIService)this.GetService(typeof(DesignerActionUIService));
        }

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
            designerActionItemCollection.Add(new DesignerActionHeaderItem("Effekte"));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("ShowEffect", "ShowEffect:", "Effekte", "Gibt an, ob ein Glanzeffekt gezeichnet werden soll."));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("ShowDonutEffect", "ShowDonutEffect:", "Effekte", "Gibt an, ob ein Donuteffekt gezeichnet werden soll."));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("SeperateSegments", "SeperateSegments:", "Effekte", "Gibt an, ob die Segmente getrennt werden sollen."));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("EffectSize", "EffectSize:", "Effekt", "Gibt an, wie dick der Glanzeffect sein soll.."));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("DonutEffectSize", "DonutEffectSize:", "Effekt", "Gibt an, wie groß der Donuteffekt sein soll."));
            designerActionItemCollection.Add(new DesignerActionHeaderItem("Stil und Farben"));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("Style", "Style:", "Stil und Farben", "Der Style des MetroCharts."));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("UseDynamicFillColors", "UseDynamicFillColors:", "Stil und Farben", "Gibt an, ob die Füllfarbe der Segmente generiert werden soll. Funktioniert nur, wenn zu den Segmente einen Umrandungsfarbe zugeordnet wurde."));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("UseDynamicBorderColors", "UseDynamicBorderColors:", "Stil und Farben", "Gibt an, ob die Umrandungsfarbe der Segmente generiert werden soll."));
            designerActionItemCollection.Add(new DesignerActionPropertyItem("DynamicColorOffset", "DynamicColorOffset:", "Stil und Farben", "Gibt an, wie stark die Füllfarbe verdunkelt werden soll, wenn \"UseDynamicFillColors\"= True entspricht."));
            return designerActionItemCollection;
        }
    }

    /// <summary>
    /// Class MetroPieChartDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    public class MetroPieChartDesigner : ControlDesigner
    {
        /// <summary>
        /// The lists
        /// </summary>
        private DesignerActionListCollection lists;

        /// <summary>
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        /// <value>The action lists.</value>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (this.lists == null)
                {
                    this.lists = new DesignerActionListCollection();
                    this.lists.Add(new MetroPieChartActionList(this.Component));
                }
                return this.lists;
            }
        }

        /// <summary>
        /// Gets the host control.
        /// </summary>
        /// <value>The host control.</value>
        private ZeroitMetroPieChart HostControl
        {
            get
            {
                return (ZeroitMetroPieChart)this.Control;
            }
        }

        /// <summary>
        /// Gets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        /// <value>The selection rules.</value>
        public override System.Windows.Forms.Design.SelectionRules SelectionRules
        {
            get
            {
                return (SelectionRules)268435456 | (SelectionRules)15;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroPieChartDesigner"/> class.
        /// </summary>
        [DebuggerNonUserCode]
        public MetroPieChartDesigner()
        {
        }

        /// <summary>
        /// Allows a designer to change or remove items from the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.
        /// </summary>
        /// <param name="properties">The properties for the class of the component.</param>
        protected override void PostFilterProperties(IDictionary properties)
        {
            properties.Remove("BorderStyle");
            properties.Remove("RightToLeft");
            properties.Remove("Text");
            properties.Remove("BorderStyle");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            base.PostFilterProperties(properties);
        }
    } 
    
    #endregion

}