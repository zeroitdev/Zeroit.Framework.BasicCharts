// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="ItemCollectionEditor.cs" company="Zeroit Dev Technologies">
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
