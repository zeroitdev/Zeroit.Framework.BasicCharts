// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-02-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="DaggerSplineGraph.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts
{
    /// <summary>
    /// Class ZeroitSplineGraph.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    [Designer(typeof(ZeroitSplineGraphDesigner))]
    [Serializable]
    public partial class ZeroitSplineGraph : Control, ISerializable
    {
        #region ENUMS
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
            /// The curved
            /// </summary>
            Curved
        }

        #endregion

        #region Private Fields
        /// <summary>
        /// The items
        /// </summary>
        private List<decimal> items = new List<decimal>(MAX_VALUE_COUNT);

        /// <summary>
        /// The show vertical lines
        /// </summary>
        private bool showVerticalLines = false;

        /// <summary>
        /// The show border
        /// </summary>
        private bool showBorder = false;

        /// <summary>
        /// The show title
        /// </summary>
        private bool showTitle = false;

        /// <summary>
        /// The show points
        /// </summary>
        private bool showPoints;

        /// <summary>
        /// The title alignment
        /// </summary>
        private StringAlignment titleAlignment = StringAlignment.Near;

        /// <summary>
        /// The point size
        /// </summary>
        private int pointSize = 7;

        //private Color backgroundColor = Color.FromArgb(102, 217, 174);

        //private Color belowLineColor = Color.FromArgb(24, 202, 142);

        /// <summary>
        /// The border color
        /// </summary>
        private Color borderColor = Color.White;

        /// <summary>
        /// The line color
        /// </summary>
        private Color lineColor = Color.DimGray;

        /// <summary>
        /// The vertical line color
        /// </summary>
        private Color verticalLineColor = Color.DimGray;

        /// <summary>
        /// The graph title color
        /// </summary>
        private Color graphTitleColor = Color.Gray;

        /// <summary>
        /// The graph title
        /// </summary>
        private string graphTitle = "Zeroit LineGraph";

        /// <summary>
        /// The graph style
        /// </summary>
        private ZeroitSplineGraph.Style graphStyle = ZeroitSplineGraph.Style.Material;

        /// <summary>
        /// The material
        /// </summary>
        private SplineMaterialColors material = new SplineMaterialColors();

        /// <summary>
        /// The randomize colors
        /// </summary>
        private bool randomizeColors = true;

        /// <summary>
        /// The spline axis
        /// </summary>
        private SplineAxis splineAxis = new SplineAxis();

        /// <summary>
        /// The show axis
        /// </summary>
        private bool showAxis = false;

        /// <summary>
        /// The point color
        /// </summary>
        private Color pointColor = Color.Red;

        #endregion

        #region Public Properties

        #region Color Transition

        #region ENUMS
        /// <summary>
        /// Enum BrushType
        /// </summary>
        public enum BrushType
        {
            /// <summary>
            /// The solid
            /// </summary>
            Solid,
            /// <summary>
            /// The gradient
            /// </summary>
            Gradient,
            /// <summary>
            /// The hatch
            /// </summary>
            Hatch
        }

        /// <summary>
        /// Enum SeedColor
        /// </summary>
        public enum SeedColor
        {
            /// <summary>
            /// The none
            /// </summary>
            None,
            /// <summary>
            /// The first color
            /// </summary>
            FirstColor,
            /// <summary>
            /// The second color
            /// </summary>
            SecondColor,
            /// <summary>
            /// The both
            /// </summary>
            Both
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// The seed colors
        /// </summary>
        private SeedColor seedColors = SeedColor.FirstColor;

        /// <summary>
        /// The colors
        /// </summary>
        private List<Color> colors = new List<Color>()
        {
            Color.FromArgb(249, 55, 98),
            Color.FromArgb(0, 162, 250),
            Color.LightPink,
            Color.Indigo,
            Color.Red,
            Color.Purple,
            Color.Salmon,
            Color.Lime,
            Color.Orange
        };

        /// <summary>
        /// The brush type
        /// </summary>
        private BrushType brushType = BrushType.Gradient;

        /// <summary>
        /// The hatch style
        /// </summary>
        private HatchStyle hatchStyle = HatchStyle.Percent05;

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
        /// Gets or sets the brush.
        /// </summary>
        /// <value>The brush.</value>
        public BrushType Brush
        {
            get { return brushType; }
            set
            {
                brushType = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the seed colors.
        /// </summary>
        /// <value>The seed colors.</value>
        public SeedColor SeedColors
        {
            get { return seedColors; }
            set
            {
                seedColors = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        /// <value>The colors.</value>
        public List<Color> Colors
        {
            get { return colors; }
            set
            {
                colors = value;
                Invalidate();
            }
        }
        #endregion

        #region Include In Paint Method

        //List<Color> colorsReversed = Colors;
        //colorsReversed.Reverse();

        //Random randColor1;

        //Random randColor2;

        //Random counter = new Random();

        //Brush brush;


        //switch (SeedColors)
        //{
        //    case SeedColor.None:
        //        randColor1 = new Random();
        //        randColor2 = new Random();
        //        switch (Brush)
        //        {
        //            case BrushType.Solid:
        //                brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

        //                break;
        //            case BrushType.Gradient:
        //                brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

        //                break;
        //            case BrushType.Hatch:
        //                brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }

        //        break;
        //    case SeedColor.FirstColor:
        //        randColor1 = new Random(Colors.Count - counter.Next(0, Colors.Count - 1));
        //        randColor2 = new Random();
        //        switch (Brush)
        //        {
        //            case BrushType.Solid:
        //                brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

        //                break;
        //            case BrushType.Gradient:
        //                brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

        //                break;
        //            case BrushType.Hatch:
        //                brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }

        //        break;
        //    case SeedColor.SecondColor:
        //        randColor1 = new Random();
        //        randColor2 = new Random(colorsReversed.Count - counter.Next(0, Colors.Count - 1));

        //        switch (Brush)
        //        {
        //            case BrushType.Solid:
        //                brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

        //                break;
        //            case BrushType.Gradient:
        //                brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

        //                break;
        //            case BrushType.Hatch:
        //                brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }


        //        break;
        //    case SeedColor.Both:
        //        randColor1 = new Random(Colors.Count - counter.Next(0, Colors.Count - 1));
        //        randColor2 = new Random(colorsReversed.Count - counter.Next(0, Colors.Count - 1));
        //        switch (Brush)
        //        {
        //            case BrushType.Solid:
        //                brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

        //                break;
        //            case BrushType.Gradient:
        //                brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

        //                break;
        //            case BrushType.Hatch:
        //                brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }
        //        break;
        //    default:
        //        throw new ArgumentOutOfRangeException();
        //}

        #endregion


        #endregion

        /// <summary>
        /// Gets or sets the color of the point.
        /// </summary>
        /// <value>The color of the point.</value>
        public Color PointColor
        {
            get { return pointColor; }
            set
            {
                pointColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show axis].
        /// </summary>
        /// <value><c>true</c> if [show axis]; otherwise, <c>false</c>.</value>
        public bool ShowAxis
	    {
	        get { return showAxis; }
	        set
	        {
                showAxis = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the axis.
        /// </summary>
        /// <value>The axis.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
	    public SplineAxis Axis
	    {
	        get { return splineAxis; }
	        set
	        {
                splineAxis = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets a value indicating whether [randomize colors].
        /// </summary>
        /// <value><c>true</c> if [randomize colors]; otherwise, <c>false</c>.</value>
        public bool RandomizeColors
	    {
	        get { return randomizeColors; }
	        set
	        {
                randomizeColors = value;
	            Invalidate();
	        }
	    }

        //[Browsable(true)]
        //[Category("Zeroit.Framework.DaggerControls")]
        //[Description("The color of the text when the tab is selected")]
        //public Color BackGroundColor
        //{
        //    get
        //    {
        //        return this.backgroundColor;
        //    }
        //    set
        //    {
        //        this.backgroundColor = value;
        //        Invalidate();
        //    }
        //}

        //[Browsable(true)]
        //[Category("Zeroit.Framework.DaggerControls")]
        //[Description("The color of the text when the tab is selected")]
        //public Color BelowLineColor
        //{
        //    get
        //    {
        //        return this.belowLineColor;
        //    }
        //    set
        //    {
        //        this.belowLineColor = value;
        //        Invalidate();
        //    }
        //}

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The color of the text when the tab is selected")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the graph style.
        /// </summary>
        /// <value>The graph style.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The style of the graph")]
        public ZeroitSplineGraph.Style GraphStyle
        {
            get
            {
                return this.graphStyle;
            }
            set
            {
                this.graphStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the graph title.
        /// </summary>
        /// <value>The graph title.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The of the graph")]
        public string GraphTitle
        {
            get
            {
                return this.graphTitle;
            }
            set
            {
                this.graphTitle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the graph title.
        /// </summary>
        /// <value>The color of the graph title.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The color of the graph title")]
        public Color GraphTitleColor
        {
            get
            {
                return this.graphTitleColor;
            }
            set
            {
                this.graphTitleColor = value;
                Invalidate();
            }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The color of the text when the tab is selected")]
        public List<decimal> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                this.items = value;
                Invalidate();
                //Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The color of the text when the tab is selected")]
        public Color LineColor
        {
            get
            {
                return this.lineColor;
            }
            set
            {
                this.lineColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the size of the point.
        /// </summary>
        /// <value>The size of the point.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The point size")]
        public int PointSize
        {
            get
            {
                return this.pointSize;
            }
            set
            {
                this.pointSize = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show border].
        /// </summary>
        /// <value><c>true</c> if [show border]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("Draw the border on the control")]
        public bool ShowBorder
        {
            get
            {
                return this.showBorder;
            }
            set
            {
                this.showBorder = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show points].
        /// </summary>
        /// <value><c>true</c> if [show points]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("Draw the points on each value")]
        public bool ShowPoints
        {
            get
            {
                return this.showPoints;
            }
            set
            {
                this.showPoints = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show title].
        /// </summary>
        /// <value><c>true</c> if [show title]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("Draw the title on the control")]
        public bool ShowTitle
        {
            get
            {
                return this.showTitle;
            }
            set
            {
                this.showTitle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show vertical lines].
        /// </summary>
        /// <value><c>true</c> if [show vertical lines]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The color of the text when the tab is selected")]
        public bool ShowVerticalLines
        {
            get
            {
                return this.showVerticalLines;
            }
            set
            {
                this.showVerticalLines = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the title alignment.
        /// </summary>
        /// <value>The title alignment.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The title alignment")]
        public StringAlignment TitleAlignment
        {
            get
            {
                return this.titleAlignment;
            }
            set
            {
                this.titleAlignment = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the vertical line.
        /// </summary>
        /// <value>The color of the vertical line.</value>
        [Browsable(true)]
        [Category("Zeroit.Framework.DaggerControls")]
        [Description("The color of the text when the tab is selected")]
        public Color VerticalLineColor
        {
            get
            {
                return this.verticalLineColor;
            }
            set
            {
                this.verticalLineColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>The material.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
	    public SplineMaterialColors Material
	    {
	        get { return material; }
	        set { material = value;
	            Invalidate();
	        }
	    }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSplineGraph"/> class.
        /// </summary>
        public ZeroitSplineGraph()
        {
            
            //base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);


            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            
            this.DoubleBuffered = true;

            base.Size = new System.Drawing.Size(200, 100);

            this.items.Add(50);
            this.items.Add(20);
            this.items.Add(100);
            this.items.Add(60);
            this.items.Add(1);
            this.items.Add(20);
            this.items.Add(80);
            this.items.Add(12);
            this.items.Add(72);
            this.items.Add(58);
            this.items.Add(19);

            perfChartStyle.BackgroundColorBottom = Color.Transparent;
            perfChartStyle.BackgroundColorTop = Color.Transparent;

            tmrRefresh.Tick += tmrRefresh_Tick;
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Gets the percent.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="maxNumber">The maximum number.</param>
        /// <returns>System.Single.</returns>
        public float GetPercent(float number, float maxNumber)
        {
            float num = number % maxNumber;
            if (number == maxNumber)
            {
                num = 100;
            }

            if (num == 0)
            {
                num = 1;
            }

            return num;
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            Pen pen = new Pen(this.LineColor, 1f);
            Pen pen1 = new Pen(this.VerticalLineColor, 1f);

            //g.Clear(Parent.BackColor);

            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }

            PerformanceOnPaint(e);

            #region Random Generated Colors

            List<Color> colorsReversed = Colors;
            colorsReversed.Reverse();

            Random randColor1;

            Random randColor2;

            Random counter = new Random();

            Brush brush;


            switch (SeedColors)
            {
                case SeedColor.None:
                    randColor1 = new Random();
                    randColor2 = new Random();
                    switch (Brush)
                    {
                        case BrushType.Solid:
                            brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

                            break;
                        case BrushType.Gradient:
                            brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                            break;
                        case BrushType.Hatch:
                            brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case SeedColor.FirstColor:
                    randColor1 = new Random(Colors.Count - counter.Next(0, Colors.Count - 1));
                    randColor2 = new Random();
                    switch (Brush)
                    {
                        case BrushType.Solid:
                            brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

                            break;
                        case BrushType.Gradient:
                            brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                            break;
                        case BrushType.Hatch:
                            brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case SeedColor.SecondColor:
                    randColor1 = new Random();
                    randColor2 = new Random(colorsReversed.Count - counter.Next(0, Colors.Count - 1));

                    switch (Brush)
                    {
                        case BrushType.Solid:
                            brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

                            break;
                        case BrushType.Gradient:
                            brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                            break;
                        case BrushType.Hatch:
                            brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }


                    break;
                case SeedColor.Both:
                    randColor1 = new Random(Colors.Count - counter.Next(0, Colors.Count - 1));
                    randColor2 = new Random(colorsReversed.Count - counter.Next(0, Colors.Count - 1));
                    switch (Brush)
                    {
                        case BrushType.Solid:
                            brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

                            break;
                        case BrushType.Gradient:
                            brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                            break;
                        case BrushType.Hatch:
                            brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            #endregion


            if (PerfChartStyle.ShowHorizontalGridLines == false && PerfChartStyle.ShowHorizontalGridLines == false)
            {
                if (this.graphStyle != ZeroitSplineGraph.Style.Material)
                {
                    g.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, base.Width, base.Height));
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Material.BackColor), new Rectangle(0, 0, base.Width, base.Height));
                }
            }

            decimal num = this.items.ToArray().Max();
            int width = base.Width / this.items.Count;
            int num1 = 0;
            int height = base.Height;
            int num2 = width;
            int height1 = 0;
            List<PointF> pointFs = new List<PointF>()
            {
                new Point(1, base.Height)
            };
            foreach (int item in this.items)
            {
                if (item <= 97)
                {
                    height1 = (item >= 3 ? base.Height - ((int)this.GetPercent(item, (int)num) * base.Height) / 100 : base.Height - ((int)this.GetPercent(3, (int)num) * base.Height) / 100);
                    
                }
                else
                {
                    height1 = base.Height - (int)this.GetPercent(97, (int)num) * base.Height / 100;
                }
                pointFs.Add(new Point(num2 - 1, height1 - 1));
                num1 = num2;
                height = height1;
                num2 = num2 + width;
            }
            pointFs.Add(new Point(base.Width, height1 - 1));
            if (this.graphStyle == ZeroitSplineGraph.Style.Curved)
            {
                if (this.ShowPoints)
                {
                    foreach (PointF pointF in pointFs)
                    {
                        if (pointF.Y - (float)(this.pointSize / 2) - 1f < 0f)
                        {
                            g.FillEllipse(new SolidBrush(this.PointColor), new RectangleF(pointF.X - (float)(this.pointSize / 2) - 1f, -1f, (float)this.pointSize, (float)this.pointSize));
                        }
                        else if (pointF.Y - (float)(this.pointSize / 2) - 1f + (float)this.pointSize <= (float)base.Height)
                        {
                            g.FillEllipse(new SolidBrush(this.PointColor), new RectangleF(pointF.X - (float)(this.pointSize / 2) - 1f, pointF.Y - (float)(this.pointSize / 2) - 1f, (float)this.pointSize, (float)this.pointSize));
                        }
                        else
                        {
                            g.FillEllipse(new SolidBrush(this.PointColor), new RectangleF(pointF.X - (float)(this.pointSize / 2) - 1f, (float)(base.Height - this.pointSize + 1), (float)this.pointSize, (float)this.pointSize));
                        }
                    }
                }
                g.DrawCurve(pen, pointFs.ToArray());
            }
            else
            {
                pointFs.Add(new Point(base.Width, base.Height));
                if (this.graphStyle != ZeroitSplineGraph.Style.Flat)
                {
                    LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, base.Width, base.Height), Colors[0], Colors[1], Material.GradientAngle);
                    if (RandomizeColors)
                    {
                        g.FillPolygon(linearGradientBrush, pointFs.ToArray());
                    }
                    else
                    {
                        g.FillPolygon(brush, pointFs.ToArray());
                    }
                    
                }
                else
                {
                    SolidBrush solidBrush = new SolidBrush(Colors[0]);

                    if (RandomizeColors)
                    {
                        g.FillPolygon(brush, pointFs.ToArray());
                    }
                    else
                    {
                        g.FillPolygon(solidBrush, pointFs.ToArray());
                    }

                    
                }
                num1 = 1;
                height = base.Height;
                num2 = width;
                height1 = 0;
                int num3 = 0;
                foreach (int item1 in this.items)
                {
                    if (item1 <= 97)
                    {
                        height1 = (item1 >= 3 ? base.Height - (int)this.GetPercent(item1, (int)num) * base.Height / 100 : base.Height - (int)this.GetPercent(3, (int)num) * base.Height / 100);
                    }
                    else
                    {
                        height1 = base.Height - (int)this.GetPercent(97, (int)num) * base.Height / 100;
                    }
                    if (this.graphStyle == ZeroitSplineGraph.Style.Flat)
                    {
                        if (this.showVerticalLines)
                        {
                            num3++;
                            if (num3 != (int)this.items.ToArray().Length)
                            {
                                if ((num2 == 0 ? false : num2 != base.Width))
                                {
                                    g.DrawLine(pen1, num2, base.Height, num2, 0);
                                }
                            }
                        }
                    }
                    g.DrawLine(pen, num1 - 1, height - 1, num2 - 1, height1 - 1);
                    if (this.ShowPoints)
                    {
                        if (height1 - this.pointSize / 2 - 1 < 0)
                        {
                            g.FillEllipse(new SolidBrush(this.PointColor), new RectangleF((float)(num2 - this.pointSize / 2 - 1), -1f, (float)this.pointSize, (float)this.pointSize));
                        }
                        else if (height1 - this.pointSize / 2 - 1 + this.pointSize <= base.Height)
                        {
                            g.FillEllipse(new SolidBrush(this.PointColor), new RectangleF((float)(num2 - this.pointSize / 2 - 1), (float)(height1 - this.pointSize / 2 - 1), (float)this.pointSize, (float)this.pointSize));
                        }
                        else
                        {
                            g.FillEllipse(new SolidBrush(this.PointColor), new RectangleF((float)(num2 - this.pointSize / 2 - 1), (float)(base.Height - this.pointSize + 1), (float)this.pointSize, (float)this.pointSize));
                        }
                    }
                    num1 = num2;
                    height = height1;
                    num2 = num2 + width;
                }
                g.DrawLine(pen, num1, height, base.Width, height);
            }
            if (this.graphStyle != ZeroitSplineGraph.Style.Material && this.showBorder)
            {
                g.DrawRectangle(new Pen(this.borderColor, 2f), new Rectangle(0, 0, base.Width - 1, base.Height - 1));
            }
            if (this.showTitle)
            {
                StringFormat stringFormat = new StringFormat()
                {
                    LineAlignment = StringAlignment.Near,
                    Alignment = this.titleAlignment
                };
                System.Drawing.Font font = new System.Drawing.Font("Arial", 14f);
                SolidBrush solidBrush1 = new SolidBrush(this.graphTitleColor);
                RectangleF rectangleF = new RectangleF(0f, 0f, (float)base.Width, (float)base.Height);
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                g.DrawString(this.graphTitle, font, solidBrush1, rectangleF, stringFormat);
            }

            if (ShowAxis)
            {
                //X-Axis
                g.DrawLine(new Pen(Axis.LineColor), new Point(5, Height - 5), new Point(Width - 5, Height - 5));
                g.DrawString(">", Axis.Font, new SolidBrush(Axis.LineColor), new PointF(Width - 10, Height - 12));

                //Y-Axis
                g.DrawLine(new Pen(Axis.LineColor), new Point(5, 0), new Point(5, Height - 5));
                g.DrawString("^", Axis.Font, new SolidBrush(Axis.LineColor), new PointF(0, -1));


                int divisor = Axis.Interval;
                decimal minValue = Items.Min();
                decimal maxValue = Items.Max();
                decimal value = ((maxValue) / divisor);
                decimal valueXaxis = ((maxValue) / (divisor));
                int x = 0;
                int y = 20;
                int j = 0;
                for (int i = 0; i < divisor; i++)
                {

                    int addition = (int)Items.Max() - ((int)value * j);
                    int additionYaxis = (int)Items.Min() + ((int)valueXaxis * j);

                    SizeF sZYaxis = g.MeasureString(additionYaxis.ToString(), Axis.Font);


                    g.DrawString(" " + addition.ToString(), Axis.Font, new SolidBrush(Axis.NumberColors), new PointF(5, x));

                    g.DrawString("- ", Axis.Font, new SolidBrush(Axis.LineColor), new PointF(2, x));

                    g.DrawString(" " + additionYaxis.ToString(), Axis.Font, new SolidBrush(Axis.NumberColors), new PointF(y, Height - 25));

                    g.DrawString("|", Axis.Font, new SolidBrush(Axis.LineColor), new PointF(y + sZYaxis.Width / 2, Height - 13));

                    x += Height / divisor;
                    y += Width / divisor;
                    j++;
                }

                g.DrawString("0", Axis.Font, new SolidBrush(Axis.NumberColors), new PointF(3, Height - 15));

            }

            #region Border

            switch (BorderStyle)
            {
                case Border3DStyle.None:

                    break;
                case Border3DStyle.Adjust:
                    ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Adjust);
                    break;
                case Border3DStyle.Bump:
                    ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Bump);
                    break;
                case Border3DStyle.Etched:
                    ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Etched);
                    break;
                case Border3DStyle.Flat:
                    ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Flat);
                    break;
                case Border3DStyle.Raised:
                    ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Raised);
                    break;
                case Border3DStyle.RaisedInner:
                    ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.RaisedInner);
                    break;
                case Border3DStyle.RaisedOuter:
                    ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.RaisedOuter);
                    break;
                case Border3DStyle.Sunken:
                    ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Sunken);
                    break;
                case Border3DStyle.SunkenInner:
                    ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.SunkenInner);
                    break;
                case Border3DStyle.SunkenOuter:
                    ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.SunkenOuter);
                    break;
                case Border3DStyle.Single:
                    g.DrawRectangle(new Pen(BorderColor), new Rectangle(0, 0, Width - 1, Height - 1));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            #endregion
        }

        /**/
        #endregion

        #region Serialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSplineGraph"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public ZeroitSplineGraph(SerializationInfo info, StreamingContext context)
	    {
	        items = (List<decimal>) info.GetValue("items", typeof(List<decimal>));
	        showPoints = info.GetBoolean("showPoints");

        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("items", items);
            info.AddValue("showPoints", showPoints);

        }

        #endregion

        #region Make Transparent





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




        #endregion
    }
}