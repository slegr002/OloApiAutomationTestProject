using OloAutomationChallengeProject.Helpers;
using OloAutomationChallengeProject.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace OloAutomationChallengeProject
{
    public class Factory
    {
        private HttpClient client;
        private User user;
        private HttpRequestHelpers requestHelpers;
        private FileManagementHelper fileManagementHelper;
        public HttpClient CreateNewClient()
        {
            return client = new HttpClient();
        }

        public User CreateNewUser()
        {
            return user = new User();
        }

        public HttpRequestHelpers CreateNewRequestHelpers()
        {
            return requestHelpers = new HttpRequestHelpers();
        }

        public FileManagementHelper CreateNewFileManagementHelper()
        {
            return fileManagementHelper = new FileManagementHelper();
        }
    }
}
