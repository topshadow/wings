using System;
using static PInvoke.User32;
using static PInvoke.Gdi32;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using PInvoke;
using Wings.worker.Framework.Common.DTO;

namespace Wings.worker.Framework.Common.Service {
    public class Finder {
        [DllImport ("user32.dll")]
        private static extern bool EnumWindows (EnumWindowsProc enumProc, IntPtr lParam);
        public delegate bool EnumWindowsProc (IntPtr hWnd, IntPtr lParam);
        public static List<GameProcess> findGameProcesses (string title) {
            var windows = FindWindowsContainsText (title);
            var gameProcesses = from window in windows select new GameProcess (GetWindowText (window), window, "", "", "");
            return new List<GameProcess> (gameProcesses);

        }
        public static string getWindowTitle(IntPtr wind)
        {
            return GetWindowText(wind);
        }

        /// <summary> Find all windows that match the given filter </summary>
        /// <param name="filter"> A delegate that returns true for windows
        ///    that should be returned and false for windows that should
        ///    not be returned </param>
        public static List<IntPtr> FindWindows (EnumWindowsProc filter) {
            IntPtr found = IntPtr.Zero;
            List<IntPtr> windows = new List<IntPtr> ();

            EnumWindows (delegate (IntPtr wnd, IntPtr param) {
                if (filter (wnd, param)) {
                    // only add the windows that pass the filter
                    windows.Add (wnd);
                }

                // but return true here so that we iterate all windows
                return true;
            }, IntPtr.Zero);

            return windows;
        }

        /// <summary> Find all windows that contain the given title text </summary>
        /// <param name="titleText"> The text that the window title must contain. </param>
        public static List<IntPtr> FindWindowsContainsText (string titleText) {
            return FindWindows (delegate (IntPtr wnd, IntPtr param) {
                return GetWindowText (wnd).Contains (titleText);
            });
        }
    }

}