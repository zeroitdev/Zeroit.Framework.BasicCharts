// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Colors-Bootstrap.cs" company="Zeroit Dev Technologies">
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

using System.Drawing;

namespace Zeroit.Framework.BasicCharts
{


    /// <summary>
    /// Class Bootstrap.
    /// </summary>
    public class Bootstrap
    {
        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public Color Background { get; set; } = Color.FromArgb(35, 40, 50);

        /// <summary>
        /// Gets or sets the orient background.
        /// </summary>
        /// <value>The orient background.</value>
        public Color OrientBackground { get; set; } = Color.FromArgb(30, 35, 40);

        /// <summary>
        /// Gets or sets the grid colors.
        /// </summary>
        /// <value>The grid colors.</value>
        public Color GridColors { get; set; } = Color.FromArgb(60, 65, 70);

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor { get; set; } = Color.FromArgb(115, 120, 125);

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public float BorderWidth { get; set; } = 1f;
    }

}