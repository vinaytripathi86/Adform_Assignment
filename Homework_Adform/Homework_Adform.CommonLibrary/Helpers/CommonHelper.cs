using Homework_Adform.CommonLibrary.Constants;
using System;

namespace Homework_Adform.CommonLibrary.Helpers
{
    /// <summary>
    /// Helper class.
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>
        /// Get log file path.
        /// </summary>
        /// <returns>Returns log file path.</returns>
        public static string GetLogFilePath()
        {
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CommonConstants.LogFile);
        }
    }
}
