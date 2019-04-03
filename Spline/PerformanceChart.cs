// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-02-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="PerformanceChart.cs" company="Zeroit Dev Technologies">
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
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts
{
    /// <summary>
    /// Class ZeroitSplineGraph.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    public partial class ZeroitSplineGraph 
	{

        #region Performance Chart

        #region ENUM 

        /// <summary>
        /// Enum Border3DStyle
        /// </summary>
        public new enum Border3DStyle
        {
            /// <summary>
            /// The none
            /// </summary>
            None,
            /// <summary>
            /// The adjust
            /// </summary>
            Adjust,
            /// <summary>
            /// The bump
            /// </summary>
            Bump,
            /// <summary>
            /// The etched
            /// </summary>
            Etched,
            /// <summary>
            /// The flat
            /// </summary>
            Flat,
            /// <summary>
            /// The raised
            /// </summary>
            Raised,
            /// <summary>
            /// The raised inner
            /// </summary>
            RaisedInner,
            /// <summary>
            /// The raised outer
            /// </summary>
            RaisedOuter,
            /// <summary>
            /// The sunken
            /// </summary>
            Sunken,
            /// <summary>
            /// The sunken inner
            /// </summary>
            SunkenInner,
            /// <summary>
            /// The sunken outer
            /// </summary>
            SunkenOuter,
            /// <summary>
            /// The single
            /// </summary>
            Single
        }


        #endregion

        #region *** Constants ***

        // Keep only a maximum MAX_VALUE_COUNT amount of values; This will allow
        /// <summary>
        /// The maximum value count
        /// </summary>
        private const int MAX_VALUE_COUNT = 512;
        // Draw a background grid with a fixed line spacing
        /// <summary>
        /// The grid spacing
        /// </summary>
        private const int GRID_SPACING = 16;

        #endregion


        #region *** Member Variables ***

        // Amount of currently visible values (calculated from control width and value spacing)
        /// <summary>
        /// The visible values
        /// </summary>
        private int visibleValues = 0;
        // Horizontal value space in Pixels
        /// <summary>
        /// The value spacing
        /// </summary>
        private int valueSpacing = 5;
        // The currently highest displayed value, required for Relative Scale Mode
        /// <summary>
        /// The current maximum value
        /// </summary>
        private decimal currentMaxValue = 0;
        // Offset value for the scrolling grid
        /// <summary>
        /// The grid scroll offset
        /// </summary>
        private int gridScrollOffset = 0;
        // The current average value
        /// <summary>
        /// The average value
        /// </summary>
        private decimal averageValue = 0;
        // Border Style
        /// <summary>
        /// The b3dstyle
        /// </summary>
        private Border3DStyle b3dstyle = Border3DStyle.None;
        // Scale mode for value aspect ratio
        /// <summary>
        /// The scale mode
        /// </summary>
        private ScaleMode scaleMode = ScaleMode.Absolute;
        // Timer Mode
        /// <summary>
        /// The timer mode
        /// </summary>
        private TimerMode timerMode;
        // List of stored values
        /// <summary>
        /// The draw values
        /// </summary>
        private List<decimal> drawValues = new List<decimal>(MAX_VALUE_COUNT);
        // Value queue for Timer Modes
        /// <summary>
        /// The waiting values
        /// </summary>
        private Queue<decimal> waitingValues = new Queue<decimal>();
        // Style and Design
        /// <summary>
        /// The perf chart style
        /// </summary>
        private PerfChartStyle perfChartStyle = new PerfChartStyle();

        #endregion


        #region *** Constructors ***


        #endregion


        #region *** Properties ***

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Appearance"), Description("Appearance and Style")]
        /// <summary>
        /// Gets or sets the perf chart style.
        /// </summary>
        /// <value>The perf chart style.</value>
        public PerfChartStyle PerfChartStyle
        {
            get { return perfChartStyle; }
            set
            {
                perfChartStyle = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the border style.
        /// </summary>
        /// <value>The border style.</value>
        [DefaultValue(typeof(Border3DStyle), "Sunken"), Description("BorderStyle"), Category("Appearance")]
        public new Border3DStyle BorderStyle
        {
            get
            {
                return b3dstyle;
            }
            set
            {
                b3dstyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the scale mode.
        /// </summary>
        /// <value>The scale mode.</value>
        public ScaleMode ScaleMode
        {
            get { return scaleMode; }
            set { scaleMode = value; }
        }

        /// <summary>
        /// Gets or sets the timer mode.
        /// </summary>
        /// <value>The timer mode.</value>
        public TimerMode TimerMode
        {
            get { return timerMode; }
            set
            {
                if (value == TimerMode.Disabled)
                {
                    // Stop and append only when changed
                    if (timerMode != TimerMode.Disabled)
                    {
                        timerMode = value;

                        tmrRefresh.Stop();
                        // If there are any values in the queue, append them
                        ChartAppendFromQueue();
                    }
                }
                else
                {
                    timerMode = value;
                    tmrRefresh.Start();
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the timer interval.
        /// </summary>
        /// <value>The timer interval.</value>
        /// <exception cref="ArgumentOutOfRangeException">TimerInterval - The Timer interval must be greater then 15</exception>
        public int TimerInterval
        {
            get { return tmrRefresh.Interval; }
            set
            {
                if (value < 15)
                    throw new ArgumentOutOfRangeException("TimerInterval", value, "The Timer interval must be greater then 15");
                else
                    tmrRefresh.Interval = value;

                Invalidate();
            }
        }



        #endregion


        #region *** Public Methods ***

        /// <summary>
        /// Clears the whole chart
        /// </summary>
        public void Clear()
        {
            Items.Clear();
            Invalidate();
        }


        /// <summary>
        /// Adds a value to the Chart Line
        /// </summary>
        /// <param name="value">progress value</param>
        /// <exception cref="Exception">
        /// </exception>
        public void AddValue(decimal value)
        {
            if (scaleMode == ScaleMode.Absolute && value > 100M)
                throw new Exception(String.Format("Items greater then 100 not allowed in ScaleMode: Absolute ({0})", value));


            switch (timerMode)
            {
                case TimerMode.Disabled:
                    ChartAppend(value);
                    Invalidate();
                    break;
                case TimerMode.Simple:
                case TimerMode.SynchronizedAverage:
                case TimerMode.SynchronizedSum:
                    // For all Timer Modes, the Items are stored in the Queue
                    AddValueToQueue(value);
                    break;
                default:
                    throw new Exception(String.Format("Unsupported TimerMode: {0}", timerMode));
            }
        }

        #endregion


        #region *** Private Methods: Common ***

        /// <summary>
        /// Add value to the queue for a timed refresh
        /// </summary>
        /// <param name="value">The value.</param>
        private void AddValueToQueue(decimal value)
        {
            waitingValues.Enqueue(value);
        }


        /// <summary>
        /// Appends value <paramref name="value" /> to the chart (without redrawing)
        /// </summary>
        /// <param name="value">performance value</param>
        private void ChartAppend(decimal value)
        {
            // Insert at first position; Negative values are flatten to 0 (zero)
            Items.Insert(0, Math.Max(value, 0));

            // Remove last item if maximum value count is reached
            if (Items.Count > MAX_VALUE_COUNT)
            {
                Items.RemoveAt(MAX_VALUE_COUNT);

            }


            // Calculate horizontal grid offset for "scrolling" effect
            gridScrollOffset += valueSpacing;
            if (gridScrollOffset > GRID_SPACING)
                gridScrollOffset = gridScrollOffset % GRID_SPACING;
        }


        /// <summary>
        /// Appends Items from queue
        /// </summary>
        private void ChartAppendFromQueue()
        {
            // Proceed only if there are values at all
            if (waitingValues.Count > 0)
            {
                if (timerMode == TimerMode.Simple)
                {
                    while (waitingValues.Count > 0)
                        ChartAppend(waitingValues.Dequeue());
                }
                else if (timerMode == TimerMode.SynchronizedAverage ||
                         timerMode == TimerMode.SynchronizedSum)
                {
                    // appendValue variable is used for calculating the average or sum value
                    decimal appendValue = Decimal.Zero;
                    int valueCount = waitingValues.Count;

                    while (waitingValues.Count > 0)
                        appendValue += waitingValues.Dequeue();

                    // Calculate Average value in SynchronizedAverage Mode
                    if (timerMode == TimerMode.SynchronizedAverage)
                        appendValue = appendValue / (decimal)valueCount;

                    // Finally append the value
                    ChartAppend(appendValue);
                }
            }
            else
            {
                // Always add 0 (Zero) if there are no values in the queue
                ChartAppend(Decimal.Zero);
            }

            // Refresh the Chart
            Invalidate();
        }

        /// <summary>
        /// Calculates the vertical Position of a value in relation the chart size,
        /// Scale Mode and, if ScaleMode is Relative, to the current maximum value
        /// </summary>
        /// <param name="value">performance value</param>
        /// <returns>vertical Point position in Pixels</returns>
        private int CalcVerticalPosition(decimal value)
        {
            decimal result = Decimal.Zero;

            if (scaleMode == ScaleMode.Absolute)
                result = value * this.Height / 100;
            else if (scaleMode == ScaleMode.Relative)
                result = (currentMaxValue > 0) ? (value * this.Height / currentMaxValue) : 0;

            result = this.Height - result;

            return Convert.ToInt32(Math.Round(result));
        }


        /// <summary>
        /// Returns the currently highest (displayed) value, for Relative ScaleMode
        /// </summary>
        /// <returns>System.Decimal.</returns>
        private decimal GetHighestValueForRelativeMode()
        {
            decimal maxValue = 0;

            for (int i = 0; i < visibleValues; i++)
            {
                // Set if higher then previous max value
                if (Items[i] > maxValue)
                    maxValue = Items[i];
            }

            return maxValue;
        }

        #endregion


        #region *** Private Methods: Drawing ***

        /// <summary>
        /// Draws the chart (w/o background or grid, but with border) to the Graphics canvas
        /// </summary>
        /// <param name="g">Graphics</param>
        private void DrawChart(Graphics g)
        {
            visibleValues = Math.Min(this.Width / valueSpacing, Items.Count);

            if (scaleMode == ScaleMode.Relative)
                currentMaxValue = GetHighestValueForRelativeMode();

            // Dirty little "trick": initialize the first previous Point outside the bounds
            Point previousPoint = new Point(Width + valueSpacing, Height);
            Point currentPoint = new Point();

            // Only draw average line when possible (visibleValues) and needed (style setting)
            if (visibleValues > 0 && perfChartStyle.ShowAverageLine)
            {
                averageValue = 0;
                DrawAverageLine(g);
            }

            // Connect all visible values with lines
            for (int i = 0; i < visibleValues; i++)
            {
                currentPoint.X = previousPoint.X - valueSpacing;
                currentPoint.Y = CalcVerticalPosition(Items[i]);

                // Actually draw the line
                g.DrawLine(perfChartStyle.ChartLinePen.Pen, previousPoint, currentPoint);

                previousPoint = currentPoint;
            }

            // Draw current relative maximum value stirng
            if (scaleMode == ScaleMode.Relative)
            {
                SolidBrush sb = new SolidBrush(perfChartStyle.ChartLinePen.Color);
                g.DrawString(currentMaxValue.ToString(), this.Font, sb, 4.0f, 2.0f);
            }

            // Draw Border on top
            #region Included in the main paint
            //switch (BorderStyle)
            //{
            //    case Border3DStyle.None:

            //        break;
            //    case Border3DStyle.Adjust:
            //        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Adjust);
            //        break;
            //    case Border3DStyle.Bump:
            //        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Bump);
            //        break;
            //    case Border3DStyle.Etched:
            //        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Etched);
            //        break;
            //    case Border3DStyle.Flat:
            //        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Flat);
            //        break;
            //    case Border3DStyle.Raised:
            //        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Raised);
            //        break;
            //    case Border3DStyle.RaisedInner:
            //        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.RaisedInner);
            //        break;
            //    case Border3DStyle.RaisedOuter:
            //        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.RaisedOuter);
            //        break;
            //    case Border3DStyle.Sunken:
            //        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.Sunken);
            //        break;
            //    case Border3DStyle.SunkenInner:
            //        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.SunkenInner);
            //        break;
            //    case Border3DStyle.SunkenOuter:
            //        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, System.Windows.Forms.Border3DStyle.SunkenOuter);
            //        break;
            //    case Border3DStyle.Single:
            //        g.DrawRectangle(new Pen(BorderColor), new Rectangle(0, 0, Width - 1, Height - 1));
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //} 
            #endregion

        }


        /// <summary>
        /// Draws the average line.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawAverageLine(Graphics g)
        {
            for (int i = 0; i < visibleValues; i++)
                averageValue += Items[i];

            averageValue = averageValue / visibleValues;

            int verticalPosition = CalcVerticalPosition(averageValue);
            g.DrawLine(perfChartStyle.AvgLinePen.Pen, 0, verticalPosition, Width, verticalPosition);
        }

        /// <summary>
        /// Draws the background gradient and the grid into Graphics <paramref name="g" />
        /// </summary>
        /// <param name="g">Graphic</param>
        private void DrawBackgroundAndGrid(Graphics g)
        {
            // Draw the background Gradient rectangle
            Rectangle baseRectangle = new Rectangle(0, 0, this.Width, this.Height);
            using (Brush gradientBrush = new LinearGradientBrush(baseRectangle, perfChartStyle.BackgroundColorTop, perfChartStyle.BackgroundColorBottom, LinearGradientMode.Vertical))
            {
                g.FillRectangle(gradientBrush, baseRectangle);
            }

            // Draw all visible, vertical gridlines (if wanted)
            if (perfChartStyle.ShowVerticalGridLines)
            {
                for (int i = Width - gridScrollOffset; i >= 0; i -= GRID_SPACING)
                {
                    g.DrawLine(perfChartStyle.VerticalGridPen.Pen, i, 0, i, Height);
                }
            }

            // Draw all visible, horizontal gridlines (if wanted)
            if (perfChartStyle.ShowHorizontalGridLines)
            {
                for (int i = 0; i < Height; i += GRID_SPACING)
                {
                    g.DrawLine(perfChartStyle.HorizontalGridPen.Pen, 0, i, Width, i);
                }
            }
        }

        #endregion


        #region *** Overrides ***

        /// <summary>
        /// Performances the on paint.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        /// Override OnPaint method
        private void PerformanceOnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            // Enable AntiAliasing, if needed
            if (perfChartStyle.AntiAliasing)
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DrawBackgroundAndGrid(e.Graphics);
            DrawChart(e.Graphics);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Invalidate();
        }

        #endregion


        #region *** Event Handlers ***

        /// <summary>
        /// Handles the ColorSetChanged event of the colorSet control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void colorSet_ColorSetChanged(object sender, EventArgs e)
        {
            //Refresh Chart on Resize
            Invalidate();
        }

        /// <summary>
        /// Handles the Tick event of the tmrRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            // Don't execute event if running in design time
            //if (this.DesignMode) return;

            ChartAppendFromQueue();

            
        }

        #endregion


        #region Designer Generated



        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode f�r die Designerunterst�tzung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
        /// </summary>
        private void InitializeComponent()
        {

            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);

        }

        #endregion

        /// <summary>
        /// The TMR refresh
        /// </summary>
        private System.Windows.Forms.Timer tmrRefresh = new System.Windows.Forms.Timer();

        #endregion

        #endregion

    }
}