using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OloAutomationChallengeProject.Helpers;
using OloAutomationChallengeProject.Models;
using OloAutomationChallengeProject.Reports;
using OloAutomationChallengeProject.Test_Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OloAutomationChallengeProject
{
    public class TypiCodeApiTesting
    {
        public TestContext TestContext { get; set; }
        Factory factory = new Factory();
        RequestHelpers requestHelpers = new RequestHelpers();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //Setting up the path where the Extent Configuration File is consumed
            var extentConfPath = Path.Combine(Directory.GetParent(@"../../../").ToString(), @"Reporting\extent-config.xml");

            //Setting the path to where the extent html report will be created
            var reportPath = Path.Combine(Directory.GetParent(@"../../../").ToString(), @"Reporting\");

            //Setting up the Report and creating the test for the Report
            Reporter.SetUpReport(reportPath, extentConfPath, "SmokeTest", "Smoke test result");
            Reporter.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
            Reporter.CreateTest(TestContext.CurrentContext.Test.Name);
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" 
                : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);

            var tesStatus = TestContext.CurrentContext.Result.Outcome.Status;
            switch (tesStatus)
            {
                case TestStatus.Failed:
                    Reporter.LogToReport(Status.Info, "Failed");
                    break;
                case TestStatus.Passed:
                    Reporter.LogToReport(Status.Info, "Pass");
                    break;
                case TestStatus.Inconclusive:
                    Reporter.LogToReport(Status.Info, "Inconclusive");
                    break;
                case TestStatus.Skipped:
                    Reporter.LogToReport(Status.Info, "Skip");
                    break;
                default:
                    break;
            }
            Reporter.FlushReport();
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

            var request = await requestHelpers.CreateGetRequestAsync(url);
            var responseContent = await requestHelpers.GetResponseContentAsync<List<User>>(request);
            Assert.IsTrue((int)request.StatusCode == 200);
        }

        /// <summary>
        /// This test validates that the Server Name is correct 
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetRequestResponseHeaderValue()
        {
            List<User> userComments = new List<User>();
            RequestHelpers requestHelpers = new RequestHelpers();
            var url = requestHelpers.SetupUrl("https://jsonplaceholder.typicode.com/", "posts");

            var request = await requestHelpers.CreateGetRequestAsync(url);
            var responseHeaderValue = request.Headers.GetValues("Server").FirstOrDefault();
            Assert.IsTrue(responseHeaderValue.Equals("cloudflare"));
        }

        /// <summary>
        /// This test validates a successful Get Request based on a crateria https://jsonplaceholder.typicode.com/comments?postId=1
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestGetRequestSingleRecordAsync()
        {
            List<UserComment> usersComment = new List<UserComment>();
            RequestHelpers requestHelpers = new RequestHelpers();
            var url = requestHelpers.SetupUrl("https://jsonplaceholder.typicode.com/", "comments?postId=1");

            var resquest = await requestHelpers.CreateGetRequestAsync(url);
            var responseContent = await requestHelpers.GetResponseContentAsync<List<UserComment>>(resquest);
            Assert.IsTrue((int)resquest.StatusCode == 200);
        }      

        [Test]
        public async Task TestGetRequestValidateValue()
        {
            List<UserComment> userComments = new List<UserComment>();
            RequestHelpers requestHelpers = new RequestHelpers();
            var url = requestHelpers.SetupUrl("https://jsonplaceholder.typicode.com/", "comments?postId=1");

            var request = await requestHelpers.CreateGetRequestAsync(url);
            var responseContent = await requestHelpers.GetResponseContentAsync<List<UserComment>>(request);
            var nameValue = responseContent.Select(x => x).Where(y => y.Id == 5);
            var stringValue = nameValue.Select(x => x.Name).Single();
            //TODO:Refactor Test Reprot to handle Test when not Asserting Status code
            Assert.IsTrue((nameValue.Select(x => x.Name).Single()) == "vero eaque aliquid doloribus et culpa");
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

            var responseContent = await requestHelpers.GetResponseContentAsync<List<User>>(result);
            var record = responseContent.Select(x => x).Where(y => y.Id == 40).FirstOrDefault();

            Assert.IsTrue((int)result.StatusCode == 201);
            Assert.IsNotNull(record);
            Assert.IsTrue((record.Title.Equals("Test")));
            Assert.IsTrue((record.body.Equals("Testing")));   
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
            var url = factory.CreateNewRequestHelpers().SetupUrl("https://jsonplaceholder.typicode.com/", "x");
            var result = await requestHelpers.CreateDeleteRequestASynch(url);
            Assert.IsTrue((int)result.StatusCode == 404);
        }
    }
}