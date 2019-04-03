// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="BasicCharts.cs" company="Zeroit Dev Technologies">
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts
{

    #region ZeroitPlotter Control with Animation

    #region Control
    /// <summary>
    /// ZeroitPlotter control.
    /// Use the <see cref="Control.ForeColor" /> property to set the plot color.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ToolboxItem(false)]
    public partial class ZeroitPlotterAnimated : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitPlotterAnimated"/> class.
        /// </summary>
        public ZeroitPlotterAnimated()
        {
            InitializeComponent();

            this.Controls.Add(this.plotArea);
            this.Controls.Add(this.scrollBar);

            this.plotArea.Paint += new PaintEventHandler(plotArea_Paint);
            this.scrollBar.Scroll += new ScrollEventHandler(scrollBar_Scroll);
            this.plotArea.Resize += delegate { this.SetScrollBarProperties(); };

            this.LayoutControls();
            this.Resize += delegate { this.LayoutControls(); };

            this.BackColorChanged += delegate { this.plotArea.Invalidate(); };
            this.ForeColorChanged += delegate { this.plotArea.Invalidate(); };
        }

        /// <summary>
        /// Layouts the controls.
        /// </summary>
        void LayoutControls()
        {
            //this.plotArea.Dock = DockStyle.Fill;
            //this.scrollBar.Dock = DockStyle.Bottom;

            // I had to lay out the controls manually because docking works slightly incorrectly
            // under certain circumstances.
            this.plotArea.Location = new Point(0, 0);
            this.plotArea.Size = new Size(this.Width, this.scrollBar.Visible ? this.Height - this.scrollBar.Height : this.Height);
            this.scrollBar.Location = new Point(0, this.plotArea.Height);
            this.scrollBar.Size = new Size(this.Width, this.scrollBar.Height);
        }

        /// <summary>
        /// Handles the Paint event of the plotArea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        void plotArea_Paint(object sender, PaintEventArgs e)
        {
            this.PaintGraph(e.Graphics);
        }

        /// <summary>
        /// Handles the Scroll event of the scrollBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ScrollEventArgs"/> instance containing the event data.</param>
        void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            this.plotArea.Invalidate();
        }

        /// <summary>
        /// Sets the scroll bar properties.
        /// </summary>
        void SetScrollBarProperties()
        {
            bool scrollBarWasVisible = scrollBar.Visible;

            int largeChange = (int)(this.plotArea.Width / this.DX);

            if (largeChange >= this.values.Count)
            {
                this.scrollBar.Visible = false;
            }
            else
            {
                int shift = this.scrollBar.Maximum - (this.scrollBar.Value + this.scrollBar.LargeChange);
                if (shift < 0) shift = 0;

                this.scrollBar.Visible = true;
                this.scrollBar.Minimum = 0;
                this.scrollBar.Maximum = this.values.Count;
                this.scrollBar.LargeChange = largeChange;
                this.scrollBar.SmallChange = 1;

                if (!scrollBarWasVisible)
                {
                    this.scrollBar.Value = this.scrollBar.Maximum - largeChange;
                }
                else
                {
                    int value = this.scrollBar.Maximum - shift - this.scrollBar.LargeChange;
                    if (value < 0) value = 0;
                    this.scrollBar.Value = value;
                }
            }

            if (scrollBarWasVisible != this.scrollBar.Visible)
            {
                // BUG: the RunningGraph control is not laid out until it is resized.
                // Calling PerformLayout does not solve this, and neither does
                // removing/adding the scroll bar.

                this.LayoutControls();

                //if (this.scrollBar.Visible)
                //{
                //    this.Controls.Add (this.scrollBar);
                //}
                //else
                //{
                //    this.Controls.Remove (this.scrollBar);
                //}
                //this.UpdateBounds ();
            }
        }

        /// <summary>
        /// The plot area
        /// </summary>
        readonly PlotArea plotArea = new PlotArea();
        /// <summary>
        /// The scroll bar
        /// </summary>
        readonly HScrollBar scrollBar = new HScrollBar();

        #region Min

        /// <summary>
        /// Gets or sets the sample value that corresponds to the bottom edge of the plot area.
        /// </summary>
        /// <value>The minimum.</value>
        public float Min
        {
            get { return this.min; }

            set
            {
                this.min = value;
                this.InvalidateAndUpdateScrollBar();
            }
        }

        /// <summary>
        /// The minimum
        /// </summary>
        float min = 0;

        #endregion

        #region Max

        /// <summary>
        /// Gets or sets the sample value that corresponds to the top edge of the plot area.
        /// </summary>
        /// <value>The maximum.</value>
        public float Max
        {
            get { return this.max; }

            set
            {
                this.max = value;
                this.InvalidateAndUpdateScrollBar();
            }
        }

        /// <summary>
        /// The maximum
        /// </summary>
        float max = 100;

        #endregion

        /// <summary>
        /// Add a sample point to the graph.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(float value)
        {
            this.values.Add(value);
            this.InvalidateAndUpdateScrollBar();
        }

        /// <summary>
        /// Invalidates the and update scroll bar.
        /// </summary>
        void InvalidateAndUpdateScrollBar()
        {
            this.SetScrollBarProperties();
            this.plotArea.Invalidate();
        }

        /// <summary>
        /// Gets the distance between sample points.
        /// TODO: allow to customize this.
        /// </summary>
        /// <value>The dx.</value>
        public float DX
        {
            get { return 1F; }
        }


        /// <summary>
        /// The values
        /// </summary>
        readonly RingBuffer<float> values = new RingBuffer<float>(1000);

        /// <summary>
        /// Paints the graph.
        /// </summary>
        /// <param name="g">The g.</param>
        void PaintGraph(Graphics g)
        {
            if (this.values.Count == 0) return;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            float dx = this.DX;

            float x = this.plotArea.Width;

            int i = this.values.Count - 1;
            if (this.scrollBar.Visible)
            {
                i = this.scrollBar.Value + this.scrollBar.LargeChange - 1;
                if (i >= this.values.Count)
                {
                    i = this.values.Count - 1;
                }
            }

            PointF p = new PointF(x, ScaleY(this.values[i]));

            for (;;)
            {
                if (--i < 0) break;
                x -= dx;

                PointF q = new PointF(x, ScaleY(this.values[i]));

                using (Pen pen = new Pen(this.ForeColor))
                {
                    g.DrawLine(pen, p, q);
                }

                p = q;
                if (x < 0) break;
            }
        }

        /// <summary>
        /// Scales the y.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <returns>System.Single.</returns>
        float ScaleY(float y)
        {
            int h = this.plotArea.Height;
            return h - (y - min) / (max - min) * h;
        }

    } 
    #endregion

    #region Designer Generated Code

    partial class ZeroitPlotterAnimated
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
    }

    #endregion

    #region Helper Classes

    /// <summary>
    /// Class PlotArea.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    class PlotArea : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlotArea"/> class.
        /// </summary>
        public PlotArea()
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint,
                true);
        }
    }

    /// <summary>
    /// Class RingBuffer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RingBuffer<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RingBuffer{T}"/> class.
        /// </summary>
        /// <param name="initialCapacity">The initial capacity.</param>
        public RingBuffer(int initialCapacity)
        {
            this.buf = new T[initialCapacity];
        }

        /// <summary>
        /// Gets the capacity.
        /// </summary>
        /// <value>The capacity.</value>
        public int Capacity
        {
            get { return this.buf.Length; }
        }

        /// <summary>
        /// The buf
        /// </summary>
        T[] buf;
        /// <summary>
        /// The head
        /// </summary>
        int head = 0, tail = 0;
        /// <summary>
        /// The empty
        /// </summary>
        bool empty = true;

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                if (empty) return 0;

                int count = head - tail;
                if (count <= 0) count += buf.Length;
                return count;
            }
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(T value)
        {
            if (!empty && head == tail)
            {
                tail = (tail + 1) % buf.Length;
            }

            buf[head] = value;

            head = (head + 1) % buf.Length;

            empty = false;
        }

        /// <summary>
        /// Gets the <see cref="T"/> with the specified i.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>T.</returns>
        public T this[int i]
        {
            get { return buf[ShiftIndex(i)]; }
        }

        /// <summary>
        /// Shifts the index.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentException">Index must be less than Count.</exception>
        int ShiftIndex(int i)
        {
            if (i > Count) throw new ArgumentException("Index must be less than Count.");

            return (tail + i) % buf.Length;
        }
    }

    #endregion
    #endregion
    
}
