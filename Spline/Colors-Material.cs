// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-02-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Colors-Material.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
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