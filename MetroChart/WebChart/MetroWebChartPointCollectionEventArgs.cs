// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-04-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="MetroWebChartPointCollectionEventArgs.cs" company="Zeroit Dev Technologies">
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

namespace Zeroit.Framework.BasicCharts.Metro
{
    /// <summary>
    /// Class ZeroitMetroWebChartPointCollectionEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ZeroitMetroWebChartPointCollectionEventArgs : EventArgs
	{
        /// <summary>
        /// The item
        /// </summary>
        private ZeroitMetroWebChartPoint _item;

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>The item.</value>
        public ZeroitMetroWebChartPoint Item
		{
			get
			{
				return this._item;
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMetroWebChartPointCollectionEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public ZeroitMetroWebChartPointCollectionEventArgs(ZeroitMetroWebChartPoint item)
		{
			this._item = item;
		}
	}
}