// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="ItemStyle.cs" company="Zeroit Dev Technologies">
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

namespace Zeroit.Framework.BasicCharts
{
    #region Pie Chart

    #region PieChartControl.PieChartItemStyle
    /// <summary>
    /// Class ZeroitUltimatePieChart.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    public partial class ZeroitUltimatePieChart
    {
        /// <summary>
        /// Represents the possible styles corresponding to a PieChartItem.
        /// </summary>
        public class PieChartItemStyle
        {
            #region Constructor
            /// <summary>
            /// Constructs a new instance of PieChartItemStyle.
            /// </summary>
            /// <param name="container">The control that contains the style.</param>
            internal PieChartItemStyle(ZeroitUltimatePieChart container)
            {
                this.container = container;
            }



            #endregion

            #region Fields
            /// <summary>
            /// The control that contains the style.
            /// </summary>
            private ZeroitUltimatePieChart container;

            /// <summary>
            /// The factor by which edge brightness will be affected.
            /// </summary>
            private float edgeBrightnessFactor = -0.3F;

            /// <summary>
            /// The surface alpha transparency factor.
            /// </summary>
            private float surfaceAlphaTransparency = 1F;

            /// <summary>
            /// The factor by which surface brightness will be affected.
            /// </summary>
            private float surfaceBrightnessFactor = 0F;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the surface alpha transparency factor.
            /// </summary>
            /// <value>The surface alpha transparency.</value>
            /// <exception cref="ArgumentOutOfRangeException">SurfaceAlphaTransparenty - The SurfaceAlphaTransparency must be between 0 and 1 inclusive.</exception>
            /// <remarks>This value must be between 0 and 1, and represents the multiplier that is applied to the
            /// alpha value of the color for pie slices that use this style.</remarks>
            public float SurfaceAlphaTransparency
            {
                get
                {
                    return surfaceAlphaTransparency;
                }
                set
                {
                    if (surfaceAlphaTransparency != value)
                    {
                        if (value < 0 || value > 1)
                            throw new ArgumentOutOfRangeException("SurfaceAlphaTransparenty", value, "The SurfaceAlphaTransparency must be between 0 and 1 inclusive.");

                        surfaceAlphaTransparency = value;
                        container.MarkVisualChange(true);
                    }
                }
            }

            /// <summary>
            /// Gets or sets the factor by which edge brightness will be affected.
            /// </summary>
            /// <value>The edge brightness factor.</value>
            /// <exception cref="ArgumentOutOfRangeException">EdgeBrightnessFactor - The EdgeBrightnessFactor must be between -1 and 1 inclusive.</exception>
            /// <remarks>See <see cref="ZeroitUltimatePieChart.DrawingMetrics.ChangeColorBrightness" /> for more information about brighness modification.</remarks>
            public float EdgeBrightnessFactor
            {
                get
                {
                    return edgeBrightnessFactor;
                }
                set
                {
                    if (edgeBrightnessFactor != value)
                    {
                        if (value < -1 || value > 1)
                            throw new ArgumentOutOfRangeException("EdgeBrightnessFactor", value, "The EdgeBrightnessFactor must be between -1 and 1 inclusive.");

                        edgeBrightnessFactor = value;
                        container.MarkVisualChange(true);
                    }
                }
            }

            /// <summary>
            /// Gets or sets the factor by which surface brightness will be affected.
            /// </summary>
            /// <value>The surface brightness factor.</value>
            /// <exception cref="ArgumentOutOfRangeException">SurfaceBrightnessFactor - The SurfaceBrightnessFactor must be between -1 and 1 inclusive.</exception>
            /// <remarks>See <see cref="ZeroitUltimatePieChart.DrawingMetrics.ChangeColorBrightness" /> for more information about brighness modification.</remarks>
            public float SurfaceBrightnessFactor
            {
                get
                {
                    return surfaceBrightnessFactor;
                }
                set
                {
                    if (surfaceBrightnessFactor != value)
                    {
                        if (value < -1 || value > 1)
                            throw new ArgumentOutOfRangeException("SurfaceBrightnessFactor", value, "The SurfaceBrightnessFactor must be between -1 and 1 inclusive.");

                        surfaceBrightnessFactor = value;
                        container.MarkVisualChange(true);
                    }
                }
            }
            #endregion
        }
    }
    #endregion
    
    #endregion
}
