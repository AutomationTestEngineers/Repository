using OpenQA.Selenium;
using System;
using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Text;
using System.Data;
using System.Linq;
using ObjectRepository;
using ObjectRepository.Pages;
using Configuration;
using System.Collections.Generic;

namespace Automation
{
    [TestFixture]
    public class Common
    {
        #region fields
        protected IWebDriver driver;
        private Exception _exception;
        protected static ExtentReports extent;
        protected static ExtentHtmlReporter htmlReporter;
        protected static ExtentTest test;

        public  DataRow _testData;
        AgreementsPage _agreementsPage;
        SelectionPage _selectionPage;
        ProductsPage _productsPage;
        ApplicantsPage _applicantsPage;
        ReviewPage _reviewPage;
        FundingPage _fundingPage;
        VerificationPage _verificationPage;


        protected AgreementsPage AgreementsPage
        {
            get
            {
                if (_agreementsPage == null)
                    return _agreementsPage = new AgreementsPage(driver);
                return _agreementsPage;
            }
        }
        protected SelectionPage SelectionPage
        {
            get
            {
                if (_selectionPage == null)
                    return _selectionPage = new SelectionPage(driver);
                return _selectionPage;
            }
        }
        protected ProductsPage ProductsPage
        {
            get
            {
                if (_productsPage == null)
                    return _productsPage = new ProductsPage(driver);
                return _productsPage;
            }
        }        
        protected ApplicantsPage ApplicantsPage
        {
            get
            {
                if (_applicantsPage == null)
                    return _applicantsPage = new ApplicantsPage(driver);
                return _applicantsPage;
            }
        }
        protected ReviewPage ReviewPage
        {
            get
            {
                if (_reviewPage == null)
                    return _reviewPage = new ReviewPage(driver);
                return _reviewPage;
            }
        }
        protected FundingPage FundingPage
        {
            get
            {
                if (_fundingPage == null)
                    return _fundingPage = new FundingPage(driver);
                return _fundingPage;
            }
        }
        protected VerificationPage VerificationPage
        {
            get
            {
                if (_verificationPage == null)
                    return _verificationPage = new VerificationPage(driver);
                return _verificationPage;
            }
        }
        

        #endregion

        #region Properties

        #endregion

        #region Pre-Requisit
        [OneTimeSetUp]
        public void SetupReporting()
        {
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug\", "") + @"Reports\report.html");
            htmlReporter = new ExtentHtmlReporter(directory);

            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = "Document";
            htmlReporter.Config.ReportName = "Test Reuslt Report";

            /*htmlReporter.Configuration().JS = "$('.brand-logo').text('test image').prepend('<img src=@"file:///D:\Users\jloyzaga\Documents\FrameworkForJoe\FrameworkForJoe\Capgemini_logo_high_res-smaller-2.jpg"> ')";*/
            htmlReporter.Config.JS = "$('.brand-logo').text('').append('<img src=D:\\Users\\jloyzaga\\Documents\\FrameworkForJoe\\FrameworkForJoe\\Capgemini_logo_high_res-smaller-2.jpg>')";
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);            
            Questions.Collect("TestData\\Questions.xml", new List<string>() { "QUESTIONS" });
        }

        [OneTimeTearDown]
        public void GenerateReport()
        {
            extent.Flush();
        }

        [SetUp]
        public void Initialize()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            _testData = GetExcel_Data_With_TestName("TestData.xlsx", TestContext.CurrentContext.Test.Name);
            driver = (new WebDriver()).InitDriver(_testData[1].ToString());
        }

        [TearDown]
        public void CleanUp()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            var errorMessage = TestContext.CurrentContext.Result.Message;
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    string screenShotPath = SaveScreenShot(TestContext.CurrentContext.Test.Name);
                    test.Log(logstatus, stacktrace + errorMessage);
                    test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            if (driver != null)
                driver.Quit();
        }
        #endregion

        #region methods
        public void RunStep(Action action, string stepInfo, bool log = true,bool screenShot=true)
        {
            try
            {
                if (_exception == null)
                {
                    if (log)
                        StepLog(stepInfo, screenShot);
                    action();
                }
            }
            catch (Exception e)
            {
                _exception = e;
                throw new Exception("Exception : " + e.Message);
            }

        }

        public void RunStep<T>(Action<T> action, T parmaeter, string stepInfo, bool log = true, bool screenShot = true)
        {
            try
            {
                if (_exception == null)
                {
                    if (log)
                        StepLog(stepInfo, screenShot);
                    action(parmaeter);
                }
            }
            catch (Exception e)
            {
                _exception = e;
                throw new Exception("Exception : " + e.Message);
            }

        }
        public void RunStep<T>(Action<T, T> action, T parmaeter1, T parmaeter2, string stepInfo, bool log = true, bool screenShot = true)
        {
            try
            {
                if (_exception == null)
                {
                    if (log)
                        StepLog(stepInfo, screenShot);
                    action(parmaeter1, parmaeter2);
                }
            }
            catch (Exception e)
            {
                _exception = e;
                throw new Exception("Exception : " + e.Message);
            }

        }
        public void RunStep<T,T1>(Action<T, T1> action, T parmaeter1, T1 parmaeter2, string stepInfo, bool log = true, bool screenShot = true)
        {
            try
            {
                if (_exception == null)
                {
                    if (log)
                        StepLog(stepInfo, screenShot);
                    action(parmaeter1, parmaeter2);                    
                }
            }
            catch (Exception e)
            {
                _exception = e;
                throw new Exception("Exception : " + e.Message);
            }

        }

        private void StepLog(string stepInfo,bool screenShot)
        {
            if(screenShot)
                test.Log(Status.Pass, $"(Step : {stepInfo}) " + test.AddScreenCaptureFromPath(SaveScreenShot(stepInfo)));
            else
                test.Log(Status.Pass, "Step : " + stepInfo);
            //test.Log(Status.Info, "Step : " + stepInfo);
            //test.Log(Status.Pass, $"(Step : {stepInfo}) " + test.AddScreenCaptureFromPath(SaveScreenShot(stepInfo)));
            //test.Log(Status.Pass, $"(Step : {stepInfo}) " + test.AddScreenCaptureFromBase64String(driver.ScreenShotBase64()));
                       
        }
        #endregion

        private string SaveScreenShot(string screenshotFirstName)
        {
            var folderLocation = Path.Combine("C:\\evidence\\screenshots", "testresults" + DateTime.Now.ToString("yyyyMMdd"));
            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            //var filename =(custom == null) ? new StringBuilder(folderLocation) :new StringBuilder();
            var filename = new StringBuilder(folderLocation);
            filename.Append(screenshotFirstName);
            filename.Append(DateTime.Now.ToString("dd-mm-yyyy HH_mm_ss"));
            filename.Append(".png");

            filename = filename.Replace('|', ' ').Replace('}', ' ');
            screenshot.SaveAsFile(filename.ToString(), ScreenshotImageFormat.Png);

            return filename.ToString();
        }

        #region Excel Reading
        // Reading Excel Data Row based on  Test case Name
        public DataRow GetExcel_Data_With_TestName(string fileName, string testName)
        {
            DataRow data = null;
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            var filePath = directory + fileName;
            var dtContent = GetDataTableFromExcel(filePath);
            Console.WriteLine(" Data Read From File : " + fileName);
            for (int i = 0; i < dtContent.Rows.Count; i++)
            {
                if ((dtContent.Rows[i])[0].ToString().Contains(testName))
                {
                    data = dtContent.Rows[i];
                    break;
                }
            }
            return data;
        }

        // Reading Entire Excel File
        private DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.FirstOrDefault();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column{0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }
        #endregion

    }
}
