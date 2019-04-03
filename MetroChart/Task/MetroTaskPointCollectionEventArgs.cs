// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-04-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="MetroTaskPointCollectionEventArgs.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Zeroit.Framework.BasicCharts.Metro
{
    /// <summary>
    /// Class MetroTaskPointCollectionEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class MetroTaskPointCollectionEventArgs : EventArgs
	{
        /// <summary>
        /// The item
        /// </summary>
        private ZeroitMetroTaskPoint _item;

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>The item.</value>
        public ZeroitMetroTaskPoint Item
		{
			get
			{
				return this._item;
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="MetroTaskPointCollectionEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public MetroTaskPointCollectionEventArgs(ZeroitMetroTaskPoint item)
		{
			this._item = item;
		}
	}
}