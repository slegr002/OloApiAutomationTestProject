using OloAutomationChallengeProject.Helpers;
using OloAutomationChallengeProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;

namespace OloAutomationChallengeProject
{
    /// <summary>
    /// This class is used to create new instances of classes in one location
    /// </summary>
    public class Factory
    {
        private HttpClient client;
        private User user;
        private RequestHelpers requestHelpers;
        private SqlConnection sqlConnection;

        public HttpClient CreateNewClient()
        {
            return client = new HttpClient();
        }

        public User CreateNewUser()
        {
            return user = new User();
        }

        public RequestHelpers CreateNewRequestHelpers()
        {
            return requestHelpers = new RequestHelpers();
        }

        public SqlConnection CreateNewSqlConnection(string connection)
        {
            return sqlConnection = new SqlConnection(connection);
        }
    }
}
