// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-02-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="SmartTag.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;

namespace Zeroit.Framework.BasicCharts
{

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitSplineGraphDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitSplineGraphDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitSplineGraphDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitSplineGraphSmartTagActionList(this.Component));
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
    /// Class ZeroitSplineGraphSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    internal class ZeroitSplineGraphSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitSplineGraph colUserControl;


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
        /// Initializes a new instance of the <see cref="ZeroitSplineGraphSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitSplineGraphSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitSplineGraph;

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

        //public Color BackGroundColor
        //{
        //    get
        //    {
        //        return colUserControl.BackGroundColor;
        //    }
        //    set
        //    {
        //        GetPropertyByName("BackGroundColor").SetValue(colUserControl, value);
        //    }
        //}

        //public Color BelowLineColor
        //{
        //    get
        //    {
        //        return colUserControl.BelowLineColor;
        //    }
        //    set
        //    {
        //        colUserControl.BelowLineColor = value;

        //    }
        //}

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor
        {
            get
            {
                return colUserControl.BorderColor;
            }
            set
            {
                GetPropertyByName("BorderColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the graph style.
        /// </summary>
        /// <value>The graph style.</value>
        public ZeroitSplineGraph.Style GraphStyle
        {
            get
            {
                return colUserControl.GraphStyle;
            }
            set
            {
                GetPropertyByName("GraphStyle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the graph title.
        /// </summary>
        /// <value>The graph title.</value>
        public string GraphTitle
        {
            get
            {
                return colUserControl.GraphTitle;
            }
            set
            {
                GetPropertyByName("GraphTitle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the graph title.
        /// </summary>
        /// <value>The color of the graph title.</value>
        public Color GraphTitleColor
        {
            get
            {
                return colUserControl.GraphTitleColor;
            }
            set
            {
                GetPropertyByName("GraphTitleColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public List<decimal> Items
        {
            get
            {
                return colUserControl.Items;
            }
            set
            {
                GetPropertyByName("Items").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        public Color LineColor
        {
            get
            {
                return colUserControl.LineColor;
            }
            set
            {
                GetPropertyByName("LineColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the point.
        /// </summary>
        /// <value>The size of the point.</value>
        public int PointSize
        {
            get
            {
                return colUserControl.PointSize;
            }
            set
            {
                GetPropertyByName("PointSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show border].
        /// </summary>
        /// <value><c>true</c> if [show border]; otherwise, <c>false</c>.</value>
        public bool ShowBorder
        {
            get
            {
                return colUserControl.ShowBorder;
            }
            set
            {
                GetPropertyByName("ShowBorder").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show points].
        /// </summary>
        /// <value><c>true</c> if [show points]; otherwise, <c>false</c>.</value>
        public bool ShowPoints
        {
            get
            {
                return colUserControl.ShowPoints;
            }
            set
            {
                GetPropertyByName("ShowPoints").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show title].
        /// </summary>
        /// <value><c>true</c> if [show title]; otherwise, <c>false</c>.</value>
        public bool ShowTitle
        {
            get
            {
                return colUserControl.ShowTitle;
            }
            set
            {
                GetPropertyByName("ShowTitle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show vertical lines].
        /// </summary>
        /// <value><c>true</c> if [show vertical lines]; otherwise, <c>false</c>.</value>
        public bool ShowVerticalLines
        {
            get
            {
                return colUserControl.ShowVerticalLines;
            }
            set
            {
                GetPropertyByName("ShowVerticalLines").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the title alignment.
        /// </summary>
        /// <value>The title alignment.</value>
        public StringAlignment TitleAlignment
        {
            get
            {
                return colUserControl.TitleAlignment;
            }
            set
            {
                GetPropertyByName("TitleAlignment").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the vertical line.
        /// </summary>
        /// <value>The color of the vertical line.</value>
        public Color VerticalLineColor
        {
            get
            {
                return colUserControl.VerticalLineColor;
            }
            set
            {
                GetPropertyByName("VerticalLineColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the material colors.
        /// </summary>
        /// <value>The material colors.</value>
        public Color[] MaterialColors
        {
            get
            {
                return colUserControl.Material.FillColor;
            }
            set
            {
                colUserControl.Material.FillColor = value;

            }
        }

        /// <summary>
        /// Gets or sets the material color angle.
        /// </summary>
        /// <value>The material color angle.</value>
        public float MaterialColorAngle
        {
            get
            {
                return colUserControl.Material.GradientAngle;
            }
            set
            {
                colUserControl.Material.GradientAngle = value;

            }
        }

        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        /// <value>The colors.</value>
        public List<Color> Colors
        {
            get
            {
                return colUserControl.Colors;
            }
            set
            {
                GetPropertyByName("Colors").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the seed colors.
        /// </summary>
        /// <value>The seed colors.</value>
        public ZeroitSplineGraph.SeedColor SeedColors
        {
            get
            {
                return colUserControl.SeedColors;
            }
            set
            {
                GetPropertyByName("SeedColors").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [randomize colors].
        /// </summary>
        /// <value><c>true</c> if [randomize colors]; otherwise, <c>false</c>.</value>
        public bool RandomizeColors
        {
            get
            {
                return colUserControl.RandomizeColors;
            }
            set
            {
                GetPropertyByName("RandomizeColors").SetValue(colUserControl, value);
            }
        }

        #region Performance Chart

        /// <summary>
        /// Gets or sets the timer interval.
        /// </summary>
        /// <value>The timer interval.</value>
        public int TimerInterval
        {
            get { return colUserControl.TimerInterval; }
            set { colUserControl.TimerInterval = value; colUserControl.Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the timer mode.
        /// </summary>
        /// <value>The timer mode.</value>
        public TimerMode TimerMode
        {
            get { return colUserControl.TimerMode; }
            set { colUserControl.TimerMode = value; colUserControl.Invalidate(); }
        }

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
        /// Gets or sets the border style.
        /// </summary>
        /// <value>The border style.</value>
        public ZeroitSplineGraph.Border3DStyle BorderStyle
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

        /// <summary>
        /// Shows the title text.
        /// </summary>
        protected virtual void ShowTitleText()
        {
            colUserControl.ShowTitle = !colUserControl.ShowTitle;
            colUserControl.Invalidate();
            RefreshComponent();
        }


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



            items.Add(new DesignerActionHeaderItem("BackChart"));


            items.Add(new DesignerActionPropertyItem("ShowHorizontalGridLines",
                "Horizontal Grid", "BackChart",
                "Set to show the horizontal grid."));

            items.Add(new DesignerActionPropertyItem("ShowVerticalGridLines",
                "Vertical Grid", "BackChart",
                "Set to show the vertical grid."));


            items.Add(new DesignerActionPropertyItem("ChartStyleBackColorBottom",
                "Bottom Back Color", "BackChart",
                "Sets the bottom back color."));

            items.Add(new DesignerActionPropertyItem("ChartStyleBackColorTop",
                "Top Back Color", "BackChart",
                "Sets the top back color."));


            items.Add(new DesignerActionPropertyItem("HorizGridPenColor",
                "Horizontal Grid Color", "BackChart",
                "Sets the horizontal grid color."));

            items.Add(new DesignerActionPropertyItem("VertGridPenColor",
                "Vertical Grid Color", "BackChart",
                "Sets the vertical grid color."));

            items.Add(new DesignerActionPropertyItem("BorderStyle",
                "Border Style", "BackChart",
                "Sets the border style."));

            items.Add(new DesignerActionPropertyItem("TimerMode",
                "Timer Mode", "BackChart",
                "Sets the timer to use."));

            items.Add(new DesignerActionPropertyItem("TimerInterval",
                "Timer Interval", "BackChart",
                "Sets the speed of the animation."));




            items.Add(new DesignerActionHeaderItem("Appearance"));


            items.Add(new DesignerActionPropertyItem("ShowBorder",
                "Show Border", "Appearance",
                "Set to show the border."));

            items.Add(new DesignerActionPropertyItem("ShowPoints",
                "Show Points", "Appearance",
                "Set to show the points."));

            items.Add(new DesignerActionPropertyItem("RandomizeColors",
                "Randomize Colors", "Appearance",
                "Set to randomly pick colors from the color list."));

            if (!colUserControl.ShowTitle)
            {
                items.Add(new DesignerActionMethodItem(this, "ShowTitleText", "Show Title", "Appearance"));

            }
            else
            {
                items.Add(new DesignerActionMethodItem(this, "ShowTitleText", "Hide Title", "Appearance"));

            }
            
            items.Add(new DesignerActionPropertyItem("ShowVerticalLines",
                "Vertical Lines", "Appearance",
                "Set to show the vertical lines."));


            items.Add(new DesignerActionPropertyItem("GraphStyle",
                "Graph Style", "Appearance",
                "Sets the graph style."));


            items.Add(new DesignerActionPropertyItem("Items",
                "Items", "Appearance",
                "Sets the items."));


            items.Add(new DesignerActionPropertyItem("TitleAlignment",
                "Title Alignment", "Appearance",
                "Sets the title alignment."));


            items.Add(new DesignerActionPropertyItem("BorderColor",
                "Border Color", "Appearance",
                "Sets the border color."));


            //items.Add(new DesignerActionPropertyItem("BelowLineColor",
            //    "Below Line Color", "Appearance",
            //    "Sets the colors below the main line."));


            items.Add(new DesignerActionPropertyItem("LineColor",
                "Line Color", "Appearance",
                "Sets the line color."));


            items.Add(new DesignerActionPropertyItem("GraphTitleColor",
                "Graph Title Color", "Appearance",
                "Sets the graph title color."));


            items.Add(new DesignerActionPropertyItem("VerticalLineColor",
                "Vertical Line Color", "Appearance",
                "Sets the vertical line colors."));

            items.Add(new DesignerActionPropertyItem("MaterialColors",
                "Material Colors", "Appearance",
                "Sets the material colors."));



            items.Add(new DesignerActionPropertyItem("PointSize",
                "Point Size", "Appearance",
                "Sets the point size."));


            items.Add(new DesignerActionPropertyItem("MaterialColorAngle",
                "Material Color Angle", "Appearance",
                "Sets the Material colors gradient angle."));
            
            //items.Add(new DesignerActionPropertyItem("GraphTitle",
            //    "Graph Title", "Appearance",
            //    "Sets the Graph Title."));



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