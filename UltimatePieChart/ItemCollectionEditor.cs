// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="ItemCollectionEditor.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts
{
    #region Pie Chart

    #region PieChartControl.ItemCollectionEditor
    /// <summary>
    /// Class ZeroitUltimatePieChart.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    public partial class ZeroitUltimatePieChart : Control
    {
        /// <summary>
        /// Class used to edit an ItemCollection in design time.
        /// </summary>
        /// <seealso cref="System.ComponentModel.Design.CollectionEditor" />
        /// <remarks>The designer uses the default CollectionEditor implementation, and overrides
        /// the display text for items in the item list.</remarks>
        internal class ItemCollectionEditor : CollectionEditor
        {
            #region Constructor
            /// <summary>
            /// Initializes a new instance of the <see cref="ItemCollectionEditor"/> class.
            /// </summary>
            /// <param name="type">The type of the collection for this editor to edit.</param>
            public ItemCollectionEditor(Type type)
              : base(type)
            {
            }
            #endregion

            #region Overrides
            /// <summary>
            /// Gets the display text for a PieChartItem in the collection list.
            /// </summary>
            /// <param name="value">The PieChartItem to get the display text for.</param>
            /// <returns>The string that will be displayed for the PieChartItem.</returns>
            protected override string GetDisplayText(object value)
            {
                if (value is PieChartItem)
                {
                    PieChartItem item = (PieChartItem)value;
                    if (!string.IsNullOrEmpty(item.Text))
                        return string.Format("{0} [weight {1:f3}]", item.Text, item.Weight);
                    else
                        return string.Format("[weight {0:f3}]", item.Weight);
                }
                return value.GetType().Name;
            }
            #endregion
        }
    }
    #endregion
    
    #endregion
}
