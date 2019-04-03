// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Signal Generator.cs" company="Zeroit Dev Technologies">
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
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts
{


    /// <summary>
    /// Class ZeroitSignal.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    [Designer(typeof(ZeroitSignalDesigner))]
    [Serializable]
    public class ZeroitSignal : Control, ISerializable
    {

        #region Enum


        /// <summary>
        /// Enum Signal
        /// </summary>
        public enum Signal
        {
            /// <summary>
            /// The sine
            /// </summary>
            Linear,
            /// <summary>
            /// The square
            /// </summary>
            LinearDouble,
            /// <summary>
            /// The sine
            /// </summary>
            Sine,
            /// <summary>
            /// The square
            /// </summary>
            Square,
            /// <summary>
            /// The triangle
            /// </summary>
            Triangle,
            /// <summary>
            /// The sawtooth
            /// </summary>
            Sawtooth,

            /// <summary>
            /// The pulse
            /// </summary>
            Pulse,
            /// <summary>
            /// The white noise
            /// </summary>
            WhiteNoise,    // random between -1 and 1
            /// <summary>
            /// The gauss noise
            /// </summary>
            GaussNoise,    // random between -1 and 1 with normal distribution
            /// <summary>
            /// The digital noise
            /// </summary>
            DigitalNoise,

            /// <summary>
            /// The user defined
            /// </summary>
            UserDefined    // user defined between -1 and 1	}
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

        #region Private Fields

        /// <summary>
        /// The initial location
        /// </summary>
        private float[] initialLocation = new float[] { 0, 0 };
        /// <summary>
        /// The periods
        /// </summary>
        private float periods = 3;
        /// <summary>
        /// The amplitude height
        /// </summary>
        private float amplitudeHeight = 3;
        /// <summary>
        /// The amplitude tension
        /// </summary>
        private float amplitudeTension = 10;
        /// <summary>
        /// The grid lines
        /// </summary>
        private int gridLines = 2;

        /// <summary>
        /// The grid pen
        /// </summary>
        private Pen gridPen = new Pen(Color.Red) { Width = 1, DashStyle = DashStyle.DashDot };
        /// <summary>
        /// The signal pen
        /// </summary>
        private Pen signalPen = new Pen(Color.Blue) { DashStyle = DashStyle.Solid, Width = 2 };
        /// <summary>
        /// The border color
        /// </summary>
        private Pen borderColor = new Pen(Color.Black) { Width = 2 };


        /// <summary>
        /// The show mid grid lines
        /// </summary>
        private bool showMidGridLines = true;
        /// <summary>
        /// The show text
        /// </summary>
        private bool showText = true;
        /// <summary>
        /// The show border
        /// </summary>
        private bool showBorder = true;

        /// <summary>
        /// The userrand
        /// </summary>
        Random userrand = new Random();
        /// <summary>
        /// The text rendering
        /// </summary>
        private TextRenderingHint textRendering = TextRenderingHint.AntiAliasGridFit;
        /// <summary>
        /// The smoothing
        /// </summary>
        private SmoothingMode smoothing = SmoothingMode.HighQuality;

        /// <summary>
        /// Random provider for noise generator
        /// </summary>
        private Random random = new Random();


        /// <summary>
        /// The colors
        /// </summary>
        List<Color> colors = new List<Color>()
        {
            Color.Green,
            Color.Blue,
            Color.LightPink,
            Color.Indigo,
            Color.Red,
            Color.Purple,
            Color.Salmon,
            Color.Lime,
            Color.Orange
        };

        /// <summary>
        /// Time the signal generator was started
        /// </summary>
        protected long startTime = Stopwatch.GetTimestamp();

        /// <summary>
        /// Ticks per second on this CPU
        /// </summary>
        protected long ticksPerSecond = Stopwatch.Frequency;

        /// <summary>
        /// The seed colors
        /// </summary>
        private SeedColor seedColors = SeedColor.FirstColor;

        /// <summary>
        /// The multiplier
        /// </summary>
        private float multiplier = 2f;

        /// <summary>
        /// The items
        /// </summary>
        private List<float> items = new List<float>()
        {
            10,
            20,
            30
        };

        /// <summary>
        /// The maximum
        /// </summary>
        private float maximum = 1000;

        /// <summary>
        /// The signal type
        /// </summary>
        private Signal signalType = Signal.Sine;
        /// <summary>
        /// The frequency
        /// </summary>
        private float frequency = 1f;
        /// <summary>
        /// The phase
        /// </summary>
        private float phase = 0f;
        /// <summary>
        /// The amplitude
        /// </summary>
        private float amplitude = 1f;
        /// <summary>
        /// The offset
        /// </summary>
        private float offset = 0f;
        /// <summary>
        /// The invert
        /// </summary>
        private float invert = 1; // Yes=-1, No=1

        /// <summary>
        /// The get value callback
        /// </summary>
        private GetValueDelegate getValueCallback = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the color of the horiz grid pen.
        /// </summary>
        /// <value>The color of the horiz grid pen.</value>
        [Browsable(false)]
        public Color HorizGridPenColor
        {
            get
            {
                return PerfChartStyle.HorizontalGridPen.Color;
            }
            set
            {
                PerfChartStyle.HorizontalGridPen.Color = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the vert grid pen.
        /// </summary>
        /// <value>The color of the vert grid pen.</value>
        [Browsable(false)]
        public Color VertGridPenColor
        {
            get
            {
                return PerfChartStyle.VerticalGridPen.Color;
            }
            set
            {
                PerfChartStyle.VerticalGridPen.Color = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the chart style back color bottom.
        /// </summary>
        /// <value>The chart style back color bottom.</value>
        [Browsable(false)]
        public Color ChartStyleBackColorBottom
        {
            get
            {
                return PerfChartStyle.BackgroundColorBottom;
            }
            set
            {
                PerfChartStyle.BackgroundColorBottom = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the chart style back color top.
        /// </summary>
        /// <value>The chart style back color top.</value>
        [Browsable(false)]
        public Color ChartStyleBackColorTop
        {
            get
            {
                return PerfChartStyle.BackgroundColorTop;
            }
            set
            {
                PerfChartStyle.BackgroundColorTop = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show horizontal grid lines].
        /// </summary>
        /// <value><c>true</c> if [show horizontal grid lines]; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool ShowHorizontalGridLines
        {
            get
            {
                return PerfChartStyle.ShowHorizontalGridLines;
            }
            set
            {
                PerfChartStyle.ShowHorizontalGridLines = value;
                Invalidate();

            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show vertical grid lines].
        /// </summary>
        /// <value><c>true</c> if [show vertical grid lines]; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool ShowVerticalGridLines
        {
            get
            {
                return PerfChartStyle.ShowVerticalGridLines;
            }
            set
            {
                PerfChartStyle.ShowVerticalGridLines = value;
                Invalidate();

            }
        }

        /// <summary>
        /// Gets or sets the smoothing.
        /// </summary>
        /// <value>The smoothing.</value>
        public SmoothingMode Smoothing
        {
            get { return smoothing; }
            set
            {
                smoothing = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text rendering.
        /// </summary>
        /// <value>The text rendering.</value>
        public TextRenderingHint TextRendering
        {
            get { return textRendering; }
            set
            {
                textRendering = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show mid grid lines].
        /// </summary>
        /// <value><c>true</c> if [show mid grid lines]; otherwise, <c>false</c>.</value>
        public bool ShowMidGridLines
        {
            get { return showMidGridLines; }
            set { showMidGridLines = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show text].
        /// </summary>
        /// <value><c>true</c> if [show text]; otherwise, <c>false</c>.</value>
        public bool ShowText
        {
            get { return showText; }
            set { showText = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show border].
        /// </summary>
        /// <value><c>true</c> if [show border]; otherwise, <c>false</c>.</value>
        public bool ShowBorder
        {
            get { return showBorder; }
            set { showBorder = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the multiplier.
        /// </summary>
        /// <value>The multiplier.</value>
        public float Multiplier
        {
            get { return multiplier; }
            set
            {
                multiplier = value;
                Invalidate();
            }
        }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public List<float> Items
        {
            get
            {
                return items;

            }
            set
            {

                items = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public float Maximum
        {
            get { return maximum; }
            set
            {
                maximum = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the periods.
        /// </summary>
        /// <value>The periods.</value>
        public float Periods
        {
            get { return periods; }
            set { periods = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the height of the amplitude.
        /// </summary>
        /// <value>The height of the amplitude.</value>
        public float AmplitudeHeight
        {
            get { return amplitudeHeight; }
            set { amplitudeHeight = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the amplitude tension.
        /// </summary>
        /// <value>The amplitude tension.</value>
        public float AmplitudeTension
        {
            get { return amplitudeTension; }
            set
            {
                amplitudeTension = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the grid lines.
        /// </summary>
        /// <value>The grid lines.</value>
        public int GridLines
        {
            get { return gridLines; }
            set { gridLines = value; Invalidate(); }
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
        /// Signal Type.
        /// </summary>
        /// <value>The type of the signal.</value>
        public Signal SignalType
        {
            get { return signalType; }
            set { signalType = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the grid pen.
        /// </summary>
        /// <value>The grid pen.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pen GridPen
        {
            get { return gridPen; }
            set { gridPen = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color of the mid grid.
        /// </summary>
        /// <value>The color of the mid grid.</value>
        public Color MidGridColor
        {
            get { return GridPen.Color; }
            set { GridPen.Color = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the mid grid dash style.
        /// </summary>
        /// <value>The mid grid dash style.</value>
        public DashStyle MidGridDashStyle
        {
            get { return GridPen.DashStyle; }
            set { GridPen.DashStyle = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the width of the mid grid.
        /// </summary>
        /// <value>The width of the mid grid.</value>
        public float MidGridWidth
        {
            get { return GridPen.Width; }
            set { GridPen.Width = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the signal pen.
        /// </summary>
        /// <value>The signal pen.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pen SignalPen
        {
            get { return signalPen; }
            set { signalPen = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the signal dash style.
        /// </summary>
        /// <value>The signal dash style.</value>
        public DashStyle SignalDashStyle
        {
            get { return SignalPen.DashStyle; }
            set { SignalPen.DashStyle = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the width of the signal.
        /// </summary>
        /// <value>The width of the signal.</value>
        public float SignalWidth
        {
            get { return SignalPen.Width; }
            set { SignalPen.Width = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the border pen.
        /// </summary>
        /// <value>The border pen.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pen BorderPen
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }


        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor
        {
            get { return BorderPen.Color; }
            set { BorderPen.Color = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public float BorderWidth
        {
            get { return BorderPen.Width; }
            set { BorderPen.Width = value; Invalidate(); }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Delegate GetValueDelegate
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>System.Single.</returns>
        public delegate float GetValueDelegate(float time);


        /// <summary>
        /// Signal Frequency.
        /// </summary>
        /// <value>The frequency.</value>
        private float Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;
            }
        }


        /// <summary>
        /// Signal Phase.
        /// </summary>
        /// <value>The phase.</value>
        private float Phase
        {
            get { return phase; }
            set
            {
                phase = value;

            }
        }


        /// <summary>
        /// Signal Amplitude.
        /// </summary>
        /// <value>The amplitude.</value>
        private float Amplitude
        {
            get { return amplitude; }
            set
            {
                amplitude = value;

            }

        }


        /// <summary>
        /// Signal Offset.
        /// </summary>
        /// <value>The offset.</value>
        private float Offset
        {
            get { return offset; }
            set
            {
                offset = value;
            }
        }



        /// <summary>
        /// Signal Inverted?
        /// </summary>
        /// <value><c>true</c> if invert; otherwise, <c>false</c>.</value>
        private bool Invert
        {
            get { return invert == -1; }
            set
            {
                invert = value ? -1 : 1;
            }
        }


        /// <summary>
        /// GetValue Callback?
        /// </summary>
        /// <value>The get value callback.</value>
        public GetValueDelegate GetValueCallback
        {
            get { return getValueCallback; }
            set { getValueCallback = value; }
        }

        /// <summary>
        /// Gets or sets the initial location.
        /// </summary>
        /// <value>The initial location.</value>
        private float[] InitialLocation
        {
            get { return initialLocation; }
            set
            {
                initialLocation = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets the get sum.
        /// </summary>
        /// <value>The get sum.</value>
        private float GetSum
        {
            get { return Sum(items.ToArray()); }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSignalGenerator" /> class.
        /// </summary>
        public ZeroitSignal()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;

            IncludeInConstructor();
            tmrRefresh.Tick += tmrRefresh_Tick;

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSignal"/> class.
        /// </summary>
        /// <param name="initialSignalType">Initial type of the signal.</param>
        public ZeroitSignal(Signal initialSignalType)
        {
            signalType = initialSignalType;

            IncludeInConstructor();
            tmrRefresh.Tick += tmrRefresh_Tick;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sums the specified sum.
        /// </summary>
        /// <param name="sum">The sum.</param>
        /// <returns>System.Single.</returns>
        private static float Sum(params float[] sum)
        {
            float addition = 0;

            for (int i = 0; i < sum.Length; i++)
            {
                addition += sum[i];
            }
            return addition;
        }


        /// <summary>
        /// Users the defined signal.
        /// </summary>
        /// <param name="Time">The time.</param>
        /// <returns>System.Single.</returns>
        public float UserDefinedSignal(float Time)
        {
            float t = frequency * Time + phase;
            return (float)Math.Sin(Multiplier * Math.PI * t);
        }


        /// <summary>
        /// Magics the division.
        /// </summary>
        /// <returns>System.Single.</returns>
        private float MagicDivision()
        {
            float division = AmplitudeTension * (GetSum/Maximum);

            if (division < 8)
            {
                division = 8;
            }

            return division;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>System.Single.</returns>
        public float GetValue(float time)

        {
            Random userLinrand = new Random();

            float value = 0f;
            float t = frequency * time + phase;
            switch (signalType)
            { // http://en.wikipedia.org/wiki/Waveform
                case Signal.Sine: // sin( 2 * pi * t )
                    value = (float)Math.Sin(2f * Math.PI * t);

                    break;
                case Signal.Square: // sign( sin( 2 * pi * t ) )
                    value = Math.Sign(Math.Sin(2f * Math.PI * t));
                    break;
                case Signal.Triangle: // 2 * abs( t - 2 * floor( t / 2 ) - 1 ) - 1
                    value = 1f - 4f * (float)Math.Abs(Math.Round(t - 0.25f) - (t - 0.25f));
                    break;
                case Signal.Sawtooth: // 2 * ( t/a - floor( t/a + 1/2 ) )
                    value = 2f * (t - (float)Math.Floor(t + 0.5f));
                    break;
                    
                case Signal.Pulse: // http://en.wikipedia.org/wiki/Pulse_wave
                    value = (Math.Abs(Math.Sin(2 * Math.PI * t)) < 1.0 - 10E-3) ? (0) : (1);
                    break;
                case Signal.WhiteNoise: // http://en.wikipedia.org/wiki/White_noise
                    value = 2f * (float)random.Next(int.MaxValue) / int.MaxValue - 1f;
                    break;
                case Signal.GaussNoise: // http://en.wikipedia.org/wiki/Gaussian_noise
                    value = (float)StatisticFunction.NORMINV((float)random.Next(int.MaxValue) / int.MaxValue, 0.0, 0.4);
                    break;
                case Signal.DigitalNoise: //Binary Bit Generators
                    value = random.Next(2);
                    break;

                case Signal.UserDefined:
                    //value = (getValueCallback == null) ? (0f) : getValueCallback(t);
                    value = UserDefinedSignal(userrand.Next());
                    break;
                case Signal.Linear:
                    //value = (getValueCallback == null) ? (0f) : getValueCallback(t);
                    value = (float)Math.Sin(10f * Math.PI * (frequency * userLinrand.Next() + phase));
                    break;
                case Signal.LinearDouble:
                    //value = (getValueCallback == null) ? (0f) : getValueCallback(t);
                    value = (float)Math.Sin(10f * Math.PI * (frequency * userLinrand.NextDouble() + phase));
                    break;
            }

            return (invert * amplitude * value + offset);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>System.Single.</returns>
        public float GetValue()
        {
            float time = (float)(Stopwatch.GetTimestamp() - startTime) / ticksPerSecond;
            return GetValue(time);
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            startTime = Stopwatch.GetTimestamp();
        }

        /// <summary>
        /// Synchronizes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Synchronize(ZeroitSignal instance)
        {
            startTime = instance.startTime;
            ticksPerSecond = instance.ticksPerSecond;
        }


        /// <summary>
        /// Checks the maximum.
        /// </summary>
        private void CheckMaximum()
        {
            if (GetSum > Maximum)
            {
                Maximum *= 10;
            }
        }



        #endregion

        #region Paint and Overrides

        /// <summary>
        /// Paints the signal.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="r">The r.</param>
        /// <param name="st">The st.</param>
        /// <param name="c">The c.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        private void PaintSignal(Graphics g, Rectangle r, Signal st, Color c)
        {
            float X1 = InitialLocation[0];
            float Y1 = InitialLocation[1];

            float X2 = X1 + r.Width;
            float Y2 = Y1 + r.Height;

            float Xw = r.Width;
            float Yh = r.Height;

            float Xm = X1 + Xw / 2;   // X possition of middle point.
            float Ym = Y1 + Yh / 2;       // Y possition of middle point.

            float Nper = Periods;       // Number of periodes that schud be shown.
            float Xpw = Xw / Nper;    // One Periode length in pixel.
            float Yah = AmplitudeHeight * Yh / MagicDivision();   // Signal amplitude height in picel. 

            // Create a custom pen:



            if (ShowMidGridLines)
            {
                //Draw vertical grid lines:
                for (int i = 1; i < GridLines * Nper; i++)
                    g.DrawLine(GridPen, X1 + (Xpw / GridLines) * i, Y1, X1 + (Xpw / GridLines) * i, Y2);

                //Draw horisontal grid lines:
                float Ypw = Yh / Nper;
                for (int i = 1; i < GridLines * Nper; i++)
                    g.DrawLine(GridPen, X1, Y1 + (Ypw / GridLines) * i, X2, Y1 + (Ypw / GridLines) * i);

                #region Initial Horizontal Code
                //g.DrawLine(GridPen, X1, Ym - Yah, X2, Ym - Yah);
                //g.DrawLine(GridPen, X1, Ym, X2, Ym);
                //g.DrawLine(GridPen, X1, Ym + Yah, X2, Ym + Yah); 
                #endregion

            }

            // Create requider signal generator: 
            ZeroitSignal sg = new ZeroitSignal(st);

            // Adjust aignal generator:

            sg.Frequency = 1 / Xpw;
            sg.Phase = 0f;
            sg.Amplitude = Yah;
            sg.Offset = 0f;
            sg.Invert = false;

            // Generate signal and draw it:
            float Xold = 0f;
            float Yold = 0f;
            float Xnew = 0f;
            float Ynew = 0f;




            #region Random Generated Colors

            List<Color> colorsReversed = Colors;
            colorsReversed.Reverse();

            Random randColor1;

            Random randColor2;

            Random counter = new Random();

            switch (SeedColors)
            {
                case SeedColor.None:
                    randColor1 = new Random();
                    randColor2 = new Random();

                    switch (Brush)
                    {
                        case BrushType.Solid:
                            SignalPen.Brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

                            break;
                        case BrushType.Gradient:
                            SignalPen.Brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                            break;
                        case BrushType.Hatch:
                            SignalPen.Brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    //SignalPen.Brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                    break;
                case SeedColor.FirstColor:
                    randColor1 = new Random(Colors.Count - counter.Next(0, Colors.Count - 1));
                    randColor2 = new Random();

                    switch (Brush)
                    {
                        case BrushType.Solid:
                            SignalPen.Brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

                            break;
                        case BrushType.Gradient:
                            SignalPen.Brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                            break;
                        case BrushType.Hatch:
                            SignalPen.Brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    //SignalPen.Brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                    break;
                case SeedColor.SecondColor:
                    randColor1 = new Random();
                    randColor2 = new Random(colorsReversed.Count - counter.Next(0, Colors.Count - 1));

                    switch (Brush)
                    {
                        case BrushType.Solid:
                            SignalPen.Brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

                            break;
                        case BrushType.Gradient:
                            SignalPen.Brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                            break;
                        case BrushType.Hatch:
                            SignalPen.Brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    //SignalPen.Brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                    break;
                case SeedColor.Both:
                    randColor1 = new Random(Colors.Count - counter.Next(0, Colors.Count - 1));
                    randColor2 = new Random(colorsReversed.Count - counter.Next(0, Colors.Count - 1));

                    switch (Brush)
                    {
                        case BrushType.Solid:
                            SignalPen.Brush = new SolidBrush(Colors[randColor1.Next(0, Colors.Count - 1)]);

                            break;
                        case BrushType.Gradient:
                            SignalPen.Brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                            break;
                        case BrushType.Hatch:
                            SignalPen.Brush = new HatchBrush(HatchStyle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)]);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    //SignalPen.Brush = new LinearGradientBrush(ClientRectangle, Colors[randColor1.Next(0, Colors.Count - 1)], colorsReversed[randColor2.Next(0, colorsReversed.Count - 1)], 90f);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (HatchAnimation)
            {
                g.RenderingOrigin = new Point(reactorOFS, 0);
            }

            SignalPen.DashOffset = dashOffset;

            #endregion


            for (int i = 0; i < Xw; i++)
            {
                Xnew = i;
                Ynew = (float)sg.GetValue(i); // NOTE: Only for debug, not for release configuration!
                if (i > 0)
                {
                    g.DrawLine(SignalPen, X1 + Xold - locationAnimator, Ym - Yold, X1 + Xnew, Ym - Ynew);
                }
                Xold = Xnew; Yold = Ynew;
            }

            // Draw the name of signal form:
            StringFormat format = new StringFormat();


            format.Alignment = StringAlignment.Center;

            SizeF stSize = g.MeasureString(st.ToString(), Font);

            if (ShowText)
            {
                g.DrawString(st.ToString(),
                    Font,
                    new SolidBrush(ForeColor),
                    (X2 - Xpw / 4) - stSize.Width/15 , 10, format);
            }


            if (ShowBorder)
            {
                // Draw border rectangle:
                g.DrawRectangle(BorderPen, X1 + 1, Y1 + 1, Xw - 2, Yh - 2);
            }

        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
            g.SmoothingMode = Smoothing;
            g.TextRenderingHint = TextRendering;

            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }

            CheckMaximum();

            PerformanceOnPaint(e);

            PaintSignal(e.Graphics, ClientRectangle, SignalType, Color.Red);

            base.OnPaint(e);

        }

        #endregion

        #region Performance Chart


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
        private Border3DStyle b3dstyle = Border3DStyle.Sunken;
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

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        /// <summary>
        /// Gets or sets the border style.
        /// </summary>
        /// <value>The border style.</value>
        [DefaultValue(typeof(Border3DStyle), "Sunken"), Description("BorderStyle"), Category("Appearance")]
        public Border3DStyle BorderStyle
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

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        private List<decimal> Values
        {
            get { return drawValues; }
            set
            {
                drawValues = value;
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
            Values.Clear();
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
                throw new Exception(String.Format("Values greater then 100 not allowed in ScaleMode: Absolute ({0})", value));


            switch (timerMode)
            {
                case TimerMode.Disabled:
                    ChartAppend(value);
                    Invalidate();
                    break;
                case TimerMode.Simple:
                case TimerMode.SynchronizedAverage:
                case TimerMode.SynchronizedSum:
                    // For all Timer Modes, the Values are stored in the Queue
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
            Values.Insert(0, Math.Max(value, 0));
            
            // Remove last item if maximum value count is reached
            if (Values.Count > MAX_VALUE_COUNT)
            {
                Values.RemoveAt(MAX_VALUE_COUNT);
                
            }
                

            // Calculate horizontal grid offset for "scrolling" effect
            gridScrollOffset += valueSpacing;
            if (gridScrollOffset > GRID_SPACING)
                gridScrollOffset = gridScrollOffset % GRID_SPACING;
        }


        /// <summary>
        /// Appends Values from queue
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
                if (Values[i] > maxValue)
                    maxValue = Values[i];
            }

            return maxValue;
        }

        #endregion


        #region *** Private Methods: Drawing ***

        /// <summary>
        /// Draws the chart (w/o background or grid, but with border) to the Graphics canvas
        /// </summary>
        /// <param name="g">Graphics</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void DrawChart(Graphics g)
        {
            visibleValues = Math.Min(this.Width / valueSpacing, Values.Count);

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
                currentPoint.Y = CalcVerticalPosition(Values[i]);

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
            //ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, b3dstyle);

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


        /// <summary>
        /// Draws the average line.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawAverageLine(Graphics g)
        {
            for (int i = 0; i < visibleValues; i++)
                averageValue += Values[i];

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
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
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

        #region Animation For Reverse

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
        /// The location animator
        /// </summary>
        private int locationAnimator = 0;
        /// <summary>
        /// The speed
        /// </summary>
        private float speed = 1;
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

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>The speed.</value>
        public int Speed
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
        /// Gets or sets the speed multiplier.
        /// </summary>
        /// <value>The speed multiplier.</value>
        public float SpeedMultiplier
        {
            get { return speed; }
            set
            {
                speed = value;
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
            MagicDivision();

            if (this.locationAnimator + 1 > 10)
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
                this.locationAnimator += 1;
                Invalidate();
            }

            if (AmplitudeTension + (0.1 * speed) > 30)
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
                this.AmplitudeTension += (0.1f * speed);
                Invalidate();
            }

            if (AmplitudeHeight + (0.1 * speed) > 5)
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
                this.AmplitudeHeight += (0.1f * speed);
                Invalidate();
            }

            if (Periods + (0.1 * speed) > 40)
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
                this.Periods += 0.1f * speed;
                Invalidate();
            }

            if (this.dashOffset + (0.1 * speed) > Math.Pow(10000000000000000000, 10000000000000000000))
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
                this.dashOffset += (0.1f * speed);
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
            MagicDivision();
            if (this.locationAnimator < 0)
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
                this.locationAnimator -= 1;
                Invalidate();
            }


            if (this.AmplitudeTension < 1)
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
                this.AmplitudeTension -= (0.1f * speed);
                Invalidate();
            }

            if (this.AmplitudeHeight < 0)
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
                this.AmplitudeHeight -= (0.1f * speed);
                Invalidate();
            }

            if (this.Periods < 0)
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
                this.Periods -= 0.1f * speed;
                Invalidate();
            }

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
                this.dashOffset -= (0.1f * speed);
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

        #endregion

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

        #endregion

        #region Private Fields


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

        #region Hatch Animation

        /// <summary>
        /// The enable hatch animation
        /// </summary>
        private bool enableHatchAnimation = true;

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
        //if (HatchAnimation)
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

        #region Serialized Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSignal"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public ZeroitSignal(SerializationInfo info, StreamingContext context)
        {

            periods = (float)info.GetValue("periods", typeof(float));
            amplitudeHeight = (float)info.GetValue("amplitudeHeight", typeof(float));
            amplitudeTension = (float)info.GetValue("amplitudeTension", typeof(float));
            gridLines = (int)info.GetValue("gridLines", typeof(int));
            //gridPen = (Pen)info.GetValue("gridPen", typeof(Pen));
            //signalPen = (Pen)info.GetValue("signalPen", typeof(Pen));
            //borderColor = (Pen)info.GetValue("borderColor", typeof(Pen));
            showMidGridLines = info.GetBoolean("showMidGridLines");
            showText = info.GetBoolean("showText");
            showBorder = info.GetBoolean("showBorder");
            textRendering = (TextRenderingHint)info.GetValue("textRendering", typeof(TextRenderingHint));
            smoothing = (SmoothingMode)info.GetValue("smoothing", typeof(SmoothingMode));
            seedColors = (ZeroitSignal.SeedColor)info.GetValue("seedColors", typeof(ZeroitSignal.SeedColor));
            multiplier = (float)info.GetValue("multiplier", typeof(float));
            items = (List<float>)info.GetValue("items", typeof(List<float>));
            maximum = (float)info.GetValue("maximum", typeof(float));
            signalType = (ZeroitSignal.Signal)info.GetValue("signalType", typeof(ZeroitSignal.Signal));
            frequency = (float)info.GetValue("frequency", typeof(float));
            phase = (float)info.GetValue("phase", typeof(float));
            amplitude = (float)info.GetValue("amplitude", typeof(float));
            offset = (float)info.GetValue("offset", typeof(float));
            invert = (float)info.GetValue("invert", typeof(float));
            b3dstyle = (Border3DStyle) info.GetValue("b3dstyle", typeof(Border3DStyle));
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("periods", periods);
            info.AddValue("amplitudeHeight", amplitudeHeight);
            info.AddValue("amplitudeTension", amplitudeTension);
            info.AddValue("gridLines", gridLines);
            //info.AddValue("gridPen", gridPen);
            //info.AddValue("signalPen", signalPen);
            //info.AddValue("borderColor", borderColor);
            info.AddValue("showMidGridLines", showMidGridLines);
            info.AddValue("showText", showText);
            info.AddValue("showBorder", showBorder);
            info.AddValue("textRendering", textRendering);
            info.AddValue("smoothing", smoothing);
            info.AddValue("seedColors", seedColors);
            info.AddValue("multiplier", multiplier);
            info.AddValue("items", items);
            info.AddValue("maximum", maximum);
            info.AddValue("signalType", signalType);
            info.AddValue("frequency", frequency);
            info.AddValue("phase", phase);
            info.AddValue("amplitude", amplitude);
            info.AddValue("offset", offset);
            info.AddValue("invert", invert);
            info.AddValue("b3dstyle", b3dstyle);

        }

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


    }

}
