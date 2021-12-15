using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Text;

namespace OloAutomationChallengeProject.Reports
{
    //This class will setup Extent Report for the Test 
    
    public static class Reporter
    {
        private static ExtentReports extentReport;
        private static ExtentHtmlReporter htmlReporter;
        private static ExtentTest extentTest;

        /// <summary>
        /// Method to setup the report and it's configurations
        /// </summary>
        /// <param name="path"></param>
        /// <param name="reportConfPath"></param>
        /// <param name="documentTitle"></param>
        /// <param name="reportName"></param>
        public static void SetUpReport(string path, string reportConfPath, string documentTitle, string reportName)
        {
            htmlReporter = new ExtentHtmlReporter(path);
            htmlReporter.LoadConfig(reportConfPath);
            extentReport = new ExtentReports();
            extentReport.AttachReporter(htmlReporter);
        }

        /// <summary>
        /// Method used for setting the test name and test description (optional)
        /// </summary>
        /// <param name="testName"></param>
        public static void CreateTest(string testName)
        {
            extentTest = extentReport.CreateTest(testName);
        }

        /// <summary>
        /// Method Used to Log the test results status and message to the test result report
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public static void LogToReport(Status status, string message)
        {
            extentTest.Log(status, message);
        }

        /// <summary>
        /// Method writes (or updates) the specified reporter’s 
        /// test information (i.e., Extent HTML Reporter) to the destination type. 
        /// </summary>
        public static void FlushReport()
        {
            extentReport.Flush();
        }

        public static void TestStatus(string status)
        {
            if (status.Equals("Pass"))
            {
                extentTest.Pass("Test case passed");
            }
            else
            {
                extentTest.Fail("test case failed");
            }
        }
    }
}
