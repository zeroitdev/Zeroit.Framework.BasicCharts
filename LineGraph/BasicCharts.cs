// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-01-2018
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
#region Imports

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;



#endregion

namespace Zeroit.Framework.BasicCharts
{

    #region Line ZeroitGraph

    #region Control
    /// <summary>
    /// Summary description for ZeroitLineGraph.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [ToolboxItem(false)]
    public class ZeroitLineGraph : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /*Create string array for the months in the year */
        /// <summary>
        /// The months
        /// </summary>
        string[] months = new string[]
                        {"JAN","FEB","MAR","APR","MAY","JUN","JUL","AUG","SEP","OCT","NOV","DEC"};

        /// <summary>
        /// The x axis value
        /// </summary>
        private int _xAxisValue = 0;//xAxis Starting value
        /// <summary>
        /// The increment value
        /// </summary>
        private int incrementValue = 0;//Increment or decrement value on the X axis
        /// <summary>
        /// The is y axis formula required
        /// </summary>
        private bool _IsYAxisFormulaRequired = false; //Flag to check if a specified formula is required to plot points
        /// <summary>
        /// The h table
        /// </summary>
        private Hashtable hTable = new Hashtable();//Hashtable that gets the data from the calling parent
        /// <summary>
        /// The x axis splits
        /// </summary>
        private int xAxisSplits = 0;//Number of points to be plotted on the xaxis this is decided based on the enumeration constant set



        /// <summary>
        /// Gets or sets the x values.
        /// </summary>
        /// <value>The x values.</value>
        public Hashtable XValues
        {
            get { return hTable; }

            set
            {
                hTable = value;
                this.Refresh();
            }


        }

        /// <summary>
        /// The set month
        /// </summary>
        private string[] _SetMonth = new string[12];

        /// <summary>
        /// Exposed to allow parent to define the values to graph.  When this property
        /// is assigned to, the control is invalidated and thus redrawn.
        /// </summary>

        #region Y Axis Scale

        public enum YAxisScale
        {
            /// <summary>
            /// The multiples of10
            /// </summary>
            MultiplesOf10 = 0,
            /// <summary>
            /// The multiples of25
            /// </summary>
            MultiplesOf25 = 1,
            /// <summary>
            /// The multiples of50
            /// </summary>
            MultiplesOf50 = 2
        }

        /// <summary>
        /// The y axis drawing scale
        /// </summary>
        public YAxisScale yAxisDrawingScale;

        /// <summary>
        /// Gets or sets the y axis scale style.
        /// </summary>
        /// <value>The y axis scale style.</value>
        public YAxisScale YAxisScaleStyle
        {

            get
            {
                return yAxisDrawingScale;
            }

            set
            {
                yAxisDrawingScale = value;
                GenerateGraph();//Redraw the graphics[Called here as to reflect the changes immediately when the user sets the scale for y axis during design time in the parent]
            }

        }

        #endregion


        #region XAxisScale

        /// <summary>
        /// Exposed to allow parent to define the values to graph.  When this property
        /// is assigned to, the control is invalidated and thus redrawn.
        /// </summary>
        public enum XAxisScale
        {
            /// <summary>
            /// The first quarter
            /// </summary>
            FirstQuarter = 0,
            /// <summary>
            /// The second quarter
            /// </summary>
            SecondQuarter = 1,
            /// <summary>
            /// The third quarter
            /// </summary>
            ThirdQuarter = 2,
            /// <summary>
            /// The fourth quarter
            /// </summary>
            FourthQuarter = 3,
            /// <summary>
            /// The first six months
            /// </summary>
            FirstSixMonths = 4,
            /// <summary>
            /// The second six months
            /// </summary>
            SecondSixMonths = 5,
            /// <summary>
            /// The year
            /// </summary>
            Year = 6
        }

        /// <summary>
        /// The x axis drawing scale
        /// </summary>
        public XAxisScale xAxisDrawingScale;


        /// <summary>
        /// Gets or sets the x axis scale style.
        /// </summary>
        /// <value>The x axis scale style.</value>
        public XAxisScale XAxisScaleStyle
        {

            get
            {
                return xAxisDrawingScale;
            }

            set
            {
                xAxisDrawingScale = value;
                GenerateGraph();//Redraw the graphics[Called here as to reflect the changes immediately when the user sets the scale for x axis during design time in the parent]
            }

        }

        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            GenerateGraph();//Redraw the graphics
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLineGraph"/> class.
        /// </summary>
        public ZeroitLineGraph()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitComponent call

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Plots the points on the y axis with respect to the scale set.
        /// </summary>
        /// <param name="yScale">Enum value</param>

        private void PlotYAxis(YAxisScale yScale)
        {

            int yAxisInterval = 0;//Y axis interval thats to be set on the Y axis
            int yAxisSetIntervalValue = 0;//Incremental value set for the y axis
            int numberOfSplits = 0;//value by which the y axis will be divided into parts
            int labelStartValue = 0;//Start value for plotting and drawing the labelling the y axis
            int labelInCrementValue = 0;//Label increment value 


            Graphics graphic = this.CreateGraphics(); //Create the graphics object


            Pen newpen = new Pen(Color.Black);//Create a pen to draw


            switch (yScale)//Check for scale assigned by the user in the Y axis and set the values correspondingly
            {
                case YAxisScale.MultiplesOf10:
                    yAxisInterval = 20;
                    numberOfSplits = 200 / yAxisInterval;
                    yAxisSetIntervalValue = 20;
                    labelInCrementValue = 10;
                    labelStartValue = 100;
                    _IsYAxisFormulaRequired = true;
                    break;
                case YAxisScale.MultiplesOf25:
                    yAxisInterval = 20;
                    numberOfSplits = 200 / 25;
                    labelInCrementValue = 25;
                    labelStartValue = 200;
                    yAxisSetIntervalValue = 25;
                    _IsYAxisFormulaRequired = false;
                    break;

                case YAxisScale.MultiplesOf50:
                    yAxisInterval = 20;
                    numberOfSplits = 200 / 50;
                    labelInCrementValue = 50;
                    yAxisSetIntervalValue = 50;
                    labelStartValue = 200;
                    _IsYAxisFormulaRequired = false;
                    break;

                default: break;
            }

            Font newfont = new Font("Arial", 8);

            //Iterate till value of number of splits and label accordingly
            for (int i = 1; i <= numberOfSplits; i++)
            {
                graphic.DrawLine(newpen, 47, yAxisInterval, 53, yAxisInterval);//Drawing lines based on interval value on the y axis
                graphic.DrawString(labelStartValue.ToString(), newfont, new SolidBrush(Color.Black), 25, yAxisInterval - 10);//Labeling the y axis 
                yAxisInterval += yAxisSetIntervalValue;//increment the y axis value
                labelStartValue -= labelInCrementValue;//Decrement the start value as to plot points from top to bottom on the y axis
            }

        }



        /// <summary>
        /// Plots the points on the y axis with respect to the scale set.
        /// </summary>
        /// <param name="xScale">Enum value</param>
        private void plotXAxis(XAxisScale xScale)
        {

            Graphics graphic = this.CreateGraphics(); //Creating the graphics object

            Pen newpen = new Pen(Color.Black);//Pen to draw and mark the x & y axis

            int startIndex = 0;//Starting index for the months array as to label the x axis based on the enum set

            int startValue = 350;//X axis starting point from the right

            switch (xScale)//Check for scale [enum value] assigned by the user in the X axis and set the values correspondingly
            {
                case XAxisScale.FirstQuarter:
                    xAxisSplits = 3;
                    startIndex = months.Length - 10;
                    incrementValue = 100;
                    _xAxisValue = 100;
                    break;
                case XAxisScale.SecondQuarter:
                    startIndex = months.Length - 7;
                    xAxisSplits = 3;
                    incrementValue = 100;
                    _xAxisValue = 100;
                    break;
                case XAxisScale.ThirdQuarter:
                    startIndex = months.Length - 4;
                    xAxisSplits = 3;
                    incrementValue = 100;
                    _xAxisValue = 100;
                    break;

                case XAxisScale.FourthQuarter:
                    startIndex = months.Length - 1;
                    xAxisSplits = 3;
                    incrementValue = 100;
                    _xAxisValue = 100;
                    break;

                case XAxisScale.FirstSixMonths:
                    startIndex = months.Length - 7;
                    incrementValue = 50;
                    xAxisSplits = 6;
                    _xAxisValue = 50;
                    break;
                case XAxisScale.SecondSixMonths:
                    incrementValue = 50;
                    startIndex = months.Length - 1;
                    xAxisSplits = 6;
                    _xAxisValue = 50;
                    break;

                case XAxisScale.Year:
                    incrementValue = 25;
                    startIndex = 0;
                    xAxisSplits = 12;
                    _xAxisValue = 25;
                    startIndex = months.Length - 1;
                    break;

                default: break;
            }


            Font newfont = new Font("Arial", 8);//specifying the font



            int idex = 0;//Index value for the month


            _SetMonth.Initialize();


            for (int i = 1; i <= xAxisSplits; i++)
            {
                graphic.DrawLine(newpen, startValue, 217, startValue, 223);
                graphic.DrawString(months[startIndex], newfont, new SolidBrush(Color.Black), startValue - 10, 227);
                _SetMonth[idex] = months[startIndex];//Getting the months that is specified by the user
                idex += 1;//Increment the index	
                startIndex -= 1;//Decrement the start index for the month 
                startValue -= incrementValue;// decrement the start value
            }

        }

        /// <summary>
        /// Generates the graph based on the hastable values provided by the user in the parent form.
        /// </summary>

        private void GenerateGraph()
        {

            Graphics graphic = Graphics.FromHwnd(this.Handle);

            graphic.Clear(Color.FloralWhite); //Clears the graph thats already written

            Pen newpen = new Pen(Color.Black);//Create a pen with black color

            //Set point of origin to be 50,100 on the form
            graphic.TranslateTransform(50, 100);

            graphic.DrawLine(newpen, 1, -80, 1, 120);//Drawing the vertical line [Y - Axis]
            graphic.DrawLine(newpen, 1, 120, 300, 120);//Drawing the vertical line [X - Axis]

            PlotYAxis(yAxisDrawingScale); //Plot the scale in the Y axis
            plotXAxis(xAxisDrawingScale); //Plot the scale in the X axis

            //Check for the plotting points in the hash table and the draw points and lines	
            if (hTable.Count < 1)
            {
                return;
            }


            Hashtable reDefinedHTable = new Hashtable(); //Local hash table to get the values from the hashtable after proper casing is done.

            /*Enumerating the hashtable and then moving the key and values to the local hash table and converting to upper for string comparisons with the month names array*/
            foreach (string key in hTable.Keys)
            {
                int val = Convert.ToInt32(hTable[key].ToString());
                reDefinedHTable.Add(key.ToString().ToUpper(), val.ToString().ToUpper());
            }

            int[] _graphValues = new int[xAxisSplits];//Holds the plotting point values

            _graphValues.Initialize();

            int j = 0;//Index value for the graph value to hold the point information
            for (int i = _SetMonth.Length - 1; i >= 0; i--)
            {
                if (_SetMonth[i] != null)//Check if the array is null
                {
                    //Check if the key exist in the hash table
                    if (reDefinedHTable.Contains(_SetMonth[i]) == true)
                    {
                        string keyValue = (string)reDefinedHTable[_SetMonth[i]];
                        _graphValues[j] = Convert.ToInt32(keyValue.ToString());
                        j = j + 1;
                    }
                }
            }


            Point[] pointarray = new Point[_graphValues.Length]; //Array for number of points on the graph area


            int graphPoint = 0;//Point thats plotted  
            int arrIndex = 0;//Array index for the point


            //Iterate through the graph values	
            foreach (int dataPoint in _graphValues)
            {
                //Check if the formula for the multiples of 10 required
                if (_IsYAxisFormulaRequired == false)
                {
                    graphPoint = 120 - dataPoint; //formula for multiples of 25 & 50 increments of y axis				
                }
                else
                {
                    graphPoint = 118 - dataPoint * 2;   //Formula for the increments of 10 in y axis
                }
                graphic.DrawEllipse(new Pen(Color.Red), _xAxisValue, graphPoint - 2, 4, 4);//Plot points 
                pointarray[arrIndex] = new Point(_xAxisValue, Convert.ToInt32(graphPoint));//move the point information to the array
                arrIndex = arrIndex + 1;//Increment the array index
                _xAxisValue += incrementValue; //Increment the x axis scale

            }

            graphic.DrawLines(new Pen(Color.OrangeRed), pointarray);//Join the points			
            this.Update();

        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // ZeroitLineGraph
            // 
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.Name = "ZeroitLineGraph";
            this.Load += new System.EventHandler(this.LineGraph_Load);

        }
        #endregion

        /// <summary>
        /// Handles the Load event of the LineGraph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LineGraph_Load(object sender, System.EventArgs e)
        {

        }
    }
    #endregion

    #endregion
    
}
