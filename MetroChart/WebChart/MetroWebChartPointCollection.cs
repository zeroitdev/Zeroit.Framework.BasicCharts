// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-04-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="MetroWebChartPointCollection.cs" company="Zeroit Dev Technologies">
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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Zeroit.Framework.BasicCharts.Metro
{
    /// <summary>
    /// Class ZeroitMetroWebChartPointCollection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.Collection{Zeroit.Framework.BasicCharts.Metro.ZeroitMetroWebChartPoint}" />
    public class ZeroitMetroWebChartPointCollection : Collection<ZeroitMetroWebChartPoint>
	{

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddItems(ZeroitMetroWebChartPoint[] items)
		{
			int length = checked(checked((int)items.Length) - 1);
			for (int i = 0; i <= length; i = checked(i + 1))
			{
				this.Add(items[i]);
				EventHandler<ZeroitMetroWebChartPointCollectionEventArgs> eventHandler = this.ItemAdded;
				if (eventHandler != null)
				{
					eventHandler(this, new ZeroitMetroWebChartPointCollectionEventArgs(items[i]));
				}
			}
		}

        /// <summary>
        /// Removes all elements from the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
        /// </summary>
        protected override void ClearItems()
		{
			IEnumerator<ZeroitMetroWebChartPoint> enumerator = null;
			using (enumerator)
			{
				enumerator = this.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ZeroitMetroWebChartPoint current = enumerator.Current;
					EventHandler<ZeroitMetroWebChartPointCollectionEventArgs> eventHandler = this.ItemRemoving;
					if (eventHandler != null)
					{
						eventHandler(this, new ZeroitMetroWebChartPointCollectionEventArgs(current));
					}
				}
			}
			base.ClearItems();
		}

        /// <summary>
        /// Inserts an element into the <see cref="T:System.Collections.ObjectModel.Collection`1" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        protected override void InsertItem(int index, ZeroitMetroWebChartPoint item)
		{
			base.InsertItem(index, item);
			EventHandler<ZeroitMetroWebChartPointCollectionEventArgs> eventHandler = this.ItemAdded;
			if (eventHandler != null)
			{
				eventHandler(this, new ZeroitMetroWebChartPointCollectionEventArgs(item));
			}
		}

        /// <summary>
        /// Removes the element at the specified index of the <see cref="T:System.Collections.ObjectModel.Collection`1" />.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        protected override void RemoveItem(int index)
		{
			EventHandler<ZeroitMetroWebChartPointCollectionEventArgs> eventHandler = this.ItemRemoving;
			if (eventHandler != null)
			{
				eventHandler(this, new ZeroitMetroWebChartPointCollectionEventArgs(this[index]));
			}
			base.RemoveItem(index);
		}

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index. The value can be null for reference types.</param>
        protected override void SetItem(int index, ZeroitMetroWebChartPoint item)
		{
			EventHandler<ZeroitMetroWebChartPointCollectionEventArgs> eventHandler = this.ItemRemoving;
			if (eventHandler != null)
			{
				eventHandler(this, new ZeroitMetroWebChartPointCollectionEventArgs(this[index]));
			}
			base.SetItem(index, item);
			eventHandler = this.ItemAdded;
			if (eventHandler != null)
			{
				eventHandler(this, new ZeroitMetroWebChartPointCollectionEventArgs(item));
			}
		}

        /// <summary>
        /// Occurs when [item added].
        /// </summary>
        public event EventHandler<ZeroitMetroWebChartPointCollectionEventArgs> ItemAdded;

        /// <summary>
        /// Occurs when [item removing].
        /// </summary>
        public event EventHandler<ZeroitMetroWebChartPointCollectionEventArgs> ItemRemoving;

	}
}