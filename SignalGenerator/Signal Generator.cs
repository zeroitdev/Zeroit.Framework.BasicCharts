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
using System.Windows.Forms;


namespace Zeroit.Framework.BasicCharts
{

    #region Signal Generator

    #region Original Code
    /// <summary>
    /// Class OriginalSignalGenerator.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component" />
    class OriginalSignalGenerator : Component
    {
        #region [ Properties ... ]

        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>The control.</value>
        public Control Control

        {
            get;
            set;
        }

        /// <summary>
        /// The signal type
        /// </summary>
        private SignalType signalType = SignalType.Sine;
        /// <summary>
        /// Signal Type.
        /// </summary>
        /// <value>The type of the signal.</value>
        public SignalType SignalType
        {
            get { return signalType; }
            set { signalType = value; }
        }

        /// <summary>
        /// The frequency
        /// </summary>
        private float frequency = 1f;
        /// <summary>
        /// Signal Frequency.
        /// </summary>
        /// <value>The frequency.</value>
        public float Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        /// <summary>
        /// The phase
        /// </summary>
        private float phase = 0f;
        /// <summary>
        /// Signal Phase.
        /// </summary>
        /// <value>The phase.</value>
        public float Phase
        {
            get { return phase; }
            set { phase = value; }
        }

        /// <summary>
        /// The amplitude
        /// </summary>
        private float amplitude = 1f;
        /// <summary>
        /// Signal Amplitude.
        /// </summary>
        /// <value>The amplitude.</value>
        public float Amplitude
        {
            get { return amplitude; }
            set { amplitude = value; }

        }

        /// <summary>
        /// The offset
        /// </summary>
        private float offset = 0f;
        /// <summary>
        /// Signal Offset.
        /// </summary>
        /// <value>The offset.</value>
        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// The invert
        /// </summary>
        private float invert = 1; // Yes=-1, No=1
                                  /// <summary>
                                  /// Signal Inverted?
                                  /// </summary>
                                  /// <value><c>true</c> if invert; otherwise, <c>false</c>.</value>
        public bool Invert
        {
            get { return invert == -1; }
            set { invert = value ? -1 : 1; }
        }

        /// <summary>
        /// The get value callback
        /// </summary>
        private GetValueDelegate getValueCallback = null;
        /// <summary>
        /// GetValue Callback?
        /// </summary>
        /// <value>The get value callback.</value>
        public GetValueDelegate GetValueCallback
        {
            get { return getValueCallback; }
            set { getValueCallback = value; }
        }

        #endregion  [ Properties ]

        #region [ Private ... ]

        /// <summary>
        /// Random provider for noise generator
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Time the signal generator was started
        /// </summary>
        protected long startTime = Stopwatch.GetTimestamp();

        /// <summary>
        /// Ticks per second on this CPU
        /// </summary>
        protected long ticksPerSecond = Stopwatch.Frequency;

        #endregion  [ Private ]

        #region [ Public ... ]

        /// <summary>
        /// Delegate GetValueDelegate
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>System.Single.</returns>
        public delegate float GetValueDelegate(float time);

        /// <summary>
        /// Initializes a new instance of the <see cref="OriginalSignalGenerator" /> class.
        /// </summary>
        /// <param name="initialSignalType">Initial type of the signal.</param>
        public OriginalSignalGenerator(SignalType initialSignalType)
        {
            signalType = initialSignalType;
            Control.Paint += ControlPaint;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="OriginalSignalGenerator" /> class.
        /// </summary>
        public OriginalSignalGenerator() { }

#if DEBUG
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>System.Single.</returns>
        public float GetValue(float time)
#else
			private float GetValue(float time)
#endif
        {
            float value = 0f;
            float t = frequency * time + phase;

            switch (signalType)
            { // http://en.wikipedia.org/wiki/Waveform
                case SignalType.Sine: // sin( 2 * pi * t )
                    value = (float)Math.Sin(2f * Math.PI * t);
                    break;
                case SignalType.Square: // sign( sin( 2 * pi * t ) )
                    value = Math.Sign(Math.Sin(2f * Math.PI * t));
                    break;
                case SignalType.Triangle: // 2 * abs( t - 2 * floor( t / 2 ) - 1 ) - 1
                    value = 1f - 4f * (float)Math.Abs(Math.Round(t - 0.25f) - (t - 0.25f));
                    break;
                case SignalType.Sawtooth: // 2 * ( t/a - floor( t/a + 1/2 ) )
                    value = 2f * (t - (float)Math.Floor(t + 0.5f));
                    break;


                case SignalType.Pulse: // http://en.wikipedia.org/wiki/Pulse_wave
                    value = (Math.Abs(Math.Sin(2 * Math.PI * t)) < 1.0 - 10E-3) ? (0) : (1);
                    break;
                case SignalType.WhiteNoise: // http://en.wikipedia.org/wiki/White_noise
                    value = 2f * (float)random.Next(int.MaxValue) / int.MaxValue - 1f;
                    break;
                case SignalType.GaussNoise: // http://en.wikipedia.org/wiki/Gaussian_noise
                    value = (float)StatisticFunction.NORMINV((float)random.Next(int.MaxValue) / int.MaxValue, 0.0, 0.4);
                    break;
                case SignalType.DigitalNoise: //Binary Bit Generators
                    value = random.Next(2);
                    break;

                case SignalType.UserDefined:
                    value = (getValueCallback == null) ? (0f) : getValueCallback(t);
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
        public void Synchronize(OriginalSignalGenerator instance)
        {
            startTime = instance.startTime;
            ticksPerSecond = instance.ticksPerSecond;
        }

        #endregion [ Public ]

        #region Usage

        /// <summary>
        /// Users the defined signal.
        /// </summary>
        /// <param name="Time">The time.</param>
        /// <returns>System.Single.</returns>
        public float UserDefinedSignal(float Time)
        {
            return (float)Math.Sin(2f * Math.PI * Time);
        }

        //private void GenerateSignal()

        /// <summary>
        /// Paints the signal.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="r">The r.</param>
        /// <param name="st">The st.</param>
        /// <param name="c">The c.</param>
        private void PaintSignal(Graphics g, Rectangle r, SignalType st, Color c)
        {
            int X1 = 0;
            int Y1 = 0;

            int X2 = X1 + r.Width;
            int Y2 = Y1 + r.Height;

            int Xw = r.Width;
            int Yh = r.Height;

            int Xm = X1 + Xw / 2;   // X possition of middle point.
            int Ym = Y1 + Yh / 2;       // Y possition of middle point.

            int Nper = 3;       // Number of periodes that schud be shown.
            int Xpw = Xw / Nper;    // One Periode length in pixel.
            int Yah = 3 * Yh / 8;   // Signal amplitude height in picel. 

            // Create a custom pen:
            Pen myPen = new Pen(Color.LightGray);
            myPen.DashStyle = DashStyle.Dot;

            //Draw vertical grid lines:
            for (int i = 1; i < 2 * Nper; i++)
                g.DrawLine(myPen, X1 + (Xpw / 2) * i, Y1, X1 + (Xpw / 2) * i, Y2);

            //Draw horisontal grid lines:
            g.DrawLine(myPen, X1, Ym - Yah, X2, Ym - Yah);
            g.DrawLine(myPen, X1, Ym, X2, Ym);
            g.DrawLine(myPen, X1, Ym + Yah, X2, Ym + Yah);

            // Create requider signal generator: 
            OriginalSignalGenerator sg = new OriginalSignalGenerator(st);

            // Adjust aignal generator:
            sg.Frequency = 1f / Xpw;
            sg.Phase = 0f;
            sg.Amplitude = Yah;
            sg.Offset = 0f;
            sg.Invert = false;

            // Generate signal and draw it:
            float Xold = 0f;
            float Yold = 0f;
            float Xnew = 0f;
            float Ynew = 0f;
            myPen.Color = c;
            myPen.DashStyle = DashStyle.Solid;
            myPen.Width = 2;
            for (int i = 0; i < Xw; i++)
            {
                Xnew = i;
                Ynew = (float)sg.GetValue(i); // NOTE: Only for debug, not for release configuration!
                if (i > 0) g.DrawLine(myPen, X1 + Xold, Ym - Yold, X1 + Xnew, Ym - Ynew);
                Xold = Xnew; Yold = Ynew;
            }

            // Draw the name of signal form:
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            g.DrawString(st.ToString(),
                         new Font("Times", 8, FontStyle.Bold),
                         new SolidBrush(Color.Black),
                         X2 - Xpw / 4, 10, format);

            // Draw border rectangle:
            g.DrawRectangle(new Pen(Color.Black, 2), X1 + 1, Y1 + 1, Xw - 2, Yh - 2);
        }

        /// <summary>
        /// Controls the paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        void ControlPaint(object sender, PaintEventArgs e)
        {
            PaintSignal(e.Graphics, Control.Bounds, SignalType.Sine, Color.Red);
        }

        #endregion
    }

    #region [ Enums ... ]

    /// <summary>
    /// Enum SignalType
    /// </summary>
    public enum SignalType
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

    #endregion [ Enums ]

    #region [ Statistic ... ]

    /// <summary>
    /// Class StatisticFunction.
    /// </summary>
    public class StatisticFunction
    {
        // http://geeks.netindonesia.net/blogs/anwarminarso/archive/2008/01/13/normsinv-function-in-c-inverse-cumulative-standard-normal-distribution-function.aspx
        // http://home.online.no/~pjacklam/notes/invnorm/impl/misra/normsinv.html

        /// <summary>
        /// Means the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>System.Double.</returns>
        public static double Mean(double[] values)
        {
            double tot = 0;
            foreach (double val in values)
                tot += val;

            return (tot / values.Length);
        }

        /// <summary>
        /// Standards the deviation.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>System.Double.</returns>
        public static double StandardDeviation(double[] values)
        {
            return Math.Sqrt(Variance(values));
        }

        /// <summary>
        /// Variances the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>System.Double.</returns>
        public static double Variance(double[] values)
        {
            double m = Mean(values);
            double result = 0;
            foreach (double d in values)
                result += Math.Pow((d - m), 2);

            return (result / values.Length);
        }


        //
        // Lower tail quantile for standard normal distribution function.
        //
        // This function returns an approximation of the inverse cumulative
        // standard normal distribution function.  I.e., given P, it returns
        // an approximation to the X satisfying P = Pr{Z <= X} where Z is a
        // random variable from the standard normal distribution.
        //
        // The algorithm uses a minimax approximation by rational functions
        // and the result has a relative error whose absolute value is less
        // than 1.15e-9.
        //
        // Author:      Peter J. Acklam
        // (Javascript version by Alankar Misra @ Digital Sutras (alankar@digitalsutras.com))
        // Time-stamp:  2003-05-05 05:15:14
        // E-mail:      pjacklam@online.no
        // WWW URL:     http://home.online.no/~pjacklam

        // An algorithm with a relative error less than 1.15*10-9 in the entire region.

        /// <summary>
        /// Normsinvs the specified p.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>System.Double.</returns>
        public static double NORMSINV(double p)
        {
            // Coefficients in rational approximations
            double[] a = {-3.969683028665376e+01,  2.209460984245205e+02,
                -2.759285104469687e+02,  1.383577518672690e+02,
                -3.066479806614716e+01,  2.506628277459239e+00};

            double[] b = {-5.447609879822406e+01,  1.615858368580409e+02,
                -1.556989798598866e+02,  6.680131188771972e+01,
                -1.328068155288572e+01 };

            double[] c = {-7.784894002430293e-03, -3.223964580411365e-01,
                -2.400758277161838e+00, -2.549732539343734e+00,
                4.374664141464968e+00,  2.938163982698783e+00};

            double[] d = { 7.784695709041462e-03,  3.224671290700398e-01,
                2.445134137142996e+00,  3.754408661907416e+00};

            // Define break-points.
            double plow = 0.02425;
            double phigh = 1 - plow;

            // Rational approximation for lower region:
            if (p < plow)
            {
                double q = Math.Sqrt(-2 * Math.Log(p));
                return (((((c[0] * q + c[1]) * q + c[2]) * q + c[3]) * q + c[4]) * q + c[5]) /
                    ((((d[0] * q + d[1]) * q + d[2]) * q + d[3]) * q + 1);
            }

            // Rational approximation for upper region:
            if (phigh < p)
            {
                double q = Math.Sqrt(-2 * Math.Log(1 - p));
                return -(((((c[0] * q + c[1]) * q + c[2]) * q + c[3]) * q + c[4]) * q + c[5]) /
                    ((((d[0] * q + d[1]) * q + d[2]) * q + d[3]) * q + 1);
            }

            // Rational approximation for central region:
            {
                double q = p - 0.5;
                double r = q * q;
                return (((((a[0] * r + a[1]) * r + a[2]) * r + a[3]) * r + a[4]) * r + a[5]) * q /
                    (((((b[0] * r + b[1]) * r + b[2]) * r + b[3]) * r + b[4]) * r + 1);
            }
        }


        /// <summary>
        /// Norminvs the specified probability.
        /// </summary>
        /// <param name="probability">The probability.</param>
        /// <param name="mean">The mean.</param>
        /// <param name="standard_deviation">The standard deviation.</param>
        /// <returns>System.Double.</returns>
        public static double NORMINV(double probability, double mean, double standard_deviation)
        {
            return (NORMSINV(probability) * standard_deviation + mean);
        }

        /// <summary>
        /// Norminvs the specified probability.
        /// </summary>
        /// <param name="probability">The probability.</param>
        /// <param name="values">The values.</param>
        /// <returns>System.Double.</returns>
        public static double NORMINV(double probability, double[] values)
        {
            return NORMINV(probability, Mean(values), StandardDeviation(values));
        }

    }
    #endregion [ Statistic ]

    #endregion

    #endregion

    #region Modified Code
    /// <summary>
    /// Class SignalGenerate.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    class SignalGenerate : Control
    {
        #region [ Properties ... ]



        /// <summary>
        /// The signal type
        /// </summary>
        private SignalType signalType = SignalType.Sine;
        /// <summary>
        /// Signal Type.
        /// </summary>
        /// <value>The type of the signal.</value>
        public SignalType SignalType
        {
            get { return signalType; }
            set
            {
                signalType = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The frequency
        /// </summary>
        private float frequency = 1f;
        /// <summary>
        /// Signal Frequency.
        /// </summary>
        /// <value>The frequency.</value>
        public float Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;
            }
        }

        /// <summary>
        /// The phase
        /// </summary>
        private float phase = 0f;
        /// <summary>
        /// Signal Phase.
        /// </summary>
        /// <value>The phase.</value>
        public float Phase
        {
            get { return phase; }
            set
            {
                phase = value;

            }
        }

        /// <summary>
        /// The amplitude
        /// </summary>
        private float amplitude = 1f;
        /// <summary>
        /// Signal Amplitude.
        /// </summary>
        /// <value>The amplitude.</value>
        public float Amplitude
        {
            get { return amplitude; }
            set
            {
                amplitude = value;

            }

        }

        /// <summary>
        /// The offset
        /// </summary>
        private float offset = 0f;
        /// <summary>
        /// Signal Offset.
        /// </summary>
        /// <value>The offset.</value>
        public float Offset
        {
            get { return offset; }
            set
            {
                offset = value;
            }
        }

        /// <summary>
        /// The invert
        /// </summary>
        private float invert = 1; // Yes=-1, No=1
                                  /// <summary>
                                  /// Signal Inverted?
                                  /// </summary>
                                  /// <value><c>true</c> if invert; otherwise, <c>false</c>.</value>
        public bool Invert
        {
            get { return invert == -1; }
            set
            {
                invert = value ? -1 : 1;
            }
        }

        /// <summary>
        /// The get value callback
        /// </summary>
        private GetValueDelegate getValueCallback = null;
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
        public float[] InitialLocation
        {
            get { return initialLocation; }
            set
            {
                initialLocation = value;
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
            set { amplitudeTension = value; Invalidate(); }
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
        /// Gets or sets the signal pen.
        /// </summary>
        /// <value>The signal pen.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pen SignalPen
        {
            get { return signalPen; }
            set { signalPen = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Pen BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show grid lines].
        /// </summary>
        /// <value><c>true</c> if [show grid lines]; otherwise, <c>false</c>.</value>
        public bool ShowGridLines
        {
            get { return showGridLines; }
            set { showGridLines = value; Invalidate(); }
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

        #endregion  [ Properties ]

        #region [ Private ... ]

        /// <summary>
        /// Random provider for noise generator
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Time the signal generator was started
        /// </summary>
        protected long startTime = Stopwatch.GetTimestamp();

        /// <summary>
        /// Ticks per second on this CPU
        /// </summary>
        protected long ticksPerSecond = Stopwatch.Frequency;

        #endregion  [ Private ]

        /// <summary>
        /// Sums the specified sum.
        /// </summary>
        /// <param name="sum">The sum.</param>
        /// <returns>System.Single.</returns>
        public static float Sum(params float[] sum)
        {
            float addition = 0;

            for (int i = 0; i < sum.Length; i++)
            {
                addition += sum[i];
            }
            return addition;
        }

        #region [ Public ... ]

        /// <summary>
        /// Gets the get sum.
        /// </summary>
        /// <value>The get sum.</value>
        public float GetSum
        {
            get { return Sum(items.ToArray()); }
        }

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
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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
        /// Delegate GetValueDelegate
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>System.Single.</returns>
        public delegate float GetValueDelegate(float time);


        /// <summary>
        /// Initializes a new instance of the <see cref="OriginalSignalGenerator" /> class.
        /// </summary>
        public SignalGenerate()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SignalGenerate"/> class.
        /// </summary>
        /// <param name="initialSignalType">Initial type of the signal.</param>
        public SignalGenerate(SignalType initialSignalType)
        {
            signalType = initialSignalType;

        }

#if DEBUG
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>System.Single.</returns>
        public float GetValue(float time)
#else
			private float GetValue(float time)
#endif
        {
            float value = 0f;
            float t = frequency * time + phase;
            switch (signalType)
            { // http://en.wikipedia.org/wiki/Waveform
                case SignalType.Sine: // sin( 2 * pi * t )
                    value = (float)Math.Sin(2f * Math.PI * t);
                    break;
                case SignalType.Square: // sign( sin( 2 * pi * t ) )
                    value = Math.Sign(Math.Sin(2f * Math.PI * t));
                    break;
                case SignalType.Triangle: // 2 * abs( t - 2 * floor( t / 2 ) - 1 ) - 1
                    value = 1f - 4f * (float)Math.Abs(Math.Round(t - 0.25f) - (t - 0.25f));
                    break;
                case SignalType.Sawtooth: // 2 * ( t/a - floor( t/a + 1/2 ) )
                    value = 2f * (t - (float)Math.Floor(t + 0.5f));
                    break;


                case SignalType.Pulse: // http://en.wikipedia.org/wiki/Pulse_wave
                    value = (Math.Abs(Math.Sin(2 * Math.PI * t)) < 1.0 - 10E-3) ? (0) : (1);
                    break;
                case SignalType.WhiteNoise: // http://en.wikipedia.org/wiki/White_noise
                    value = 2f * (float)random.Next(int.MaxValue) / int.MaxValue - 1f;
                    break;
                case SignalType.GaussNoise: // http://en.wikipedia.org/wiki/Gaussian_noise
                    value = (float)StatisticFunction.NORMINV((float)random.Next(int.MaxValue) / int.MaxValue, 0.0, 0.4);
                    break;
                case SignalType.DigitalNoise: //Binary Bit Generators
                    value = random.Next(2);
                    break;

                case SignalType.UserDefined:
                    value = (getValueCallback == null) ? (0f) : getValueCallback(t);
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
        public void Synchronize(SignalGenerate instance)
        {
            startTime = instance.startTime;
            ticksPerSecond = instance.ticksPerSecond;
        }

        #endregion [ Public ]

        #region Usage

        /// <summary>
        /// Users the defined signal.
        /// </summary>
        /// <param name="Time">The time.</param>
        /// <returns>System.Single.</returns>
        public float UserDefinedSignal(float Time)
        {
            return (float)Math.Sin(2f * Math.PI * Time);
        }

        //private void GenerateSignal()


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
        private float amplitudeTension = 8;
        /// <summary>
        /// The grid lines
        /// </summary>
        private int gridLines = 2;

        /// <summary>
        /// The grid pen
        /// </summary>
        private Pen gridPen = new Pen(Color.Green) { Width = 1, DashStyle = DashStyle.DashDot };
        /// <summary>
        /// The signal pen
        /// </summary>
        private Pen signalPen = new Pen(Color.Blue) { DashStyle = DashStyle.Solid, Width = 2 };
        /// <summary>
        /// The border color
        /// </summary>
        private Pen borderColor = new Pen(Color.Black) { Width = 2 };


        /// <summary>
        /// The show grid lines
        /// </summary>
        private bool showGridLines = true;
        /// <summary>
        /// The show text
        /// </summary>
        private bool showText = true;
        /// <summary>
        /// The show border
        /// </summary>
        private bool showBorder = true;

        /// <summary>
        /// Paints the signal.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="r">The r.</param>
        /// <param name="st">The st.</param>
        /// <param name="c">The c.</param>
        private void PaintSignal(Graphics g, Rectangle r, SignalType st, Color c)
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
            float Yah = AmplitudeHeight * Yh / (AmplitudeTension * (GetSum / Maximum));   // Signal amplitude height in picel. 

            // Create a custom pen:


            if (ShowGridLines)
            {
                //Draw vertical grid lines:
                for (int i = 1; i < GridLines * Nper; i++)
                    g.DrawLine(GridPen, X1 + (Xpw / GridLines) * i, Y1, X1 + (Xpw / GridLines) * i, Y2);

                //Draw horisontal grid lines:
                g.DrawLine(GridPen, X1, Ym - Yah, X2, Ym - Yah);
                g.DrawLine(GridPen, X1, Ym, X2, Ym);
                g.DrawLine(GridPen, X1, Ym + Yah, X2, Ym + Yah);

            }

            // Create requider signal generator: 
            SignalGenerate sg = new SignalGenerate(st);

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


            //signalPen.Color = c;
            //signalPen.DashStyle = DashStyle.Solid;
            //signalPen.Width = 2;
            for (int i = 0; i < Xw; i++)
            {
                Xnew = i;
                Ynew = (float)sg.GetValue(i); // NOTE: Only for debug, not for release configuration!
                if (i > 0) g.DrawLine(SignalPen, X1 + Xold, Ym - Yold, X1 + Xnew, Ym - Ynew);
                Xold = Xnew; Yold = Ynew;
            }

            // Draw the name of signal form:
            StringFormat format = new StringFormat();


            format.Alignment = StringAlignment.Center;

            if (ShowText)
            {
                g.DrawString(st.ToString(),
                    Font,
                    new SolidBrush(ForeColor),
                    X2 - Xpw / 4, 10, format);
            }


            if (ShowBorder)
            {
                // Draw border rectangle:
                g.DrawRectangle(BorderColor, X1 + 1, Y1 + 1, Xw - 2, Yh - 2);
            }

        }


        /// <summary>
        /// Checks the maximum.
        /// </summary>
        public void CheckMaximum()
        {
            if (GetSum > Maximum)
            {
                Maximum *= 10;
            }
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            CheckMaximum();

            PaintSignal(e.Graphics, ClientRectangle, SignalType, Color.Red);
        }



        #endregion
    }

    #endregion
}
