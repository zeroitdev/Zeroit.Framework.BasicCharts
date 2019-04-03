// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-04-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="MetroTaskPoint.cs" company="Zeroit Dev Technologies">
//    This program is for creating a progress bar control.
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
using System.ComponentModel;
using System.Drawing;

namespace Zeroit.Framework.BasicCharts.Metro
{
    

	public class ZeroitMetroTaskPoint : INotifyPropertyChanged
	{
		
		private bool _Finished;

		private bool _Enabled;

		private Color _CirceColor;

		private Image _Icon;

		private int _CircleWidth;

		private string _Text;

		public Color CirceColor
		{
			get
			{
				return this._CirceColor;
			}
			set
			{
				this._CirceColor = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("CirceColor"));
				}
			}
		}

		public int CircleWidth
		{
			get
			{
				return this._CircleWidth;
			}
			set
			{
				this._CircleWidth = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("CircleWidth"));
				}
			}
		}

		public bool Enabled
		{
			get
			{
				return this._Enabled;
			}
			set
			{
				this._Enabled = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("Enabled"));
				}
			}
		}

		public bool Finished
		{
			get
			{
				return this._Finished;
			}
			set
			{
				this._Finished = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("Finished"));
				}
			}
		}

		public Image Icon
		{
			get
			{
				return this._Icon;
			}
			set
			{
				this._Icon = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("Icon"));
				}
			}
		}

		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				this._Text = value;
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				if (propertyChangedEventHandler != null)
				{
					propertyChangedEventHandler(this, new PropertyChangedEventArgs("Text"));
				}
			}
		}

		
		public ZeroitMetroTaskPoint()
		{
			this._Finished = false;
			this._Enabled = true;
			this._CirceColor = Design.MetroColors.AccentBlue;
			this._Icon = null;
			this._CircleWidth = 20;
			this._Text = string.Empty;
		}

		
		public event PropertyChangedEventHandler PropertyChanged;
	}
    


}