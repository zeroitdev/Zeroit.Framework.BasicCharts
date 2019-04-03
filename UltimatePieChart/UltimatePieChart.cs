// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="UltimatePieChart.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts
{
    #region PieChartControl

    /// <summary>
    /// A control for displaying pie charts.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ToolboxBitmap(typeof(ZeroitUltimatePieChart))]
    [Designer(typeof(ZeroitUltimatePieChartDesigner))]
    public partial class ZeroitUltimatePieChart : Control
    {
        #region Constructor
        /// <summary>
        /// Constructs a new instance of a ZeroitUltimatePieChart.
        /// </summary>
        public ZeroitUltimatePieChart()
        {
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);


            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor, true);


            this.items = new ItemCollection(this);
            this.style = new PieChartStyle(this);
            this.itemStyle = new PieChartItemStyle(this);
            this.focusedItemStyle = new PieChartItemStyle(this);

            
            this.toolTip = new ToolTip();

            IncludeInConstructor();
        }

        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    items.ReturnPieChart(this);
        //    style.ReturnPieChartStyle(this);
        //    itemStyle.ReturnPieChartItemStyle(this);
        //    focusedItemStyle.ReturnPieChartItemStyle(this);

        //    // check to see if the structure has changed and if we're not in the middle of a transaction
        //    if (isStructureChanged && transactionRef == 0)
        //    {
        //        DestructDrawingMetrics(this.drawingMetrics);
        //        this.drawingMetrics = ConstructDrawingMetrics(this.ClientRectangle, this.Padding);
        //    }
        //    else if (isVisualChanged && recreateGraphics && transactionRef == 0)
        //    {
        //        RecreateGraphics(this.drawingMetrics, this.ClientRectangle);
        //    }

        //    // clear any change markings
        //    isStructureChanged = false;
        //    isVisualChanged = false;
        //    recreateGraphics = false;

        //    Render(pevent.Graphics, this.drawingMetrics, ClientRectangle, Padding);
        //    //base.OnPaintBackground(pevent);

        //}

        #endregion

        #region Fields
        /// <summary>
        /// The collection which holds PieChartItems
        /// </summary>
        private ItemCollection items;

        /// <summary>
        /// The collection of styles that apply to this ZeroitUltimatePieChart.
        /// </summary>
        private PieChartStyle style;

        /// <summary>
        /// The style for default (non-focused) items.
        /// </summary>
        private PieChartItemStyle itemStyle;

        /// <summary>
        /// The style for focused items.
        /// </summary>
        private PieChartItemStyle focusedItemStyle;

        /// <summary>
        /// The PieChartItem that has mouse focus.
        /// </summary>
        private PieChartItem focusedItem;

        /// <summary>
        /// True if the structure of the pie has changed and the layout needs to be recalculated.
        /// </summary>
        private bool isStructureChanged = true;

        /// <summary>
        /// True if the pie needs to be redrawn.
        /// </summary>
        private bool isVisualChanged = true;

        /// <summary>
        /// True if the underlying pens and brushes need to be recreated when the control is redrawn.
        /// </summary>
        private bool recreateGraphics = true;

        /// <summary>
        /// A reference counter for the number of change transactions that have been begun and not ended.
        /// </summary>
        private int transactionRef = 0;

        /// <summary>
        /// A list of DrawingMetrics objects that store calculated drawing data about each pie slice.
        /// </summary>
        private List<DrawingMetrics> drawingMetrics = new List<DrawingMetrics>();

        /// <summary>
        /// The ToolTip control that is used when hovering over pie slices.
        /// </summary>
        private ToolTip toolTip;

        /// <summary>
        /// The default ToolTip delay, which is stored when the delay is overwritten by this control.
        /// </summary>
        private int toolTipDefaultDelay;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the edge brightness factor.
        /// </summary>
        /// <value>The edge brightness factor.</value>
        [Browsable(false)]
        public float EdgeBrightnessFactor
        {
            get
            {
                return ItemStyle.EdgeBrightnessFactor;
            }
            set
            {
                ItemStyle.EdgeBrightnessFactor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the surface alpha transparency.
        /// </summary>
        /// <value>The surface alpha transparency.</value>
        [Browsable(false)]
        public float SurfaceAlphaTransparency
        {
            get
            {
                return ItemStyle.SurfaceAlphaTransparency;
            }
            set
            {
                ItemStyle.SurfaceAlphaTransparency = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the surface brightness factor.
        /// </summary>
        /// <value>The surface brightness factor.</value>
        [Browsable(false)]
        public float SurfaceBrightnessFactor
        {
            get
            {
                return ItemStyle.SurfaceBrightnessFactor;
            }
            set
            {
                ItemStyle.SurfaceBrightnessFactor = value;
                Invalidate();
            }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        /// <summary>
        /// Gets or sets the tool tip.
        /// </summary>
        /// <value>The tool tip.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ToolTip ToolTip
        {
            get { return toolTip; }
            set
            {
                toolTip = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The collection which holds PieChartItems
        /// </summary>
        /// <value>The items.</value>
        [Browsable(true)]
        [Category("Pie Chart")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ItemCollection Items
        {
            get
            {
                return items;
            }
            
        }

        /// <summary>
        /// The collection of styles that apply to this ZeroitUltimatePieChart.
        /// </summary>
        /// <value>The style.</value>
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PieChartStyle Style
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The collection of styles that apply to this ZeroitUltimatePieChart.
        /// </summary>
        /// <value>The item style.</value>
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PieChartItemStyle ItemStyle
        {
            get
            {
                return itemStyle;
            }
            set
            {
                itemStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The collection of styles that apply to this ZeroitUltimatePieChart.
        /// </summary>
        /// <value>The focused item style.</value>
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PieChartItemStyle FocusedItemStyle
        {
            get
            {
                return focusedItemStyle;
            }
            set
            {
                focusedItemStyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The PieChartItem that has mouse focus.
        /// </summary>
        /// <value>The focused item.</value>
        [Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        private PieChartItem FocusedItem
        {
            get
            {
                return focusedItem;
            }
            set
            {
                focusedItem = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the rotation of the pie chart.  This is represented in radians, with positive values indicating
        /// a rotation in the clockwise direction.
        /// </summary>
        /// <value>The rotation.</value>
        [Browsable(true)]
        [Category("Pie Chart")]
        [DefaultValue(0F)]
        [Description("The rotation around the center of the control, in radians.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public float Rotation
        {
            get
            {
                return Style.Rotation;
            }
            set
            {
                Style.Rotation = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the inclination of the control.  This is represented in radians, where an angle of 0
        /// represents looking at the edge of the control and an angle of pi represents looking
        /// straight down at the top of the pie.
        /// </summary>
        /// <value>The inclination.</value>
        /// <remarks>The angle must be greater than 0 and less than or equal to pi radians.</remarks>
        [Browsable(true)]
        [Category("Pie Chart")]
        [DefaultValue((float)(Math.PI / 6))]
        [Description("The inclination of the control, in radians.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public float Inclination
        {
            get
            {
                return Style.Inclination;
            }
            set
            {
                Style.Inclination = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets thickness of the pie, in pixels.
        /// </summary>
        /// <value>The thickness.</value>
        /// <remarks>This represents the three-dimensional thickness of the control.
        /// The actual visual thickness of the control depends on the inclination.  To determine what the apparent
        /// thickness of the control is, use the Style.VisualHeight property.  The thickness must be greater than or equal to 0.</remarks>
        [Browsable(true)]
        [Category("Pie Chart")]
        [DefaultValue(10)]
        [Description("The thickness of the pie, in pixels.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public float Thickness
        {
            get
            {
                return Style.Thickness;
            }
            set
            {
                Style.Thickness = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets radius of the control, in pixels.  If AutoSizePie is set to true, this value will be ignored.
        /// </summary>
        /// <value>The radius.</value>
        [Browsable(true)]
        [Category("Pie Chart")]
        [DefaultValue(200)]
        [Description("The radius of the pie, in pixels.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public float Radius
        {
            get
            {
                return Style.Radius;
            }
            set
            {
                Style.Radius = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets if the pie should be sized to fit the control.  If this property is true,
        /// the Radius property is ignored.
        /// </summary>
        /// <value><c>true</c> if [automatic size pie]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Pie Chart")]
        [DefaultValue(false)]
        [Description("True if the control should size the pie to fit the control.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool AutoSizePie
        {
            get
            {
                return Style.AutoSizePie;
            }
            set
            {
                Style.AutoSizePie = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets if edges should be drawn on pie slices.  If false, edges are not drawn.
        /// </summary>
        /// <value><c>true</c> if [show edges]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Pie Chart")]
        [DefaultValue(true)]
        [Description("True if the edges of pie slices should be drawn.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool ShowEdges
        {
            get
            {
                return Style.ShowEdges;
            }
            set
            {
                Style.ShowEdges = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets if text should be drawn on pie slices.
        /// </summary>
        /// <value>The text display mode.</value>
        /// <remarks>This can have one of three values.  If TextDisplayTypes.Always, the text is always drawn.
        /// If TextDisplayTypes.FitOnly, the text is drawn only if it fits in the wedge.  If TextDisplayTypes.Never,
        /// the text is never drawn.</remarks>
        [Browsable(true)]
        [Category("Pie Chart")]
        [DefaultValue(ZeroitUltimatePieChart.TextDisplayTypes.FitOnly)]
        [Description("TextDisplayModeTypes.Always if text should always be drawn, TextDisplayModeTypes.Never if text should never be drawn, or TextDisplayModeTypes.FitOnly if text should be drawn only when it fits in the pie slice.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TextDisplayTypes TextDisplayMode
        {
            get
            {
                return Style.TextDisplayMode;
            }
            set
            {
                Style.TextDisplayMode = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets if tool tips should be shown when the mouse hovers over pie slices.  If false, tool tips are not shown.
        /// </summary>
        /// <value><c>true</c> if [show tool tips]; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Category("Pie Chart")]
        [DefaultValue(true)]
        [Description("True if tool tips for pie slices should be drawn.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool ShowToolTips
        {
            get
            {
                return Style.ShowToolTips;
            }
            set
            {
                Style.ShowToolTips = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Determines how transparent surfaces of pie slices are.  A SurfaceAlphaTransparency of 1 means the surfaces
        /// are completely opaque, while a SurfaceAlphaTransparency of 0 means the surfaces are completely transparent.
        /// </summary>
        /// <param name="padding">The padding.</param>
        /// <returns>Size.</returns>
        //[Browsable(true)]
        //[Category("Pie Chart")]
        //[DefaultValue(1F)]
        //[Description("The alpha transparency multiplier that should be applied to surface colors (between 0 and 1).")]
        //public float SurfaceAlphaTransparency
        //{
        //  get
        //  {
        //    return Style.SurfaceAlphaTransparency;
        //  }
        //  set
        //  {
        //    Style.SurfaceAlphaTransparency = value;
        //  }
        //}
        #endregion

        #region Methods
        public Size GetChartSize(Padding padding)
        {
            float maxOffset = GetMaximumItemOffset();
            int width = (int)(2 * (Radius + maxOffset) + padding.Horizontal);
            int height = (int)(2 * (Radius * Style.HeightWidthRatio + maxOffset) + Style.VisualThickness + padding.Vertical);
            return new Size(width, height);
        }

        /// <summary>
        /// Gets the maximum offset of all PieChartItems in the Items collection.
        /// </summary>
        /// <returns>The maximum offset of all items.</returns>
        private float GetMaximumItemOffset()
        {
            float max = 0;
            foreach (PieChartItem item in Items)
                max = Math.Max(max, item.Offset);
            return max;
        }

        /// <summary>
        /// Calculates the radius that will be used for autosizing the pie to fit the control.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        /// <param name="padding">The padding.</param>
        /// <returns>The radius that will fit the pie in the control bounds.</returns>
        private float GetAutoSizeRadius(Rectangle bounds, Padding padding)
        {
            float maxOffset = GetMaximumItemOffset();
            float widthHeightRatio = Style.HeightWidthRatio;
            float width = (bounds.Width - padding.Horizontal) / 2;
            float height = (bounds.Height - padding.Vertical - Style.VisualThickness) / 2;

            float radius = Math.Max(PieChartStyle.AutoSizeMinimumRadius, Math.Min(width - maxOffset, (height - maxOffset) / widthHeightRatio));

            return radius;
        }

        /// <summary>
        /// Constructs the array of DrawingMetrics, which store drawing information about each pie slice.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        /// <param name="padding">The padding.</param>
        /// <returns>List&lt;DrawingMetrics&gt;.</returns>
        private List<DrawingMetrics> ConstructDrawingMetrics(Rectangle bounds, Padding padding)
        {
            List<DrawingMetrics> results = new List<DrawingMetrics>();
            try
            {
                // increment the transaction reference counter so that any modifications in this method don't lead to a recursive redrawing of the control.
                transactionRef++;

                if (Items.TotalItemWeight == 0)
                    return results;

                if (Style.AutoSizePie)
                    Style.RadiusInternal = GetAutoSizeRadius(bounds, padding);

                float angleUsed = Style.Rotation;
                for (int i = 0; i < Items.Count; i++)
                {
                    DrawingMetrics dm = new DrawingMetrics(this, Items[i], bounds, angleUsed, (float)(Items[i].Percent * Math.PI * 2));
                    results.Add(dm);
                    angleUsed += (float)dm.SweepAngle;
                }

                // sort the drawing metrics in the order they should be drawn
                results.Sort();
            }
            finally
            {
                // end our transaction
                transactionRef--;
            }

            return results;
        }

        /// <summary>
        /// Recreates all of the pens and brushes used by the DrawingMetrics that have been constructed.
        /// </summary>
        /// <param name="drawingMetrics">The drawing metrics.</param>
        /// <param name="bounds">The bounds.</param>
        private void RecreateGraphics(List<DrawingMetrics> drawingMetrics, Rectangle bounds)
        {
            for (int i = 0; i < drawingMetrics.Count; i++)
            {
                drawingMetrics[i].DrawingBounds = bounds;
                drawingMetrics[i].RecreateGraphics();
            }
        }

        /// <summary>
        /// Destroys all of the DrawingMetrics currently in the array by releasing all of their resources.
        /// </summary>
        /// <param name="drawingMetrics">The drawing metrics.</param>
        private void DestructDrawingMetrics(List<DrawingMetrics> drawingMetrics)
        {
            foreach (DrawingMetrics metric in drawingMetrics)
                metric.DestroyResources();

            drawingMetrics.Clear();
        }

        /// <summary>
        /// Set the currently focused PieChartItem.
        /// </summary>
        /// <param name="item">The item that currently has mouse focus.</param>
        private void SetFocusedItem(PieChartItem item)
        {
            if (item != this.FocusedItem)
            {
                FireItemFocusChanging(this.FocusedItem, item);
                this.focusedItem = item;
                FireItemFocusChanged();

                MarkVisualChange(true);
            }

            // check to see if the item has a tool tip and if it should be displayed
            if (this.FocusedItem != null && this.FocusedItem.ToolTipText != null && this.Style.ShowToolTips)
            {
                toolTip.SetToolTip(this, this.FocusedItem.ToolTipText);
            }
            else
            {
                toolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Performs a hit test to see which PieChartItem is under the current mouse position.
        /// </summary>
        /// <param name="controlPoint">The control point.</param>
        /// <returns>The DrawingMetrics of the item under the point, or null if no item is there.</returns>
        private DrawingMetrics HitTest(PointF controlPoint)
        {
            if (drawingMetrics.Count == 0)
                return null;

            // translated the point so the origin is at the center of the pie
            PointF transPoint = new PointF(controlPoint.X - Width / 2, controlPoint.Y - (Height + Style.VisualThickness) / 2);

            // if a single item is both the frontmost (bottom) and rearmost (top) item in the display, special hit testing is needed
            bool itemBottomTop = drawingMetrics[0].IsBottomItem && drawingMetrics[0].IsTopItem;
            if (itemBottomTop)
            {
                // check to see if the top surface or exterior surface of the control is hit, but not the interior surface
                if (drawingMetrics[0].TopRegion.IsVisible(transPoint) || drawingMetrics[0].ExteriorRegion.IsVisible(transPoint))
                    return drawingMetrics[0];
            }

            // check surfaces of all controls in order, returning the first hit
            for (int i = drawingMetrics.Count - 1; i >= 0; i--)
            {
                if (drawingMetrics[i].VisibleRegion.IsVisible(transPoint))
                    return drawingMetrics[i];
            }

            return null;
        }

        /// <summary>
        /// Handles the MouseEnter event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            toolTipDefaultDelay = toolTip.AutoPopDelay;
            toolTip.AutoPopDelay = int.MaxValue;
        }

        /// <summary>
        /// Handles the MouseLeave event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            toolTip.AutoPopDelay = toolTipDefaultDelay;
            SetFocusedItem(null);
        }

        /// <summary>
        /// Handles the MouseMove event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            DrawingMetrics m = HitTest(new PointF(e.X, e.Y));
            SetFocusedItem(m == null ? null : m.Item);
        }

        /// <summary>
        /// Handles the MouseClick event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (this.FocusedItem != null)
                FireItemClicked(this.FocusedItem);
        }

        /// <summary>
        /// Handles the DoubleClick event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (this.FocusedItem != null)
                FireItemDoubleClicked(this.FocusedItem);
        }

        /// <summary>
        /// Handles the SizeChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (this.AutoSizePie)
                MarkStructuralChange();
        }

        /// <summary>
        /// Handles the PaddingChanged event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);

            if (this.AutoSizePie)
                MarkStructuralChange();
        }

        /// <summary>
        /// Renders the given DrawingMetrics, which are calculated using ConstructDrawingMetrics.
        /// </summary>
        /// <param name="g">The graphics surface on which the chart is being rendered.</param>
        /// <param name="drawingMetrics">The drawing metrics to render.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="padding">The padding.</param>
        private void Render(Graphics g, List<DrawingMetrics> drawingMetrics, Rectangle bounds, Padding padding)
        {
            // use a high-quality smoothing mode
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TranslateTransform(bounds.Left + padding.Left + (bounds.Width - padding.Horizontal) / 2, bounds.Top + padding.Top + (bounds.Height - padding.Vertical + Style.VisualThickness) / 2);

            // don't draw anything if there's nothing to draw
            if (drawingMetrics.Count == 0)
                return;

            // if there is an item that is both at the bottom and top of the pie, special drawing considerations
            // are needed
            bool itemBottomTop = drawingMetrics[0].IsBottomItem && drawingMetrics[0].IsTopItem;
            if (itemBottomTop)
            {
                drawingMetrics[0].RenderBottom(g);
                drawingMetrics[0].RenderInterior(g);
            }
            else
            {
                drawingMetrics[0].RenderBottom(g);
                drawingMetrics[0].RenderInterior(g);
                drawingMetrics[0].RenderExterior(g);
            }

            for (int i = 1; i < drawingMetrics.Count; i++)
            {
                drawingMetrics[i].RenderBottom(g);
                drawingMetrics[i].RenderInterior(g);
                drawingMetrics[i].RenderExterior(g);
                drawingMetrics[i].RenderTop(g);
            }

            if (itemBottomTop)
                drawingMetrics[0].RenderExterior(g);
            drawingMetrics[0].RenderTop(g);

            foreach (DrawingMetrics metric in this.drawingMetrics)
            {
                metric.RenderText(g);
            }
        }

        /// <summary>
        /// Save the chart as an image.
        /// </summary>
        /// <param name="fileName">The path to the file where the image will be saved.</param>
        /// <param name="format">The format to save the image in.</param>
        /// <param name="sizeInPixels">The size of the image, in pixels.</param>
        public void SaveAs(string fileName, ImageFormat format, Size sizeInPixels)
        {
            SaveAs(fileName, format, sizeInPixels, Padding.Empty);
        }

        /// <summary>
        /// Saves the chart as an image.
        /// </summary>
        /// <param name="fileName">The path to the file where the image will be saved.</param>
        /// <param name="format">The format to save the image in.</param>
        /// <param name="sizeInPixels">The size of the image, in pixels.</param>
        /// <param name="padding">The padding which defines the border of the image.</param>
        public void SaveAs(string fileName, ImageFormat format, Size sizeInPixels, Padding padding)
        {
            List<DrawingMetrics> metrics = ConstructDrawingMetrics(new Rectangle(Point.Empty, sizeInPixels), padding);
            using (Bitmap bitmap = new Bitmap(sizeInPixels.Width, sizeInPixels.Height))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // fill in the background
                g.FillRectangle(Brushes.White, 0, 0, sizeInPixels.Width, sizeInPixels.Height);

                // render the chart
                Render(g, metrics, new Rectangle(Point.Empty, sizeInPixels), padding);

                // save the image
                bitmap.Save(fileName, format);
            }

            DestructDrawingMetrics(metrics);
        }

        /// <summary>
        /// Registers a PrintDocument to print this pie chart.
        /// </summary>
        /// <param name="doc">The PrintDocument to register.</param>
        public void AttachPrintDocument(PrintDocument doc)
        {
            doc.PrintPage += new PrintPageEventHandler(OnPrintPage);
        }

        /// <summary>
        /// Called by a registered PrintDocument to control printing of the chart.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            List<DrawingMetrics> metrics = ConstructDrawingMetrics(e.MarginBounds, Padding.Empty);

            e.Graphics.SetClip(e.MarginBounds);
            Render(e.Graphics, metrics, e.MarginBounds, Padding.Empty);
            e.HasMorePages = false;

            DestructDrawingMetrics(metrics);
        }

        /// <summary>
        /// Handles the painting of the control.
        /// </summary>
        /// <param name="pe">The paint event arguments.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {

            if (AllowTransparency)
            {
                MakeTransparent(this, pe.Graphics);
            }

            // check to see if the structure has changed and if we're not in the middle of a transaction
            if (isStructureChanged && transactionRef == 0)
            {
                DestructDrawingMetrics(this.drawingMetrics);
                this.drawingMetrics = ConstructDrawingMetrics(this.ClientRectangle, this.Padding);
            }
            else if (isVisualChanged && recreateGraphics && transactionRef == 0)
            {
                RecreateGraphics(this.drawingMetrics, this.ClientRectangle);
            }

            // clear any change markings
            isStructureChanged = false;
            isVisualChanged = false;
            recreateGraphics = false;

            Render(pe.Graphics, this.drawingMetrics, ClientRectangle, Padding);

        }
        #endregion

        #region Transaction Methods
        /// <summary>
        /// Starts a modification transaction.  As long as any modification trasactions are open,
        /// the changes made to the control will not be reflected.  It is necessary to call
        /// EndModification for each call to BeginModification; otherwise, the control will
        /// never redraw.
        /// </summary>
        public void BeginModification()
        {
            this.transactionRef++;
        }

        /// <summary>
        /// Ends a modification transaction.  As long as any modification trasactions are open,
        /// the changes made to the control will not be reflected.  It is necessary to call
        /// EndModification for each call to BeginModification; otherwise, the control will
        /// never redraw.
        /// </summary>
        public void EndModification()
        {
            this.transactionRef = Math.Max(0, this.transactionRef - 1);
            if (this.transactionRef == 0 && this.isVisualChanged)
                this.Invalidate();
        }

        /// <summary>
        /// Sets a flag that indicates the control has changed structurally, and that DrawingMetrics
        /// will need to be completely recreated.
        /// </summary>
        internal void MarkStructuralChange()
        {
            this.isStructureChanged = true;
            this.isVisualChanged = true;
            if (this.transactionRef == 0)
                this.Invalidate();
            else
                Console.WriteLine("Pie chart not invalidated");
        }

        /// <summary>
        /// Sets a flag that indicates that the control needs to be refreshed, but that no structural
        /// or resource (pen/brush) altering changes were made.
        /// </summary>
        internal void MarkVisualChange()
        {
            MarkVisualChange(false);
        }

        /// <summary>
        /// Sets a flag that indicates the control needs to be refreshed.  If recreateGraphics is true,
        /// then pens and brushes will be recreated.
        /// </summary>
        /// <param name="recreateGraphics">True if pens and brushes should be recreated.</param>
        internal void MarkVisualChange(bool recreateGraphics)
        {
            this.isVisualChanged = true;
            this.recreateGraphics = this.recreateGraphics || recreateGraphics;
            if (this.transactionRef == 0)
                this.Invalidate();
        }
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
        /// The speed multiplier
        /// </summary>
        private float speedMultiplier = 1;
        /// <summary>
        /// The change
        /// </summary>
        private float change = 0.01f;
        /// <summary>
        /// The reverse
        /// </summary>
        private bool reverse = true;
        /// <summary>
        /// The sluggish
        /// </summary>
        private bool sluggish = false;
        #endregion

        #region Include in Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [automatic animate].
        /// </summary>
        /// <value><c>true</c> if [automatic animate]; otherwise, <c>false</c>.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
        /// Gets or sets a value indicating whether this <see cref="ZeroitUltimatePieChart"/> is reverse.
        /// </summary>
        /// <value><c>true</c> if reverse; otherwise, <c>false</c>.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float Change
        {
            get { return change; }
            set
            {
                if (value > 0.03)
                {
                    value = 0.03f;
                }

                if (value < 0.01f)
                {
                    value = 0.01f;
                }

                change = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the timer interval.
        /// </summary>
        /// <value>The timer interval.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
        /// Gets or sets a value indicating whether this <see cref="ZeroitUltimatePieChart"/> is sluggish.
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
        /// Handles the Tick event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {

            //Inclination
            if (Reverse)
            {

                if (this.Inclination + (Change) > 1)
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
                    this.Inclination += (Change);
                    Invalidate();
                }


            }
            else
            {

                if (Sluggish)
                {
                    if (this.Inclination + (Change) > 1)
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
                        this.Inclination += (Change);
                        Invalidate();
                    }
                }
                else
                {
                    if (this.Inclination + (Change ) > 1)
                    {
                        timerDecrement.Enabled = false;
                        timerDecrement.Stop();
                        //timerDecrement.Tick += TimerDecrement_Tick;
                        Inclination = 0;
                        Invalidate();
                    }

                    else
                    {
                        this.Inclination += (Change );
                        Invalidate();
                    }
                }

            }

            //Rotation
            if (Reverse)
            {

                if (this.Rotation + (Change ) > 6)
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
                    this.Rotation += (Change );
                    Invalidate();
                }


            }
            else
            {

                if (Sluggish)
                {
                    if (this.Rotation + (Change ) > 6)
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
                        this.Rotation += (Change );
                        Invalidate();
                    }
                }
                else
                {
                    if (this.Rotation + (Change ) > 6)
                    {
                        timerDecrement.Enabled = false;
                        timerDecrement.Stop();
                        //timerDecrement.Tick += TimerDecrement_Tick;
                        Rotation = 0;
                        Invalidate();
                    }

                    else
                    {
                        this.Rotation += (Change );
                        Invalidate();
                    }
                }

            }

            //Radius
            if (Reverse)
            {

                if (this.Radius + (1 ) > (0.55f * Width))
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
                    this.Radius += (1 );
                    Invalidate();
                }


            }
            else
            {

                if (Sluggish)
                {
                    if (this.Radius + (1 ) > (0.55f * Width))
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
                        this.Radius += (1);
                        Invalidate();
                    }
                }
                else
                {
                    if (this.Radius + (1 ) > (0.55f * Width))
                    {
                        timerDecrement.Enabled = false;
                        timerDecrement.Stop();
                        //timerDecrement.Tick += TimerDecrement_Tick;
                        Radius = 0;
                        Invalidate();
                    }

                    else
                    {
                        this.Radius += (1 );
                        Invalidate();
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
            if (this.Inclination < 0.01)
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
                this.Inclination -= (Change );
                Invalidate();
            }

            if (this.Rotation < 0.01)
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
                this.Rotation -= (Change );
                Invalidate();
            }

            if (this.Radius < (0.2f * Width))
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
                this.Radius -= (1 );
                Invalidate();
            }
        }


        #endregion

        #region Old Event

        #region Old Code

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    if (this.Inclination + 0.01 > 1)
        //    {
        //        timer.Stop();
        //        timer.Enabled = false;
        //        timerDecrement.Enabled = true;
        //        timerDecrement.Start();
        //        //timerDecrement.Tick += TimerDecrement_Tick;
        //        Invalidate();
        //    }

        //    else
        //    {
        //        this.Inclination += 0.01f;
        //        Invalidate();
        //    }


        //    if (this.Rotation + 0.01 > 6)
        //    {
        //        timer.Stop();
        //        timer.Enabled = false;
        //        timerDecrement.Enabled = true;
        //        timerDecrement.Start();
        //        //timerDecrement.Tick += TimerDecrement_Tick;
        //        Invalidate();
        //    }

        //    else
        //    {
        //        this.Rotation += 0.01f;
        //        Invalidate();
        //    }

        //    if (this.Radius + 1 > (0.55f * Width))
        //    {
        //        timer.Stop();
        //        timer.Enabled = false;
        //        timerDecrement.Enabled = true;
        //        timerDecrement.Start();
        //        //timerDecrement.Tick += TimerDecrement_Tick;
        //        Invalidate();
        //    }

        //    else
        //    {
        //        this.Radius += 1f;
        //        Invalidate();
        //    }


        //}

        //private void TimerDecrement_Tick(object sender, EventArgs e)
        //{
        //    if (this.Inclination < 0.01)
        //    {
        //        timerDecrement.Stop();
        //        timerDecrement.Enabled = false;
        //        timer.Enabled = true;
        //        timer.Start();
        //        //timer.Tick += Timer_Tick;
        //        Invalidate();
        //    }

        //    else
        //    {
        //        this.Inclination -= 0.01f;
        //        Invalidate();
        //    }

        //    if (this.Rotation < 0.01)
        //    {
        //        timerDecrement.Stop();
        //        timerDecrement.Enabled = false;
        //        timer.Enabled = true;
        //        timer.Start();
        //        //timer.Tick += Timer_Tick;
        //        Invalidate();
        //    }

        //    else
        //    {
        //        this.Rotation -= 0.01f;
        //        Invalidate();
        //    }


        //    if (this.Radius < (0.2f * Width))
        //    {
        //        timerDecrement.Stop();
        //        timerDecrement.Enabled = false;
        //        timer.Enabled = true;
        //        timer.Start();
        //        //timer.Tick += Timer_Tick;
        //        Invalidate();
        //    }

        //    else
        //    {
        //        this.Radius -= 1f;
        //        Invalidate();
        //    }
        //}

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
                    timerDecrement.Interval = 500;
                    timer.Interval = 500;
                    timer.Start();
                }
            }

            if (!DesignMode)
            {
                timer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 500;
                    timer.Interval = 500;
                    timer.Start();
                }
            }

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

    #endregion
}
