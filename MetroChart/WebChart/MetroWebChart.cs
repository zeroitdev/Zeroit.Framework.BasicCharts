// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-04-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="MetroWebChart.cs" company="Zeroit Dev Technologies">
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts.Metro
{
    /// <summary>
    /// A class collection for rendering a metro-style web chart.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [DefaultEvent("Click")]
	[Description("A class for creating a web chart.")]
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(ZeroitMetroWebChart))]
    [Designer(typeof(ZeroitMetroWebChartDesigner))]
    
    public class ZeroitMetroWebChart : Control
	{
        #region ENUM
        /// <summary>
        /// Enum CornerShapes
        /// </summary>
        public enum CornerShapes
	    {
            /// <summary>
            /// The rectangular
            /// </summary>
            Rectangular,
            /// <summary>
            /// The circular
            /// </summary>
            Circular
        }

        /// <summary>
        /// Enum FillModes
        /// </summary>
        public enum FillModes
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
            /// The multi gradient
            /// </summary>
            MultiGradient,
            /// <summary>
            /// The hatch
            /// </summary>
            Hatch
        }
        #endregion

        #region Private Fields
        /// <summary>
        /// The point collection
        /// </summary>
        private ZeroitMetroWebChartPointCollection _PointCollection = new ZeroitMetroWebChartPointCollection()
        {
            //new ZeroitMetroWebChartPoint()
            //{
            //    Color = Color.Blue,
            //    Text = "Point 1",
            //    Value = 0

            //},
            //new ZeroitMetroWebChartPoint()
            //{
            //    Color = Color.Red,
            //    Text = "Point 2",
            //    Value = 50

            //},
            //new ZeroitMetroWebChartPoint()
            //{
            //    Color = Color.Yellow,
            //    Text = "Point 3",
            //    Value = 100

            //}
        };
        /// <summary>
        /// The web points
        /// </summary>
        private ZeroitMetroWebChartPoint webPoints = new ZeroitMetroWebChartPoint();
        /// <summary>
        /// The web chart points
        /// </summary>
        private List<ZeroitMetroWebChartPoint> webChartPoints = new List<ZeroitMetroWebChartPoint>();
        /// <summary>
        /// The enumerator
        /// </summary>
        IEnumerator<ZeroitMetroWebChartPoint> enumerator = null;
        /// <summary>
        /// The enumerator1
        /// </summary>
        IEnumerator<ZeroitMetroWebChartPoint> enumerator1 = null;

        /// <summary>
        /// The tool tip
        /// </summary>
        [AccessedThroughProperty("ToolTip")]
        private System.Windows.Forms.ToolTip toolTip = new ToolTip();

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style = Design.Style.Custom;

        /// <summary>
        /// The chart width
        /// </summary>
        private int _ChartWidth = 220;

        /// <summary>
        /// The point size
        /// </summary>
        private int _PointSize = 6;

        /// <summary>
        /// The inner structure stages
        /// </summary>
        private int _InnerStructureStages = 3;

        /// <summary>
        /// The web point width
        /// </summary>
        private int _WebPointWidth = 4;

        /// <summary>
        /// The web border width
        /// </summary>
        private int _WebBorderWidth = 2;

        /// <summary>
        /// The inner structure width
        /// </summary>
        private int _InnerStructureWidth = 2;

        /// <summary>
        /// The full circle
        /// </summary>
        private const int FullCircle = 360;

        /// <summary>
        /// The web border is gradient
        /// </summary>
        private bool _WebBorderIsGradient = false;

        /// <summary>
        /// The draw web points
        /// </summary>
        private bool _DrawWebPoints = true;

        //private bool _DrawDesignModeControl;

        /// <summary>
        /// The draw inner structure
        /// </summary>
        private bool _DrawInnerStructure = true;

        /// <summary>
        /// The show tool tip
        /// </summary>
        private bool _ShowToolTip = false;

        /// <summary>
        /// The fill color
        /// </summary>
        private Color _FillColor;

        /// <summary>
        /// The fill second color
        /// </summary>
        private Color _FillSecondColor;

        /// <summary>
        /// The web border color
        /// </summary>
        private Color _WebBorderColor;

        /// <summary>
        /// The outer structure border
        /// </summary>
        private Color _OuterStructureBorder;

        /// <summary>
        /// The design mode color
        /// </summary>
        private Color _DesignModeColor;

        /// <summary>
        /// The corner border color
        /// </summary>
        private Color _CornerBorderColor;

        /// <summary>
        /// The corner fill color
        /// </summary>
        private Color _CornerFillColor;

        /// <summary>
        /// The inner structure color
        /// </summary>
        private Color _InnerStructureColor;

        /// <summary>
        /// The hatch style
        /// </summary>
        private System.Drawing.Drawing2D.HatchStyle _HatchStyle;

        /// <summary>
        /// The fill mode
        /// </summary>
        private ZeroitMetroWebChart.FillModes _FillMode;

        /// <summary>
        /// The corner shape
        /// </summary>
        private ZeroitMetroWebChart.CornerShapes _CornerShape = ZeroitMetroWebChart.CornerShapes.Circular;

        /// <summary>
        /// The outer points
        /// </summary>
        private List<Rectangle> _OuterPoints = new List<Rectangle>();

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        /// <summary>
        /// The mouse state
        /// </summary>
        private Helpers.MouseState _MouseState;

        /// <summary>
        /// The hot index
        /// </summary>
        private int _HotIndex = -1;

        /// <summary>
        /// The hot rectangle
        /// </summary>
        private Rectangle _HotRectangle = new Rectangle();

        /// <summary>
        /// The bezier curve
        /// </summary>
        private bool bezierCurve = false;

        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets or sets the tool tip.
        /// </summary>
        /// <value>The tool tip.</value>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual System.Windows.Forms.ToolTip ToolTip
        {

            get
            {
                return this.toolTip;
            }

            set
            {
                this.toolTip = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable/disable automatic style.
        /// </summary>
        /// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to enable/disable automatic style.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AutoStyle
        {
            get
            {
                return this._AutoStyle;
            }
            set
            {
                if (this._AutoStyle != value)
                {
                    this._AutoStyle = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the chart.
        /// </summary>
        /// <value>The width of the chart.</value>
        [Category("Appearance")]
        [DefaultValue(220)]
        [Description("Sets the width of the chart.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ChartWidth
        {
            get
            {
                return this._ChartWidth;
            }
            set
            {
                if (this._ChartWidth != value && value > 10)
                {
                    this._ChartWidth = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.
        /// </summary>
        /// <value>The context menu strip.</value>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {

            get;
            set;
        }

        /// <summary>
        /// Gets or sets the color of the border's corner.
        /// </summary>
        /// <value>The color of the border's corner.</value>
        [Category("Appearance")]
        [Description("The color of the border's corner.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color CornerBorderColor
        {
            get
            {
                return this._CornerBorderColor;
            }
            set
            {
                if (this._CornerBorderColor != value)
                {
                    this._CornerBorderColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the corner fill.
        /// </summary>
        /// <value>The color of the corner fill.</value>
        [Category("Appearance")]
        [Description("Sets the color of the corner fill.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color CornerFillColor
        {
            get
            {
                return this._CornerFillColor;
            }
            set
            {
                if (this._CornerFillColor != value)
                {
                    this._CornerFillColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the corner shape.
        /// </summary>
        /// <value>The corner shape.</value>
        [Category("Appearance")]
        [DefaultValue(1)]
        [Description("The corner shape.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ZeroitMetroWebChart.CornerShapes CornerShape
        {
            get
            {
                return this._CornerShape;
            }
            set
            {
                if (this._CornerShape != value)
                {
                    this._CornerShape = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of control when in design mode.
        /// </summary>
        /// <value>The color of the design mode.</value>
        [Category("Appearance")]
        [Description("Sets the color of control when in design mode.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DesignModeColor
        {
            get
            {
                return this._DesignModeColor;
            }
            set
            {
                if (this._DesignModeColor != value)
                {
                    this._DesignModeColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw inner structure.
        /// </summary>
        /// <value><c>true</c> if draw inner structure; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to draw inner structure.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool DrawInnerStructure
        {
            get
            {
                return this._DrawInnerStructure;
            }
            set
            {
                if (this._DrawInnerStructure != value)
                {
                    this._DrawInnerStructure = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw web points.
        /// </summary>
        /// <value><c>true</c> if draw web points; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to draw web points.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool DrawWebPoints
        {
            get
            {
                return this._DrawWebPoints;
            }
            set
            {
                if (this._DrawWebPoints != value)
                {
                    this._DrawWebPoints = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        [Category("Appearance")]
        [Description("Sets the color of the fill.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FillColor
        {
            get
            {
                return this._FillColor;
            }
            set
            {
                if (this._FillColor != value)
                {
                    this._FillColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the fill mode.
        /// </summary>
        /// <value>The fill mode.</value>
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("Sets the fill mode.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ZeroitMetroWebChart.FillModes FillMode
        {
            get
            {
                return this._FillMode;
            }
            set
            {
                if (this._FillMode != value)
                {
                    this._FillMode = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the second fill.
        /// </summary>
        /// <value>The color of the fill second.</value>
        [Category("Appearance")]
        [Description("Sets the color of the second fill.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FillSecondColor
        {
            get
            {
                return this._FillSecondColor;
            }
            set
            {
                if (this._FillSecondColor != value)
                {
                    this._FillSecondColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the hatch style.
        /// </summary>
        /// <value>The hatch style.</value>
        [Category("Appearance")]
        [DefaultValue(3)]
        [Description("Sets the hatch style.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public System.Drawing.Drawing2D.HatchStyle HatchStyle
        {
            get
            {
                return this._HatchStyle;
            }
            set
            {
                if (this._HatchStyle != value)
                {
                    this._HatchStyle = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the inner structure.
        /// </summary>
        /// <value>The color of the inner structure.</value>
        [Category("Appearance")]
        [Description("Sets the color of the inner structure.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color InnerStructureColor
        {
            get
            {
                return this._InnerStructureColor;
            }
            set
            {
                if (this._InnerStructureColor != value)
                {
                    this._InnerStructureColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the inner structure stages.
        /// </summary>
        /// <value>The inner structure stages.</value>
        [Category("Appearance")]
        [DefaultValue(3)]
        [Description("The inner structure stages.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int InnerStructureStages
        {
            get
            {
                return this._InnerStructureStages;
            }
            set
            {
                if (this._InnerStructureStages != value)
                {
                    this._InnerStructureStages = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the inner structure.
        /// </summary>
        /// <value>The width of the inner structure.</value>
        [Category("Appearance")]
        [DefaultValue(2)]
        [Description("The width of the inner structure.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int InnerStructureWidth
        {
            get
            {
                return this._InnerStructureWidth;
            }
            set
            {
                if (this._InnerStructureWidth != value)
                {
                    this._InnerStructureWidth = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the outer structure border.
        /// </summary>
        /// <value>The outer structure border.</value>
        [Category("Appearance")]
        [Description("The outer structure border.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color OuterStructureBorder
        {
            get
            {
                return this._OuterStructureBorder;
            }
            set
            {
                if (this._OuterStructureBorder != value)
                {
                    this._OuterStructureBorder = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        [Browsable(true)]
        [Category("Data")]
        [Description("The points.")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ZeroitMetroWebChartPointCollection Points
        {
            get
            {
                return this._PointCollection;
            }
            //set
            //{
            //    _PointCollection = value;
            //    Invalidate();
            //}
        }

        /// <summary>
        /// Gets or sets the web points.
        /// </summary>
        /// <value>The web points.</value>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ZeroitMetroWebChartPoint WebPoints
        {
            get { return webPoints; }
            set
            {
                webPoints = value;
                Invalidate();

            }
        }

        /// <summary>
        /// Gets or sets the size of the point.
        /// </summary>
        /// <value>The size of the point.</value>
        [Category("Appearance")]
        [DefaultValue(6)]
        [Description("The size of the point.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int PointSize
        {
            get
            {
                return this._PointSize;
            }
            set
            {
                if (this._PointSize != value && value >= 2)
                {
                    this._PointSize = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.
        /// </summary>
        /// <value>The right to left.</value>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.RightToLeft RightToLeft
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show tool tip.
        /// </summary>
        /// <value><c>true</c> if show tool tip; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to show tool tip.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowToolTip
        {
            get
            {
                return this._ShowToolTip;
            }
            set
            {
                if (this._ShowToolTip != value)
                {
                    this._ShowToolTip = value;
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
        [DefaultValue(0)]
        [Description("Sets the style.")]
        [RefreshProperties(RefreshProperties.All)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Design.Style Style
        {
            get
            {
                return this._Style;
            }
            set
            {
                if (value != this._Style)
                {
                    this._Style = value;
                    switch (value)
                    {
                        case Design.Style.Light:
                            {
                                this._FillColor = Color.FromArgb(50, Design.MetroColors.AccentBlue);
                                this._FillSecondColor = Design.MetroColors.ChangeColorBrightness(Design.MetroColors.AccentBlue, 0.3f);
                                this._WebBorderColor = Color.FromArgb(100, Design.MetroColors.ChangeColorBrightness(Design.MetroColors.AccentBlue, -0.2f));
                                this._OuterStructureBorder = Design.MetroColors.LightBorder;
                                this._DesignModeColor = Design.MetroColors.LightBorder;
                                this._CornerBorderColor = Design.MetroColors.LightBorder;
                                this._CornerFillColor = Design.MetroColors.ChangeColorBrightness(Design.MetroColors.LightBorder, 0.2f);
                                this._InnerStructureColor = Color.FromArgb(100, Design.MetroColors.LightBorder);
                                this.ForeColor = Design.MetroColors.LightFont;
                                break;
                            }
                        case Design.Style.Dark:
                            {
                                this._FillColor = Color.FromArgb(50, Design.MetroColors.AccentBlue);
                                this._FillSecondColor = Design.MetroColors.ChangeColorBrightness(Design.MetroColors.AccentBlue, 0.3f);
                                this._WebBorderColor = Color.FromArgb(100, Design.MetroColors.ChangeColorBrightness(Design.MetroColors.AccentBlue, -0.2f));
                                this._OuterStructureBorder = Design.MetroColors.LightBorder;
                                this._DesignModeColor = Design.MetroColors.LightBorder;
                                this._CornerBorderColor = Design.MetroColors.LightBorder;
                                this._CornerFillColor = Design.MetroColors.ChangeColorBrightness(Design.MetroColors.LightBorder, 0.2f);
                                this._InnerStructureColor = Color.FromArgb(100, Design.MetroColors.LightBorder);
                                this.ForeColor = Design.MetroColors.DarkFont;
                                break;
                            }
                        default:
                            {
                                this._AutoStyle = false;
                                break;
                            }
                    }
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the color of the web border.
        /// </summary>
        /// <value>The color of the web border.</value>
        [Category("Appearance")]
        [Description("Sets the color of the web border.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color WebBorderColor
        {
            get
            {
                return this._WebBorderColor;
            }
            set
            {
                if (this._WebBorderColor != value)
                {
                    this._WebBorderColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether web border is gradient.
        /// </summary>
        /// <value><c>true</c> if web border is gradient; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether web border is gradient.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool WebBorderIsGradient
        {
            get
            {
                return this._WebBorderIsGradient;
            }
            set
            {
                if (this._WebBorderIsGradient != value)
                {
                    this._WebBorderIsGradient = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the web border.
        /// </summary>
        /// <value>The width of the web border.</value>
        [Category("Appearance")]
        [DefaultValue(2)]
        [Description("Sets the width of the web border.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int WebBorderWidth
        {
            get
            {
                return this._WebBorderWidth;
            }
            set
            {
                if (this._WebBorderWidth != value)
                {
                    this._WebBorderWidth = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the web point.
        /// </summary>
        /// <value>The width of the web point.</value>
        [Category("Appearance")]
        [DefaultValue(4)]
        [Description("Sets the width of the web point.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int WebPointWidth
        {
            get
            {
                return this._WebPointWidth;
            }
            set
            {
                if (this._WebPointWidth != value)
                {
                    this._WebPointWidth = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [bezier curve].
        /// </summary>
        /// <value><c>true</c> if [bezier curve]; otherwise, <c>false</c>.</value>
        public bool BezierCurve
	    {
	        get { return bezierCurve; }
	        set { bezierCurve = value;
	            Invalidate();
	        }
	    }

        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMetroWebChart" /> class.
        /// </summary>
        public ZeroitMetroWebChart()
        {
            //this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);


            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            
            
            this._FillColor = Color.FromArgb(50, Design.MetroColors.AccentBlue);
            this._FillSecondColor = Design.MetroColors.ChangeColorBrightness(Design.MetroColors.AccentBlue, 0.3f);
            this._WebBorderColor = Color.FromArgb(100, Design.MetroColors.ChangeColorBrightness(Design.MetroColors.AccentBlue, -0.2f));
            this._OuterStructureBorder = Design.MetroColors.LightBorder;
            this._DesignModeColor = Design.MetroColors.LightBorder;
            this._CornerBorderColor = Design.MetroColors.LightBorder;
            this._CornerFillColor = Design.MetroColors.ChangeColorBrightness(Design.MetroColors.LightBorder, 0.2f);
            this._InnerStructureColor = Color.FromArgb(100, Design.MetroColors.LightBorder);
            this._HatchStyle = System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal;
            this._FillMode = ZeroitMetroWebChart.FillModes.Gradient;
            
           
            this._AutoStyle = true;
            this._MouseState = Helpers.MouseState.None;
            
            
            this.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.Size = new System.Drawing.Size(250, 250);
            IncludeInConstructor();
            this.UpdateStyles();
            Invalidate();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Draws the design control.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="r">The r.</param>
        protected void DrawDesignControl(Graphics g, Rectangle r)
        {
            using (Pen pen = new Pen(this._DesignModeColor))
            {
                g.DrawEllipse(pen, r);
                using (SolidBrush solidBrush = new SolidBrush(this._DesignModeColor))
                {
                    StringFormat stringFormat = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    using (StringFormat stringFormat1 = stringFormat)
                    {
                        g.DrawString("Points will appear on circle, once data added!\r\n(Will disappear once loaded)", new System.Drawing.Font(this.Font.FontFamily, 6f), solidBrush, r, stringFormat1);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the inner structure grid.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <param name="outerPoints">The outer points.</param>
        /// <param name="stageCount">The stage count.</param>
        private void DrawInnerStructureGrid(Graphics g, Point centerPoint, Point[] outerPoints, int stageCount)
        {
            int num = 0;
            int num1 = stageCount;
            int num2 = 1;
            while (num2 <= num1)
            {
                if (num2 != stageCount)
                {
                    num = checked(num + checked((int)Math.Round(100 / (double)stageCount)));
                    List<Point> points = new List<Point>();
                    using (Pen pen = new Pen(this._InnerStructureColor, (float)this._InnerStructureWidth))
                    {
                        int length = checked(checked((int)outerPoints.Length) - 1);
                        for (int i = 0; i <= length; i = checked(i + 1))
                        {
                            points.Add(this.GetPointOnLine(centerPoint, outerPoints[i], num));
                            g.DrawLine(pen, centerPoint, outerPoints[i]);
                        }

                        if (!BezierCurve)
                        {
                            g.DrawPolygon(pen, points.ToArray());
                        }
                        else
                        {
                            g.DrawClosedCurve(pen, points.ToArray());
                        }
                        
                    }
                    num2 = checked(num2 + 1);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the point on line.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="perc">The perc.</param>
        /// <returns>Point.</returns>
        private Point GetPointOnLine(Point p1, Point p2, int perc)
        {
            float x = (float)((double)(checked(p2.X - p1.X)) * ((double)perc / 100) + (double)p1.X);
            float y = (float)((double)(checked(p2.Y - p1.Y)) * ((double)perc / 100) + (double)p1.Y);
            Point point = new Point(checked((int)Math.Round((double)x)), checked((int)Math.Round((double)y)));
            return point;
        }



        /// <summary>
        /// Draws the web.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="item">The item.</param>
        /// <param name="point1">The point1.</param>
        /// <param name="points">The points.</param>
        /// <param name="points1">The points1.</param>
        /// <param name="enumerator">The enumerator.</param>
        /// <param name="enumerator1">The enumerator1.</param>
        /// <param name="graphics">The graphics.</param>
        /// <param name="linearGradientBrush">The linear gradient brush.</param>
        private void DrawWeb(
            Point point,
            Point item,
            Point point1,
            List<Point> points,
            List<Point> points1,
            IEnumerator<ZeroitMetroWebChartPoint> enumerator,
            IEnumerator<ZeroitMetroWebChartPoint> enumerator1,
            Graphics graphics,
            Brush linearGradientBrush
            )
        {
            if (this.Points.Count >= 3)
            {
                
                int num = 0;

                enumerator = this.Points.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ZeroitMetroWebChartPoint current = enumerator.Current;
                    num = checked(num + checked((int)Math.Round(360 / (double)this.Points.Count)));
                    Point point2 = new Point(checked((int)Math.Round((double)point1.X + (double)(checked(point1.X - this._PointSize)) * Math.Cos((double)num * 3.14159265358979 / 180))), checked((int)Math.Round((double)point1.Y + (double)(checked(point1.X - this._PointSize)) * Math.Sin((double)num * 3.14159265358979 / 180))));
                    points1.Add(point2);
                    points.Add(this.GetPointOnLine(point2, point1, checked(100 - current.Value)));
                }

                if (this._DrawInnerStructure)
                {
                    this.DrawInnerStructureGrid(graphics, point1, points1.ToArray(), this._InnerStructureStages);
                }
                using (Pen pen = new Pen(this._WebBorderColor, (float)this._WebBorderWidth))
                {
                    if (!this._WebBorderIsGradient)
                    {
                        if (!BezierCurve)
                        {
                            graphics.DrawPolygon(pen, points.ToArray());
                        }
                        else
                        {
                            graphics.DrawClosedCurve(pen, points.ToArray());
                        }
                        
                    }
                    else
                    {
                        int count = checked(points.Count - 1);
                        for (int i = 0; i <= count; i = checked(i + 1))
                        {
                            using (LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(points[i], (i == checked(points.Count - 1) ? points[0] : points[checked(i + 1)]), this.Points[i].Color, ((i == checked(points.Count - 1) ? this.Points[0] : this.Points[checked(i + 1)])).Color))
                            {
                                using (Pen pen1 = new Pen(linearGradientBrush1, (float)this._WebBorderWidth))
                                {
                                    graphics.DrawLine(pen1, points[i], (i == checked(points.Count - 1) ? points[0] : points[checked(i + 1)]));
                                }
                            }
                        }
                    }
                    pen.Color = this._OuterStructureBorder;
                    if (!BezierCurve)
                    {
                        graphics.DrawPolygon(pen, points1.ToArray());
                    }
                    else
                    {
                        graphics.DrawClosedCurve(pen, points1.ToArray());
                    }
                    
                }
                switch (this._FillMode)
                {
                    case ZeroitMetroWebChart.FillModes.Gradient:
                        {
                            point = new Point(checked((int)Math.Round((double)this._ChartWidth / 2)), this._ChartWidth);
                            item = new Point(checked((int)Math.Round((double)this._ChartWidth / 2)), 0);
                            linearGradientBrush = new LinearGradientBrush(point, item, this._FillColor, this._FillSecondColor);
                            break;
                        }
                    case ZeroitMetroWebChart.FillModes.MultiGradient:
                        {
                            List<Color> colors = new List<Color>();


                            enumerator1 = this.Points.GetEnumerator();
                            while (enumerator1.MoveNext())
                            {
                                colors.Add(enumerator1.Current.Color);
                            }

                            PathGradientBrush pathGradientBrush = new PathGradientBrush(points.ToArray())
                            {
                                CenterColor = this._FillColor,
                                SurroundColors = colors.ToArray()
                            };

                            linearGradientBrush = pathGradientBrush;
                            break;
                        }
                    case ZeroitMetroWebChart.FillModes.Hatch:
                        {
                            linearGradientBrush = new HatchBrush(this._HatchStyle, this._FillColor, this._FillSecondColor);
                            break;
                        }
                    default:
                        {
                            linearGradientBrush = new SolidBrush(this._FillColor);
                            break;
                        }
                }

                if (!BezierCurve)
                {
                    graphics.FillPolygon(linearGradientBrush, points.ToArray());
                }
                else
                {
                    graphics.FillClosedCurve(linearGradientBrush, points.ToArray());
                }
                
                linearGradientBrush.Dispose();
                int count1 = checked(points1.Count - 1);
                for (int j = 0; j <= count1; j = checked(j + 1))
                {
                    item = points1[j];
                    int x = checked(item.X - checked((int)Math.Round((double)this._PointSize / 2)));
                    point = points1[j];
                    Rectangle rectangle1 = new Rectangle(x, checked(point.Y - checked((int)Math.Round((double)this._PointSize / 2))), this._PointSize, this._PointSize);
                    using (SolidBrush solidBrush = new SolidBrush(this._CornerFillColor))
                    {
                        using (Pen pen2 = new Pen(this._CornerBorderColor))
                        {
                            if (this._CornerShape != ZeroitMetroWebChart.CornerShapes.Rectangular)
                            {
                                graphics.DrawEllipse(pen2, rectangle1);
                                graphics.FillEllipse(solidBrush, rectangle1);
                            }
                            else
                            {
                                graphics.DrawRectangle(pen2, rectangle1);
                                graphics.FillRectangle(solidBrush, rectangle1);
                            }
                        }
                    }
                    this._OuterPoints.Add(rectangle1);
                }
                if (this._DrawWebPoints)
                {
                    int num1 = checked(points.Count - 1);
                    for (int k = 0; k <= num1; k = checked(k + 1))
                    {
                        using (SolidBrush solidBrush1 = new SolidBrush(this.Points[k].Color))
                        {
                            item = points[k];
                            int x1 = checked(item.X - checked((int)Math.Round((double)this._WebPointWidth / 2)));
                            point = points[k];
                            graphics.FillEllipse(solidBrush1, x1, checked(point.Y - checked((int)Math.Round((double)this._WebPointWidth / 2))), this._WebPointWidth, this._WebPointWidth);
                        }
                    }
                }
            }
        }


        #endregion

        #region Overrides


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            if (AllowTransparency)
            {
                MakeTransparent(this, graphics);
            }

            graphics.SmoothingMode = SmoothingMode.HighQuality;


            _ChartWidth = ((Width / 2) - 5) + ((Height / 2) - 5);


            Point point = new Point();
            Point item = new Point();
            LinearGradientBrush linbrush = new LinearGradientBrush(new Point(0, 0), new Point(10, 10), Color.AliceBlue, Color.Red);
            List<Point> points = new List<Point>();
            List<Point> points1 = new List<Point>();
            Point point1 = new Point(checked(this._PointSize + checked((int)Math.Round((double)this._ChartWidth / 2))), checked(this._PointSize + checked((int)Math.Round((double)this._ChartWidth / 2))));

            Rectangle rectangle = new Rectangle(this._PointSize, this._PointSize, this._ChartWidth, this._ChartWidth);
            points1.Clear();
            this._OuterPoints.Clear();



            if (this.Points.Count < 3)
            {

                this.DrawDesignControl(graphics, rectangle);

            }

            DrawWeb(point, item, point1, points, points1, enumerator, enumerator1, graphics, linbrush);

            base.OnPaint(e);

        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnBackColorChanged(EventArgs e)
	    {
	        //if (this.FindForm() is ZeroitMetroForm)
	        //{
	        //    if (this._AutoStyle)
	        //    {
	        //        ZeroitMetroForm metroForm = (ZeroitMetroForm)this.FindForm();
	        //        this.Style = metroForm.Style;
	        //        this._Style = metroForm.Style;
	        //        this.Invalidate();
	        //    }
	        //}
	        base.OnBackColorChanged(e);
	    }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
	    {
	        this._HotIndex = -1;
	        this._HotRectangle = new Rectangle();
	        base.OnMouseLeave(e);
	    }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
	    {
	        Rectangle rectangle = new Rectangle();
	        if (this._ShowToolTip)
	        {
	            if (this._HotRectangle == rectangle)
	            {
	                int count = checked(this._OuterPoints.Count - 1);
	                for (int i = 0; i <= count; i = checked(i + 1))
	                {
	                    if (this._OuterPoints[i].Contains(e.Location))
	                    {
	                        if (this._HotIndex != i)
	                        {
	                            if (this._HotRectangle == rectangle)
	                            {
	                                this._HotIndex = i;
	                                this._HotRectangle = this._OuterPoints[i];
	                                this.ToolTip.Show(this.Points[i].Text, this);
	                            }
	                        }
	                    }
	                }
	            }
	            else if (!this._HotRectangle.Contains(e.Location))
	            {
	                this._HotIndex = -1;
	                this._HotRectangle = new Rectangle();
	                this.ToolTip.Hide(this);
	            }
	        }
	        base.OnMouseMove(e);
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




        #endregion

        #region Include in Private Field


        /// <summary>
        /// The automatic animate
        /// </summary>
        private bool autoAnimate = false;
        /// <summary>
        /// The timer
        /// </summary>
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        /// <summary>
        /// The timer decrement
        /// </summary>
        private System.Windows.Forms.Timer timerDecrement = new System.Windows.Forms.Timer();

        #endregion

        #region Include in Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [automatic animate].
        /// </summary>
        /// <value><c>true</c> if [automatic animate]; otherwise, <c>false</c>.</value>
        public bool AutoAnimate
        {
            get { return autoAnimate; }
            set
            {
                autoAnimate = value;

                if (value == true)
                {
                    timer.Enabled = true;
                }

                else
                {
                    timer.Enabled = false;
                    timerDecrement.Enabled = false;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the timer interval.
        /// </summary>
        /// <value>The timer interval.</value>
        public int TimerInterval
        {
            get { return timer.Interval; }
            set
            {
                timer.Interval = value;
                timerDecrement.Interval = value;
                Invalidate();
            }
        }


        #endregion

        #region Event

        /// <summary>
        /// Handles the Tick event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {

            if (this.InnerStructureStages + 1 > 20)
            {
                timer.Stop();
                timer.Enabled = false;
                timerDecrement.Enabled = true;
                timerDecrement.Start();
                //timerDecrement.Tick += TimerDecrement_Tick;
                Invalidate();
            }

            else
            {
                this.InnerStructureStages += 1;
                Invalidate();
            }

            if (this.InnerStructureWidth + 1 > 10)
            {
                timer.Stop();
                timer.Enabled = false;
                timerDecrement.Enabled = true;
                timerDecrement.Start();
                //timerDecrement.Tick += TimerDecrement_Tick;
                Invalidate();
            }

            else
            {
                this.InnerStructureWidth += 1;
                Invalidate();
            }

            if (this.PointSize + 1 > 30)
            {
                timer.Stop();
                timer.Enabled = false;
                timerDecrement.Enabled = true;
                timerDecrement.Start();
                //timerDecrement.Tick += TimerDecrement_Tick;
                Invalidate();
            }

            else
            {
                this.PointSize += 1;
                Invalidate();
            }

            if (this.WebBorderWidth + 1 > 5)
            {
                timer.Stop();
                timer.Enabled = false;
                timerDecrement.Enabled = true;
                timerDecrement.Start();
                //timerDecrement.Tick += TimerDecrement_Tick;
                Invalidate();
            }

            else
            {
                this.WebBorderWidth += 1;
                Invalidate();
            }

            if (this.WebPointWidth + 1 > 5)
            {
                timer.Stop();
                timer.Enabled = false;
                timerDecrement.Enabled = true;
                timerDecrement.Start();
                //timerDecrement.Tick += TimerDecrement_Tick;
                Invalidate();
            }

            else
            {
                this.WebPointWidth += 1;
                Invalidate();
            }

        }


        /// <summary>
        /// Handles the Tick event of the TimerDecrement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TimerDecrement_Tick(object sender, EventArgs e)
        {
            if (this.InnerStructureStages < 2)
            {
                timerDecrement.Stop();
                timerDecrement.Enabled = false;
                timer.Enabled = true;
                timer.Start();
                //timer.Tick += Timer_Tick;
                Invalidate();
            }

            else
            {
                this.InnerStructureStages -= 1;
                Invalidate();
            }

            if (this.InnerStructureWidth < 1)
            {
                timerDecrement.Stop();
                timerDecrement.Enabled = false;
                timer.Enabled = true;
                timer.Start();
                //timer.Tick += Timer_Tick;
                Invalidate();
            }

            else
            {
                this.InnerStructureWidth -= 1;
                Invalidate();
            }

            if (this.PointSize < 2)
            {
                timerDecrement.Stop();
                timerDecrement.Enabled = false;
                timer.Enabled = true;
                timer.Start();
                //timer.Tick += Timer_Tick;
                Invalidate();
            }

            else
            {
                this.PointSize -= 1;
                Invalidate();
            }

            if (this.WebBorderWidth < 1)
            {
                timerDecrement.Stop();
                timerDecrement.Enabled = false;
                timer.Enabled = true;
                timer.Start();
                //timer.Tick += Timer_Tick;
                Invalidate();
            }

            else
            {
                this.WebBorderWidth -= 1;
                Invalidate();
            }

            if (this.WebPointWidth < 1)
            {
                timerDecrement.Stop();
                timerDecrement.Enabled = false;
                timer.Enabled = true;
                timer.Start();
                //timer.Tick += Timer_Tick;
                Invalidate();
            }

            else
            {
                this.WebPointWidth -= 1;
                Invalidate();
            }


        }


        #endregion

        #region Constructor

        /// <summary>
        /// Includes the in constructor.
        /// </summary>
        private void IncludeInConstructor()
        {

            if (DesignMode)
            {
                timer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 100;
                    timer.Interval = 100;
                    timer.Start();
                }
            }

            if (!DesignMode)
            {
                timer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 100;
                    timer.Interval = 100;
                    timer.Start();
                }
            }

        }

        #endregion

        #region Serialization

	    //public ZeroitMetroWebChart(SerializationInfo info, StreamingContext context)
	    //{
	    //    _PointCollection = (ZeroitMetroWebChartPointCollection) info.GetValue("_PointCollection",
	    //        typeof(ZeroitMetroWebChartPointCollection));
	    //}

     //   public void GetObjectData(SerializationInfo info, StreamingContext context)
     //   {
     //       info.AddValue("_PointCollection", _PointCollection);
     //   } 
        #endregion
    }

}