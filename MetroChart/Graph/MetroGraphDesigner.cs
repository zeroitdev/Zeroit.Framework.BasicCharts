// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-04-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="MetroGraphDesigner.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;

namespace Zeroit.Framework.BasicCharts.Metro
{

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitMetroGraphDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitMetroGraphDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitMetroGraphDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitMetroGraphSmartTagActionList(this.Component));
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
    /// Class ZeroitMetroGraphSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class ZeroitMetroGraphSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitMetroGraph colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMetroGraphSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitMetroGraphSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitMetroGraph;

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
        /// Gets or sets the color of the classic fill.
        /// </summary>
        /// <value>The color of the classic fill.</value>
        public Color ClassicFillColor
        {
            get
            {
                return colUserControl.ClassicFillColor;
            }
            set
            {
                GetPropertyByName("ClassicFillColor").SetValue(colUserControl, value);
            }
        }

        //public Color ClassicLineColor
        //{
        //    get
        //    {
        //        return colUserControl.ClassicLineColor;
        //    }
        //    set
        //    {
        //        GetPropertyByName("ClassicLineColor").SetValue(colUserControl, value);
        //    }
        //}

        //public Color DefaultColor
        //{
        //    get
        //    {
        //        return colUserControl.DefaultColor;
        //    }
        //    set
        //    {
        //        GetPropertyByName("DefaultColor").SetValue(colUserControl, value);
        //    }
        //}

        /// <summary>
        /// Gets or sets the color of the grid.
        /// </summary>
        /// <value>The color of the grid.</value>
        public Color GridColor
        {
            get
            {
                return colUserControl.GridColor;
            }
            set
            {
                GetPropertyByName("GridColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the single line.
        /// </summary>
        /// <value>The color of the single line.</value>
        public Color[] SingleLineColor
        {
            get
            {
                return colUserControl.SingleLineColor;
            }
            set
            {
                GetPropertyByName("SingleLineColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover box border.
        /// </summary>
        /// <value>The color of the hover box border.</value>
        public Color HoverBoxBorderColor
        {
            get
            {
                return colUserControl.HoverBoxBorderColor;
            }
            set
            {
                GetPropertyByName("HoverBoxBorderColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover box.
        /// </summary>
        /// <value>The color of the hover box.</value>
        public Color HoverBoxColor
        {
            get
            {
                return colUserControl.HoverBoxColor;
            }
            set
            {
                GetPropertyByName("HoverBoxColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the hover.
        /// </summary>
        /// <value>The color of the hover.</value>
        public Color HoverColor
        {
            get
            {
                return colUserControl.HoverColor;
            }
            set
            {
                GetPropertyByName("HoverColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [outer border].
        /// </summary>
        /// <value><c>true</c> if [outer border]; otherwise, <c>false</c>.</value>
        public bool OuterBorder
        {
            get
            {
                return colUserControl.OuterBorder;
            }
            set
            {
                GetPropertyByName("OuterBorder").SetValue(colUserControl, value);
            }
        }

        //public int ClassicLineThickness
        //{
        //    get
        //    {
        //        return colUserControl.ClassicLineThickness;
        //    }
        //    set
        //    {
        //        GetPropertyByName("ClassicLineThickness").SetValue(colUserControl, value);
        //    }
        //}

        /// <summary>
        /// Gets or sets the dash style.
        /// </summary>
        /// <value>The dash style.</value>
        public System.Drawing.Drawing2D.DashStyle DashStyle
        {
            get
            {
                return colUserControl.DashStyle;
            }
            set
            {
                GetPropertyByName("DashStyle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw horizontal lines].
        /// </summary>
        /// <value><c>true</c> if [draw horizontal lines]; otherwise, <c>false</c>.</value>
        public bool DrawHorizontalLines
        {
            get
            {
                return colUserControl.DrawHorizontalLines;
            }
            set
            {
                GetPropertyByName("DrawHorizontalLines").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw hover data].
        /// </summary>
        /// <value><c>true</c> if [draw hover data]; otherwise, <c>false</c>.</value>
        public bool DrawHoverData
        {
            get
            {
                return colUserControl.DrawHoverData;
            }
            set
            {
                GetPropertyByName("DrawHoverData").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw hover line].
        /// </summary>
        /// <value><c>true</c> if [draw hover line]; otherwise, <c>false</c>.</value>
        public bool DrawHoverLine
        {
            get
            {
                return colUserControl.DrawHoverLine;
            }
            set
            {
                GetPropertyByName("DrawHoverLine").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draw vertical lines].
        /// </summary>
        /// <value><c>true</c> if [draw vertical lines]; otherwise, <c>false</c>.</value>
        public bool DrawVerticalLines
        {
            get
            {
                return colUserControl.DrawVerticalLines;
            }
            set
            {
                GetPropertyByName("DrawVerticalLines").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public float[] Values
        {
            get
            {
                return colUserControl.Values;
            }
            set
            {
                GetPropertyByName("Values").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [side padding].
        /// </summary>
        /// <value><c>true</c> if [side padding]; otherwise, <c>false</c>.</value>
        public bool SidePadding
        {
            get
            {
                return colUserControl.SidePadding;
            }
            set
            {
                GetPropertyByName("SidePadding").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [single line].
        /// </summary>
        /// <value><c>true</c> if [single line]; otherwise, <c>false</c>.</value>
        public bool SingleLine
        {
            get
            {
                return colUserControl.SingleLine;
            }
            set
            {
                GetPropertyByName("SingleLine").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [single line shadow].
        /// </summary>
        /// <value><c>true</c> if [single line shadow]; otherwise, <c>false</c>.</value>
        public bool SingleLineShadow
        {
            get
            {
                return colUserControl.SingleLineShadow;
            }
            set
            {
                GetPropertyByName("SingleLineShadow").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the single line thickness.
        /// </summary>
        /// <value>The single line thickness.</value>
        public int SingleLineThickness
        {
            get
            {
                return colUserControl.SingleLineThickness;
            }
            set
            {
                GetPropertyByName("SingleLineThickness").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public Design.Style Style
        {
            get
            {
                return colUserControl.Style;
            }
            set
            {
                GetPropertyByName("Style").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use gradient].
        /// </summary>
        /// <value><c>true</c> if [use gradient]; otherwise, <c>false</c>.</value>
        public bool UseGradient
        {
            get
            {
                return colUserControl.UseGradient;
            }
            set
            {
                GetPropertyByName("UseGradient").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the gradient point a.
        /// </summary>
        /// <value>The gradient point a.</value>
        public Point GradientPointA
        {
            get
            {
                return colUserControl.GradientPointA;
            }
            set
            {
                GetPropertyByName("GradientPointA").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the gradient point b.
        /// </summary>
        /// <value>The gradient point b.</value>
        public Point GradientPointB
        {
            get
            {
                return colUserControl.GradientPointB;
            }
            set
            {
                GetPropertyByName("GradientPointB").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the gradient.
        /// </summary>
        /// <value>The color of the gradient.</value>
        public Color GradientColor
        {
            get
            {
                return colUserControl.GradientColor;
            }
            set
            {
                GetPropertyByName("GradientColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [override maximum].
        /// </summary>
        /// <value><c>true</c> if [override maximum]; otherwise, <c>false</c>.</value>
        public bool OverrideMaximum
        {
            get
            {
                return colUserControl.OverrideMaximum;
            }
            set
            {
                GetPropertyByName("OverrideMaximum").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [override minimum].
        /// </summary>
        /// <value><c>true</c> if [override minimum]; otherwise, <c>false</c>.</value>
        public bool OverrideMinimum
        {
            get
            {
                return colUserControl.OverrideMinimum;
            }
            set
            {
                GetPropertyByName("OverrideMinimum").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the overridden maximum.
        /// </summary>
        /// <value>The overridden maximum.</value>
        public int OverriddenMaximum
        {
            get
            {
                return colUserControl.OverriddenMaximum;
            }
            set
            {
                GetPropertyByName("OverriddenMaximum").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the overridden minimum.
        /// </summary>
        /// <value>The overridden minimum.</value>
        public int OverriddenMinimum
        {
            get
            {
                return colUserControl.OverriddenMinimum;
            }
            set
            {
                GetPropertyByName("OverriddenMinimum").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitMetroGraphSmartTagActionList"/> is curved.
        /// </summary>
        /// <value><c>true</c> if curved; otherwise, <c>false</c>.</value>
        public bool Curved
        {
            get
            {
                return colUserControl.Curved;
            }
            set
            {
                GetPropertyByName("Curved").SetValue(colUserControl, value);
            }
        }

        #endregion

        #region DesignerActionItemCollection

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Behaviour"));


            items.Add(new DesignerActionPropertyItem("DrawHorizontalLines",
                "Draw Horizontal Lines", "Behaviour",
                "Set to show the horizontal line."));


            items.Add(new DesignerActionPropertyItem("OuterBorder",
                "Outer Border", "Behaviour",
                "Set to show the outer border."));

            items.Add(new DesignerActionPropertyItem("SidePadding",
                "Show Side Padding", "Behaviour",
                "Set to show side padding."));


            items.Add(new DesignerActionPropertyItem("DrawHoverData",
                "Show Hover Data", "Behaviour",
                "Set to show the data when hovered."));

            items.Add(new DesignerActionPropertyItem("DrawHoverLine",
                "Show Hover Line", "Behaviour",
                "Set to show the hover line."));

            items.Add(new DesignerActionPropertyItem("DrawVerticalLines",
                "Show Vertical Lines", "Behaviour",
                "Set to show the vertical lines."));

            items.Add(new DesignerActionPropertyItem("UseGradient",
                "Use Gradient", "Behaviour",
                "Set to enable the use of gradient colors."));

            items.Add(new DesignerActionPropertyItem("SingleLine",
                "Single Line", "Behaviour",
                "Set to show the single line."));

            items.Add(new DesignerActionPropertyItem("Curved",
                "Curved", "Behaviour",
                "Set to control to be curved."));


            items.Add(new DesignerActionPropertyItem("OverrideMaximum",
                "Override Maximum", "Behaviour",
                "Set to override the maximum value."));

            items.Add(new DesignerActionPropertyItem("OverrideMinimum",
                "Override Minimum", "Behaviour",
                "Set to override the minimum value."));

            items.Add(new DesignerActionHeaderItem("Appearance"));


            items.Add(new DesignerActionPropertyItem("Values",
                "Values", "Appearance",
                "Sets the value."));

            items.Add(new DesignerActionPropertyItem("DashStyle",
                "Dash Style", "Appearance",
                "Sets the dash style of the line."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                "Back Color", "Appearance",
                "Sets the background color."));


            items.Add(new DesignerActionPropertyItem("HoverColor",
                                 "Hover Color", "Appearance",
                                 "Sets the hover color."));

            items.Add(new DesignerActionPropertyItem("HoverBoxBorderColor",
                                 "Hovered Box Border", "Appearance",
                                 "Sets the hover color of the box's border."));

            items.Add(new DesignerActionPropertyItem("HoverBoxColor",
                "Hovered Box", "Appearance",
                "Sets the hover color of the box."));

            items.Add(new DesignerActionPropertyItem("ClassicFillColor",
                                 "Classic Fill Color", "Appearance",
                                 "Sets the fill color of the graph."));


            //items.Add(new DesignerActionPropertyItem("ClassicLineColor",
            //    "Classic Line Color", "Appearance",
            //    "Sets the line's color."));

            items.Add(new DesignerActionPropertyItem("GradientColor",
                "Gradient Color", "Appearance",
                "Sets the gradient color to use."));


            items.Add(new DesignerActionPropertyItem("GridColor",
                "Grid Color", "Appearance",
                "Sets the grid color."));

            
            items.Add(new DesignerActionPropertyItem("BorderColor",
                "Border Color", "Appearance",
                "Sets the border color."));


            items.Add(new DesignerActionPropertyItem("SingleLineColor",
                "Line Color", "Appearance",
                "Sets the line color."));

            //items.Add(new DesignerActionPropertyItem("ClassicLineThickness",
            //    "Classic Line Thickness", "Appearance",
            //    "Sets the classic line thickness."));


            items.Add(new DesignerActionPropertyItem("SingleLineThickness",
                "Line Thickness", "Appearance",
                "Sets the line thickness."));


            items.Add(new DesignerActionPropertyItem("OverriddenMaximum",
                "Overridden Maximum", "Appearance",
                "Override the default maximum value of 100."));

            items.Add(new DesignerActionPropertyItem("OverriddenMinimum",
                "Overridden Minimum", "Appearance",
                "Override the default minimum value of 0."));


            

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