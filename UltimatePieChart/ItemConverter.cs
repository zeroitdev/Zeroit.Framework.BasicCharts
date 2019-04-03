// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-01-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="ItemConverter.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;

namespace Zeroit.Framework.BasicCharts
{
    #region Pie Chart

    #region PieChartItem.ItemConverter
    /// <summary>
    /// Class PieChartItem.
    /// </summary>
    /// <seealso cref="System.ICloneable" />
    /// <seealso cref="System.IComparable{Zeroit.Framework.BasicCharts.PieChartItem}" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    public partial class PieChartItem
    {
        /// <summary>
        /// Used to convert PieChartItems to an InstanceDescriptor for runtime design.
        /// </summary>
        /// <seealso cref="System.ComponentModel.TypeConverter" />
        public class ItemConverter : TypeConverter
        {
            /// <summary>
            /// Returns whether this converter can convert the object to the specified type.
            /// </summary>
            /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
            /// <param name="destinationType">A Type that represents the type you want to convert to.</param>
            /// <returns>True if this converter can perform the conversion; otherwise, false.</returns>
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(InstanceDescriptor))
                    return true;

                return base.CanConvertTo(context, destinationType);
            }

            /// <summary>
            /// Converts the given value object to the specified type, using the specified context and culture information.
            /// </summary>
            /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
            /// <param name="culture">A CultureInfo. If a null reference is passed, the current culture is assumed.</param>
            /// <param name="value">The Object to convert.</param>
            /// <param name="destinationType">The type to convert the value parameter to.</param>
            /// <returns>An object that represents the converted value.</returns>
            /// <exception cref="ArgumentNullException">destinationType</exception>
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == null)
                {
                    throw new ArgumentNullException("destinationType");
                }

                if (destinationType == typeof(InstanceDescriptor) && (value is PieChartItem))
                {
                    PieChartItem item = (PieChartItem)value;
                    ArrayList collection = new ArrayList();
                    collection.Add(item.Weight);
                    collection.Add(item.Color);
                    collection.Add(item.Text);
                    collection.Add(item.ToolTipText);
                    collection.Add(item.Offset);
                    return new InstanceDescriptor(value.GetType().GetConstructor(new Type[] { typeof(double), typeof(Color), typeof(string), typeof(string), typeof(float) }), collection);
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
    #endregion

    #endregion
}
