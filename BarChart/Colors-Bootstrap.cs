// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Colors-Bootstrap.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
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