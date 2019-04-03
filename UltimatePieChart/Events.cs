// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Events.cs" company="Zeroit Dev Technologies">
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
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts
{
    #region Pie Chart

    #region PieChartControl.Events
    /// <summary>
    /// Delegate PieChartItemEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ZeroitUltimatePieChart.PieChartItemEventArgs"/> instance containing the event data.</param>
    public delegate void PieChartItemEventHandler(object sender, ZeroitUltimatePieChart.PieChartItemEventArgs e);
    /// <summary>
    /// Delegate PieChartItemFocusEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ZeroitUltimatePieChart.PieChartItemFocusEventArgs"/> instance containing the event data.</param>
    public delegate void PieChartItemFocusEventHandler(object sender, ZeroitUltimatePieChart.PieChartItemFocusEventArgs e);

    /// <summary>
    /// Class ZeroitUltimatePieChart.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    public partial class ZeroitUltimatePieChart : Control
    {
        #region Events
        /// <summary>
        /// Fired when an item is clicked.
        /// </summary>
        public event PieChartItemEventHandler ItemClicked;

        /// <summary>
        /// Fired when an item is double-clicked.
        /// </summary>
        public event PieChartItemEventHandler ItemDoubleClicked;

        /// <summary>
        /// Fired when the focus is changing from one item to another.
        /// </summary>
        public event PieChartItemFocusEventHandler ItemFocusChanging;

        /// <summary>
        /// Fired when the focus has changed to another item.
        /// </summary>
        public event EventHandler ItemFocusChanged;

        /// <summary>
        /// Fired when the AutoSizePie property has changed.
        /// </summary>
        public event EventHandler AutoSizePieChanged;

        /// <summary>
        /// Fired when the radius of the control has changed.
        /// </summary>
        public event EventHandler RadiusChanged;

        /// <summary>
        /// Fired when the inclination of the control has changed.
        /// </summary>
        public event EventHandler InclinationChanged;

        /// <summary>
        /// Fired when the rotation has changed.
        /// </summary>
        public event EventHandler RotationChanged;

        /// <summary>
        /// Fired when the thickness of the control has changed.
        /// </summary>
        public event EventHandler ThicknessChanged;

        /// <summary>
        /// Fired when the ShowEdges property has changed.
        /// </summary>
        public event EventHandler ShowEdgesChanged;

        /// <summary>
        /// Fired when the TextDisplayMode property has changed.
        /// </summary>
        public event EventHandler TextDisplayModeChanged;

        /// <summary>
        /// Fired when the ShowToolTips property has changed.
        /// </summary>
        public event EventHandler ShowToolTipsChanged;

        /// <summary>
        /// Called to fire an ItemClicked event.
        /// </summary>
        /// <param name="item">The event arguments.</param>
        private void FireItemClicked(PieChartItem item)
        {
            if (ItemClicked != null)
                ItemClicked(this, new PieChartItemEventArgs(item));
        }

        /// <summary>
        /// Called to fire an ItemDoubleClicked event.
        /// </summary>
        /// <param name="item">The event arguments.</param>
        private void FireItemDoubleClicked(PieChartItem item)
        {
            if (ItemDoubleClicked != null)
                ItemDoubleClicked(this, new PieChartItemEventArgs(item));
        }

        /// <summary>
        /// Called to fire an ItemFocusChanging event.
        /// </summary>
        /// <param name="oldItem">The item that was previously focused.</param>
        /// <param name="newItem">The item that is gaining focus.</param>
        private void FireItemFocusChanging(PieChartItem oldItem, PieChartItem newItem)
        {
            if (ItemFocusChanging != null)
                ItemFocusChanging(this, new PieChartItemFocusEventArgs(oldItem, newItem));
        }

        /// <summary>
        /// Called to fire an ItemFocusChanged event.
        /// </summary>
        private void FireItemFocusChanged()
        {
            if (ItemFocusChanged != null)
                ItemFocusChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called to fire an AutoSizePieChanged event.
        /// </summary>
        internal void FireAutoSizePieChanged()
        {
            if (AutoSizePieChanged != null)
                AutoSizePieChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called to fire an RadiusChanged event.
        /// </summary>
        internal void FireRadiusChanged()
        {
            if (RadiusChanged != null)
                RadiusChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called to fire an InclinationChanged event.
        /// </summary>
        internal void FireInclinationChanged()
        {
            if (InclinationChanged != null)
                InclinationChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called to fire an RotationChanged event.
        /// </summary>
        internal void FireRotationChanged()
        {
            if (RotationChanged != null)
                RotationChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called to fire an ThicknessChanged event.
        /// </summary>
        internal void FireThicknessChanged()
        {
            if (ThicknessChanged != null)
                ThicknessChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called to fire an ShowEdgesChanged event.
        /// </summary>
        internal void FireShowEdgesChanged()
        {
            if (ShowEdgesChanged != null)
                ShowEdgesChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called to fire an TextDisplayModeChanged event.
        /// </summary>
        internal void FireTextDisplayModeChanged()
        {
            if (TextDisplayModeChanged != null)
                TextDisplayModeChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called to fire an ShowToolTipsChanged event.
        /// </summary>
        internal void FireShowToolTipsChanged()
        {
            if (ShowToolTipsChanged != null)
                ShowToolTipsChanged(this, EventArgs.Empty);
        }
        #endregion

        #region PieChartItemEventArgs
        /// <summary>
        /// Stores a PieChartItem that is involved with an event.
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class PieChartItemEventArgs : EventArgs
        {
            #region Constructor
            /// <summary>
            /// Constructs a new instance.
            /// </summary>
            /// <param name="item">The item involved with an event.</param>
            public PieChartItemEventArgs(PieChartItem item)
            {
                this.item = item;
            }
            #endregion

            #region Fields
            /// <summary>
            /// The item involved with the event.
            /// </summary>
            private PieChartItem item;
            #endregion

            #region Methods
            /// <summary>
            /// Gets the item involved with the event.
            /// </summary>
            /// <value>The item.</value>
            public PieChartItem Item
            {
                get
                {
                    return item;
                }
            }
            #endregion
        }
        #endregion

        #region PieChartFocusEventArgs
        /// <summary>
        /// Stores the PieChartItems that are involved with an focus changing event.
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class PieChartItemFocusEventArgs : EventArgs
        {
            #region Constructor
            /// <summary>
            /// Constructs a new instance.
            /// </summary>
            /// <param name="oldItem">The item that is losing focus.</param>
            /// <param name="newItem">The item that is gaining focus.</param>
            public PieChartItemFocusEventArgs(PieChartItem oldItem, PieChartItem newItem)
            {
                this.oldItem = oldItem;
                this.newItem = newItem;
            }
            #endregion

            #region Fields
            /// <summary>
            /// The item that is losing focus.
            /// </summary>
            private PieChartItem oldItem;

            /// <summary>
            /// The item that is gaining focus.
            /// </summary>
            private PieChartItem newItem;
            #endregion

            #region Methods
            /// <summary>
            /// Gets the item that is losing focus.
            /// </summary>
            /// <value>The old item.</value>
            public PieChartItem OldItem
            {
                get
                {
                    return oldItem;
                }
            }

            /// <summary>
            /// Gets the item that is gaining focus.
            /// </summary>
            /// <value>The new item.</value>
            public PieChartItem NewItem
            {
                get
                {
                    return newItem;
                }
            }
            #endregion
        }
        #endregion
    }
    #endregion
    
    #endregion
}
