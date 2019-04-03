// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-03-2018
// ***********************************************************************
// <copyright file="BarChart.cs" company="Zeroit Dev Technologies">
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

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts
{

    /// <summary>
    /// Class ZeroitBarChart.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(ZeroitBarChartDesigner))]
    public class ZeroitBarChart : Control
	{

        #region ENUMS
        /// <summary>
        /// Enum DrawType
        /// </summary>
        public enum DrawType
        {
            /// <summary>
            /// The gradient
            /// </summary>
            Gradient,
            /// <summary>
            /// The solid
            /// </summary>
            Solid,
            /// <summary>
            /// The hatch
            /// </summary>
            Hatch
        }


        /// <summary>
        /// Enum Aligning
        /// </summary>
        public enum Aligning
        {
            /// <summary>
            /// The near
            /// </summary>
            Near,
            /// <summary>
            /// The center
            /// </summary>
            Center,
            /// <summary>
            /// The far
            /// </summary>
            Far
        }

        /// <summary>
        /// Enum Orientation
        /// </summary>
        public enum Orientation
        {
            /// <summary>
            /// The horizontal
            /// </summary>
            Horizontal,
            /// <summary>
            /// The vertical
            /// </summary>
            Vertical
        }

        /// <summary>
        /// Enum SortStyle
        /// </summary>
        public enum SortStyle
        {
            /// <summary>
            /// The ascending
            /// </summary>
            Ascending,
            /// <summary>
            /// The descending
            /// </summary>
            Descending,
            /// <summary>
            /// The normal
            /// </summary>
            Normal
        }

        /// <summary>
        /// Enum Style
        /// </summary>
        public enum Style
        {
            /// <summary>
            /// The flat
            /// </summary>
            Flat,
            /// <summary>
            /// The material
            /// </summary>
            Material,
            /// <summary>
            /// The bootstrap
            /// </summary>
            Bootstrap
        }
        #endregion

        #region Private Fields

        /// <summary>
        /// The material colors
        /// </summary>
        private Material materialColors = new Material();
        /// <summary>
        /// The bootstrap colors
        /// </summary>
        private Bootstrap bootstrapColors = new Bootstrap();

        /// <summary>
        /// The draw mode
        /// </summary>
        private DrawType _drawMode = DrawType.Solid;

        /// <summary>
        /// The items
        /// </summary>
        private List<int> items = new List<int>();

        /// <summary>
        /// The filled gradient
        /// </summary>
        private Color[] filledGradient = new Color[]
        {
            Color.Beige,
            Color.Crimson
        };

        /// <summary>
        /// The linear gradient mode
        /// </summary>
        private LinearGradientMode linMode = LinearGradientMode.ForwardDiagonal;

        /// <summary>
        /// The grid size
        /// </summary>
        private float gridSize = 1f;

        /// <summary>
        /// The filled color
        /// </summary>
        private Color filledColor = Color.FromArgb(30, 33, 38);

        /// <summary>
        /// The unfilled color
        /// </summary>
        private Color unfilledColor = Color.FromArgb(37, 40, 49);

        /// <summary>
        /// The splitter color
        /// </summary>
        private Color splitterColor = Color.FromArgb(59, 62, 71);

        /// <summary>
        /// The text color
        /// </summary>
        private Color textColor = Color.FromArgb(120, 120, 120);

        /// <summary>
        /// The sorting
        /// </summary>
        private ZeroitBarChart.SortStyle sorting = ZeroitBarChart.SortStyle.Normal;

        /// <summary>
        /// The text alignment
        /// </summary>
        private ZeroitBarChart.Aligning textAlignment = ZeroitBarChart.Aligning.Far;

        /// <summary>
        /// The graph orientation
        /// </summary>
        private ZeroitBarChart.Orientation graphOrientation = ZeroitBarChart.Orientation.Vertical;

        /// <summary>
        /// The graph style
        /// </summary>
        private ZeroitBarChart.Style graphStyle = ZeroitBarChart.Style.Material;

        /// <summary>
        /// The show grid
        /// </summary>
        private bool showGrid = false;

        /// <summary>
        /// The hatch brushgradient1
        /// </summary>
        private Color[] hatchBrush = new Color[] { Color.Black, Color.Transparent };

        /// <summary>
        /// The hatch style
        /// </summary>
        private HatchStyle hatchStyle = HatchStyle.BackwardDiagonal;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the hatch style.
        /// </summary>
        /// <value>The hatch style.</value>
        public HatchStyle HatchStyle
	    {
	        get { return hatchStyle; }
	        set
	        {
	            hatchStyle = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the color hatch brush.
        /// </summary>
        /// <value>The color hatch brush.</value>
        public Color[] HatchBrush
	    {
	        get { return hatchBrush; }
	        set
	        {
	            hatchBrush = value;
	            Invalidate();
	        }
	    }


        /// <summary>
        /// Gets or sets the size of the grid.
        /// </summary>
        /// <value>The size of the grid.</value>
        public float GridSize
        {
            get { return gridSize; }
            set
            {
                gridSize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the filled gradient.
        /// </summary>
        /// <value>The filled gradient.</value>
        public Color[] FilledGradient
        {
            get { return filledGradient; }
            set
            {
                filledGradient = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the gradient mode.
        /// </summary>
        /// <value>The gradient mode.</value>
        public LinearGradientMode GradientMode
        {
            get { return linMode; }
            set
            {
                linMode = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the draw mode.
        /// </summary>
        /// <value>The draw mode.</value>
        public DrawType DrawMode
        {
            get { return _drawMode; }
            set
            {
                _drawMode = value;
                Invalidate();
            }

        }

        /// <summary>
        /// Gets or sets the color of the filled.
        /// </summary>
        /// <value>The color of the filled.</value>
        [Browsable(true)]

        [Description("The filled color")]
        public Color FilledColor
        {
            get
            {
                return this.filledColor;
            }
            set
            {
                this.filledColor = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the graph orientation.
        /// </summary>
        /// <value>The graph orientation.</value>
        [Browsable(true)]

        [Description("The orientation of the graph")]
        public ZeroitBarChart.Orientation GraphOrientation
        {
            get
            {
                return this.graphOrientation;
            }
            set
            {
                this.graphOrientation = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the graph style.
        /// </summary>
        /// <value>The graph style.</value>
        [Browsable(true)]

        [Description("The style of the graph")]
        public ZeroitBarChart.Style GraphStyle
        {
            get
            {
                return this.graphStyle;
            }
            set
            {
                this.graphStyle = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        [Browsable(true)]

        [Description("a collection of input numbers, will base the percentage of all numbers by the highest number")]
        public List<int> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                this.items = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show grid].
        /// </summary>
        /// <value><c>true</c> if [show grid]; otherwise, <c>false</c>.</value>
        [Browsable(true)]

        [Description("Show the item grid")]
        public bool ShowGrid
        {
            get
            {
                return this.showGrid;
            }
            set
            {
                this.showGrid = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the sorting.
        /// </summary>
        /// <value>The sorting.</value>
        [Browsable(true)]

        [Description("The item sorting style")]
        public ZeroitBarChart.SortStyle Sorting
        {
            get
            {
                return this.sorting;
            }
            set
            {
                this.sorting = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the splitter.
        /// </summary>
        /// <value>The color of the splitter.</value>
        [Browsable(true)]

        [Description("The splitter color")]
        public Color SplitterColor
        {
            get
            {
                return this.splitterColor;
            }
            set
            {
                this.splitterColor = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        /// <value>The text alignment.</value>
        [Browsable(true)]

        [Description("The text aligning")]
        public ZeroitBarChart.Aligning TextAlignment
        {
            get
            {
                return this.textAlignment;
            }
            set
            {
                this.textAlignment = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        [Browsable(true)]

        [Description("The text color")]
        public Color TextColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                this.textColor = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the unfilled.
        /// </summary>
        /// <value>The color of the unfilled.</value>
        [Browsable(true)]

        [Description("The unfilled color")]
        public Color UnfilledColor
        {
            get
            {
                return this.unfilledColor;
            }
            set
            {
                this.unfilledColor = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the material colors.
        /// </summary>
        /// <value>The material colors.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Material MaterialColors
	    {
	        get { return materialColors; }
	        set { materialColors = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the bootstrap colors.
        /// </summary>
        /// <value>The bootstrap colors.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
	    public Bootstrap BootstrapColors
	    {
	        get { return bootstrapColors; }
	        set { bootstrapColors = value; Invalidate(); }
	    }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitBarChart" /> class.
        /// </summary>
        public ZeroitBarChart()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;

            this.items.Clear();
            this.items.Add(50);
            this.items.Add(75);
            this.items.Add(10);
            this.items.Add(30);
            this.items.Add(90);
            this.items.Add(60);
            this.items.Add(80);
            this.items.Add(45);
            this.items.Add(70);
            this.items.Add(5);
            this.items.Add(25);
            this.items.Add(85);
            this.items.Add(55);
            this.items.Add(75);
            base.Size = new System.Drawing.Size(294, 200);
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Clears the items.
        /// </summary>
        public void ClearItems()
        {
            this.items = null;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, e.Graphics);
            }

            if (this.items == null)
            {
                e.Graphics.FillRectangle(new SolidBrush(this.unfilledColor), 0, 0, base.Width, base.Height);
            }
            else
            {
                if (this.graphStyle == ZeroitBarChart.Style.Flat)
                {
                    e.Graphics.FillRectangle(new SolidBrush(this.unfilledColor), 0, 0, base.Width, base.Height);
                    List<int> nums = new List<int>();
                    if (this.sorting == ZeroitBarChart.SortStyle.Normal)
                    {
                        nums = this.items;
                    }
                    if (this.sorting == ZeroitBarChart.SortStyle.Descending)
                    {
                        nums = this.items;
                        nums = (
                            from p in nums
                            orderby p descending
                            select p).ToList<int>();
                    }
                    if (this.sorting == ZeroitBarChart.SortStyle.Ascending)
                    {
                        nums = this.items;
                        nums.Sort();
                    }
                    int num = 0;
                    if (this.graphOrientation != ZeroitBarChart.Orientation.Horizontal)
                    {
                        int width = base.Width / this.items.Count;
                        decimal height = base.Height / 100;
                        foreach (int num1 in nums)
                        {
                            switch (_drawMode)
                            {
                                case DrawType.Gradient:
                                    LinearGradientBrush linBrush = new LinearGradientBrush(new RectangleF((float)num, (float)(base.Height - (int)(num1 * height)), (float)width, (float)base.Height), filledGradient[0], filledGradient[1], linMode);
                                    e.Graphics.FillRectangle(linBrush, new RectangleF((float)num, (float)(base.Height - (int)(num1 * height)), (float)width, (float)base.Height));
                                    break;
                                case DrawType.Solid:
                                    e.Graphics.FillRectangle(new SolidBrush(this.filledColor), new RectangleF((float)num, (float)(base.Height - (int)(num1 * height)), (float)width, (float)base.Height));
                                    break;
                                case DrawType.Hatch:

                                    HatchBrush HB = new HatchBrush(HatchStyle, HatchBrush[0], HatchBrush[1]);
                                    e.Graphics.FillRectangle(HB, new RectangleF((float)num, (float)(base.Height - (int)(num1 * height)), (float)width, (float)base.Height));
                                    
                                    break;
                                default:
                                    break;
                            }


                            StringFormat stringFormat = new StringFormat()
                            {
                                Alignment = StringAlignment.Center
                            };
                            if (this.textAlignment == ZeroitBarChart.Aligning.Near)
                            {
                                stringFormat.LineAlignment = StringAlignment.Near;
                            }
                            if (this.textAlignment == ZeroitBarChart.Aligning.Center)
                            {
                                stringFormat.LineAlignment = StringAlignment.Center;
                            }
                            if (this.textAlignment == ZeroitBarChart.Aligning.Far)
                            {
                                stringFormat.LineAlignment = StringAlignment.Far;
                            }
                            SolidBrush solidBrush = new SolidBrush(this.textColor);
                            RectangleF rectangleF = new RectangleF((float)num, 5f, (float)width, (float)(base.Height - 5));
                            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                            e.Graphics.DrawString(num1.ToString(), this.Font, solidBrush, rectangleF, stringFormat);
                            num = num + width;
                        }
                        if (this.showGrid)
                        {
                            num = 0;
                            foreach (int num2 in nums)
                            {
                                e.Graphics.DrawRectangle(new Pen(this.splitterColor, gridSize), new Rectangle(num, 0, num + width, base.Height));
                                num = num + width;
                            }
                            e.Graphics.DrawRectangle(new Pen(this.splitterColor, gridSize), 1, 1, base.Width, base.Height);
                        }
                    }
                    else
                    {
                        int height1 = base.Height / this.items.Count;
                        decimal width1 = base.Width / 100;
                        foreach (int num3 in nums)
                        {
                            switch (_drawMode)
                            {
                                case DrawType.Gradient:
                                    LinearGradientBrush linBrush = new LinearGradientBrush(new RectangleF(0f, (float)num, (float)((int)(num3 * width1)), (float)height1), filledGradient[0], filledGradient[1], linMode);

                                    e.Graphics.FillRectangle(new SolidBrush(this.filledColor), new RectangleF(0f, (float)num, (float)((int)(num3 * width1)), (float)height1));

                                    break;
                                case DrawType.Solid:
                                    e.Graphics.FillRectangle(new SolidBrush(this.filledColor), new RectangleF(0f, (float)num, (float)((int)(num3 * width1)), (float)height1));
                                    break;
                                case DrawType.Hatch:

                                    HatchBrush HB = new HatchBrush(HatchStyle, HatchBrush[0], HatchBrush[1]);
                                    e.Graphics.FillRectangle(HB, new RectangleF(0f, (float)num, (float)((int)(num3 * width1)), (float)height1));

                                    
                                    break;
                                default:
                                    break;
                            }
                            StringFormat stringFormat1 = new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center
                            };
                            if (this.textAlignment == ZeroitBarChart.Aligning.Near)
                            {
                                stringFormat1.Alignment = StringAlignment.Near;
                            }
                            if (this.textAlignment == ZeroitBarChart.Aligning.Center)
                            {
                                stringFormat1.Alignment = StringAlignment.Center;
                            }
                            if (this.textAlignment == ZeroitBarChart.Aligning.Far)
                            {
                                stringFormat1.Alignment = StringAlignment.Far;
                            }
                            SolidBrush solidBrush1 = new SolidBrush(this.textColor);
                            RectangleF rectangleF1 = new RectangleF(5f, (float)num, (float)(base.Width - 5), (float)height1);
                            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                            e.Graphics.DrawString(num3.ToString(), this.Font, solidBrush1, rectangleF1, stringFormat1);
                            num = num + height1;
                        }
                        if (this.showGrid)
                        {
                            num = 0;
                            foreach (int num4 in nums)
                            {
                                e.Graphics.DrawRectangle(new Pen(this.splitterColor, gridSize), new Rectangle(0, num, base.Width, num + height1));
                                num = num + height1;
                            }
                            e.Graphics.DrawRectangle(new Pen(this.splitterColor, gridSize), 1, 1, base.Width, base.Height);
                        }
                    }
                }
                if (this.graphStyle == ZeroitBarChart.Style.Material)
                {
                    e.Graphics.FillRectangle(new SolidBrush(MaterialColors.Background), 0, 0, base.Width, base.Height);
                    List<int> list = new List<int>();
                    if (this.sorting == ZeroitBarChart.SortStyle.Normal)
                    {
                        list = this.items;
                    }
                    if (this.sorting == ZeroitBarChart.SortStyle.Descending)
                    {
                        list = this.items;
                        list = (
                            from p in list
                            orderby p descending
                            select p).ToList<int>();
                    }
                    if (this.sorting == ZeroitBarChart.SortStyle.Ascending)
                    {
                        list = this.items;
                        list.Sort();
                    }
                    int num5 = 0;
                    List<Color> colors = MaterialColors.Colors;
                    int num6 = 0;
                    if (this.graphOrientation == ZeroitBarChart.Orientation.Vertical)
                    {
                        int width2 = base.Width / this.items.Count;
                        int height2 = base.Height / 100;
                        foreach (int num7 in list)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(colors[num6]), new RectangleF((float)num5, (float)(base.Height - num7 * height2), (float)width2, (float)base.Height));
                            StringFormat stringFormat2 = new StringFormat()
                            {
                                Alignment = StringAlignment.Center
                            };
                            SolidBrush solidBrush2 = new SolidBrush(colors[num6]);
                            RectangleF rectangleF2 = new RectangleF();
                            rectangleF2 = new RectangleF((float)num5, (float)(base.Height - num7 * height2) - this.Font.Size / 2f * 3f, (float)width2, this.Font.Size * 2f);
                            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                            e.Graphics.DrawString(num7.ToString(), this.Font, solidBrush2, rectangleF2, stringFormat2);
                            num5 = num5 + width2;
                            num6++;
                            if (num6 == 14)
                            {
                                colors.Reverse();
                                num6 = 0;
                            }
                        }
                    }
                    if (this.graphOrientation == ZeroitBarChart.Orientation.Horizontal)
                    {
                        int height3 = base.Height / this.items.Count;
                        int width3 = base.Width / 100;
                        foreach (int num8 in list)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(colors[num6]), new RectangleF(0f, (float)num5, (float)(num8 * width3), (float)height3));
                            StringFormat stringFormat3 = new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Near
                            };
                            SolidBrush solidBrush3 = new SolidBrush(colors[num6]);
                            RectangleF rectangleF3 = new RectangleF();
                            rectangleF3 = new RectangleF((float)(num8 * width3), (float)num5, (float)(base.Width - num8 * width3), (float)height3);
                            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                            e.Graphics.DrawString(num8.ToString(), this.Font, solidBrush3, rectangleF3, stringFormat3);
                            num5 = num5 + height3;
                            num6++;
                            if (num6 == 14)
                            {
                                colors.Reverse();
                                num6 = 0;
                            }
                        }
                    }
                }
                if (this.graphStyle == ZeroitBarChart.Style.Bootstrap)
                {
                    e.Graphics.FillRectangle(new SolidBrush(BootstrapColors.Background), 0, 0, base.Width, base.Height);
                    List<int> nums1 = new List<int>();
                    if (this.sorting == ZeroitBarChart.SortStyle.Normal)
                    {
                        nums1 = this.items;
                    }
                    if (this.sorting == ZeroitBarChart.SortStyle.Descending)
                    {
                        nums1 = this.items;
                        nums1 = (
                            from p in nums1
                            orderby p descending
                            select p).ToList<int>();
                    }
                    if (this.sorting == ZeroitBarChart.SortStyle.Ascending)
                    {
                        nums1 = this.items;
                        nums1.Sort();
                    }
                    int num9 = 0;
                    if (this.graphOrientation != ZeroitBarChart.Orientation.Horizontal)
                    {
                        int width4 = base.Width / this.items.Count;
                        decimal height4 = base.Height / 100;
                        foreach (int num10 in nums1)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(BootstrapColors.OrientBackground), new RectangleF((float)num9, (float)(base.Height - (int)(num10 * height4)), (float)width4, (float)base.Height));
                            StringFormat stringFormat4 = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Near
                            };
                            SolidBrush solidBrush4 = new SolidBrush(BootstrapColors.TextColor);
                            RectangleF rectangleF4 = new RectangleF((float)num9, 5f, (float)width4, (float)(base.Height - 5));
                            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                            e.Graphics.DrawString(num10.ToString(), this.Font, solidBrush4, rectangleF4, stringFormat4);
                            num9 = num9 + width4;
                        }
                        num9 = 0;
                        foreach (int num11 in nums1)
                        {
                            e.Graphics.DrawRectangle(new Pen(BootstrapColors.GridColors, BootstrapColors.BorderWidth), new Rectangle(num9, 0, num9 + width4, base.Height));
                            num9 = num9 + width4;
                        }
                        e.Graphics.DrawRectangle(new Pen(BootstrapColors.GridColors, BootstrapColors.BorderWidth), 1, 1, base.Width, base.Height);
                    }
                    else
                    {
                        int height5 = base.Height / this.items.Count;
                        decimal width5 = base.Width / 100;
                        foreach (int num12 in nums1)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(BootstrapColors.OrientBackground), new RectangleF(0f, (float)num9, (float)((int)(num12 * width5)), (float)height5));
                            StringFormat stringFormat5 = new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Far
                            };
                            SolidBrush solidBrush5 = new SolidBrush(BootstrapColors.TextColor);
                            RectangleF rectangleF5 = new RectangleF(5f, (float)num9, (float)(base.Width - 5), (float)height5);
                            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                            e.Graphics.DrawString(num12.ToString(), this.Font, solidBrush5, rectangleF5, stringFormat5);
                            num9 = num9 + height5;
                        }
                        num9 = 0;
                        foreach (int num13 in nums1)
                        {
                            e.Graphics.DrawRectangle(new Pen(BootstrapColors.GridColors, BootstrapColors.BorderWidth), new Rectangle(0, num9, base.Width, num9 + height5));
                            num9 = num9 + height5;
                        }
                        e.Graphics.DrawRectangle(new Pen(BootstrapColors.GridColors, BootstrapColors.BorderWidth), 1, 1, base.Width, base.Height);
                    }
                }
            }

            base.OnPaint(e);

        }

        #endregion






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

}