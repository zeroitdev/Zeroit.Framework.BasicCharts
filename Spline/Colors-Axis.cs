// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-03-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Colors-Axis.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Drawing;

namespace Zeroit.Framework.BasicCharts
{
    /// <summary>
    /// Class SplineAxis.
    /// </summary>
    public class SplineAxis
    {
        /// <summary>
        /// Gets or sets the number colors.
        /// </summary>
        /// <value>The number colors.</value>
        public Color NumberColors { get; set; } = Color.White;

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        public Color LineColor { get; set; } = Color.Gray;

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>The font.</value>
        public Font Font { get; set; } = new Font("Microsoft Sans Serif", 8.25f);

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>The interval.</value>
        public int Interval { get; set; } = 20;
    }
}