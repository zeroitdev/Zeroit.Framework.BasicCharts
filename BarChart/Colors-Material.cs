// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="BarChart.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
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