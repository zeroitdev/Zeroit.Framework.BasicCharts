// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-04-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="MetroPieChartSegment.cs" company="Zeroit Dev Technologies">
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
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Zeroit.Framework.BasicCharts.Metro
{
    /// <summary>
    /// Class ZeroitMetroPieChartSegment.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class ZeroitMetroPieChartSegment : INotifyPropertyChanged
	{
        /// <summary>
        /// The refer
        /// </summary>
        private static List<WeakReference> refer;

        /// <summary>
        /// The value
        /// </summary>
        private int _value;

        /// <summary>
        /// The fill color
        /// </summary>
        private Color _fillColor;

        /// <summary>
        /// The name
        /// </summary>
        private string _name;

        /// <summary>
        /// The maximum stored
        /// </summary>
        private int maxStored;

        /// <summary>
        /// The border color
        /// </summary>
        private Color _borderColor;

        /// <summary>
        /// The style
        /// </summary>
        private ZeroitMetroPieChartSegment.eStyle _style;

        /// <summary>
        /// The fill style
        /// </summary>
        private HatchStyle _fillStyle;

        /// <summary>
        /// The use fill style
        /// </summary>
        private bool _UseFillStyle;

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor
		{
			get
			{
				return this._borderColor;
			}
			set
			{
				if (this._borderColor != value)
				{
					this._borderColor = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("BorderColor"));
					}
				}
			}
		}

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        public Color FillColor
		{
			get
			{
				return this._fillColor;
			}
			set
			{
				if (this._fillColor != value)
				{
					this._fillColor = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("FillColor"));
					}
				}
			}
		}

        /// <summary>
        /// Gets or sets the fill style.
        /// </summary>
        /// <value>The fill style.</value>
        public HatchStyle FillStyle
		{
			get
			{
				return this._fillStyle;
			}
			set
			{
				if (this._fillStyle != value)
				{
					this._fillStyle = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("FillStyle"));
					}
				}
			}
		}

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (Operators.CompareString(this._name, value, false) != 0)
				{
					this._name = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("Name"));
					}
				}
			}
		}

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>The style.</value>
        public ZeroitMetroPieChartSegment.eStyle Style
		{
			get
			{
				return this._style;
			}
			set
			{
				if (this._style != value)
				{
					this._style = value;
					if (this._style == ZeroitMetroPieChartSegment.eStyle.AbstractBlue)
					{
						this.ApplyStyle(ZeroitMetroPieChartSegment.eStyle.AbstractBlue);
					}
					else if (this._style == ZeroitMetroPieChartSegment.eStyle.AbstractPurple)
					{
						this.ApplyStyle(ZeroitMetroPieChartSegment.eStyle.AbstractPurple);
					}
					else if (this._style == ZeroitMetroPieChartSegment.eStyle.AbstractRed)
					{
						this.ApplyStyle(ZeroitMetroPieChartSegment.eStyle.AbstractRed);
					}
					else if (this._style == ZeroitMetroPieChartSegment.eStyle.LightBlue)
					{
						this.ApplyStyle(ZeroitMetroPieChartSegment.eStyle.LightBlue);
					}
					else if (this._style == ZeroitMetroPieChartSegment.eStyle.LightOrange)
					{
						this.ApplyStyle(ZeroitMetroPieChartSegment.eStyle.LightOrange);
					}
					else if (this._style == ZeroitMetroPieChartSegment.eStyle.LightRed)
					{
						this.ApplyStyle(ZeroitMetroPieChartSegment.eStyle.LightRed);
					}
					else if (this._style == ZeroitMetroPieChartSegment.eStyle.LightCyan)
					{
						this.ApplyStyle(ZeroitMetroPieChartSegment.eStyle.LightCyan);
					}
					else if (this._style == ZeroitMetroPieChartSegment.eStyle.DarkBlue)
					{
						this.ApplyStyle(ZeroitMetroPieChartSegment.eStyle.DarkBlue);
					}
					else if (this._style == ZeroitMetroPieChartSegment.eStyle.SoapGreen)
					{
						this.ApplyStyle(ZeroitMetroPieChartSegment.eStyle.SoapGreen);
					}
					else if (this._style != ZeroitMetroPieChartSegment.eStyle.SoapRed)
					{
						this._style = ZeroitMetroPieChartSegment.eStyle.Custom;
					}
					else
					{
						this.ApplyStyle(ZeroitMetroPieChartSegment.eStyle.SoapRed);
					}
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("Style"));
					}
				}
			}
		}

        /// <summary>
        /// Gets or sets a value indicating whether [use fill style].
        /// </summary>
        /// <value><c>true</c> if [use fill style]; otherwise, <c>false</c>.</value>
        public bool UseFillStyle
		{
			get
			{
				return this._UseFillStyle;
			}
			set
			{
				if (this._UseFillStyle != value)
				{
					this._UseFillStyle = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("UseFillStyle"));
					}
				}
			}
		}

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._value != value)
				{
					this._value = value;
					PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
					if (propertyChangedEventHandler != null)
					{
						propertyChangedEventHandler(this, new PropertyChangedEventArgs("Value"));
					}
				}
			}
		}

        /// <summary>
        /// Initializes static members of the <see cref="ZeroitMetroPieChartSegment"/> class.
        /// </summary>
        [DebuggerNonUserCode]
		static ZeroitMetroPieChartSegment()
		{
			ZeroitMetroPieChartSegment.refer = new List<WeakReference>();
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMetroPieChartSegment"/> class.
        /// </summary>
        public ZeroitMetroPieChartSegment()
		{
			ZeroitMetroPieChartSegment.__ENCAddToList(this);
			this._style = ZeroitMetroPieChartSegment.eStyle.Custom;
			this._fillStyle = HatchStyle.BackwardDiagonal;
			this._UseFillStyle = false;
			this._value = 10;
			this._fillColor = Color.FromArgb(255, 129, 0);
			this._borderColor = Color.FromArgb(255, 129, 0);
			this._style = ZeroitMetroPieChartSegment.eStyle.LightOrange;
		}

        /// <summary>
        /// Encs the add to list.
        /// </summary>
        /// <param name="value">The value.</param>
        [DebuggerNonUserCode]
		private static void __ENCAddToList(object value)
		{
			List<WeakReference> _ENCList = ZeroitMetroPieChartSegment.refer;
			Monitor.Enter(_ENCList);
			try
			{
				if (ZeroitMetroPieChartSegment.refer.Count == ZeroitMetroPieChartSegment.refer.Capacity)
				{
					int item = 0;
					int count = checked(ZeroitMetroPieChartSegment.refer.Count - 1);
					for (int i = 0; i <= count; i = checked(i + 1))
					{
						if (ZeroitMetroPieChartSegment.refer[i].IsAlive)
						{
							if (i != item)
							{
								ZeroitMetroPieChartSegment.refer[item] = ZeroitMetroPieChartSegment.refer[i];
							}
							item = checked(item + 1);
						}
					}
					ZeroitMetroPieChartSegment.refer.RemoveRange(item, checked(ZeroitMetroPieChartSegment.refer.Count - item));
					ZeroitMetroPieChartSegment.refer.Capacity = ZeroitMetroPieChartSegment.refer.Count;
				}
				ZeroitMetroPieChartSegment.refer.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
			}
			finally
			{
				Monitor.Exit(_ENCList);
			}
		}

        /// <summary>
        /// Applies the style.
        /// </summary>
        /// <param name="eStyle">The e style.</param>
        private void ApplyStyle(ZeroitMetroPieChartSegment.eStyle eStyle)
		{
			switch (eStyle)
			{
				case ZeroitMetroPieChartSegment.eStyle.LightCyan:
				{
					this._fillColor = Color.FromArgb(0, 255, 155);
					this._borderColor = Color.FromArgb(0, 255, 155);
					break;
				}
				case ZeroitMetroPieChartSegment.eStyle.LightBlue:
				{
					this._fillColor = Color.FromArgb(30, 151, 227);
					this._borderColor = Color.FromArgb(30, 151, 227);
					break;
				}
				case ZeroitMetroPieChartSegment.eStyle.LightRed:
				{
					this._fillColor = Color.FromArgb(255, 42, 0);
					this._borderColor = Color.FromArgb(255, 42, 0);
					break;
				}
				case ZeroitMetroPieChartSegment.eStyle.LightOrange:
				{
					this._fillColor = Color.FromArgb(255, 129, 0);
					this._borderColor = Color.FromArgb(255, 129, 0);
					break;
				}
				case ZeroitMetroPieChartSegment.eStyle.AbstractRed:
				{
					this._fillColor = Color.FromArgb(91, 46, 49);
					this._borderColor = Color.FromArgb(193, 66, 72);
					break;
				}
				case ZeroitMetroPieChartSegment.eStyle.AbstractBlue:
				{
					this._fillColor = Color.FromArgb(33, 73, 130);
					this._borderColor = Color.FromArgb(50, 109, 212);
					break;
				}
				case ZeroitMetroPieChartSegment.eStyle.AbstractPurple:
				{
					this._fillColor = Color.FromArgb(79, 50, 136);
					this._borderColor = Color.FromArgb(124, 68, 208);
					break;
				}
				case ZeroitMetroPieChartSegment.eStyle.DarkBlue:
				{
					this._fillColor = Color.FromArgb(40, 40, 40);
					this._borderColor = Color.FromArgb(0, 164, 240);
					break;
				}
				case ZeroitMetroPieChartSegment.eStyle.SoapRed:
				{
					this._fillColor = Color.FromArgb(255, 63, 53);
					this._borderColor = Color.White;
					break;
				}
				case ZeroitMetroPieChartSegment.eStyle.SoapGreen:
				{
					this._fillColor = Color.FromArgb(21, 159, 79);
					this._borderColor = Color.White;
					break;
				}
			}
		}

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Enum eStyle
        /// </summary>
        public enum eStyle
		{
            /// <summary>
            /// The light cyan
            /// </summary>
            LightCyan,
            /// <summary>
            /// The light blue
            /// </summary>
            LightBlue,
            /// <summary>
            /// The light red
            /// </summary>
            LightRed,
            /// <summary>
            /// The light orange
            /// </summary>
            LightOrange,
            /// <summary>
            /// The abstract red
            /// </summary>
            AbstractRed,
            /// <summary>
            /// The abstract blue
            /// </summary>
            AbstractBlue,
            /// <summary>
            /// The abstract purple
            /// </summary>
            AbstractPurple,
            /// <summary>
            /// The dark blue
            /// </summary>
            DarkBlue,
            /// <summary>
            /// The SOAP red
            /// </summary>
            SoapRed,
            /// <summary>
            /// The SOAP green
            /// </summary>
            SoapGreen,
            /// <summary>
            /// The custom
            /// </summary>
            Custom
        }
	}
}