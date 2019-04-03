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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel.Design;
using System.Text;
using System.Collections;

namespace Zeroit.Framework.BasicCharts
{


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(ZeroitSignalDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class ZeroitSignalDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class ZeroitSignalDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new ZeroitSignalSmartTagActionList(this.Component));
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
            Properties.Remove("BackColor");
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
    /// Class ZeroitSignalSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    internal class ZeroitSignalSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag



        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitSignal colUserControl;


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
        /// Initializes a new instance of the <see cref="ZeroitSignalSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public ZeroitSignalSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitSignal;

            //IFormatter formatter = new BinaryFormatter();

            //Stream stream = new FileStream("SignalTag.bin", FileMode.Create, FileAccess.Write, FileShare.None);

            //formatter.Serialize(stream, colUserControl);
            //stream.Close();


            //IFormatter formatterRetrieve = new BinaryFormatter();

            //Stream streamRetrieve = new FileStream("SignalTag.bin", FileMode.Open, FileAccess.Read, FileShare.Read);

            //colUserControl = (ZeroitSignal)formatterRetrieve.Deserialize(streamRetrieve);

            //streamRetrieve.Close();



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
        /// Gets or sets a value indicating whether [show mid grid lines].
        /// </summary>
        /// <value><c>true</c> if [show mid grid lines]; otherwise, <c>false</c>.</value>
        public bool ShowMidGridLines
        {
            get
            {
                return colUserControl.ShowMidGridLines;
            }
            set
            {
                GetPropertyByName("ShowMidGridLines").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show text].
        /// </summary>
        /// <value><c>true</c> if [show text]; otherwise, <c>false</c>.</value>
        public bool ShowText
        {
            get
            {
                return colUserControl.ShowText;
            }
            set
            {
                GetPropertyByName("ShowText").SetValue(colUserControl, value);
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
        /// Gets or sets the multiplier.
        /// </summary>
        /// <value>The multiplier.</value>
        public float Multiplier
        {
            get
            {
                return colUserControl.Multiplier;
            }
            set
            {
                GetPropertyByName("Multiplier").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public List<float> Items
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
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public float Maximum
        {
            get
            {
                return colUserControl.Maximum;
            }
            set
            {
                GetPropertyByName("Maximum").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the periods.
        /// </summary>
        /// <value>The periods.</value>
        public float Periods
        {
            get
            {
                return colUserControl.Periods;
            }
            set
            {
                GetPropertyByName("Periods").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the height of the amplitude.
        /// </summary>
        /// <value>The height of the amplitude.</value>
        public float AmplitudeHeight
        {
            get
            {
                return colUserControl.AmplitudeHeight;
            }
            set
            {
                GetPropertyByName("AmplitudeHeight").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the amplitude tension.
        /// </summary>
        /// <value>The amplitude tension.</value>
        public float AmplitudeTension
        {
            get
            {
                return colUserControl.AmplitudeTension;
            }
            set
            {
                GetPropertyByName("AmplitudeTension").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the grid lines.
        /// </summary>
        /// <value>The grid lines.</value>
        public int GridLines
        {
            get
            {
                return colUserControl.GridLines;
            }
            set
            {
                GetPropertyByName("GridLines").SetValue(colUserControl, value);
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
        public ZeroitSignal.SeedColor SeedColors
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
        /// Gets or sets the type of the signal.
        /// </summary>
        /// <value>The type of the signal.</value>
        public ZeroitSignal.Signal SignalType
        {
            get
            {
                return colUserControl.SignalType;
            }
            set
            {
                GetPropertyByName("SignalType").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the mid grid.
        /// </summary>
        /// <value>The color of the mid grid.</value>
        public Color MidGridColor
        {
            get
            {
                return colUserControl.MidGridColor;
            }
            set
            {
                GetPropertyByName("MidGridColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the mid grid dash style.
        /// </summary>
        /// <value>The mid grid dash style.</value>
        public DashStyle MidGridDashStyle
        {
            get
            {
                return colUserControl.MidGridDashStyle;
            }
            set
            {
                GetPropertyByName("MidGridDashStyle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the mid grid.
        /// </summary>
        /// <value>The width of the mid grid.</value>
        public float MidGridWidth
        {
            get
            {
                return colUserControl.MidGridWidth;
            }
            set
            {
                GetPropertyByName("MidGridWidth").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the signal dash style.
        /// </summary>
        /// <value>The signal dash style.</value>
        public DashStyle SignalDashStyle
        {
            get
            {
                return colUserControl.SignalDashStyle;
            }
            set
            {
                GetPropertyByName("SignalDashStyle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the signal.
        /// </summary>
        /// <value>The width of the signal.</value>
        public float SignalWidth
        {
            get
            {
                return colUserControl.SignalWidth;
            }
            set
            {
                GetPropertyByName("SignalWidth").SetValue(colUserControl, value);
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
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public float BorderWidth
        {
            get
            {
                return colUserControl.BorderWidth;
            }
            set
            {
                GetPropertyByName("BorderWidth").SetValue(colUserControl, value);
            }
        }



        #region Performance Chart

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
                GetPropertyByName("TimerMode").SetValue(colUserControl, value);
            }
        }


        /// <summary>
        /// Gets or sets the color of the horiz grid pen.
        /// </summary>
        /// <value>The color of the horiz grid pen.</value>
        public Color HorizGridPenColor
        {
            get
            {
                return colUserControl.HorizGridPenColor;
            }
            set
            {
                GetPropertyByName("HorizGridPenColor").SetValue(colUserControl, value);
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
                return colUserControl.VertGridPenColor;
            }
            set
            {
                GetPropertyByName("VertGridPenColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the border style.
        /// </summary>
        /// <value>The border style.</value>
        public ZeroitSignal.Border3DStyle BorderStyle
        {
            get
            {
                return colUserControl.BorderStyle;
            }
            set
            {
                GetPropertyByName("BorderStyle").SetValue(colUserControl, value);
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
                return colUserControl.ChartStyleBackColorBottom;
            }
            set
            {
                GetPropertyByName("ChartStyleBackColorBottom").SetValue(colUserControl, value);
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
                return colUserControl.ChartStyleBackColorTop;
            }
            set
            {
                GetPropertyByName("ChartStyleBackColorTop").SetValue(colUserControl, value);
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
                return colUserControl.ShowHorizontalGridLines;
            }
            set
            {
                GetPropertyByName("ShowHorizontalGridLines").SetValue(colUserControl, value);
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
                return colUserControl.ShowVerticalGridLines;
            }
            set
            {
                GetPropertyByName("ShowVerticalGridLines").SetValue(colUserControl, value);
            }
        }

        #endregion

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
        //    colUserControl.
        //    RefreshComponent();
        //}


        //protected virtual void AddButton()
        //{

        //    var item = "Added";
        //    colUserControl.Items.Add(item);
        //    colUserControl.
        //    RefreshComponent();
        //}

        //protected virtual void ClearButtons()
        //{
        //    colUserControl.Items.Clear();
        //    colUserControl.
        //    RefreshComponent();
        //}

        //protected virtual void DeleteItem()
        //{
        //    colUserControl.Items.Remove("Added");
        //    colUserControl.
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


            items.Add(new DesignerActionPropertyItem("ShowMidGridLines",
                "Mid-Grid Lines", "Appearance",
                "Set to show the mid-grid lines."));

            items.Add(new DesignerActionPropertyItem("ShowText",
                "Show Text", "Appearance",
                "Set to show the text."));

            items.Add(new DesignerActionPropertyItem("ShowBorder",
                "Show Border", "Appearance",
                "Set to show the border."));


            items.Add(new DesignerActionPropertyItem("SignalType",
                "Signal Type", "Appearance",
                "Sets the signal type."));


            items.Add(new DesignerActionPropertyItem("TimerMode",
                "Timer Mode", "Appearance",
                "Sets the timer mode."));

            items.Add(new DesignerActionPropertyItem("MidGridDashStyle",
                "Mid-Grid DashStyle", "Appearance",
                "Sets the mid-grid dash style."));
            
            items.Add(new DesignerActionPropertyItem("SignalDashStyle",
                "Signal DashStyle", "Appearance",
                "Sets the signal dash style."));

            items.Add(new DesignerActionPropertyItem("SeedColors",
                "Seed Colors", "Appearance",
                "Sets how the color should be mixed."));

            
            items.Add(new DesignerActionPropertyItem("Colors",
                "Colors", "Appearance",
                "Sets the list of colors to use for the signal."));


            items.Add(new DesignerActionPropertyItem("BorderColor",
                "Border Color", "Appearance",
                "Sets the border color."));

            items.Add(new DesignerActionPropertyItem("MidGridColor",
                "Mid-Grid Color", "Appearance",
                "Sets the mid-grid color."));

            items.Add(new DesignerActionPropertyItem("Items",
                "Items", "Appearance",
                "Sets the values."));

            items.Add(new DesignerActionPropertyItem("Periods",
                "Divisions", "Appearance",
                "Sets the number of divisions."));

            items.Add(new DesignerActionPropertyItem("MidGridWidth",
                "Mid-Grid Width", "Appearance",
                "Sets the mid-grid width."));

            items.Add(new DesignerActionPropertyItem("AmplitudeHeight",
                "Amplitude Height", "Appearance",
                "Sets the amplitude height."));

            items.Add(new DesignerActionPropertyItem("AmplitudeTension",
                "Amplitude Tension", "Appearance",
                "Sets the amplitude height."));


            items.Add(new DesignerActionPropertyItem("GridLines",
                "Grid Lines", "Appearance",
                "Sets the number of mid-grid lines."));


            items.Add(new DesignerActionPropertyItem("Multiplier",
                "Multiplier", "Appearance",
                "Set to render effects on the tension."));
            
            items.Add(new DesignerActionPropertyItem("SignalWidth",
                "Signal Width", "Appearance",
                "Sets the signal width."));

            items.Add(new DesignerActionPropertyItem("BorderWidth",
                "Border Width", "Appearance",
                "Sets the border width."));


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
