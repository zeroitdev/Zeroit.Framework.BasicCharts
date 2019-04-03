// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-04-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="MetroWebChartPointCollectionEventArgs.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
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