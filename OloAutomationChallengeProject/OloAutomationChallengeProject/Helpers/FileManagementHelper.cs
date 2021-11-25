using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OloAutomationChallengeProject.Helpers
{
    public class FileManagementHelper
    {
        /// <summary>
        /// Setting up the URL used for a Http aRequests
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="endpoint"></param>
        /// <returns>string</returns>
        public string SetupUrl(string baseUrl, string endpoint)
        {
            var url = Path.Combine(baseUrl, endpoint);
            return url;
        }
        /// <summary>
        /// Gets the path to files in the current project directory
        /// </summary>
        /// <param name="dataFolderName"></param>
        /// <param name="dataFileName"></param>
        /// <returns></returns>
        public string GetDataFilePath(string dataFolderName, string dataFileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), dataFolderName, dataFileName);
        }
    }
}
