// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-02-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Colors-Material.cs" company="Zeroit Dev Technologies">
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
    /// Class SplineMaterialColors.
    /// </summary>
    public class SplineMaterialColors
    {
        /// <summary>
        /// The fill color
        /// </summary>
        private Color[] fillColor = new Color[]
        {
            Color.FromArgb(249, 55, 98),
            Color.FromArgb(0, 162, 250)
        };

        /// <summary>
        /// The back color
        /// </summary>
        private Color backColor = Color.FromArgb(40, 40, 40);


        /// <summary>
        /// The gradient angle
        /// </summary>
        private float gradientAngle = 1f;

        /// <summary>
        /// Gets or sets the color of the fill.
        /// </summary>
        /// <value>The color of the fill.</value>
        public Color[] FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        /// <summary>
        /// Gets or sets the gradient angle.
        /// </summary>
        /// <value>The gradient angle.</value>
        public float GradientAngle
        {
            get { return gradientAngle; }
            set { gradientAngle = value; }
        }

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }
    }
}