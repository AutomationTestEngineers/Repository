using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectRepository;
using ObjectRepository.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCases.Tests
{
   [TestClass]
    public class Common
    {
        #region fields
        protected RemoteWebDriver driver { get; set; }
        private Exception _exception;
        protected static ExtentReports extent;
        protected static ExtentHtmlReporter htmlReporter;
        protected static ExtentTest test;
        static string environment;
        public TestContext TestContext { get; set; }

        WebDriver _webDriver;
        HomePage _homePage;
        AgreementsPage _agreementsPage;
        SelectionPage _selectionPage;
        ProductsPage _productsPage;
        ApplicantsPage _applicantsPage;
        ReviewPage _reviewPage;
        FundingPage _fundingPage;
        VerificationPage _verificationPage;
        ConfirmationPage _confirmationPage;

        #endregion

        #region Properties
        public string TestName { get; set; }
        public XmlParameterCollector XmlParameterCollector { get { return new XmlParameterCollector(); } }
        public WebDriver WebDriver
        {
            get
            {
                if(_webDriver==null)
                    _webDriver = new WebDriver();
                return _webDriver;
            }
        }
        public HomePage HomePage
        {
            get
            {
                if (_homePage == null)
                    return _homePage = new HomePage(driver);
                return _homePage;
            }
        }
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
        protected ConfirmationPage ConfirmationPage
        {
            get
            {
                if (_confirmationPage == null)
                    return _confirmationPage = new ConfirmationPage(driver);
                return _confirmationPage;
            }
        }
        #endregion

        #region Pre-Requisit
        [AssemblyInitialize]
        public static void StartUp(TestContext test)
        {
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug", ""), "Reports", "report1.html");
            htmlReporter = new ExtentHtmlReporter(directory);           
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Config.DocumentTitle = "Automation Test Reuslt Report";
            htmlReporter.Config.ReportName = "Automation Test Reuslt Report";
            
            htmlReporter.Config.JS = "$('.brand-logo').text('').append('<img src=D:\\Users\\jloyzaga\\Documents\\FrameworkForJoe\\FrameworkForJoe\\Capgemini_logo_high_res-smaller-2.jpg>')";
            extent = new ExtentReports();
            extent.AddSystemInfo("Host Name", Environment.MachineName);            
            extent.AddSystemInfo("User Name", Environment.UserName);
            extent.AttachReporter(htmlReporter);
        }

        [AssemblyCleanup]
        public static void GenerateReport()
        {
            extent.AddSystemInfo("Environment", environment);
            extent.Flush();
        }

        [TestInitialize]
        public virtual void Initialize()
        {
            TestName = TestContext.TestName;
            Parameter.Add<string>("TestName", TestName);

            // Log Report Creation
            var folderLocation = Path.Combine(new FileInfo(AppDomain.CurrentDomain.BaseDirectory).Directory.Parent.FullName, "Reports", "Log_" + DateTime.Now.ToString("MM_dd_yyyy"));
            if (!Directory.Exists(folderLocation))
                Directory.CreateDirectory(folderLocation);
            Logger.CreateTestLogFile(Path.Combine(folderLocation, TestName + ".txt"));
            Logger.BeginTestIteration(TestName);
            test = extent.CreateTest($"{TestName} <a href='{Path.Combine(folderLocation, TestName + ".txt")}' target='_blank'> click here for log</a>");            
        }              

        [TestCleanup]
        public void CleanUp()
        {            
            var status = TestContext.CurrentTestOutcome;
            Status logstatus;
            switch (status)
            {
                case UnitTestOutcome.Failed:
                    logstatus = Status.Fail;
                    string name = SaveScreenShot(TestName);    
                    if(name.Contains("WebDriver may not exist"))
                        test.Log(logstatus, name);
                    else
                        test.Log(logstatus, $"<a href='{name}' target='_blank'> click here for screen shot</a>" + "   " + test.AddScreenCaptureFromPath(name, TestName));
                    break;
                case UnitTestOutcome.Inconclusive:
                    logstatus = Status.Warning;
                    test.Log(logstatus);
                    break;
                case UnitTestOutcome.Aborted:
                    logstatus = Status.Skip;
                    test.Log(logstatus);
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
                _exception = null;
            }
            Logger.EndTestIteration(logstatus);
            Parameter.Clear();
        }
        #endregion

        #region Read Parameters
        string sharedFileName = "Parameter.xml";
        string testParameterFile = "TestCase_Specific.xml";

        public void CollectAllParameters()
        {
            ReadSharedParameters();
            ReadClientParameters();
            ReadTestSpecificParameters(TestName);
        }
        public void ReadSharedParameters()
        {            
            Parameter.Add<string>("SharedXML", sharedFileName);
            var collectionCriteria = new List<string>() { "/PARAMETER/SHARED", "/PARAMETER/QUESTIONS" };
            XmlParameterCollector.Collect(sharedFileName, collectionCriteria);

        }
        public void ReadClientParameters(string client=null)
        {
            // Collect Environment Based
            client = (client == null) ? Parameter.Get<string>("Client") : client;
            environment = Parameter.Get<string>("Environment");            
            var collectionCriteria = new List<string>() { $"/PARAMETER/CLIENT/{client}/{environment}" };
            XmlParameterCollector.Collect(sharedFileName, collectionCriteria);
        }
        public void ReadTestSpecificParameters(string testName)
        {
            /// <Summary>
            /// Based On Test Case Name
            /// Note : <TestName>  should be present in TestCase_Specific.xml file 
            testName = (testName == null) ? Parameter.Get<string>("TestName") : testName;
            var collectionCriteria = new List<string>() { $"/PARAMETER/TESTSPECIFIC/{testName}" };
            XmlParameterCollector.Collect(testParameterFile, collectionCriteria);
        }
        #endregion

        #region Step methods
        public void RunStep(Action action, string stepInfo, bool log = true, bool screenShot = false)
        {
            try
            {
                if (_exception == null)
                {
                    Logger.StepLog(stepInfo);
                    action();
                    if (log)
                        StepLog(stepInfo, screenShot);
                }
            }
            catch (Exception e)
            {
                LogException(stepInfo, e);
            }

        }

        public void RunStep<T>(Action<T> action, T parmaeter, string stepInfo, bool log = true, bool screenShot = false)
        {
            try
            {
                if (_exception == null)
                {
                    Logger.StepLog(stepInfo);
                    action(parmaeter);
                    if (log)
                        StepLog(stepInfo, screenShot);                    
                }
            }
            catch (Exception e)
            {
                LogException(stepInfo, e);
            }

        }       
       
        public void RunStep<T, T1, T2>(Action<T, T1, T1, T2> action, T parmaeter1, T1 parmaeter2, T1 parmaeter3, T2 parmaeter4, string stepInfo, bool log = true, bool screenShot = false)
        {
            try
            {
                if (_exception == null)
                {
                    Logger.StepLog(stepInfo);
                    action(parmaeter1, parmaeter2, parmaeter3, parmaeter4);
                    if (log)
                        StepLog(stepInfo, screenShot);                    
                }
            }
            catch (Exception e)
            {
                LogException(stepInfo,e);
            }

        }

        private void StepLog(string stepInfo, bool screenShot)
        {
            if (screenShot)
            {
                test.Log(Status.Pass, $"(Step : {stepInfo}) " + test.AddScreenCaptureFromPath(SaveScreenShot(stepInfo)));
                test.Log(Status.Pass, "Step : " + stepInfo);
            }
            else
            {                
                test.Log(Status.Pass, "Step : " + stepInfo);                
            }
                
        }

        private void LogException(string stepInfo,Exception e)
        {
            test.Log(Status.Fail, $"Step : {stepInfo}, [Exception] : {e.Message}");            
            Logger.Log("Exception : " + e);
            _exception = e;
            throw new Exception("Exception : " + e.Message);
        }
        #endregion

        private string SaveScreenShot(string screenshotFirstName,string filePrefix= "TestFailure")
        {
            try
            {
                var folderLocation = Path.Combine("C:\\evidence\\screenshots", filePrefix+"_On_" + DateTime.Now.ToString("yyyy_MM_dd")+"\\");
                if (!Directory.Exists(folderLocation))
                    Directory.CreateDirectory(folderLocation);
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var filename = new StringBuilder(folderLocation);
                filename.Append(screenshotFirstName);
                filename.Append(DateTime.Now.ToString("dd-mm-yyyy HH_mm_ss"));
                filename.Append(".png");

                filename = filename.Replace('|', ' ').Replace('}', ' ');
                screenshot.SaveAsFile(filename.ToString(), ScreenshotImageFormat.Png);
                return filename.ToString();
            }
            catch(Exception e)
            {
                return "ScreenShot not to capture WebDriver may not exist, Exception : "+e.Message;
            }
        }
    }
}
