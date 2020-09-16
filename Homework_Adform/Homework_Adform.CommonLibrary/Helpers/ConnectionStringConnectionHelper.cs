using Homework_Adform.CommonLibrary.Constants;
using Microsoft.Extensions.Configuration;
using System;

namespace Homework_Adform.CommonLibrary.Helpers
{
    /// <summary>
    /// Helper class for db connection string.
    /// </summary>
    public static class ConnectionStringConnectionHelper
    {
        private static string _connectionString;
        /// <summary>
        /// Get db connection string.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        /// <returns>Returns connection string.</returns>
        public static string GetConnectionString(IConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CommonConstants.DatabaseFolderName);
                _connectionString = configuration.GetConnectionString(CommonConstants.SqlConnectionString).Replace(CommonConstants.DataDirectoryName, path);
            }

            return _connectionString;
        }
    }
}
