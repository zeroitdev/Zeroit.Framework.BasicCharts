// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-04-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="MetroGraph.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts.Metro
{
    /// <summary>
    /// A class collection for rendering metro graph.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Description("A class for creating a metro graph.")]
	[DesignerCategory("Code")]
	[ToolboxBitmap(typeof(ZeroitMetroGraph), "ZeroitMetroGraph.bmp")]
    [Designer(typeof(ZeroitMetroGraphDesigner))]
    public class ZeroitMetroGraph : Control
	{
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

        #endregion

        #region Private Fields

        
        /// <summary>
        /// The draw mode
        /// </summary>
        private BrushType drawMode = BrushType.Solid;

        /// <summary>
        /// The style
        /// </summary>
        private Design.Style _Style;

        //private Color _DefaultColor;

        /// <summary>
        /// The grid color
        /// </summary>
        private Color _GridColor;

        /// <summary>
        /// The single line color
        /// </summary>
        private Color[] _SingleLineColor = new Color[]{Color.DeepSkyBlue, Color.Orange};

        //private Color _ClassicLineColor;

        /// <summary>
        /// The classic fill color
        /// </summary>
        private Color _ClassicFillColor;

        /// <summary>
        /// The gradient color
        /// </summary>
        private Color _GradientColor;

        /// <summary>
        /// The hover color
        /// </summary>
        private Color _HoverColor;

        /// <summary>
        /// The hover box color
        /// </summary>
        private Color _HoverBoxColor;

        /// <summary>
        /// The hover box border color
        /// </summary>
        private Color _HoverBoxBorderColor;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _BorderColor;

        /// <summary>
        /// The draw hover line
        /// </summary>
        private bool _DrawHoverLine;

        /// <summary>
        /// The draw hover data
        /// </summary>
        private bool _DrawHoverData;

        //private int _ClassicLineThickness;

        /// <summary>
        /// The use gradient
        /// </summary>
        private bool _UseGradient;

        /// <summary>
        /// The dash style
        /// </summary>
        private System.Drawing.Drawing2D.DashStyle _DashStyle;

        /// <summary>
        /// The draw vertical lines
        /// </summary>
        private bool _DrawVerticalLines;

        /// <summary>
        /// The draw horizontal lines
        /// </summary>
        private bool _DrawHorizontalLines;

        /// <summary>
        /// The single line
        /// </summary>
        private bool _SingleLine;

        /// <summary>
        /// The curved
        /// </summary>
        private bool curved = false;

        /// <summary>
        /// The single line thickness
        /// </summary>
        private int _SingleLineThickness=2;

        /// <summary>
        /// The single line shadow
        /// </summary>
        private bool _SingleLineShadow;

        /// <summary>
        /// The side padding
        /// </summary>
        private bool _SidePadding;

        /// <summary>
        /// The override minimum
        /// </summary>
        private bool _OverrideMinimum;

        /// <summary>
        /// The override maximum
        /// </summary>
        private bool _OverrideMaximum;

        /// <summary>
        /// The overridden minimum
        /// </summary>
        private int _OverriddenMinimum;

        /// <summary>
        /// The overridden maximum
        /// </summary>
        private int _OverriddenMaximum;

        /// <summary>
        /// The values
        /// </summary>
        private List<float> _Values = new List<float>()
        {
            10,50,100,40,20, 80, 100, 10, 0, 20, 90,40
        };

        /// <summary>
        /// The smooth values
        /// </summary>
        private List<float> _SmoothValues;

        /// <summary>
        /// The current value
        /// </summary>
        private float _CurrentValue;

        /// <summary>
        /// The minimum
        /// </summary>
        private float _Minimum;

        /// <summary>
        /// The maximum
        /// </summary>
        private float _Maximum;

        /// <summary>
        /// The index
        /// </summary>
        private int _Index;

        /// <summary>
        /// The gradient point a
        /// </summary>
        private Point _GradientPointA;

        /// <summary>
        /// The gradient point b
        /// </summary>
        private Point _GradientPointB;

        /// <summary>
        /// The f b1
        /// </summary>
        private float FB1;

        /// <summary>
        /// The f b2
        /// </summary>
        private float FB2;

        /// <summary>
        /// The r1
        /// </summary>
        private Rectangle R1;

        /// <summary>
        /// The r2
        /// </summary>
        private Rectangle R2;

        /// <summary>
        /// The r3
        /// </summary>
        private Rectangle R3;

        /// <summary>
        /// The sw
        /// </summary>
        private int SW;

        /// <summary>
        /// The sh
        /// </summary>
        private int SH;

        /// <summary>
        /// The SHH
        /// </summary>
        private int SHH;

        /// <summary>
        /// The last move
        /// </summary>
        private DateTime LastMove;

        /// <summary>
        /// The automatic style
        /// </summary>
        private bool _AutoStyle;

        /// <summary>
        /// The outer border
        /// </summary>
        private bool outerBorder = true;

        /// <summary>
        /// The hover circle transparency
        /// </summary>
        private int hoverCircleTransparency = 100;
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the hover circle alpha.
        /// </summary>
        /// <value>The hover circle alpha.</value>
        public int HoverCircleAlpha
	    {
	        get { return hoverCircleTransparency; }
	        set
	        {
	            if (value > 333)
	            {
	                value = 333;
	            }

	            if (value < 0)
	            {
	                value = 0;
	            }
                hoverCircleTransparency = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the draw mode.
        /// </summary>
        /// <value>The draw mode.</value>
        public BrushType DrawMode
	    {
	        get { return drawMode; }
	        set
	        {
                drawMode = value;
	            Invalidate();
	        }
	    }

        #region Hatch Animation

        /// <summary>
        /// The enable hatch animation
        /// </summary>
        private bool enableHatchAnimation = true;

        /// <summary>
        /// The hatch style
        /// </summary>
        private HatchStyle hatchStyle = HatchStyle.Percent05;

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
        /// Gets or sets a value indicating whether [hatch animation].
        /// </summary>
        /// <value><c>true</c> if [hatch animation]; otherwise, <c>false</c>.</value>
        public bool HatchAnimation
	    {
	        get { return enableHatchAnimation; }
	        set
	        {
	            enableHatchAnimation = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the hatch speed.
        /// </summary>
        /// <value>The hatch speed.</value>
        public int HatchSpeed
	    {
	        get { return hatchSpeed; }
	        set
	        {
	            hatchSpeed = value;
	            Invalidate();
	        }
	    }



        //---------------------------Include in Paint--------------------//
        //
        //if (EnableHatchAnimation)
        //{
        //    G.RenderingOrigin = new Point(reactorOFS, 0);
        //}
        //
        //---------------------------Include in Paint--------------------//

        /// <summary>
        /// The reactor ofs
        /// </summary>
        private int reactorOFS = 20;
        /// <summary>
        /// The hatch speed
        /// </summary>
        private int hatchSpeed = 50;

        /// <summary>
        /// Reactors the create handle.
        /// </summary>
        private void ReactorCreateHandle()
	    {

	        if (HatchAnimation)
	        {
	            // Dim tmr As New Timer With {.Interval = hatchSpeed}
	            // AddHandler tmr.Tick, AddressOf ReactorAnimate
	            // tmr.Start()
	            System.Threading.Thread T = new System.Threading.Thread(ReactorAnimate);
	            T.IsBackground = true;
	            T.Start();
	        }

	    }

        /// <summary>
        /// Creates a handle for the control.
        /// </summary>
        protected override void CreateHandle()
	    {
	        base.CreateHandle();

	        ReactorCreateHandle();
	    }

        /// <summary>
        /// Reactors the animate.
        /// </summary>
        public void ReactorAnimate()
	    {
	        while (true)
	        {
	            if (reactorOFS <= Width)
	            {
	                reactorOFS += 1;
	            }
	            else
	            {
	                reactorOFS = 0;
	            }
	            Invalidate();
	            System.Threading.Thread.Sleep(hatchSpeed);
	        }
	    }


        #endregion

        #region Axis

        /// <summary>
        /// The show axis
        /// </summary>
        bool showAxis = false;

        /// <summary>
        /// The spline axis
        /// </summary>
        private SplineAxis splineAxis = new SplineAxis();

        /// <summary>
        /// Gets or sets a value indicating whether [show axis].
        /// </summary>
        /// <value><c>true</c> if [show axis]; otherwise, <c>false</c>.</value>
        public bool ShowAxis
	    {
	        get
	        {
	            return showAxis;
	        }
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


        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether to draw the outer border.
        /// </summary>
        /// <value><c>true</c> if outer border; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Draws the outer border.")]
        public bool OuterBorder
        {
            get { return outerBorder; }
            set
            {
                outerBorder = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to automatically style the control.
        /// </summary>
        /// <value><c>true</c> if automatic style; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to automatically style the control.")]
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

        ///// <summary>
        ///// Gets or sets the background color for the control.
        ///// </summary>
        ///// <value>The color of the back.</value>
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public new Color BackColor
        //{
        //    [DebuggerNonUserCode]
        //    get;
        //    [DebuggerNonUserCode]
        //    set;
        //}

        /// <summary>
        /// Gets or sets the background image displayed in the control.
        /// </summary>
        /// <value>The background image.</value>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Image BackgroundImage
        {
            [DebuggerNonUserCode]
            get;
            [DebuggerNonUserCode]
            set;
        }

        /// <summary>
        /// Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.
        /// </summary>
        /// <value>The background image layout.</value>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ImageLayout BackgroundImageLayout
        {
            [DebuggerNonUserCode]
            get;
            [DebuggerNonUserCode]
            set;
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
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
                if (value != this._BorderColor)
                {
                    this._BorderColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the classic fill.
        /// </summary>
        /// <value>The color of the classic fill.</value>
        [Category("Appearance")]
        [Description("Sets the color of the classic fill.")]
        public Color ClassicFillColor
        {
            get
            {
                return this._ClassicFillColor;
            }
            set
            {
                if (value != this._ClassicFillColor)
                {
                    this._ClassicFillColor = value;
                    this.Invalidate();
                }
            }
        }

        ///// <summary>
        ///// Gets or sets the color of the classic line.
        ///// </summary>
        ///// <value>The color of the classic line.</value>
        //[Category("Appearance")]
        //[Description(".")]
        //public Color ClassicLineColor
        //{
        //    get
        //    {
        //        return this._ClassicLineColor;
        //    }
        //    set
        //    {
        //        if (value != this._ClassicLineColor)
        //        {
        //            this._ClassicLineColor = value;
        //            this.Invalidate();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the classic line thickness.
        ///// </summary>
        ///// <value>
        ///// The classic line thickness.
        ///// </value>
        //[Category("Behavior")]
        //[DefaultValue(2)]
        //[Description("Sets the classic line thickness.")]
        //public int ClassicLineThickness
        //{
        //    get
        //    {
        //        return this._ClassicLineThickness;
        //    }
        //    set
        //    {
        //        if (value != this._ClassicLineThickness)
        //        {
        //            this._ClassicLineThickness = value;
        //            this.Invalidate();
        //        }
        //    }
        //}

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.
        /// </summary>
        /// <value>The context menu strip.</value>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            [DebuggerNonUserCode]
            get;
            [DebuggerNonUserCode]
            set;
        }

        /// <summary>
        /// Gets or sets the dash style.
        /// </summary>
        /// <value>The dash style.</value>
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("Sets the dash style.")]
        public System.Drawing.Drawing2D.DashStyle DashStyle
        {
            get
            {
                return this._DashStyle;
            }
            set
            {
                if (value != this._DashStyle)
                {
                    this._DashStyle = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the default color.
        /// </summary>
        /// <value>The default color.</value>
        [Category("Appearance")]
        [Description("Sets the default color.")]
        public new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw horizontal lines.
        /// </summary>
        /// <value><c>true</c> if draw horizontal lines; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to draw horizontal lines.")]
        public bool DrawHorizontalLines
        {
            get
            {
                return this._DrawHorizontalLines;
            }
            set
            {
                if (value != this._DrawHorizontalLines)
                {
                    this._DrawHorizontalLines = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw hover data.
        /// </summary>
        /// <value><c>true</c> if [draw hover data]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to draw hover data.")]
        public bool DrawHoverData
        {
            get
            {
                return this._DrawHoverData;
            }
            set
            {
                if (value != this._DrawHoverData)
                {
                    this._DrawHoverData = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw hover line.
        /// </summary>
        /// <value><c>true</c> if draw hover line; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to draw a hover line.")]
        public bool DrawHoverLine
        {
            get
            {
                return this._DrawHoverLine;
            }
            set
            {
                if (value != this._DrawHoverLine)
                {
                    this._DrawHoverLine = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw vertical lines.
        /// </summary>
        /// <value><c>true</c> if draw vertical lines; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to draw vertical lines.")]
        public bool DrawVerticalLines
        {
            get
            {
                return this._DrawVerticalLines;
            }
            set
            {
                if (value != this._DrawVerticalLines)
                {
                    this._DrawVerticalLines = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the gradient.
        /// </summary>
        /// <value>The color of the gradient.</value>
        [Category("Appearance")]
        [Description("Sets the color of the gradient.")]
        public Color GradientColor
        {
            get
            {
                return this._GradientColor;
            }
            set
            {
                if (value != this._GradientColor)
                {
                    this._GradientColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the gradient point.
        /// </summary>
        /// <value>The gradient point a.</value>
        [Browsable(false)]
        [Category("Appereance")]
        [Description("Sets the gradient point.")]
        public Point GradientPointA
        {
            get
            {
                return this._GradientPointA;
            }
            set
            {
                if (this._GradientPointA != value)
                {
                    this._GradientPointA = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the gradient point.
        /// </summary>
        /// <value>The gradient point b.</value>
        [Browsable(false)]
        [Category("Appereance")]
        [Description("Sets the gradient point.")]
        public Point GradientPointB
        {
            get
            {
                return this._GradientPointB;
            }
            set
            {
                if (this._GradientPointB != value)
                {
                    this._GradientPointB = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the grid.
        /// </summary>
        /// <value>The color of the grid.</value>
        [Category("Appearance")]
        [Description("Sets the color of the grid.")]
        public Color GridColor
        {
            get
            {
                return this._GridColor;
            }
            set
            {
                if (value != this._GridColor)
                {
                    this._GridColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover box border.
        /// </summary>
        /// <value>The color of the hover box border.</value>
        [Category("Appearance")]
        [Description("Sets the color of the hover box border.")]
        public Color HoverBoxBorderColor
        {
            get
            {
                return this._HoverBoxBorderColor;
            }
            set
            {
                if (value != this._HoverBoxBorderColor)
                {
                    this._HoverBoxBorderColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover box.
        /// </summary>
        /// <value>The color of the hover box.</value>
        [Category("Appearance")]
        [Description("Sets the color of the hover box.")]
        public Color HoverBoxColor
        {
            get
            {
                return this._HoverBoxColor;
            }
            set
            {
                if (value != this._HoverBoxColor)
                {
                    this._HoverBoxColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover.
        /// </summary>
        /// <value>The color of the hover.</value>
        [Category("Appearance")]
        [Description("Sets the color of the hover.")]
        public Color HoverColor
        {
            get
            {
                return this._HoverColor;
            }
            set
            {
                if (value != this._HoverColor)
                {
                    this._HoverColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        [Category("Data")]
        [Description("Der Maximumwert des Steuerelements.")]
        public float Maximum
        {
            get
            {
                return this._Maximum;
            }
        }

        /// <summary>
        /// Gets the Minimum.
        /// </summary>
        /// <value>The minimum.</value>
        [Category("Data")]
        [Description("Gets the Minimum.")]
        public float Minimum
        {
            get
            {
                return this._Minimum;
            }
        }

        /// <summary>
        /// Gets or sets the overridden maximum.
        /// </summary>
        /// <value>The overridden maximum.</value>
        [Category("Data")]
        [DefaultValue(100)]
        [Description("Sets the overridden maximum.")]
        public int OverriddenMaximum
        {
            get
            {
                return this._OverriddenMaximum;
            }
            set
            {
                if (value != this._OverriddenMaximum)
                {
                    this._OverriddenMaximum = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the overridden minimum.
        /// </summary>
        /// <value>The overridden minimum.</value>
        [Category("Data")]
        [DefaultValue(0)]
        [Description("Sets the overridden minimum.")]
        public int OverriddenMinimum
        {
            get
            {
                return this._OverriddenMinimum;
            }
            set
            {
                if (value != this._OverriddenMinimum)
                {
                    this._OverriddenMinimum = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to override the maximum.
        /// </summary>
        /// <value><c>true</c> if override maximum; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to override the maximum.")]
        public bool OverrideMaximum
        {
            get
            {
                return this._OverrideMaximum;
            }
            set
            {
                if (value != this._OverrideMaximum)
                {
                    this._OverrideMaximum = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to override minimum.
        /// </summary>
        /// <value><c>true</c> if override minimum; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to override minimum.")]
        public bool OverrideMinimum
        {
            get
            {
                return this._OverrideMinimum;
            }
            set
            {
                if (value != this._OverrideMinimum)
                {
                    this._OverrideMinimum = value;
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
            [DebuggerNonUserCode]
            get;
            [DebuggerNonUserCode]
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable side padding.
        /// </summary>
        /// <value><c>true</c> if side padding; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to enable side padding.")]
        public bool SidePadding
        {
            get
            {
                return this._SidePadding;
            }
            set
            {
                if (value != this._SidePadding)
                {
                    this._SidePadding = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable single line.
        /// </summary>
        /// <value><c>true</c> if single line; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Sets a value indicating whether to enable single line.")]
        public bool SingleLine
        {
            get
            {
                return this._SingleLine;
            }
            set
            {
                if (value != this._SingleLine)
                {
                    this._SingleLine = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the single line.
        /// </summary>
        /// <value>The color of the single line.</value>
        [Category("Appearance")]
        [Description("Sets the color of the single line.")]
        public Color[] SingleLineColor
        {
            get
            {
                return this._SingleLineColor;
            }
            set
            {
                if (value != this._SingleLineColor)
                {
                    this._SingleLineColor = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable single line shadow.
        /// </summary>
        /// <value><c>true</c> if [single line shadow]; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to enable single line shadow.")]
        public bool SingleLineShadow
        {
            get
            {
                return this._SingleLineShadow;
            }
            set
            {
                if (value != this._SingleLineShadow)
                {
                    this._SingleLineShadow = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the single line thickness.
        /// </summary>
        /// <value>The single line thickness.</value>
        [Category("Appearance")]
        [DefaultValue(5)]
        [Description("Sets the single line thickness.")]
        public int SingleLineThickness
        {
            get
            {
                return this._SingleLineThickness;
            }
            set
            {
                if (value != this._SingleLineThickness)
                {
                    this._SingleLineThickness = value;
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
                                this.BackColor = Design.MetroColors.LightDefault;
                                this._GridColor = Design.MetroColors.PopUpBorder;
                                //this._SingleLineColor = Design.MetroColors.AccentBlue;
                                //this._ClassicLineColor = Design.MetroColors.AccentBlue;
                                this._ClassicFillColor = Color.FromArgb(50, Design.MetroColors.AccentBlue);
                                this._GradientColor = Color.FromArgb(50, Design.MetroColors.ChangeColorBrightness(Design.MetroColors.AccentBlue, -0.2f));
                                this._HoverColor = Design.MetroColors.AccentLightBlue;
                                this._HoverBoxColor = Design.MetroColors.LightDefault;
                                this._HoverBoxBorderColor = Design.MetroColors.PopUpBorder;
                                this._BorderColor = Design.MetroColors.LightBorder;
                                this.ForeColor = Design.MetroColors.LightFont;
                                break;
                            }
                        case Design.Style.Dark:
                            {
                                this.BackColor = Design.MetroColors.DarkDefault;
                                this._GridColor = Design.MetroColors.LightBorder;
                                //this._SingleLineColor = Design.MetroColors.AccentBlue;
                                //this._ClassicLineColor = Design.MetroColors.AccentBlue;
                                this._ClassicFillColor = Color.FromArgb(50, Design.MetroColors.AccentBlue);
                                this._GradientColor = Color.FromArgb(50, Design.MetroColors.ChangeColorBrightness(Design.MetroColors.AccentBlue, -0.2f));
                                this._HoverColor = Design.MetroColors.AccentLightBlue;
                                this._HoverBoxColor = Design.MetroColors.DarkDefault;
                                this._HoverBoxBorderColor = Design.MetroColors.LightBorder;
                                this._BorderColor = Design.MetroColors.LightBorder;
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
        /// Gets or sets a value indicating whether to use gradient.
        /// </summary>
        /// <value><c>true</c> if use gradient; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Sets a value indicating whether to use gradient.")]
        public bool UseGradient
        {
            get
            {
                return this._UseGradient;
            }
            set
            {
                if (value != this._UseGradient)
                {
                    this._UseGradient = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        [Browsable(true)]
        [Category("Data")]
        [Description("Sets the values.")]
        public float[] Values
        {
            get
            {
                return this._Values.ToArray();
            }
            set
            {
                this.Clear();
                this.AddValues(value);
                this.FindMinMax();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitMetroGraph"/> is curved.
        /// </summary>
        /// <value><c>true</c> if curved; otherwise, <c>false</c>.</value>
        public bool Curved
	    {
	        get { return curved; }
	        set { curved = value;
	            Invalidate();
	        }
	    }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMetroGraph" /> class.
        /// </summary>
        public ZeroitMetroGraph()
        {
            //this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            
            this.UpdateStyles();

            this._Style = Design.Style.Light;
            this.BackColor = Color.Transparent;
            this._GridColor = Design.MetroColors.PopUpBorder;
            //this._SingleLineColor = Design.MetroColors.AccentBlue;
            //this._ClassicLineColor = Design.MetroColors.AccentBlue;
            this._ClassicFillColor = Color.FromArgb(50, Design.MetroColors.AccentBlue);
            this._GradientColor = Color.FromArgb(50, Design.MetroColors.ChangeColorBrightness(Design.MetroColors.AccentBlue, -0.2f));
            this._HoverColor = Design.MetroColors.AccentLightBlue;
            this._HoverBoxColor = Color.FromArgb(15, Color.Pink);
            this._HoverBoxBorderColor = Design.MetroColors.PopUpBorder;
            this._BorderColor = Design.MetroColors.LightBorder;
            this._DrawHoverLine = true;
            this._DrawHoverData = true;
            //this._ClassicLineThickness = 2;
            this._UseGradient = true;
            this._DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this._DrawVerticalLines = false;
            this._DrawHorizontalLines = true;
            this._SingleLine = false;
            
            this._SingleLineShadow = true;
            this._SidePadding = false;
            this._OverrideMinimum = false;
            this._OverrideMaximum = false;
            this._OverriddenMinimum = 0;
            this._OverriddenMaximum = 100;
            this._Minimum = float.MinValue;
            this._Maximum = float.MaxValue;
            this._Index = -1;
            this._GradientPointA = new Point(checked((int)Math.Round((double)this.Width / 2)), 0);
            this._GradientPointB = new Point(checked((int)Math.Round((double)this.Width / 2)), this.Height);
            this._AutoStyle = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9f);

            IncludeInConstructor();
            HoverIncludeInConstructor();
            this._SmoothValues = new List<float>();
            Invalidate();
        }

        #endregion

        #region Methods and Overrides

        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void AddValue(float value)
        {
            this._Index = -1;
            this._Values.Add(value);
            this.CleanValues();
            this.FindMinMax();
            this.Invalidate();
        }

        /// <summary>
        /// Adds the values.
        /// </summary>
        /// <param name="values">The values.</param>
        public void AddValues(float[] values)
        {
            this._Index = -1;
            this._Values.AddRange(values);
            this.CleanValues();
            this.FindMinMax();
            this.Invalidate();
        }

        /// <summary>
        /// Cleans the values.
        /// </summary>
        private void CleanValues()
        {
            if (this._Values.Count > this.Width)
            {
                this._Values.RemoveRange(0, checked(this._Values.Count - this.Width));
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this._Values.Clear();
            this._SmoothValues.Clear();
            this._Maximum = float.MinValue;
            this._Minimum = float.MaxValue;
            this.InvalidateMinMax();
        }

        /// <summary>
        /// Finds the minimum maximum.
        /// </summary>
        private void FindMinMax()
        {
            this._Maximum = float.MinValue;
            this._Minimum = float.MaxValue;
            int count = checked(this._Values.Count - 1);
            for (int i = 0; i <= count; i = checked(i + 1))
            {
                this._CurrentValue = this._Values[i];
                if (this._CurrentValue > this._Maximum)
                {
                    this._Maximum = this._CurrentValue;
                }
                if (this._CurrentValue < this._Minimum)
                {
                    this._Minimum = this._CurrentValue;
                }
            }
            this.InvalidateMinMax();
        }

        /// <summary>
        /// Invalidates the minimum maximum.
        /// </summary>
        private void InvalidateMinMax()
        {
            if (this._OverrideMinimum)
            {
                this._Minimum = (float)this._OverriddenMinimum;
            }
            if (this._OverrideMaximum)
            {
                this._Maximum = (float)this._OverriddenMaximum;
            }
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
            //        this.Style = ((ZeroitMetroForm)this.FindForm()).Style;
            //        this.Invalidate();
            //    }
            //}
            base.OnBackColorChanged(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            HoverCircleAutoAnimate = true;
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (this._DrawHoverData)
            {
                this._Index = -1;
                this.Invalidate();
            }

            HoverCircleAutoAnimate = false;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this._DrawHoverData)
            {
                this.R1 = new Rectangle(this.SW, 0, checked(this.Width - this.SW), checked(this.Height - this.SH));
                this.R2 = new Rectangle(checked(this.R1.X + 8), checked(this.R1.Y + 8), checked(this.R1.Width - 16), checked(this.R1.Height - 16));
                this.FB1 = (float)((double)(checked(this.R2.Width - 1)) / (double)(checked(this._Values.Count - 1)));
                if (!this.R1.Contains(e.Location))
                {
                    this._Index = -1;
                }
                else
                {
                    this._Index = checked((int)Math.Round((double)((float)((float)(checked(e.X - this.R2.X)) / this.FB1))));
                    if (this._Index >= this._Values.Count)
                    {
                        this._Index = -1;
                    }
                }
                if (DateTime.Compare(DateTime.Now, this.LastMove.AddMilliseconds(33)) > 0)
                {
                    this.LastMove = DateTime.Now;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        protected override void OnPaint(PaintEventArgs e)
        {
            Point point;
            Graphics graphics = e.Graphics;

            if (AllowTransparency)
            {
                MakeTransparent(this, graphics);
            }

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            //---------------------------Include in Paint--------------------//
            //
            if (HatchAnimation)
            {
                graphics.RenderingOrigin = new Point(reactorOFS, 0);
            }
            //
            //---------------------------Include in Paint--------------------//


            GraphicsPath graphicsPath = new GraphicsPath();
            this.R1 = new Rectangle(this.SW, 0, checked(this.Width - this.SW), this.Height);
            this.R2 = new Rectangle(checked(this.R1.X + 10), checked(this.R1.Y + 10), checked(this.R1.Width - 20), checked(this.R1.Height - 20));
            if (!this._SidePadding)
            {
                this.R2.X = this.R1.X;
                this.R2.Width = this.R1.Width;
            }
            using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
            {
                graphics.FillRectangle(solidBrush, this.R1);
            }

            Pen penGrid = new Pen(this._GridColor);

            Pen pen = new Pen(SingleLineColor[0]);
            pen.DashOffset = dashOffset;

            switch (DrawMode)
            {
                case BrushType.Solid:
                    pen.Brush = new SolidBrush(SingleLineColor[0]);
                    break;
                case BrushType.Gradient:
                    pen.Brush = new LinearGradientBrush(ClientRectangle, SingleLineColor[0], SingleLineColor[1], 90f);
                    break;
                case BrushType.Hatch:
                    pen.Brush = new HatchBrush(HatchStyle, SingleLineColor[0], SingleLineColor[1]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            pen.Width = (float)this._SingleLineThickness;
            pen.DashStyle = this._DashStyle;

            Pen singleLineShadowPen = new Pen(Design.MetroColors.TextShadow);
            int num = 0;
            do
            {
                this.FB1 = (float)this.R2.Y + (float)((double)(checked(this.R2.Height - 1)) * ((double)num * 0.1));
                if (this._DrawHorizontalLines)
                {
                    graphics.DrawLine(penGrid, (float)this.R1.X, this.FB1, (float)(checked(this.R1.Right - 1)), this.FB1);
                }
                num = checked(num + 1);
            }
            while (num <= 10);
            if (this._Values.Count > 1)
            {
                PointF[] pointF = new PointF[checked(checked(this._Values.Count + 1) + 1)];
                this.FB1 = (float)((double)(checked(this.R2.Width - 1)) / (double)(checked(this._Values.Count - 1)));
                int count = checked(this._Values.Count - 1);
                for (int i = 0; i <= count; i = checked(i + 1))
                {
                    this.FB2 = (float)this.R2.X + (float)i * this.FB1;
                    this._CurrentValue = (this._Values[i] - this._Minimum) / Math.Max(this._Maximum - this._Minimum, 1f);
                    if (this._CurrentValue > 1f)
                    {
                        this._CurrentValue = 1f;
                    }
                    else if (this._CurrentValue < 0f)
                    {
                        this._CurrentValue = 0f;
                    }
                    pointF[i] = new PointF(this.FB2, (float)(checked((int)Math.Round((double)((float)((float)this.R2.Bottom - (float)(checked(this.R2.Height - 1)) * this._CurrentValue - 1f))))));
                    if (this._DrawVerticalLines)
                    {
                        graphics.DrawLine(penGrid, this.FB2, (float)this.R1.Y, this.FB2, (float)this.R1.Bottom);
                    }
                }
                pointF[checked(checked((int)pointF.Length) - 2)] = new PointF((float)(checked(this.R2.Right - 1)), (float)(checked(this.R1.Bottom - 1)));
                pointF[checked(checked((int)pointF.Length) - 1)] = new PointF((float)this.R2.X, (float)(checked(this.R1.Bottom - 1)));
                

                if (!this._SingleLine)
                {
                    graphicsPath.AddPolygon(pointF);
                    graphicsPath.CloseFigure();
                    
                    //pen.Color = this.SingleLineColor;
                    pen.Width = (float)this.SingleLineThickness;
                    Color color = (this._UseGradient ? this._GradientColor : this._ClassicFillColor);

                    Brush linearGradientBrush;
                    Pen pen1;
                    Point point1 = new Point(0, this.Height);
                    switch (DrawMode)
                    {
                        case BrushType.Solid:
                            linearGradientBrush = new SolidBrush(color);
                            graphics.FillPath(linearGradientBrush, graphicsPath);
                            graphics.DrawPath(pen, graphicsPath);

                            pen1 = new Pen(linearGradientBrush, 3f);
                            
                            graphics.DrawLine(pen1, graphicsPath.PathPoints[0], point1);
                            point1 = new Point(checked((int)Math.Round((double)((float)(graphicsPath.PathPoints[checked(checked((int)pointF.Length) - 3)].X - 1f)))), checked(checked((int)Math.Round((double)graphicsPath.PathPoints[checked(checked((int)pointF.Length) - 3)].Y)) + 4));
                            point = new Point(checked(this.Width - 2), this.Height);
                            graphics.DrawLine(pen1, point1, point);
                            point = new Point(this.Width, checked(this.Height - 1));
                            point1 = new Point(0, checked(this.Height - 1));
                            graphics.DrawLine(pen1, point, point1);
                            break;
                        case BrushType.Gradient:
                            linearGradientBrush = new LinearGradientBrush(this._GradientPointA,
                                this._GradientPointB, this._ClassicFillColor, color);

                            graphics.FillPath(linearGradientBrush, graphicsPath);
                            graphics.DrawPath(pen, graphicsPath);

                            pen1 = new Pen(linearGradientBrush, 3f);
                            graphics.DrawLine(pen1, graphicsPath.PathPoints[0], point1);
                            point1 = new Point(checked((int)Math.Round((double)((float)(graphicsPath.PathPoints[checked(checked((int)pointF.Length) - 3)].X - 1f)))), checked(checked((int)Math.Round((double)graphicsPath.PathPoints[checked(checked((int)pointF.Length) - 3)].Y)) + 4));
                            point = new Point(checked(this.Width - 2), this.Height);
                            graphics.DrawLine(pen1, point1, point);
                            point = new Point(this.Width, checked(this.Height - 1));
                            point1 = new Point(0, checked(this.Height - 1));
                            graphics.DrawLine(pen1, point, point1);
                            break;
                        case BrushType.Hatch:

                            linearGradientBrush = new HatchBrush(HatchStyle,this._ClassicFillColor, color);

                            graphics.FillPath(linearGradientBrush, graphicsPath);
                            graphics.DrawPath(pen, graphicsPath);

                            pen1 = new Pen(linearGradientBrush, 3f);
                            graphics.DrawLine(pen1, graphicsPath.PathPoints[0], point1);
                            point1 = new Point(checked((int)Math.Round((double)((float)(graphicsPath.PathPoints[checked(checked((int)pointF.Length) - 3)].X - 1f)))), checked(checked((int)Math.Round((double)graphicsPath.PathPoints[checked(checked((int)pointF.Length) - 3)].Y)) + 4));
                            point = new Point(checked(this.Width - 2), this.Height);
                            graphics.DrawLine(pen1, point1, point);
                            point = new Point(this.Width, checked(this.Height - 1));
                            point1 = new Point(0, checked(this.Height - 1));
                            graphics.DrawLine(pen1, point, point1);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    

                    graphicsPath.Reset();
                    graphicsPath.Dispose();
                }
                else
                {
                    Array.Resize<PointF>(ref pointF, checked(checked((int)pointF.Length) - 2));
                    if (this._SingleLineShadow)
                    {
                        
                        //singleLineShadowPen.Color = Design.MetroColors.TextShadow;
                        singleLineShadowPen.Width = (float)(checked(this._SingleLineThickness + 2));
                        if (Curved)
                        {
                            graphics.DrawCurve(singleLineShadowPen, pointF);
                        }
                        else
                        {
                            graphics.DrawLines(singleLineShadowPen, pointF);
                        }
                    }
                    
                    if (Curved)
                    {
                        graphics.DrawCurve(pen, pointF);
                    }
                    else
                    {
                        graphics.DrawLines(pen, pointF);
                    }
                    
                }
                if ((!this._DrawHoverData || this._Index < 0 ? false : true))
                {
                    graphics.SetClip(this.R1);
                    Point client = new Point(checked((int)Math.Round((double)pointF[this._Index].X)), checked((int)Math.Round((double)pointF[this._Index].Y)));
                    this.R3 = new Rectangle(checked(client.X - 4), checked(client.Y - 4), 8, 8);

                    Pen hoverPen = new Pen(_HoverColor);
                    //hoverPen.Color = this._HoverColor;
                    hoverPen.Width = 1f;
                    if (this._DrawHoverLine)
                    {
                        graphics.DrawLine(hoverPen, client.X, this.R1.Y, client.X, checked(this.R1.Bottom - 1));
                    }
                    SolidBrush foreColor = new SolidBrush(/*Design.MetroColors.ChangeColorBrightness(this._HoverColor, 0.2f)*/ Color.FromArgb(50, HoverColor));

                    graphics.FillEllipse(new SolidBrush(Color.FromArgb((int)(HoverCircleAlpha * 0.25f), HoverColor)), new Rectangle(R3.X - 10, R3.Y - 10, R3.Width + 20, R3.Height + 20));
                    graphics.DrawEllipse(new Pen(Color.FromArgb((int)(HoverCircleAlpha * 0.5f), HoverColor)), new Rectangle(R3.X - 10, R3.Y - 10, R3.Width + 20, R3.Height + 20));


                    graphics.FillEllipse(new SolidBrush(Color.FromArgb((int)(HoverCircleAlpha * 0.25f), HoverColor)), new Rectangle(R3.X - 5, R3.Y - 5, R3.Width + 10, R3.Height + 10));
                    graphics.DrawEllipse(new Pen(Color.FromArgb((int)(HoverCircleAlpha * 0.5f), HoverColor)), new Rectangle(R3.X - 5, R3.Y - 5, R3.Width + 10, R3.Height + 10));

                    graphics.FillEllipse(new SolidBrush(Color.FromArgb((int)(HoverCircleAlpha * 0.75f), HoverColor)), this.R3);
                    graphics.DrawEllipse(new Pen(Color.FromArgb((int)(HoverCircleAlpha * 0.75f), HoverColor)), this.R3);
                    string str = this._Values[this._Index].ToString();
                    SizeF sizeF = graphics.MeasureString(str, this.Font);
                    System.Drawing.Size size = sizeF.ToSize();
                    client = this.PointToClient(Control.MousePosition);
                    this.R3 = new Rectangle(checked(client.X + 24), client.Y, checked(size.Width + 20), checked(size.Height + 10));
                    if (checked(this.R3.X + this.R3.Width) > checked(this.R1.Right - 1))
                    {
                        this.R3.X = checked(checked(client.X - this.R3.Width) - 16);
                    }
                    if (checked(this.R3.Y + this.R3.Height) > checked(this.R1.Bottom - 1))
                    {
                        this.R3.Y = checked(checked(this.R1.Bottom - this.R3.Height) - 1);
                    }

                    Pen hoverBoxBorderPen = new Pen(_HoverBoxBorderColor);
                    foreColor.Color = this._HoverBoxColor;
                    //hoverBoxBorderPen.Color = this._HoverBoxBorderColor;
                    graphics.FillRectangle(foreColor, this.R3);
                    graphics.DrawRectangle(hoverBoxBorderPen, this.R3);
                    foreColor.Color = this.ForeColor;
                    System.Drawing.Font font = this.Font;
                    point = new Point(checked(this.R3.X + 10), checked(this.R3.Y + 5));
                    graphics.DrawString(str, font, foreColor, point);
                    foreColor.Dispose();
                }
                graphics.ResetClip();
                graphics.SmoothingMode = SmoothingMode.None;
            }

            Pen borderPen = new Pen(_BorderColor);
            borderPen.Color = this._BorderColor;
            borderPen.Width = 1f;
            borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            if (outerBorder)
            {
                graphics.DrawRectangle(borderPen, this.R1.X, this.R1.Y, checked(this.R1.Width - 1), checked(this.R1.Height - 1));
            }

            if (ShowAxis)
            {
                //X-Axis
                graphics.DrawLine(new Pen(Axis.LineColor), new Point(0, Height - 5), new Point(Width - 5, Height - 5));
                graphics.DrawString(">", Axis.Font, new SolidBrush(Axis.LineColor), new PointF(Width - 10, Height - 12));

                //Y-Axis
                graphics.DrawLine(new Pen(Axis.LineColor), new Point(0, 0), new Point(0, Height - 5));
                graphics.DrawString("^", Axis.Font, new SolidBrush(Axis.LineColor), new PointF(-4, -1));


                int divisor = Axis.Interval;
                decimal minValue = (decimal)Values.Min();
                decimal maxValue = (decimal)Values.Max();
                decimal value = ((maxValue) / divisor);
                decimal valueXaxis = ((maxValue) / (divisor));
                int x = 0;
                int y = -4;
                int j = 0;
                for (int i = 0; i < divisor; i++)
                {

                    int additionXaxis = (int)Values.Min() + ((int)valueXaxis * j);
                    int additionYaxis = (int)Values.Max() - ((int)value * j);
                    

                    SizeF sZYaxis = graphics.MeasureString(additionXaxis.ToString(), Axis.Font);

                    //X Axis
                    graphics.DrawString(" " + additionXaxis.ToString(), Axis.Font, new SolidBrush(Axis.NumberColors), new PointF(y, Height - 25));

                    graphics.DrawString("|", Axis.Font, new SolidBrush(Axis.LineColor), new PointF(y + sZYaxis.Width / 2, Height - 13));


                    //Y Axis
                    graphics.DrawString(" " + additionYaxis.ToString(), Axis.Font, new SolidBrush(Axis.NumberColors), new PointF(2, x));

                    graphics.DrawString("- ", Axis.Font, new SolidBrush(Axis.LineColor), new PointF(0, x));

                    
                    x += Height / divisor;
                    y += Width / divisor;
                    j++;
                }

                //graphics.DrawString("0", Axis.Font, new SolidBrush(Axis.NumberColors), new PointF(1, Height - 15));

            }

            borderPen.Dispose();
            pen.Dispose();
            base.OnPaint(e);
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.DesignMode)
            {
                this._GradientPointA = new Point(checked((int)Math.Round((double)this.Width / 2)), 0);
                this._GradientPointB = new Point(checked((int)Math.Round((double)this.Width / 2)), this.Height);
            }
            base.OnSizeChanged(e);
        }
        /**/

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

        /// <summary>
        /// The dash offset
        /// </summary>
        private float dashOffset = 0f;
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

        //public float Speed
        //{
        //    get { return speed; }
        //    set
        //    {
        //        speed = value;
        //        Invalidate();
        //    }
        //}


        #endregion

        #region Event

        #region Old Code
        //private float speed = 1;
        //   private void Timer_Tick(object sender, EventArgs e)
        //   {

        //       if (this.dashOffset + (0.1 * speed) > Math.Pow(10000000000000000000,10000000000000000000))
        //       {
        //           timer.Stop();
        //           timer.Enabled = false;
        //           timerDecrement.Enabled = true;
        //           timerDecrement.Start();
        //           //timerDecrement.Tick += TimerDecrement_Tick;
        //           Invalidate();
        //       }

        //       else
        //       {
        //           this.dashOffset += (0.1f * speed);
        //           Invalidate();
        //       }
        //   }


        //   private void TimerDecrement_Tick(object sender, EventArgs e)
        //   {

        //       if (this.dashOffset < 0)
        //       {
        //           timerDecrement.Stop();
        //           timerDecrement.Enabled = false;
        //           timer.Enabled = true;
        //           timer.Start();
        //           //timer.Tick += Timer_Tick;
        //           Invalidate();
        //       }

        //       else
        //       {
        //           this.dashOffset -= (0.1f * speed);
        //           Invalidate();
        //       }


        //   } 
        #endregion


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
                    timerDecrement.Interval = 1;
                    timer.Interval = 1;
                    timer.Start();
                }
            }

            if (!DesignMode)
            {
                timer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 1;
                    timer.Interval = 1;
                    timer.Start();
                }
            }

            

        }

        #endregion
        
        #region New Animation Timer
        
        #region Include in Private Field

        /// <summary>
        /// The speed multiplier
        /// </summary>
        private float speedMultiplier = 1;
        /// <summary>
        /// The change
        /// </summary>
        private float change = 0.1f;
        /// <summary>
        /// The reverse
        /// </summary>
        private bool reverse = true;
        /// <summary>
        /// The value
        /// </summary>
        private float value = 0;
        /// <summary>
        /// The minimum
        /// </summary>
        private float minimum = 0;
        /// <summary>
        /// The maximum
        /// </summary>
        private float maximum = 100;
        /// <summary>
        /// The sluggish
        /// </summary>
        private bool sluggish = false;
        #endregion

        #region Include in Public Properties


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitMetroGraph"/> is reverse.
        /// </summary>
        /// <value><c>true</c> if reverse; otherwise, <c>false</c>.</value>
        public bool Reverse
        {
            get { return reverse; }
            set
            {

                reverse = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the change.
        /// </summary>
        /// <value>The change.</value>
        public float Change
        {
            get { return change; }
            set
            {
                change = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the speed multiplier.
        /// </summary>
        /// <value>The speed multiplier.</value>
        public float SpeedMultiplier
        {
            get { return speedMultiplier; }
            set
            {
                speedMultiplier = value;
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


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitMetroGraph"/> is sluggish.
        /// </summary>
        /// <value><c>true</c> if sluggish; otherwise, <c>false</c>.</value>
        public bool Sluggish
        {
            get { return sluggish; }
            set
            {
                sluggish = value;
                Invalidate();
            }
        }

        #endregion

        #region Event
        /// <summary>
        /// The smooth pause
        /// </summary>
        private bool smoothPause = false;

        /// <summary>
        /// Gets or sets a value indicating whether [smooth pause].
        /// </summary>
        /// <value><c>true</c> if [smooth pause]; otherwise, <c>false</c>.</value>
        public bool SmoothPause
	    {
	        get { return smoothPause; }
	        set
	        {
                smoothPause = value;
	            Invalidate();
	        }
	    }
        /// <summary>
        /// Handles the Tick event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {

            if (Reverse)
            {
                if (SmoothPause)
                {
                    if (this.dashOffset + (Change * SpeedMultiplier) > 100 /*Math.Pow(10000000000000000000, 10000000000000000000)*/)
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
                        this.dashOffset += (Change * SpeedMultiplier);
                        Invalidate();
                    }
                }
                else
                {
                    if (this.dashOffset + (Change * SpeedMultiplier) > Math.Pow(10000000000000000000, 10000000000000000000))
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
                        this.dashOffset += (Change * SpeedMultiplier);
                        Invalidate();
                    }
                }
                


            }
            else
            {

                if (Sluggish)
                {
                    if (SmoothPause)
                    {
                        if (this.dashOffset + (Change * SpeedMultiplier) > 100 /* Math.Pow(10000000000000000000, 10000000000000000000)*/)
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
                            this.dashOffset += (Change * SpeedMultiplier);
                            Invalidate();
                        }
                    }
                    else
                    {
                        if (this.dashOffset + (Change * SpeedMultiplier) > Math.Pow(10000000000000000000, 10000000000000000000))
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
                            this.dashOffset += (Change * SpeedMultiplier);
                            Invalidate();
                        }
                    }
                    
                }
                else
                {
                    if (SmoothPause)
                    {
                        if (this.dashOffset + (Change * SpeedMultiplier) > 100 /*Math.Pow(10000000000000000000, 10000000000000000000)*/)
                        {
                            timerDecrement.Enabled = false;
                            timerDecrement.Stop();
                            //timerDecrement.Tick += TimerDecrement_Tick;
                            dashOffset = 0;
                            Invalidate();
                        }

                        else
                        {
                            this.dashOffset += (Change * SpeedMultiplier);
                            Invalidate();
                        }
                    }
                    else
                    {
                        if (this.dashOffset + (Change * SpeedMultiplier) > Math.Pow(10000000000000000000, 10000000000000000000))
                        {
                            timerDecrement.Enabled = false;
                            timerDecrement.Stop();
                            //timerDecrement.Tick += TimerDecrement_Tick;
                            dashOffset = 0;
                            Invalidate();
                        }

                        else
                        {
                            this.dashOffset += (Change * SpeedMultiplier);
                            Invalidate();
                        }
                    }
                    
                }

            }
        }


        /// <summary>
        /// Handles the Tick event of the TimerDecrement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TimerDecrement_Tick(object sender, EventArgs e)
        {
            if (this.dashOffset < 0)
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
                this.dashOffset -= (Change * SpeedMultiplier);
                Invalidate();
            }


        }


        #endregion

        #endregion

        #region Transparency


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

        #region Hover Animation

        #region Include in Private Field

        /// <summary>
        /// The hover circle automatic animate
        /// </summary>
        private bool hoverCircleAutoAnimate = false;
        /// <summary>
        /// The hover circle timer
        /// </summary>
        private System.Windows.Forms.Timer hoverCircleTimer = new System.Windows.Forms.Timer();
        /// <summary>
        /// The hover circle timer decrement
        /// </summary>
        private System.Windows.Forms.Timer hoverCircleTimerDecrement = new System.Windows.Forms.Timer();
        /// <summary>
        /// The hover circle speed multiplier
        /// </summary>
        private float hoverCircleSpeedMultiplier = 1;
        /// <summary>
        /// The hover circle change
        /// </summary>
        private float hoverCircleChange = 10f;
        /// <summary>
        /// The hover circle reverse
        /// </summary>
        private bool hoverCircleReverse = true;
        #endregion

        #region Include in Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [hover circle automatic animate].
        /// </summary>
        /// <value><c>true</c> if [hover circle automatic animate]; otherwise, <c>false</c>.</value>
        public bool HoverCircleAutoAnimate
        {
            get { return hoverCircleAutoAnimate; }
            set
            {
                hoverCircleAutoAnimate = value;

                if (value == true)
                {
                    hoverCircleTimer.Enabled = true;
                }

                else
                {
                    hoverCircleTimer.Enabled = false;
                    hoverCircleTimerDecrement.Enabled = false;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [hover cirlce reverse].
        /// </summary>
        /// <value><c>true</c> if [hover cirlce reverse]; otherwise, <c>false</c>.</value>
        public bool HoverCirlceReverse
        {
            get { return hoverCircleReverse; }
            set
            {

                hoverCircleReverse = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the hover circle change.
        /// </summary>
        /// <value>The hover circle change.</value>
        public float HoverCircleChange
        {
            get { return hoverCircleChange; }
            set
            {
                hoverCircleChange = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the hover circle speed multiplier.
        /// </summary>
        /// <value>The hover circle speed multiplier.</value>
        public float HoverCircleSpeedMultiplier
        {
            get { return hoverCircleSpeedMultiplier; }
            set
            {
                hoverCircleSpeedMultiplier = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the hover timer interval.
        /// </summary>
        /// <value>The hover timer interval.</value>
        public int HoverTimerInterval
        {
            get { return hoverCircleTimer.Interval; }
            set
            {
                hoverCircleTimer.Interval = value;
                timerDecrement.Interval = value;
                Invalidate();
            }
        }


        #endregion

        #region Event

        /// <summary>
        /// Handles the Tick event of the HoverTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HoverTimer_Tick(object sender, EventArgs e)
        {

            if (Reverse)
            {
                if (this.HoverCircleAlpha + (Change * SpeedMultiplier) > 333)
                {
                    hoverCircleTimer.Stop();
                    hoverCircleTimer.Enabled = false;
                    timerDecrement.Enabled = true;
                    timerDecrement.Start();
                    //timerDecrement.Tick += TimerDecrement_Tick;
                    Invalidate();
                }

                else
                {
                    this.HoverCircleAlpha += (int)(Change * SpeedMultiplier);
                    Invalidate();
                }
            }
            else
            {
                if (this.HoverCircleAlpha + (Change * SpeedMultiplier) > 333)
                {

                    timerDecrement.Enabled = false;
                    timerDecrement.Stop();
                    //timerDecrement.Tick += TimerDecrement_Tick;
                    HoverCircleAlpha = 100;
                    Invalidate();
                }

                else
                {
                    HoverCircleAlpha += (int)(Change * SpeedMultiplier);
                    Invalidate();
                }
            }
        }


        /// <summary>
        /// Handles the Tick event of the HoverTimerDecrement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void HoverTimerDecrement_Tick(object sender, EventArgs e)
        {
            if (this.HoverCircleAlpha < 100)
            {
                timerDecrement.Stop();
                timerDecrement.Enabled = false;
                hoverCircleTimer.Enabled = true;
                hoverCircleTimer.Start();
                //timer.Tick += Timer_Tick;
                Invalidate();
            }

            else
            {
                HoverCircleAlpha -= 1;
                Invalidate();
            }


        }


        #endregion

        #region Constructor

        /// <summary>
        /// Include in constructor.
        /// </summary>
        private void HoverIncludeInConstructor()
        {

            if (DesignMode)
            {
                hoverCircleTimer.Tick += HoverTimer_Tick;
                hoverCircleTimerDecrement.Tick += HoverTimerDecrement_Tick;
                if (HoverCircleAutoAnimate)
                {
                    hoverCircleTimerDecrement.Interval = 10;
                    hoverCircleTimer.Interval = 10;
                    hoverCircleTimer.Start();
                }
            }

            if (!DesignMode)
            {
                hoverCircleTimer.Tick += HoverTimer_Tick;
                hoverCircleTimerDecrement.Tick += HoverTimerDecrement_Tick;
                if (HoverCircleAutoAnimate)
                {
                    hoverCircleTimerDecrement.Interval = 10;
                    hoverCircleTimer.Interval = 10;
                    hoverCircleTimer.Start();
                }
            }

        }

        #endregion


        #endregion



    }

    

}