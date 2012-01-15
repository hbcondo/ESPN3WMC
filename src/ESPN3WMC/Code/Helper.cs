using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Microsoft.MediaCenter.UI;
using Microsoft.MediaCenter.Hosting;

using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ESPN3WMC
{
    public class Helper : ModelItem
    {
        #region Fields

        /* http://msdn.microsoft.com/en-us/library/ms633548%28v=vs.85%29.aspx */
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImportAttribute("User32.DLL")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion

        #region Public Methods

        /// <summary>
        /// Opens a web page in a new broswer outside of media center.
        /// </summary>
        /// <param name="url">The website address to open</param>
        public static void OpenWebpage(string url)
        {            
            // attempt to open web page
            try
            {
                AddInHost.Current.MediaCenterEnvironment.NavigateToPage(Microsoft.MediaCenter.PageId.ExtensibilityUrl, url);
            }

            catch (Exception ex)
            {
                // log error
                Helper.WriteToLogFile(string.Concat(ex.Message, " - ", ex.StackTrace));
            }
        }

        /// <summary>
        /// Writes to log file.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteToLogFile(string message)
        {
            string fileName = Properties.Settings.Default.LogFilename;
            string fileDirectory = GetTempPath();
            string fileLocation = string.Concat(fileDirectory, fileName);
            string fileText = string.Concat(DateTime.Now.ToString(), "\n", message, "\n\n");

            File.AppendAllText(fileLocation, fileText);
        }

        /// <summary>
        /// Gets the temp path.
        /// </summary>
        /// <returns>string</returns>
        public static string GetTempPath()
        {
            string path = System.Environment.GetEnvironmentVariable("TEMP");

            if (!path.EndsWith("\\")) path += "\\";
                return path;
        }

        #endregion
    }
}
