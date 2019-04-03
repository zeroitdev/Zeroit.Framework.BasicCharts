// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="BarChart.cs" company="Zeroit Dev Technologies">
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

using System.Collections.Generic;
using System.Drawing;

namespace Zeroit.Framework.BasicCharts
{


    /// <summary>
    /// Class Material.
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public Color Background { get; set; } = Color.FromArgb(40, 40, 40);

        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        /// <value>The colors.</value>
        public List<Color> Colors { get; set; } =
            new List<Color>()
            {
                Color.FromArgb(249, 55, 98),
                Color.FromArgb(219, 55, 128),
                Color.FromArgb(193, 58, 151),
                Color.FromArgb(166, 58, 182),
                Color.FromArgb(147, 61, 180),
                Color.FromArgb(126, 66, 186),
                Color.FromArgb(107, 70, 188),
                Color.FromArgb(77, 94, 210),
                Color.FromArgb(48, 119, 227),
                Color.FromArgb(23, 144, 249),
                Color.FromArgb(10, 148, 249),
                Color.FromArgb(0, 152, 250),
                Color.FromArgb(0, 162, 250),
                Color.FromArgb(0, 150, 212)
            };

    }

}