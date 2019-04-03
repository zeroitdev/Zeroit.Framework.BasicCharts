// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="SmartTag.cs" company="Zeroit Dev Technologies">
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts
{

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitPerformanceChartDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitPerformanceChartDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitPerformanceChartDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        /// <summary>
        /// The action lists
        /// </summary>
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        /// <summary>
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        /// <value>The action lists.</value>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new ZeroitPerformanceChartSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }

        #region Zeroit Filter (Remove Properties)
        /// <summary>
        /// Remove Button and Control properties that are
        /// not supported by the <see cref="MACButton" />.
        /// </summary>
        /// <param name="Properties">The properties.</param>
        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("AllowDrop");
            //Properties.Remove("FlatStyle");
            //Properties.Remove("ForeColor");
            //Properties.Remove("ImageIndex");
            //Properties.Remove("ImageList");
        }
        #endregion

    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class ZeroitPerformanceChartSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    internal class ZeroitPerformanceChartSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitPerformanceChart colUserControl;


        /// <summary>
        /// Gets the designer action UI service.
        /// </summary>
        /// <value>The designer action UI service.</value>
        private DesignerActionUIService DesignerActionUIService
        {
            get { return GetService(typeof(DesignerActionUIService)) as DesignerActionUIService; }
        }


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitPerformanceChartSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitPerformanceChartSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitPerformanceChart;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        /// <returns>PropertyDescriptor.</returns>
        /// <exception cref="ArgumentException">Matching ColorLabel property not found!</exception>
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        #region Properties that are targets of DesignerActionPropertyItem entries.

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get
            {
                return colUserControl.BackColor;
            }
            set
            {
                GetPropertyByName("BackColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor
        {
            get
            {
                return colUserControl.ForeColor;
            }
            set
            {
                GetPropertyByName("ForeColor").SetValue(colUserControl, value);
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [anti aliasing].
        /// </summary>
        /// <value><c>true</c> if [anti aliasing]; otherwise, <c>false</c>.</value>
        public bool AntiAliasing
        {
            get
            {
                return colUserControl.PerfChartStyle.AntiAliasing;
            }
            set
            {
                colUserControl.PerfChartStyle.AntiAliasing = value;
                colUserControl.Invalidate();
                RefreshComponent();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show average line].
        /// </summary>
        /// <value><c>true</c> if [show average line]; otherwise, <c>false</c>.</value>
        public bool ShowAverageLine
        {
            get
            {
                return colUserControl.PerfChartStyle.ShowAverageLine;
            }
            set
            {
                colUserControl.PerfChartStyle.ShowAverageLine = value;
                colUserControl.Invalidate();
                RefreshComponent();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show horizontal grid lines].
        /// </summary>
        /// <value><c>true</c> if [show horizontal grid lines]; otherwise, <c>false</c>.</value>
        public bool ShowHorizontalGridLines
        {
            get
            {
                return colUserControl.PerfChartStyle.ShowHorizontalGridLines;
            }
            set
            {
                colUserControl.PerfChartStyle.ShowHorizontalGridLines = value;
                colUserControl.Invalidate();
                RefreshComponent();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show vertical grid lines].
        /// </summary>
        /// <value><c>true</c> if [show vertical grid lines]; otherwise, <c>false</c>.</value>
        public bool ShowVerticalGridLines
        {
            get
            {
                return colUserControl.PerfChartStyle.ShowVerticalGridLines;
            }
            set
            {
                colUserControl.PerfChartStyle.ShowVerticalGridLines = value;
                colUserControl.Invalidate();
                RefreshComponent();
            }
        }

        /// <summary>
        /// Gets or sets the border style.
        /// </summary>
        /// <value>The border style.</value>
        public Border3DStyle BorderStyle
        {
            get
            {
                return colUserControl.BorderStyle;
            }
            set
            {
                colUserControl.BorderStyle = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the scale mode.
        /// </summary>
        /// <value>The scale mode.</value>
        public ScaleMode ScaleMode
        {
            get
            {
                return colUserControl.ScaleMode;
            }
            set
            {
                colUserControl.ScaleMode = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the timer mode.
        /// </summary>
        /// <value>The timer mode.</value>
        public TimerMode TimerMode
        {
            get
            {
                return colUserControl.TimerMode;
            }
            set
            {
                colUserControl.TimerMode = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the timer interval.
        /// </summary>
        /// <value>The timer interval.</value>
        public int TimerInterval
        {
            get
            {
                return colUserControl.TimerInterval;
            }
            set
            {
                colUserControl.TimerInterval = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public List<decimal> Values
        {
            get
            {
                return colUserControl.Values;
            }
            set
            {
                colUserControl.Values = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the chart style back color bottom.
        /// </summary>
        /// <value>The chart style back color bottom.</value>
        public Color ChartStyleBackColorBottom
        {
            get
            {
                return colUserControl.PerfChartStyle.BackgroundColorBottom;
            }
            set
            {
                colUserControl.PerfChartStyle.BackgroundColorBottom = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the chart style back color top.
        /// </summary>
        /// <value>The chart style back color top.</value>
        public Color ChartStyleBackColorTop
        {
            get
            {
                return colUserControl.PerfChartStyle.BackgroundColorTop;
            }
            set
            {
                colUserControl.PerfChartStyle.BackgroundColorTop = value;
                colUserControl.Invalidate();
            }
        }


        #region Chart Pen

        /// <summary>
        /// Gets or sets the average color of the line pen.
        /// </summary>
        /// <value>The average color of the line pen.</value>
        public Color AvgLinePenColor
        {
            get
            {
                return colUserControl.PerfChartStyle.AvgLinePen.Color;
            }
            set
            {
                colUserControl.PerfChartStyle.AvgLinePen.Color = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the average line dash style.
        /// </summary>
        /// <value>The average line dash style.</value>
        public DashStyle AvgLineDashStyle
        {
            get
            {
                return colUserControl.PerfChartStyle.AvgLinePen.DashStyle;
            }
            set
            {
                colUserControl.PerfChartStyle.AvgLinePen.DashStyle = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the average width of the line pen.
        /// </summary>
        /// <value>The average width of the line pen.</value>
        public float AvgLinePenWidth
        {
            get
            {
                return colUserControl.PerfChartStyle.AvgLinePen.Width;
            }
            set
            {
                colUserControl.PerfChartStyle.AvgLinePen.Width = value;
                colUserControl.Invalidate();
            }
        }

        #endregion

        #region Chart Line Pen

        /// <summary>
        /// Gets or sets the color of the chart line pen.
        /// </summary>
        /// <value>The color of the chart line pen.</value>
        public Color ChartLinePenColor
        {
            get
            {
                return colUserControl.PerfChartStyle.ChartLinePen.Color;
            }
            set
            {
                colUserControl.PerfChartStyle.ChartLinePen.Color = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the chart line dash style.
        /// </summary>
        /// <value>The chart line dash style.</value>
        public DashStyle ChartLineDashStyle
        {
            get
            {
                return colUserControl.PerfChartStyle.ChartLinePen.DashStyle;
            }
            set
            {
                colUserControl.PerfChartStyle.ChartLinePen.DashStyle = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the width of the chart line pen.
        /// </summary>
        /// <value>The width of the chart line pen.</value>
        public float ChartLinePenWidth
        {
            get
            {
                return colUserControl.PerfChartStyle.ChartLinePen.Width;
            }
            set
            {
                colUserControl.PerfChartStyle.ChartLinePen.Width = value;
                colUserControl.Invalidate();
            }
        }

        #endregion

        #region Horizontal Grid Pen

        /// <summary>
        /// Gets or sets the color of the horiz grid pen.
        /// </summary>
        /// <value>The color of the horiz grid pen.</value>
        public Color HorizGridPenColor
        {
            get
            {
                return colUserControl.PerfChartStyle.HorizontalGridPen.Color;
            }
            set
            {
                colUserControl.PerfChartStyle.HorizontalGridPen.Color = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the horiz grid pen dash style.
        /// </summary>
        /// <value>The horiz grid pen dash style.</value>
        public DashStyle HorizGridPenDashStyle
        {
            get
            {
                return colUserControl.PerfChartStyle.HorizontalGridPen.DashStyle;
            }
            set
            {
                colUserControl.PerfChartStyle.HorizontalGridPen.DashStyle = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the width of the horiz grid pen.
        /// </summary>
        /// <value>The width of the horiz grid pen.</value>
        public float HorizGridPenWidth
        {
            get
            {
                return colUserControl.PerfChartStyle.HorizontalGridPen.Width;
            }
            set
            {
                colUserControl.PerfChartStyle.HorizontalGridPen.Width = value;
                colUserControl.Invalidate();
            }
        }
        #endregion

        #region Vertical Grid Pen

        /// <summary>
        /// Gets or sets the color of the vert grid pen.
        /// </summary>
        /// <value>The color of the vert grid pen.</value>
        public Color VertGridPenColor
        {
            get
            {
                return colUserControl.PerfChartStyle.VerticalGridPen.Color;
            }
            set
            {
                colUserControl.PerfChartStyle.VerticalGridPen.Color = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the vert grid pen dash style.
        /// </summary>
        /// <value>The vert grid pen dash style.</value>
        public DashStyle VertGridPenDashStyle
        {
            get
            {
                return colUserControl.PerfChartStyle.VerticalGridPen.DashStyle;
            }
            set
            {
                colUserControl.PerfChartStyle.VerticalGridPen.DashStyle = value;
                colUserControl.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the width of the vert grid pen.
        /// </summary>
        /// <value>The width of the vert grid pen.</value>
        public float VertGridPenWidth
        {
            get
            {
                return colUserControl.PerfChartStyle.VerticalGridPen.Width;
            }
            set
            {
                colUserControl.PerfChartStyle.VerticalGridPen.Width = value;
                colUserControl.Invalidate();
            }
        }
        #endregion


        #endregion

        #region DesignerActionItemCollection

        #region Template Methods
        /// <summary>
        /// Refreshes the component.
        /// </summary>
        internal void RefreshComponent()
        {
            if (DesignerActionUIService != null)
                DesignerActionUIService.Refresh(colUserControl);
        }


        //protected virtual void Export()
        //{
        //    var editor = new System.Windows.Forms.Form();
        //    editor.ShowDialog();
        //}

        //protected virtual void Import()
        //{
        //    var editor = new System.Windows.Forms.Form();
        //    editor.ShowDialog();
        //}

        //protected virtual void ShowBorders()
        //{
        //    colUserControl.ShowBorders = !colUserControl.ShowBorders;
        //    colUserControl.Invalidate();
        //    RefreshComponent();
        //}


        //protected virtual void AddButton()
        //{

        //    var item = "Added";
        //    colUserControl.Items.Add(item);
        //    colUserControl.Invalidate();
        //    RefreshComponent();
        //}

        //protected virtual void ClearButtons()
        //{
        //    colUserControl.Items.Clear();
        //    colUserControl.Invalidate();
        //    RefreshComponent();
        //}

        //protected virtual void DeleteItem()
        //{
        //    colUserControl.Items.Remove("Added");
        //    colUserControl.Invalidate();
        //    RefreshComponent();
        //}


        #endregion

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            #region Add Private Methods

            //items.Add(new DesignerActionHeaderItem("Template"));

            //items.Add(new DesignerActionMethodItem(this, "Export",
            //                     "Export Template", "Template",
            //                     "Selects the template to display"));

            //items.Add(new DesignerActionMethodItem(this, "Import",
            //                     "Import Template", "Template", true)); //Alternative Method



            //items.Add(new DesignerActionHeaderItem("Visuals"));

            //if (!colUserControl.ShowBorders)
            //    items.Add(new DesignerActionMethodItem(this, "ShowBorders", "Show Borders", "Visuals", true));
            //else
            //    items.Add(new DesignerActionMethodItem(this, "ShowBorders", "Hide Borders", "Visuals", true));

            ////List or Collections
            //items.Add(new DesignerActionHeaderItem("Collection"));
            //if (colUserControl.Items.Count > 0)
            //    items.Add(new DesignerActionMethodItem(this, "ClearButtons", "Clear Buttons", "Collection", true));
            //items.Add(new DesignerActionMethodItem(this, "AddButton", "Add Button", "Collection", true));
            //if (colUserControl.Items.Count > 0)
            //    items.Add(new DesignerActionMethodItem(this, "DeleteButton", "Delete Button", "Collection", true));



            #endregion

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));


            items.Add(new DesignerActionPropertyItem("AntiAliasing",
                "Anti-Aliasing", "Appearance",
                "Set to enable or disable anti-aliasing."));

            items.Add(new DesignerActionPropertyItem("ShowAverageLine",
                "Average Line", "Appearance",
                "Set to show the average line."));

            items.Add(new DesignerActionPropertyItem("ShowHorizontalGridLines",
                " Horizontal Grid Lines", "Appearance",
                "Set to show horizontal grid lines."));

            items.Add(new DesignerActionPropertyItem("ShowVerticalGridLines",
                "Vertical Grid Lines", "Appearance",
                "Set to show vertical grid lines."));

            items.Add(new DesignerActionPropertyItem("BorderStyle",
                "Border Style", "Appearance",
                "Sets the border style."));

            items.Add(new DesignerActionPropertyItem("ScaleMode",
                "Scale Mode", "Appearance",
                "Sets the scale mode."));

            items.Add(new DesignerActionPropertyItem("TimerMode",
                "Timer Mode", "Appearance",
                "Sets the timer mode."));

            items.Add(new DesignerActionPropertyItem("TimerInterval",
                "Timer Interval", "Appearance",
                "Sets the speed of the transition."));

            items.Add(new DesignerActionPropertyItem("ChartStyleBackColorBottom",
                "Bottom Color", "Appearance",
                "Sets the background bottom color."));

            items.Add(new DesignerActionPropertyItem("ChartStyleBackColorTop",
                "Top Color", "Appearance",
                "Sets the background top color."));

            items.Add(new DesignerActionPropertyItem("Values",
                "Values", "Appearance",
                "Sets the values to use for the chart."));



            items.Add(new DesignerActionHeaderItem("MainChart"));


            items.Add(new DesignerActionPropertyItem("AvgLinePenColor",
                "Average Line Color", "MainChart",
                "Sets the average line color."));

            items.Add(new DesignerActionPropertyItem("AvgLineDashStyle",
                "Average Line DashStyle", "MainChart",
                "Sets the average line dash style."));

            items.Add(new DesignerActionPropertyItem("AvgLinePenWidth",
                "Average Line Width", "MainChart",
                "Sets the average line width."));


            items.Add(new DesignerActionHeaderItem("ChartLine"));


            items.Add(new DesignerActionPropertyItem("ChartLinePenColor",
                "Chart Line Color", "ChartLine",
                "Sets the chart line color."));

            items.Add(new DesignerActionPropertyItem("ChartLineDashStyle",
                "Chart Line DashStyle", "ChartLine",
                "Sets the chart line dash style."));

            items.Add(new DesignerActionPropertyItem("ChartLinePenWidth",
                "Chart Line Width", "ChartLine",
                "Sets the chart line width."));


            items.Add(new DesignerActionHeaderItem("HorizontalGrid"));


            items.Add(new DesignerActionPropertyItem("HorizGridPenColor",
                "Horizontal Grid Color", "HorizontalGrid",
                "Sets the horizontal grid color."));

            items.Add(new DesignerActionPropertyItem("HorizGridPenDashStyle",
                "Horizontal Grid Dash", "HorizontalGrid",
                "Sets the horizontal grid dash style."));

            items.Add(new DesignerActionPropertyItem("HorizGridPenWidth",
                "Horizontal Grid Width", "HorizontalGrid",
                "Sets the horizontal grid width."));


            items.Add(new DesignerActionHeaderItem("VerticalGrid"));


            items.Add(new DesignerActionPropertyItem("VertGridPenColor",
                "Vertical Grid Color", "VerticalGrid",
                "Sets the vertical grid color."));

            items.Add(new DesignerActionPropertyItem("VertGridPenDashStyle",
                "Vertical Grid Dash", "VerticalGrid",
                "Sets the vertical grid dash style."));

            items.Add(new DesignerActionPropertyItem("VertGridPenWidth",
                "Vertical Grid Width", "VerticalGrid",
                "Sets the vertical grid width."));


            //Create entries for static Information section.
            StringBuilder location = new StringBuilder("Product: ");
            location.Append(colUserControl.ProductName);
            StringBuilder size = new StringBuilder("Version: ");
            size.Append(colUserControl.ProductVersion);
            items.Add(new DesignerActionTextItem(location.ToString(),
                             "Information"));
            items.Add(new DesignerActionTextItem(size.ToString(),
                             "Information"));

            return items;
        }

        #endregion




    }

    #endregion

    #endregion

}
