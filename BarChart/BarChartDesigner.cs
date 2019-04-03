// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="BarChartDesigner.cs" company="Zeroit Dev Technologies">
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
using System.Drawing.Drawing2D;
using System.Text;

namespace Zeroit.Framework.BasicCharts
{


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitBarChartDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitBarChartDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitBarChartDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitBarChartSmartTagActionList(this.Component));
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
            Properties.Remove("ForeColor");
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
    /// Class ZeroitBarChartSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    internal class ZeroitBarChartSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitBarChart colUserControl;


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
        /// Initializes a new instance of the <see cref="ZeroitBarChartSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitBarChartSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitBarChart;

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
        /// Gets or sets the hatch brush.
        /// </summary>
        /// <value>The hatch brush.</value>
        public Color[] HatchBrush
        {
            get
            {
                return colUserControl.HatchBrush;
            }
            set
            {
                GetPropertyByName("HatchBrush").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the filled gradient.
        /// </summary>
        /// <value>The filled gradient.</value>
        public Color[] FilledGradient
        {
            get
            {
                return colUserControl.FilledGradient;
            }
            set
            {
                GetPropertyByName("FilledGradient").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the filled.
        /// </summary>
        /// <value>The color of the filled.</value>
        public Color FilledColor
        {
            get
            {
                return colUserControl.FilledColor;
            }
            set
            {
                GetPropertyByName("FilledColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the splitter.
        /// </summary>
        /// <value>The color of the splitter.</value>
        public Color SplitterColor
        {
            get
            {
                return colUserControl.SplitterColor;
            }
            set
            {
                GetPropertyByName("SplitterColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get
            {
                return colUserControl.TextColor;
            }
            set
            {
                GetPropertyByName("TextColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the unfilled.
        /// </summary>
        /// <value>The color of the unfilled.</value>
        public Color UnfilledColor
        {
            get
            {
                return colUserControl.UnfilledColor;
            }
            set
            {
                GetPropertyByName("UnfilledColor").SetValue(colUserControl, value);
            }
        }


        /// <summary>
        /// Gets or sets the hatch style.
        /// </summary>
        /// <value>The hatch style.</value>
        public HatchStyle HatchStyle
        {
            get
            {
                return colUserControl.HatchStyle;
            }
            set
            {
                GetPropertyByName("HatchStyle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the gradient mode.
        /// </summary>
        /// <value>The gradient mode.</value>
        public LinearGradientMode GradientMode
        {
            get
            {
                return colUserControl.GradientMode;
            }
            set
            {
                GetPropertyByName("GradientMode").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the draw mode.
        /// </summary>
        /// <value>The draw mode.</value>
        public ZeroitBarChart.DrawType DrawMode
        {
            get
            {
                return colUserControl.DrawMode;
            }
            set
            {
                GetPropertyByName("DrawMode").SetValue(colUserControl, value);
            }

        }

        /// <summary>
        /// Gets or sets the graph orientation.
        /// </summary>
        /// <value>The graph orientation.</value>
        public ZeroitBarChart.Orientation GraphOrientation
        {
            get
            {
                return colUserControl.GraphOrientation;
            }
            set
            {
                GetPropertyByName("GraphOrientation").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the graph style.
        /// </summary>
        /// <value>The graph style.</value>
        public ZeroitBarChart.Style GraphStyle
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
        /// Gets or sets the size of the grid.
        /// </summary>
        /// <value>The size of the grid.</value>
        public float GridSize
        {
            get
            {
                return colUserControl.GridSize;
            }
            set
            {
                GetPropertyByName("GridSize").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public List<int> Items
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
        /// Gets or sets the sorting.
        /// </summary>
        /// <value>The sorting.</value>
        public ZeroitBarChart.SortStyle Sorting
        {
            get
            {
                return colUserControl.Sorting;
            }
            set
            {
                GetPropertyByName("Sorting").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        /// <value>The text alignment.</value>
        public ZeroitBarChart.Aligning TextAlignment
        {
            get
            {
                return colUserControl.TextAlignment;
            }
            set
            {
                GetPropertyByName("TextAlignment").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show grid].
        /// </summary>
        /// <value><c>true</c> if [show grid]; otherwise, <c>false</c>.</value>
        public bool ShowGrid
        {
            get
            {
                return colUserControl.ShowGrid;
            }
            set
            {
                GetPropertyByName("ShowGrid").SetValue(colUserControl, value);
            }
        }

        //Material

        /// <summary>
        /// Gets or sets the color of the material.
        /// </summary>
        /// <value>The color of the material.</value>
        public List<Color> MaterialColor
        {
            get { return colUserControl.MaterialColors.Colors; }
            set { colUserControl.MaterialColors.Colors = value; }
        }

        /// <summary>
        /// Gets or sets the material background.
        /// </summary>
        /// <value>The material background.</value>
        public Color MaterialBackground
        {
            get { return colUserControl.MaterialColors.Background; }
            set { colUserControl.MaterialColors.Background = value; }
        }

        //Bootstrap

        /// <summary>
        /// Gets or sets the bootstrap background.
        /// </summary>
        /// <value>The bootstrap background.</value>
        public Color BootstrapBackground
        {
            get { return colUserControl.BootstrapColors.Background; }
            set
            {
                colUserControl.BootstrapColors.Background = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the bootstrap oriented.
        /// </summary>
        /// <value>The color of the bootstrap oriented.</value>
        public Color BootstrapOrientedColor
        {
            get { return colUserControl.BootstrapColors.OrientBackground; }
            set
            {
                colUserControl.BootstrapColors.OrientBackground = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the bootstrap grid.
        /// </summary>
        /// <value>The color of the bootstrap grid.</value>
        public Color BootstrapGridColor
        {
            get { return colUserControl.BootstrapColors.GridColors; }
            set
            {
                colUserControl.BootstrapColors.GridColors = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the bootstrap text.
        /// </summary>
        /// <value>The color of the bootstrap text.</value>
        public Color BootstrapTextColor
        {
            get { return colUserControl.BootstrapColors.TextColor; }
            set
            {
                colUserControl.BootstrapColors.TextColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the bootstrap border.
        /// </summary>
        /// <value>The width of the bootstrap border.</value>
        public float BootstrapBorderWidth
        {
            get { return colUserControl.BootstrapColors.BorderWidth; }
            set
            {
                colUserControl.BootstrapColors.BorderWidth = value;
            }
        }

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


        #region Template Code
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
        /// Shows the grid lines.
        /// </summary>
        protected virtual void ShowGridLines()
        {
            colUserControl.ShowGrid = !colUserControl.ShowGrid;
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


        #endregion

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            #region Add Private Methods

            #region Template Code
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


            #endregion

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Behaviour"));

            if (!colUserControl.ShowGrid)
                items.Add(new DesignerActionMethodItem(this, "ShowGridLines", "Show Grid", "Behaviour", true));
            else
                items.Add(new DesignerActionMethodItem(this, "ShowGridLines", "Hide Grid", "Behaviour", true));


            items.Add(new DesignerActionHeaderItem("Appearance"));


            items.Add(new DesignerActionPropertyItem("Items",
                "Items", "Appearance",
                "Sets the values to use for the bar chart."));


            items.Add(new DesignerActionPropertyItem("DrawMode",
                "Draw Mode", "Appearance",
                "Sets how the bars should be drawn."));

            items.Add(new DesignerActionPropertyItem("HatchStyle",
                "Hatch Style", "Appearance",
                "Sets the hatch style."));

            items.Add(new DesignerActionPropertyItem("GradientMode",
                "Gradient Mode", "Appearance",
                "Sets the gradient mode."));

            items.Add(new DesignerActionPropertyItem("GraphOrientation",
                "Graph Orientation", "Appearance",
                "Sets the graph orientation."));

            items.Add(new DesignerActionPropertyItem("GraphStyle",
                "Graph Style", "Appearance",
                "Sets the graph style."));

            //items.Add(new DesignerActionPropertyItem("BackColor",
            //    "Back Color", "Appearance",
            //    "Sets the background color."));

            //items.Add(new DesignerActionPropertyItem("ForeColor",
            //    "Fore Color", "Appearance",
            //    "Sets the fore color."));

            items.Add(new DesignerActionPropertyItem("HatchBrush",
                "HatchBrush", "Appearance",
                "Sets the hatch brush color."));

            items.Add(new DesignerActionPropertyItem("FilledGradient",
                "Filled Gradient", "Appearance",
                "Sets the gradient colors to use."));
            
            items.Add(new DesignerActionPropertyItem("FilledColor",
                "Filled Color", "Appearance",
                "Sets the filled color."));

            items.Add(new DesignerActionPropertyItem("SplitterColor",
                "Splitter Color", "Appearance",
                "Sets the splitter color."));

            items.Add(new DesignerActionPropertyItem("UnfilledColor",
                "Unfilled Color", "Appearance",
                "Sets the inactive colors."));

            items.Add(new DesignerActionPropertyItem("TextColor",
                "Text Color", "Appearance",
                "Sets the text color."));
            
            items.Add(new DesignerActionPropertyItem("Sorting",
                "Sorting", "Appearance",
                "Sets how the graph should be sorted."));

            items.Add(new DesignerActionPropertyItem("TextAlignment",
                "Text Alignment", "Appearance",
                "Sets the text alignment."));


            items.Add(new DesignerActionPropertyItem("GridSize",
                "Grid Size", "Appearance",
                "Sets the grid size."));

            items.Add(new DesignerActionHeaderItem("Material"));

            items.Add(new DesignerActionPropertyItem("MaterialBackground",
                "Background", "Material",
                "Sets the material background color."));

            items.Add(new DesignerActionPropertyItem("MaterialColor",
                "Colors", "Material",
                "Sets the material colors."));

            
            items.Add(new DesignerActionHeaderItem("Bootstrap"));

            items.Add(new DesignerActionPropertyItem("BootstrapBackground",
                "Background", "Bootstrap",
                "Sets the material colors."));

            items.Add(new DesignerActionPropertyItem("BootstrapOrientedColor",
                "Background Orientation", "Bootstrap",
                "Sets the background orientation color."));

            items.Add(new DesignerActionPropertyItem("BootstrapTextColor",
                "Text Color", "Bootstrap",
                "Sets the bootstrap text color."));

            items.Add(new DesignerActionPropertyItem("BootstrapGridColor",
                "Grid Color", "Bootstrap",
                "Sets the bootstrap grid color."));

            items.Add(new DesignerActionPropertyItem("BootstrapBorderWidth",
                "Grid Size", "Bootstrap",
                "Sets the bootstrap grid size."));
            

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