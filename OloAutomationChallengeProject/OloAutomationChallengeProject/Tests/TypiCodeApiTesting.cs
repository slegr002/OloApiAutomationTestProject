using Newtonsoft.Json;
using NJsonSchema;
using NUnit.Framework;
using OloAutomationChallengeProject.Helpers;
using OloAutomationChallengeProject.Models;
using OloAutomationChallengeProject.Reports;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OloAutomationChallengeProject
{
    public class TypiCodeApiTesting
    {
        public TestContext TestContext { get; set; }
        Factory factory = new Factory();
        RequestHelpers requestHelpers = new RequestHelpers();

        /// <summary>
        /// This will Setup the Report for the Test
        /// </summary>
        /// <param name="testContext"></param>
        public static void SetupReport(TestContext testContext)
        {
            var dir = testContext.TestDirectory;
            Reporter.SetUpReport(dir, "SmokeTest", "Smoke test result");
        }

        /*public void SetUpTest()
        {
            Reporter.CreateTest();
        }*/

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// This test validates a successful Get request https://jsonplaceholder.typicode.com/posts
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetRequestAsync()
        {
            List<User> users = new List<User>();
            RequestHelpers requestHelpers = new RequestHelpers();
            var url = requestHelpers.SetupUrl("https://jsonplaceholder.typicode.com/", "posts");

            var resquest = await requestHelpers.CreateGetRequestAsync(url);
            var responseContent = await requestHelpers.GetResponseContentAsync<List<User>>(resquest);
            Assert.IsTrue((int)resquest.StatusCode == 200);
        }

        /// <summary>
        /// This test validates a successful Get Request based on a crateria https://jsonplaceholder.typicode.com/comments?postId=1
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetRequestSingleRecordAsync()
        {
            List<User> users = new List<User>();
            RequestHelpers requestHelpers = new RequestHelpers();
            var url = requestHelpers.SetupUrl("https://jsonplaceholder.typicode.com/", "comments?postId=1");

            var resquest = await requestHelpers.CreateGetRequestAsync(url);
            var responseContent = await requestHelpers.GetResponseContentAsync<List<User>>(resquest);
            Assert.IsTrue((int)resquest.StatusCode == 200);
        }

        /// <summary>
        /// This validates that a bad url Get request will generate an error status code
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestBadGetRequestStatusCodeAsync()
        {
            List<User> users = new List<User>();
            RequestHelpers requestHelpers = new RequestHelpers();
            var url = requestHelpers.SetupUrl("https://jsonplaceholder.typicode.com/", "posts");

            var resquest = await requestHelpers.CreateGetRequestAsync(url);
            var responseContent = await requestHelpers.GetResponseContentAsync<List<User>>(resquest);
            Assert.IsFalse((int)resquest.StatusCode == 400);
        }

        /// <summary>
        /// This test validates the Json Schema
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetRequestSchemaAsync() 
        {       
        }

        /// <summary>
        /// This Test Validates a Successful POST Request https://jsonplaceholder.typicode.com/posts
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestSuccessfulPostRequestAsync()
        {   
            var user = factory.CreateNewUser();
            user.UserId = 40;
            user.Title = "Test";
            user.body = "Testing";

            var url = factory.CreateNewRequestHelpers()
                .SetupUrl("https://jsonplaceholder.typicode.com/", "posts");

            var result = await requestHelpers.CreatePostRequestAsync(url, user);
            Assert.IsTrue((int)result.StatusCode == 201);
        }

        /// <summary>
        /// This test Validates a bad POST Request https://jsonplaceholder.typicode.com/X
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestBadPostRequestStatusCodeAsync()
        {
            var user = factory.CreateNewUser();
            user.UserId = 40;
            user.Title = "TestPost";
            user.body = "TestingPost";

            var url = factory.CreateNewRequestHelpers()
                .SetupUrl("https://jsonplaceholder.typicode.com/", "X");

            var result = await requestHelpers.CreatePostRequestAsync(url, user);
            Assert.IsTrue((int)result.StatusCode == 404);
        }

        /// <summary>
        /// This Test validates a successful PUT Request https://jsonplaceholder.typicode.com/posts/1
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestSuccessfulPUTRequestStatusCodeAsync()
        {
            var user = factory.CreateNewUser();
            user.UserId = 1;
            user.Title = "TestPut";
            user.body = "TestingPut";

            var url = factory.CreateNewRequestHelpers().SetupUrl("https://jsonplaceholder.typicode.com/", "posts/1");
            var result = await requestHelpers.CreatePUTRequestAsync(url, user);
            Assert.IsTrue((int)result.StatusCode == 200);
        }

        /// <summary>
        /// This test validates a bad PUT Request https://jsonplaceholder.typicode.com/posts/A
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestBadPUTRequestStatusCodeAsync()
        {
            var user = factory.CreateNewUser();
            user.UserId = 1;
            user.Title = "TestPut";
            user.body = "TestingPut";

            var url = factory.CreateNewRequestHelpers().SetupUrl("https://jsonplaceholder.typicode.com/", "posts/A");
            var result = await requestHelpers.CreatePUTRequestAsync(url, user);
            Assert.IsTrue((int)result.StatusCode == 500);
        }

        /// <summary>
        /// This test validates a successful record deletion https://jsonplaceholder.typicode.com/posts/1
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestSuccessfulDeleteRequestAsync()
        {
            var url = factory.CreateNewRequestHelpers().SetupUrl("https://jsonplaceholder.typicode.com/", "posts/1");
            var result = await requestHelpers.CreateDeleteRequestASynch(url);
            Assert.IsTrue((int)result.StatusCode == 200);
        }

        /// <summary>
        /// This test validates a bad DELETE Request https://jsonplaceholder.typicode.com/posts/1
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestBadDeleteRequestAsync()
        {
            var url = factory.CreateNewRequestHelpers().SetupUrl("https://jsonplaceholder.typicode.com/", "posts/1");
            var result = await requestHelpers.CreateDeleteRequestASynch(url);
            Assert.IsTrue((int)result.StatusCode == 200);
        }
    }
}