// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-11-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="WindowMessagesNativeMethods.cs" company="Zeroit Dev Technologies">
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
using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace Zeroit.Framework.BasicCharts
{

    #region WindowMessagesNativeMethods
    /// <summary>
    /// Class WindowMessagesNativeMethods.
    /// </summary>
    static class WindowMessagesNativeMethods
    {
        #region [ Suspend / Resume Drawing ]
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="wMsg">The w MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The wm setredraw
        /// </summary>
        private const int WM_SETREDRAW = 11;
        /// <summary>
        /// Suspends the drawing.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public static void SuspendDrawing(Control parent) { SendMessage(parent.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero); }
        /// <summary>
        /// Resumes the drawing.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public static void ResumeDrawing(Control parent) { SendMessage(parent.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero); parent.Refresh(); }
        #endregion

    }
    #endregion

}
