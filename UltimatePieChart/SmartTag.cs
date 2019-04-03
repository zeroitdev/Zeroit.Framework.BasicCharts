// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;

namespace Zeroit.Framework.BasicCharts
{

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitUltimatePieChartDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitUltimatePieChartDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitUltimatePieChartDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitUltimatePieChartSmartTagActionList(this.Component));
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
    /// Class ZeroitUltimatePieChartSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    internal class ZeroitUltimatePieChartSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitUltimatePieChart colUserControl;


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
        /// Initializes a new instance of the <see cref="ZeroitUltimatePieChartSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitUltimatePieChartSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitUltimatePieChart;

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
        /// Gets or sets a value indicating whether [automatic size pie].
        /// </summary>
        /// <value><c>true</c> if [automatic size pie]; otherwise, <c>false</c>.</value>
        public bool AutoSizePie
        {
            get
            {
                return colUserControl.AutoSizePie;
            }
            set
            {
                GetPropertyByName("AutoSizePie").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show edges].
        /// </summary>
        /// <value><c>true</c> if [show edges]; otherwise, <c>false</c>.</value>
        public bool ShowEdges
        {
            get
            {
                return colUserControl.ShowEdges;
            }
            set
            {
                GetPropertyByName("ShowEdges").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show tool tips].
        /// </summary>
        /// <value><c>true</c> if [show tool tips]; otherwise, <c>false</c>.</value>
        public bool ShowToolTips
        {
            get
            {
                return colUserControl.ShowToolTips;
            }
            set
            {
                GetPropertyByName("ShowToolTips").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public ZeroitUltimatePieChart.ItemCollection Items
        {
            get
            {
                return colUserControl.Items;
            }
            
        }

        /// <summary>
        /// Gets or sets the focused item style.
        /// </summary>
        /// <value>The focused item style.</value>
        public ZeroitUltimatePieChart.PieChartItemStyle FocusedItemStyle
        {
            get
            {
                return colUserControl.FocusedItemStyle;
            }
            set
            {
                GetPropertyByName("FocusedItemStyle").SetValue(colUserControl, value);
            }
        }


        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>The rotation.</value>
        public float Rotation
        {
            get
            {
                return colUserControl.Rotation;
            }
            set
            {
                GetPropertyByName("Rotation").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the inclination.
        /// </summary>
        /// <value>The inclination.</value>
        public float Inclination
        {
            get
            {
                return colUserControl.Inclination;
            }
            set
            {
                GetPropertyByName("Inclination").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        /// <value>The thickness.</value>
        public float Thickness
        {
            get
            {
                return colUserControl.Thickness;
            }
            set
            {
                GetPropertyByName("Thickness").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>The radius.</value>
        public float Radius
        {
            get
            {
                return colUserControl.Radius;
            }
            set
            {
                GetPropertyByName("Radius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the text display mode.
        /// </summary>
        /// <value>The text display mode.</value>
        public ZeroitUltimatePieChart.TextDisplayTypes TextDisplayMode
        {
            get
            {
                return colUserControl.TextDisplayMode;
            }
            set
            {
                GetPropertyByName("TextDisplayMode").SetValue(colUserControl, value);
            }
        }


        /// <summary>
        /// Gets or sets the edge brightness factor.
        /// </summary>
        /// <value>The edge brightness factor.</value>
        public float EdgeBrightnessFactor
        {
            get
            {
                return colUserControl.EdgeBrightnessFactor;
            }
            set
            {
                GetPropertyByName("EdgeBrightnessFactor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the surface alpha transparency.
        /// </summary>
        /// <value>The surface alpha transparency.</value>
        public float SurfaceAlphaTransparency
        {
            get
            {
                return colUserControl.SurfaceAlphaTransparency;
            }
            set
            {
                GetPropertyByName("SurfaceAlphaTransparency").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the surface brightness factor.
        /// </summary>
        /// <value>The surface brightness factor.</value>
        public float SurfaceBrightnessFactor
        {
            get
            {
                return colUserControl.SurfaceBrightnessFactor;
            }
            set
            {
                GetPropertyByName("SurfaceBrightnessFactor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic animate].
        /// </summary>
        /// <value><c>true</c> if [automatic animate]; otherwise, <c>false</c>.</value>
        public bool AutoAnimate
        {
            get
            {
                return colUserControl.AutoAnimate;
            }
            set
            {
                GetPropertyByName("AutoAnimate").SetValue(colUserControl, value);
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
                GetPropertyByName("TimerInterval").SetValue(colUserControl, value);
            }
        }

        #endregion

        #region DesignerActionItemCollection

        #region Template Methods
        //internal void RefreshComponent()
        //{
        //    if (DesignerActionUIService != null)
        //        DesignerActionUIService.Refresh(colUserControl);
        //}


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
            items.Add(new DesignerActionHeaderItem("Behaviour"));

            items.Add(new DesignerActionPropertyItem("AutoAnimate",
                "Auto Animate", "Behaviour",
                "Set to automatically animate the control."));

            items.Add(new DesignerActionPropertyItem("AutoSizePie",
                "Auto-Size Pie", "Behaviour",
                "Set to automatically size the pie."));

            items.Add(new DesignerActionPropertyItem("ShowEdges",
                "Show Edges", "Behaviour",
                "Set to show edges."));

            items.Add(new DesignerActionPropertyItem("ShowToolTips",
                "Show ToolTips", "Behaviour",
                "Set to show display names."));

            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("BackColor",
                "Back Color", "Appearance",
                "Sets the background color."));

            items.Add(new DesignerActionPropertyItem("Items",
                "Items", "Appearance",
                "Sets the values."));

            items.Add(new DesignerActionPropertyItem("TextDisplayMode",
                "Text Display Mode", "Appearance",
                "Sets the text should be displayed."));

            
            items.Add(new DesignerActionPropertyItem("Rotation",
                "Rotation", "Appearance",
                "Sets the Rotation."));

            items.Add(new DesignerActionPropertyItem("Inclination",
                "Inclination", "Appearance",
                "Sets the inclination."));

            items.Add(new DesignerActionPropertyItem("Thickness",
                "Thickness", "Appearance",
                "Sets the thickness."));

            items.Add(new DesignerActionPropertyItem("Radius",
                "Radius", "Appearance",
                "Sets the radius."));


            items.Add(new DesignerActionPropertyItem("EdgeBrightnessFactor",
                "Edge Brightness Factor", "Appearance",
                "Sets the edge brightness."));

            items.Add(new DesignerActionPropertyItem("SurfaceAlphaTransparency",
                "Surface Alpha Transparency", "Appearance",
                "Sets the surface transparency."));

            items.Add(new DesignerActionPropertyItem("SurfaceBrightnessFactor",
                "Surface Brightness Factor", "Appearance",
                "Sets the surface brightness."));

            items.Add(new DesignerActionPropertyItem("TimerInterval",
                "Animation Speed", "Appearance",
                "Sets the animation speed."));



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
