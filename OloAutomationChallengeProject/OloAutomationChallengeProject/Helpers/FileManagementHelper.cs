using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OloAutomationChallengeProject.Helpers
{
    public class FileManagementHelper
    {
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
