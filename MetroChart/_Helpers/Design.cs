// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 11-30-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="Design.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace Zeroit.Framework.BasicCharts.Metro
{
    /// <summary>
    /// Class Design.
    /// </summary>
    public class Design
    {
        

        /// <summary>
        /// Class Controls.
        /// </summary>
        public class Controls
        {
            

            /// <summary>
            /// Sets the double buffered.
            /// </summary>
            /// <param name="ctrl">The control.</param>
            /// <param name="value">if set to <c>true</c> [value].</param>
            public static void SetDoubleBuffered(Control ctrl, bool value)
            {
                if (!SystemInformation.TerminalServerSession)
                {
                    PropertyInfo property = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                    property.SetValue(ctrl, value, null);
                }
            }
        }

        /// <summary>
        /// Class Drawing.
        /// </summary>
        public class Drawing
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Drawing"/> class.
            /// </summary>
            [DebuggerNonUserCode]
            public Drawing()
            {
            }

            /// <summary>
            /// Draws the rounded path.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="size">The size.</param>
            /// <param name="rect">The rect.</param>
            /// <param name="curve">The curve.</param>
            /// <param name="TopLeft">if set to <c>true</c> [top left].</param>
            /// <param name="TopRight">if set to <c>true</c> [top right].</param>
            /// <param name="BottomLeft">if set to <c>true</c> [bottom left].</param>
            /// <param name="BottomRight">if set to <c>true</c> [bottom right].</param>
            public static void DrawRoundedPath(Graphics g, Color c, float size, Rectangle rect, int curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
            {
                using (Pen pen = new Pen(c, size))
                {
                    g.DrawPath(pen, Design.Drawing.RoundRectangle(rect, curve, TopLeft, TopRight, BottomLeft, BottomRight));
                }
            }

            /// <summary>
            /// Extracts the icon.
            /// </summary>
            /// <param name="original">The original.</param>
            /// <param name="size">The size.</param>
            /// <returns>Bitmap.</returns>
            public static Bitmap ExtractIcon(Icon original, int size)
            {
                Bitmap bitmap;
                using (Icon icon = new Icon(original, new Size(size, size)))
                {
                    bitmap = icon.ToBitmap();
                }
                return bitmap;
            }

            /// <summary>
            /// Fades the ellipse.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="rect">The rect.</param>
            /// <param name="stages">The stages.</param>
            /// <param name="stageWidth">Width of the stage.</param>
            public static void FadeEllipse(Graphics g, Color c, Rectangle rect, int stages = 5, int stageWidth = 4)
            {
                int num = checked(checked((int)Math.Round((double)c.A / (double)stages)) - 1);
                int num1 = stages;
                for (int i = 0; i <= num1; i = checked(i + 1))
                {
                    using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(checked(num * (num == 0 ? 1 : i)), c)))
                    {
                        Rectangle rectangle = new Rectangle(checked(rect.X + checked(stageWidth * i)), checked(rect.Y + checked(stageWidth * i)), checked(rect.Width - checked(checked(stageWidth * i) * 2)), checked(rect.Height - checked(checked(stageWidth * i) * 2)));
                        g.FillEllipse(solidBrush, rectangle);
                    }
                }
            }

            /// <summary>
            /// Fades the ellipse.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            /// <param name="width">The width.</param>
            /// <param name="height">The height.</param>
            /// <param name="stages">The stages.</param>
            /// <param name="stageWidth">Width of the stage.</param>
            public static void FadeEllipse(Graphics g, Color c, int x, int y, int width, int height, int stages = 5, int stageWidth = 4)
            {
                Rectangle rectangle = new Rectangle(x, y, width, height);
                Design.Drawing.FadeEllipse(g, c, rectangle, stages, stageWidth);
            }

            /// <summary>
            /// Fades the rectangle.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="rect">The rect.</param>
            /// <param name="stages">The stages.</param>
            /// <param name="stageWidth">Width of the stage.</param>
            public static void FadeRectangle(Graphics g, Color c, Rectangle rect, int stages = 5, int stageWidth = 4)
            {
                int num = checked(checked((int)Math.Round(255 / (double)stages)) - 1);
                int num1 = stages;
                for (int i = 0; i <= num1; i = checked(i + 1))
                {
                    using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(checked(num * (num == 0 ? 1 : i)), c)))
                    {
                        Rectangle rectangle = new Rectangle(checked(rect.X + checked(stageWidth * i)), checked(rect.Y + checked(stageWidth * i)), checked(rect.Width - checked(checked(stageWidth * i) * 2)), checked(rect.Height - checked(checked(stageWidth * i) * 2)));
                        g.FillRectangle(solidBrush, rectangle);
                    }
                }
            }

            /// <summary>
            /// Fades the rectangle.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            /// <param name="width">The width.</param>
            /// <param name="height">The height.</param>
            /// <param name="stages">The stages.</param>
            /// <param name="stageWidth">Width of the stage.</param>
            public static void FadeRectangle(Graphics g, Color c, int x, int y, int width, int height, int stages = 5, int stageWidth = 4)
            {
                Rectangle rectangle = new Rectangle(x, y, width, height);
                Design.Drawing.FadeRectangle(g, c, rectangle, stages, stageWidth);
            }

            /// <summary>
            /// Fills the rounded path.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="c">The c.</param>
            /// <param name="rect">The rect.</param>
            /// <param name="curve">The curve.</param>
            /// <param name="TopLeft">if set to <c>true</c> [top left].</param>
            /// <param name="TopRight">if set to <c>true</c> [top right].</param>
            /// <param name="BottomLeft">if set to <c>true</c> [bottom left].</param>
            /// <param name="BottomRight">if set to <c>true</c> [bottom right].</param>
            public static void FillRoundedPath(Graphics g, Color c, Rectangle rect, int curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
            {
                using (SolidBrush solidBrush = new SolidBrush(c))
                {
                    g.FillPath(solidBrush, Design.Drawing.RoundRectangle(rect, curve, TopLeft, TopRight, BottomLeft, BottomRight));
                }
            }

            /// <summary>
            /// Fills the rounded path.
            /// </summary>
            /// <param name="g">The g.</param>
            /// <param name="b">The b.</param>
            /// <param name="rect">The rect.</param>
            /// <param name="curve">The curve.</param>
            /// <param name="TopLeft">if set to <c>true</c> [top left].</param>
            /// <param name="TopRight">if set to <c>true</c> [top right].</param>
            /// <param name="BottomLeft">if set to <c>true</c> [bottom left].</param>
            /// <param name="BottomRight">if set to <c>true</c> [bottom right].</param>
            public static void FillRoundedPath(Graphics g, Brush b, Rectangle rect, int curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
            {
                g.FillPath(b, Design.Drawing.RoundRectangle(rect, curve, TopLeft, TopRight, BottomLeft, BottomRight));
            }

            /// <summary>
            /// Gets the circle intersection points.
            /// </summary>
            /// <param name="ellipse">The ellipse.</param>
            /// <param name="point1">The point1.</param>
            /// <param name="point2">The point2.</param>
            /// <returns>Point[].</returns>
            public static Point[] GetCircleIntersectionPoints(Rectangle ellipse, Point point1, Point point2)
            {
                float single;
                Point point;
                List<Point> points = new List<Point>();
                float x = (float)(checked(ellipse.X + checked((int)Math.Round((double)ellipse.Width / 2))));
                float y = (float)(checked(ellipse.Y + checked((int)Math.Round((double)ellipse.Height / 2))));
                float x1 = (float)(checked(point2.X - point1.X));
                float y1 = (float)(checked(point2.Y - point1.Y));
                float single1 = x1 * x1 + y1 * y1;
                float x2 = 2f * (x1 * ((float)point1.X - x) + y1 * ((float)point1.Y - y));
                float single2 = ((float)point1.X - x) * ((float)point1.X - x) + ((float)point1.Y - y) * ((float)point1.Y - y) - (float)(checked(checked((int)Math.Round((double)ellipse.Width / 2)) * checked((int)Math.Round((double)ellipse.Width / 2))));
                float single3 = x2 * x2 - 4f * single1 * single2;
                if (!((double)single1 <= 1E-07 | single3 < 0f))
                {
                    if (single3 != 0f)
                    {
                        single = (float)(((double)(-x2) + Math.Sqrt((double)single3)) / (double)(2f * single1));
                        point = new Point(checked((int)Math.Round((double)((float)((float)point1.X + single * x1)))), checked((int)Math.Round((double)((float)((float)point1.Y + single * y1)))));
                        points.Add(point);
                        single = (float)(((double)(-x2) - Math.Sqrt((double)single3)) / (double)(2f * single1));
                        point = new Point(checked((int)Math.Round((double)((float)((float)point1.X + single * x1)))), checked((int)Math.Round((double)((float)((float)point1.Y + single * y1)))));
                        points.Add(point);
                    }
                    else
                    {
                        single = -x2 / (2f * single1);
                        point = new Point(checked((int)Math.Round((double)((float)((float)point1.X + single * x1)))), checked((int)Math.Round((double)((float)((float)point1.Y + single * y1)))));
                        points.Add(point);
                    }
                }
                return points.ToArray();
            }

            /// <summary>
            /// Gets the circle intersection points.
            /// </summary>
            /// <param name="ellipse">The ellipse.</param>
            /// <param name="x1">The x1.</param>
            /// <param name="y1">The y1.</param>
            /// <param name="x2">The x2.</param>
            /// <param name="y2">The y2.</param>
            /// <returns>Point[].</returns>
            public static Point[] GetCircleIntersectionPoints(Rectangle ellipse, int x1, int y1, int x2, int y2)
            {
                Point point = new Point(x1, y1);
                Point point1 = new Point(x2, y2);
                return Design.Drawing.GetCircleIntersectionPoints(ellipse, point, point1);
            }

            /// <summary>
            /// Gets the point on line.
            /// </summary>
            /// <param name="p1">The p1.</param>
            /// <param name="p2">The p2.</param>
            /// <param name="perc">The perc.</param>
            /// <returns>Point.</returns>
            public static Point GetPointOnLine(Point p1, Point p2, int perc)
            {
                float x = (float)((double)(checked(p2.X - p1.X)) * ((double)perc / 100) + (double)p1.X);
                float y = (float)((double)(checked(p2.Y - p1.Y)) * ((double)perc / 100) + (double)p1.Y);
                Point point = new Point(checked((int)Math.Round((double)x)), checked((int)Math.Round((double)y)));
                return point;
            }

            /// <summary>
            /// Gets the point on line.
            /// </summary>
            /// <param name="x1">The x1.</param>
            /// <param name="y1">The y1.</param>
            /// <param name="x2">The x2.</param>
            /// <param name="y2">The y2.</param>
            /// <param name="perc">The perc.</param>
            /// <returns>Point.</returns>
            public static Point GetPointOnLine(int x1, int y1, int x2, int y2, int perc)
            {
                Point point = new Point(x1, y1);
                Point point1 = new Point(x2, y2);
                return Design.Drawing.GetPointOnLine(point, point1, perc);
            }

            /// <summary>
            /// Gets the point on line.
            /// </summary>
            /// <param name="x1">The x1.</param>
            /// <param name="y1">The y1.</param>
            /// <param name="point2">The point2.</param>
            /// <param name="perc">The perc.</param>
            /// <returns>Point.</returns>
            public static Point GetPointOnLine(int x1, int y1, Point point2, int perc)
            {
                Point point = new Point(x1, y1);
                return Design.Drawing.GetPointOnLine(point, point2, perc);
            }

            /// <summary>
            /// Gets the point on line.
            /// </summary>
            /// <param name="point1">The point1.</param>
            /// <param name="x2">The x2.</param>
            /// <param name="y2">The y2.</param>
            /// <param name="perc">The perc.</param>
            /// <returns>Point.</returns>
            public static Point GetPointOnLine(Point point1, int x2, int y2, int perc)
            {
                Point point = new Point(x2, y2);
                return Design.Drawing.GetPointOnLine(point1, point, perc);
            }

            /// <summary>
            /// Measures the point distance.
            /// </summary>
            /// <param name="p1">The p1.</param>
            /// <param name="p2">The p2.</param>
            /// <returns>System.Double.</returns>
            public static double MeasurePointDistance(Point p1, Point p2)
            {
                int x = checked(p2.X - p1.X);
                int y = checked(p2.Y - p1.Y);
                double num = Math.Sqrt((double)(checked(checked(x * x) + checked(y * y))));
                return num;
            }

            /// <summary>
            /// Measures the point distance.
            /// </summary>
            /// <param name="x1">The x1.</param>
            /// <param name="y1">The y1.</param>
            /// <param name="x2">The x2.</param>
            /// <param name="y2">The y2.</param>
            /// <returns>System.Double.</returns>
            public static double MeasurePointDistance(int x1, int y1, int x2, int y2)
            {
                Point point = new Point(x1, y1);
                return Design.Drawing.MeasurePointDistance(point, new Point(x2, y2));
            }

            /// <summary>
            /// Rounds the rectangle.
            /// </summary>
            /// <param name="r">The r.</param>
            /// <param name="Curve">The curve.</param>
            /// <param name="TopLeft">if set to <c>true</c> [top left].</param>
            /// <param name="TopRight">if set to <c>true</c> [top right].</param>
            /// <param name="BottomLeft">if set to <c>true</c> [bottom left].</param>
            /// <param name="BottomRight">if set to <c>true</c> [bottom right].</param>
            /// <returns>GraphicsPath.</returns>
            public static GraphicsPath RoundRectangle(Rectangle r, int Curve, bool TopLeft = true, bool TopRight = true, bool BottomLeft = true, bool BottomRight = true)
            {
                GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
                if (!TopLeft)
                {
                    graphicsPath.AddLine(r.X, r.Y, r.X, r.Y);
                }
                else
                {
                    graphicsPath.AddArc(r.X, r.Y, Curve, Curve, 180f, 90f);
                }
                if (!TopRight)
                {
                    graphicsPath.AddLine(checked(r.Right - r.Width), r.Y, r.Width, r.Y);
                }
                else
                {
                    graphicsPath.AddArc(checked(r.Right - Curve), r.Y, Curve, Curve, 270f, 90f);
                }
                if (!BottomRight)
                {
                    graphicsPath.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);
                }
                else
                {
                    graphicsPath.AddArc(checked(r.Right - Curve), checked(r.Bottom - Curve), Curve, Curve, 0f, 90f);
                }
                if (!BottomLeft)
                {
                    graphicsPath.AddLine(r.X, r.Bottom, r.X, r.Bottom);
                }
                else
                {
                    graphicsPath.AddArc(r.X, checked(r.Bottom - Curve), Curve, Curve, 90f, 90f);
                }
                graphicsPath.CloseFigure();
                return graphicsPath;
            }
        }

        /// <summary>
        /// Enum GridStyle
        /// </summary>
        public enum GridStyle
        {
            /// <summary>
            /// The none
            /// </summary>
            None,
            /// <summary>
            /// The horizontal
            /// </summary>
            Horizontal,
            /// <summary>
            /// The vertical
            /// </summary>
            Vertical,
            /// <summary>
            /// The crossed
            /// </summary>
            Crossed
        }

        /// <summary>
        /// Enum Orientation
        /// </summary>
        public enum Orientation
        {
            /// <summary>
            /// The horizontal
            /// </summary>
            Horizontal,
            /// <summary>
            /// The vertical
            /// </summary>
            Vertical
        }


        /// <summary>
        /// Class MetroColors.
        /// </summary>
        public class MetroColors
        {
            /// <summary>
            /// The accent blue
            /// </summary>
            private static Color _AccentBlue;

            /// <summary>
            /// The accent purple
            /// </summary>
            private static Color _AccentPurple;

            /// <summary>
            /// The accent orange
            /// </summary>
            private static Color _AccentOrange;

            /// <summary>
            /// The accent dark blue
            /// </summary>
            private static Color _AccentDarkBlue;

            /// <summary>
            /// The accent light blue
            /// </summary>
            private static Color _AccentLightBlue;

            /// <summary>
            /// The selection color
            /// </summary>
            private static Color _SelectionColor;

            /// <summary>
            /// The pop up border
            /// </summary>
            private static Color _PopUpBorder;

            /// <summary>
            /// The pop up font
            /// </summary>
            private static Color _PopUpFont;

            /// <summary>
            /// The tab item hover
            /// </summary>
            private static Color _TabItemHover;

            /// <summary>
            /// The disabled border
            /// </summary>
            private static Color _DisabledBorder;

            /// <summary>
            /// The task color
            /// </summary>
            private static Color _TaskColor;

            /// <summary>
            /// The dark task color
            /// </summary>
            private static Color _DarkTaskColor;

            /// <summary>
            /// The light default
            /// </summary>
            private static Color _LightDefault;

            /// <summary>
            /// The light dark default
            /// </summary>
            private static Color _LightDarkDefault;

            /// <summary>
            /// The light hover
            /// </summary>
            private static Color _LightHover;

            /// <summary>
            /// The light icon
            /// </summary>
            private static Color _LightIcon;

            /// <summary>
            /// The light border
            /// </summary>
            private static Color _LightBorder;

            /// <summary>
            /// The light font
            /// </summary>
            private static Color _LightFont;

            /// <summary>
            /// The light disabled
            /// </summary>
            private static Color _LightDisabled;

            /// <summary>
            /// The light disabled font
            /// </summary>
            private static Color _LightDisabledFont;

            /// <summary>
            /// The light switch rail
            /// </summary>
            private static Color _LightSwitchRail;

            /// <summary>
            /// The dark default
            /// </summary>
            private static Color _DarkDefault;

            /// <summary>
            /// The dark hover
            /// </summary>
            private static Color _DarkHover;

            /// <summary>
            /// The dark icon
            /// </summary>
            private static Color _DarkIcon;

            /// <summary>
            /// The dark font
            /// </summary>
            private static Color _DarkFont;

            /// <summary>
            /// The dark disabled
            /// </summary>
            private static Color _DarkDisabled;

            /// <summary>
            /// The dark disabled font
            /// </summary>
            private static Color _DarkDisabledFont;

            /// <summary>
            /// The text shadow
            /// </summary>
            private static Color _TextShadow;

            /// <summary>
            /// Gets the accent blue.
            /// </summary>
            /// <value>The accent blue.</value>
            public static Color AccentBlue
            {
                get
                {
                    return Design.MetroColors._AccentBlue;
                }
            }

            /// <summary>
            /// Gets the accent dark blue.
            /// </summary>
            /// <value>The accent dark blue.</value>
            public static Color AccentDarkBlue
            {
                get
                {
                    return Design.MetroColors._AccentDarkBlue;
                }
            }

            /// <summary>
            /// Gets the accent light blue.
            /// </summary>
            /// <value>The accent light blue.</value>
            public static Color AccentLightBlue
            {
                get
                {
                    return Design.MetroColors._AccentLightBlue;
                }
            }

            /// <summary>
            /// Gets the accent orange.
            /// </summary>
            /// <value>The accent orange.</value>
            public static Color AccentOrange
            {
                get
                {
                    return Design.MetroColors._AccentOrange;
                }
            }

            /// <summary>
            /// Gets the accent purple.
            /// </summary>
            /// <value>The accent purple.</value>
            public static Color AccentPurple
            {
                get
                {
                    return Design.MetroColors._AccentPurple;
                }
            }

            /// <summary>
            /// Gets the dark default.
            /// </summary>
            /// <value>The dark default.</value>
            public static Color DarkDefault
            {
                get
                {
                    return Design.MetroColors._DarkDefault;
                }
            }

            /// <summary>
            /// Gets the dark disabled.
            /// </summary>
            /// <value>The dark disabled.</value>
            public static Color DarkDisabled
            {
                get
                {
                    return Design.MetroColors._DarkDisabled;
                }
            }

            /// <summary>
            /// Gets the dark disabled font.
            /// </summary>
            /// <value>The dark disabled font.</value>
            public static Color DarkDisabledFont
            {
                get
                {
                    return Design.MetroColors._DarkDisabledFont;
                }
            }

            /// <summary>
            /// Gets the dark font.
            /// </summary>
            /// <value>The dark font.</value>
            public static Color DarkFont
            {
                get
                {
                    return Design.MetroColors._DarkFont;
                }
            }

            /// <summary>
            /// Gets the dark hover.
            /// </summary>
            /// <value>The dark hover.</value>
            public static Color DarkHover
            {
                get
                {
                    return Design.MetroColors._DarkHover;
                }
            }

            /// <summary>
            /// Gets the dark icon.
            /// </summary>
            /// <value>The dark icon.</value>
            public static Color DarkIcon
            {
                get
                {
                    return Design.MetroColors._DarkIcon;
                }
            }

            /// <summary>
            /// Gets the color of the dark task.
            /// </summary>
            /// <value>The color of the dark task.</value>
            public static Color DarkTaskColor
            {
                get
                {
                    return Design.MetroColors._DarkTaskColor;
                }
            }

            /// <summary>
            /// Gets the disabled border.
            /// </summary>
            /// <value>The disabled border.</value>
            public static Color DisabledBorder
            {
                get
                {
                    return Design.MetroColors._DisabledBorder;
                }
            }

            /// <summary>
            /// Gets the light border.
            /// </summary>
            /// <value>The light border.</value>
            public static Color LightBorder
            {
                get
                {
                    return Design.MetroColors._LightBorder;
                }
            }

            /// <summary>
            /// Gets the light dark default.
            /// </summary>
            /// <value>The light dark default.</value>
            public static Color LightDarkDefault
            {
                get
                {
                    return Design.MetroColors._LightDarkDefault;
                }
            }

            /// <summary>
            /// Gets the light default.
            /// </summary>
            /// <value>The light default.</value>
            public static Color LightDefault
            {
                get
                {
                    return Design.MetroColors._LightDefault;
                }
            }

            /// <summary>
            /// Gets the light disabled.
            /// </summary>
            /// <value>The light disabled.</value>
            public static Color LightDisabled
            {
                get
                {
                    return Design.MetroColors._LightDisabled;
                }
            }

            /// <summary>
            /// Gets the light disabled font.
            /// </summary>
            /// <value>The light disabled font.</value>
            public static Color LightDisabledFont
            {
                get
                {
                    return Design.MetroColors._LightDisabledFont;
                }
            }

            /// <summary>
            /// Gets the light font.
            /// </summary>
            /// <value>The light font.</value>
            public static Color LightFont
            {
                get
                {
                    return Design.MetroColors._LightFont;
                }
            }

            /// <summary>
            /// Gets the light hover.
            /// </summary>
            /// <value>The light hover.</value>
            public static Color LightHover
            {
                get
                {
                    return Design.MetroColors._LightHover;
                }
            }

            /// <summary>
            /// Gets the light icon.
            /// </summary>
            /// <value>The light icon.</value>
            public static Color LightIcon
            {
                get
                {
                    return Design.MetroColors._LightIcon;
                }
            }

            /// <summary>
            /// Gets the light switch rail.
            /// </summary>
            /// <value>The light switch rail.</value>
            public static Color LightSwitchRail
            {
                get
                {
                    return Design.MetroColors._LightSwitchRail;
                }
            }

            /// <summary>
            /// Gets the pop up border.
            /// </summary>
            /// <value>The pop up border.</value>
            public static Color PopUpBorder
            {
                get
                {
                    return Design.MetroColors._PopUpBorder;
                }
            }

            /// <summary>
            /// Gets the pop up font.
            /// </summary>
            /// <value>The pop up font.</value>
            public static Color PopUpFont
            {
                get
                {
                    return Design.MetroColors._PopUpFont;
                }
            }

            /// <summary>
            /// Gets the color of the selection.
            /// </summary>
            /// <value>The color of the selection.</value>
            public static Color SelectionColor
            {
                get
                {
                    return Design.MetroColors._SelectionColor;
                }
            }

            /// <summary>
            /// Gets the tab item hover.
            /// </summary>
            /// <value>The tab item hover.</value>
            public static Color TabItemHover
            {
                get
                {
                    return Design.MetroColors._TabItemHover;
                }
            }

            /// <summary>
            /// Gets the color of the task.
            /// </summary>
            /// <value>The color of the task.</value>
            public static Color TaskColor
            {
                get
                {
                    return Design.MetroColors._TaskColor;
                }
            }

            /// <summary>
            /// Gets the text shadow.
            /// </summary>
            /// <value>The text shadow.</value>
            public static Color TextShadow
            {
                get
                {
                    return Design.MetroColors._TextShadow;
                }
            }

            /// <summary>
            /// Initializes static members of the <see cref="MetroColors"/> class.
            /// </summary>
            static MetroColors()
            {
                Design.MetroColors._AccentBlue = Color.FromArgb(0, 122, 204);
                Design.MetroColors._AccentPurple = Color.FromArgb(104, 33, 122);
                Design.MetroColors._AccentOrange = Color.FromArgb(202, 81, 0);
                Design.MetroColors._AccentDarkBlue = Color.FromArgb(0, 99, 165);
                Design.MetroColors._AccentLightBlue = Color.FromArgb(0, 153, 255);
                Design.MetroColors._SelectionColor = Color.FromArgb(30, 0, 122, 204);
                Design.MetroColors._PopUpBorder = Color.FromArgb(240, 240, 240);
                Design.MetroColors._PopUpFont = Color.FromArgb(106, 115, 124);
                Design.MetroColors._TabItemHover = Color.FromArgb(10, 0, 122, 204);
                Design.MetroColors._DisabledBorder = Color.FromArgb(200, 200, 200);
                Design.MetroColors._TaskColor = Color.FromArgb(230, 230, 230);
                Design.MetroColors._DarkTaskColor = Color.FromArgb(25, 25, 25);
                Design.MetroColors._LightDefault = Color.White;
                Design.MetroColors._LightDarkDefault = Color.FromArgb(230, 230, 230);
                Design.MetroColors._LightHover = Color.FromArgb(240, 240, 240);
                Design.MetroColors._LightIcon = Color.Black;
                Design.MetroColors._LightBorder = Color.FromArgb(98, 98, 98);
                Design.MetroColors._LightFont = Color.Black;
                Design.MetroColors._LightDisabled = Color.FromArgb(250, 250, 250);
                Design.MetroColors._LightDisabledFont = Color.Gray;
                Design.MetroColors._LightSwitchRail = Color.FromArgb(190, 190, 190);
                Design.MetroColors._DarkDefault = Color.FromArgb(40, 40, 40);
                Design.MetroColors._DarkHover = Color.FromArgb(63, 63, 63);
                Design.MetroColors._DarkIcon = Color.FromArgb(241, 241, 241);
                Design.MetroColors._DarkFont = Color.FromArgb(153, 153, 153);
                Design.MetroColors._DarkDisabled = Color.FromArgb(35, 35, 35);
                Design.MetroColors._DarkDisabledFont = Color.FromArgb(133, 133, 133);
                Design.MetroColors._TextShadow = Color.FromArgb(30, Color.Black);
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MetroColors"/> class.
            /// </summary>
            [DebuggerNonUserCode]
            public MetroColors()
            {
            }

            /// <summary>
            /// Changes the color brightness.
            /// </summary>
            /// <param name="color">The color.</param>
            /// <param name="correctionFactor">The correction factor.</param>
            /// <returns>Color.</returns>
            public static Color ChangeColorBrightness(Color color, float correctionFactor)
            {
                float r = (float)color.R;
                float g = (float)color.G;
                float b = (float)color.B;
                if (correctionFactor >= 0f)
                {
                    r = (255f - r) * correctionFactor + r;
                    g = (255f - g) * correctionFactor + g;
                    b = (255f - b) * correctionFactor + b;
                }
                else
                {
                    correctionFactor = 1f + correctionFactor;
                    r *= correctionFactor;
                    g *= correctionFactor;
                    b *= correctionFactor;
                }
                Color color1 = Color.FromArgb((int)color.A, checked((int)Math.Round((double)r)), checked((int)Math.Round((double)g)), checked((int)Math.Round((double)b)));
                return color1;
            }

            /// <summary>
            /// Colors to HTML.
            /// </summary>
            /// <param name="C">The c.</param>
            /// <returns>System.String.</returns>
            public static string ColorToHTML(Color C)
            {
                return ColorTranslator.ToHtml(C);
            }

            /// <summary>
            /// Gets the color of the correct back.
            /// </summary>
            /// <param name="style">The style.</param>
            /// <param name="defaultColor">The default color.</param>
            /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
            /// <returns>Color.</returns>
            public static Color GetCorrectBackColor(Design.Style style, Color defaultColor, bool isEnabled = true)
            {
                Color color = defaultColor;
                if (style == Design.Style.Light)
                {
                    color = (isEnabled ? Design.MetroColors.LightDefault : Design.MetroColors.LightDisabled);
                }
                else if (style == Design.Style.Dark)
                {
                    color = (isEnabled ? Design.MetroColors.DarkDefault : Design.MetroColors.DarkDisabled);
                }
                return color;
            }

            /// <summary>
            /// Gets the color of the correct fore.
            /// </summary>
            /// <param name="style">The style.</param>
            /// <param name="defaultColor">The default color.</param>
            /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
            /// <returns>Color.</returns>
            public static Color GetCorrectForeColor(Design.Style style, Color defaultColor, bool isEnabled = true)
            {
                Color color = defaultColor;
                if (style == Design.Style.Light)
                {
                    color = (isEnabled ? Design.MetroColors.LightFont : Design.MetroColors.LightDisabledFont);
                }
                else if (style == Design.Style.Dark)
                {
                    color = (isEnabled ? Design.MetroColors.DarkFont : Design.MetroColors.DarkDisabledFont);
                }
                return color;
            }

            /// <summary>
            /// HTMLs to color.
            /// </summary>
            /// <param name="sColor">Color of the s.</param>
            /// <returns>Color.</returns>
            public static Color HTMLToColor(string sColor)
            {
                return ColorTranslator.FromHtml(string.Concat("#", sColor));
            }

            /// <summary>
            /// Inverts the color.
            /// </summary>
            /// <param name="c">The c.</param>
            /// <returns>Color.</returns>
            public static Color InvertColor(Color c)
            {
                return Color.FromArgb(c.ToArgb() ^ 16777215);
            }
        }

        /// <summary>
        /// Enum Style
        /// </summary>
        public enum Style
        {
            /// <summary>
            /// The light
            /// </summary>
            Light,
            /// <summary>
            /// The dark
            /// </summary>
            Dark,
            /// <summary>
            /// The custom
            /// </summary>
            Custom
        }
    }

}
