// ***********************************************************************
// Assembly         : Zeroit.Framework.BasicCharts
// Author           : ZEROIT
// Created          : 12-11-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="WindowMessagesNativeMethods.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
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
