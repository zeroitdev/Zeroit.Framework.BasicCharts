// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="PerfChartStyle.cs" company="Zeroit Dev Technologies">
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
using System.ComponentModel;
using System.Drawing;

namespace Zeroit.Framework.BasicCharts
{
    /// <summary>
    /// Class PerfChartStyle.
    /// </summary>
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class PerfChartStyle
    {
        /// <summary>
        /// The vertical grid pen
        /// </summary>
        private ChartPen verticalGridPen;
        /// <summary>
        /// The horizontal grid pen
        /// </summary>
        private ChartPen horizontalGridPen;
        /// <summary>
        /// The average line pen
        /// </summary>
        private ChartPen avgLinePen;
        /// <summary>
        /// The chart line pen
        /// </summary>
        private ChartPen chartLinePen;

        /// <summary>
        /// The background color top
        /// </summary>
        private Color backgroundColorTop = Color.DarkGreen;
        /// <summary>
        /// The background color bottom
        /// </summary>
        private Color backgroundColorBottom = Color.DarkGreen;

        /// <summary>
        /// The show vertical grid lines
        /// </summary>
        private bool showVerticalGridLines = true;
        /// <summary>
        /// The show horizontal grid lines
        /// </summary>
        private bool showHorizontalGridLines = true;
        /// <summary>
        /// The show average line
        /// </summary>
        private bool showAverageLine = true;
        /// <summary>
        /// The anti aliasing
        /// </summary>
        private bool antiAliasing = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerfChartStyle"/> class.
        /// </summary>
        public PerfChartStyle() {
            verticalGridPen = new ChartPen();
            horizontalGridPen = new ChartPen();
            avgLinePen = new ChartPen();
            chartLinePen = new ChartPen();
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show vertical grid lines].
        /// </summary>
        /// <value><c>true</c> if [show vertical grid lines]; otherwise, <c>false</c>.</value>
        public bool ShowVerticalGridLines {
            get { return showVerticalGridLines; }
            set { showVerticalGridLines = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show horizontal grid lines].
        /// </summary>
        /// <value><c>true</c> if [show horizontal grid lines]; otherwise, <c>false</c>.</value>
        public bool ShowHorizontalGridLines {
            get { return showHorizontalGridLines; }
            set { showHorizontalGridLines = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show average line].
        /// </summary>
        /// <value><c>true</c> if [show average line]; otherwise, <c>false</c>.</value>
        public bool ShowAverageLine {
            get { return showAverageLine; }
            set { showAverageLine = value; }
        }

        /// <summary>
        /// Gets or sets the vertical grid pen.
        /// </summary>
        /// <value>The vertical grid pen.</value>
        public ChartPen VerticalGridPen {
            get { return verticalGridPen; }
            set { verticalGridPen = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal grid pen.
        /// </summary>
        /// <value>The horizontal grid pen.</value>
        public ChartPen HorizontalGridPen {
            get { return horizontalGridPen; }
            set { horizontalGridPen = value; }
        }

        /// <summary>
        /// Gets or sets the average line pen.
        /// </summary>
        /// <value>The average line pen.</value>
        public ChartPen AvgLinePen {
            get { return avgLinePen; }
            set { avgLinePen = value; }
        }

        /// <summary>
        /// Gets or sets the chart line pen.
        /// </summary>
        /// <value>The chart line pen.</value>
        public ChartPen ChartLinePen {
            get { return chartLinePen; }
            set { chartLinePen = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [anti aliasing].
        /// </summary>
        /// <value><c>true</c> if [anti aliasing]; otherwise, <c>false</c>.</value>
        public bool AntiAliasing {
            get { return antiAliasing; }
            set { antiAliasing = value; }
        }

        /// <summary>
        /// Gets or sets the background color top.
        /// </summary>
        /// <value>The background color top.</value>
        public Color BackgroundColorTop {
            get { return backgroundColorTop; }
            set { backgroundColorTop = value; }
        }

        /// <summary>
        /// Gets or sets the background color bottom.
        /// </summary>
        /// <value>The background color bottom.</value>
        public Color BackgroundColorBottom {
            get { return backgroundColorBottom; }
            set { backgroundColorBottom = value; }
        }
    }

    /// <summary>
    /// Class ChartPen.
    /// </summary>
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public class ChartPen
    {
        /// <summary>
        /// The pen
        /// </summary>
        private Pen pen;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartPen"/> class.
        /// </summary>
        public ChartPen() {
            pen = new Pen(Color.Black);
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public Color Color {
            get { return pen.Color; }
            set { pen.Color = value; }
        }

        /// <summary>
        /// Gets or sets the dash style.
        /// </summary>
        /// <value>The dash style.</value>
        public System.Drawing.Drawing2D.DashStyle DashStyle {
            get { return pen.DashStyle; }
            set { pen.DashStyle = value; }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public float Width {
            get { return pen.Width; }
            set { pen.Width = value; }
        }

        /// <summary>
        /// Gets the pen.
        /// </summary>
        /// <value>The pen.</value>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Pen Pen {
            get { return pen; }
        }
    }
}
