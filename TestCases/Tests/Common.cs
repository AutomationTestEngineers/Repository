using Configuration;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
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
   // [TestClass]
    public class Common
    {
        #region fields
        protected RemoteWebDriver driver { get; set; }
        private Exception _exception;
               
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
        

        //[TestCleanup]
        public void CleanUp()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
                //driver.Dispose();
                driver = null;
                _exception = null;
            }
        }
        #endregion

        #region Read Parameters

        string testParameterFile = "TestCase_Specific.xml";
        public void CollectSharedParameters()
        {
            string sharedFileName = "Parameter.xml";
            Parameter.Add<string>("SharedXML", sharedFileName);
            var collectionCriteria = new List<string>() { "/PARAMETER/SHARED", "/PARAMETER/QUESTIONS" };
            XmlParameterCollector.Collect(sharedFileName, collectionCriteria);

            // Collect Environment Based
            var environment = Parameter.Get<string>("Environment");
            var client = Parameter.Get<string>("Client");
            collectionCriteria = new List<string>() { $"/PARAMETER/CLIENT/{client}/{environment}" };
            XmlParameterCollector.Collect(sharedFileName, collectionCriteria);

            // Collect Test Case Specific Parameters
            //this.CollectTestSpecificParameters(TestName);
        }
        public void CollectTestSpecificParameters(string testName)
        {
            var collectionCriteria = new List<string>() { $"/PARAMETER/TESTSPECIFIC/{testName.Replace("(", "").Replace(")", "")}" };
            XmlParameterCollector.Collect(testParameterFile, collectionCriteria);
        }
        #endregion

        #region methods
        public void RunStep(Action action, string stepInfo, bool log = true, bool screenShot = true)
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
        public void RunStep<T, T1>(Action<T, T1> action, T parmaeter1, T1 parmaeter2, string stepInfo, bool log = true, bool screenShot = true)
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

        public void RunStep<T, T1>(Action<T, T1, T1> action, T parmaeter1, T1 parmaeter2, T1 parmaeter3, string stepInfo, bool log = true, bool screenShot = true)
        {
            try
            {
                if (_exception == null)
                {
                    if (log)
                        StepLog(stepInfo, screenShot);
                    action(parmaeter1, parmaeter2, parmaeter3);
                }
            }
            catch (Exception e)
            {
                _exception = e;
                throw new Exception("Exception : " + e.Message);
            }
        }
        public void RunStep<T, T1, T2>(Action<T, T1, T1, T2> action, T parmaeter1, T1 parmaeter2, T1 parmaeter3, T2 parmaeter4, string stepInfo, bool log = true, bool screenShot = true)
        {
            try
            {
                if (_exception == null)
                {
                    if (log)
                        StepLog(stepInfo, screenShot);
                    action(parmaeter1, parmaeter2, parmaeter3, parmaeter4);
                }
            }
            catch (Exception e)
            {
                _exception = e;
                throw new Exception("Exception : " + e.Message);
            }

        }

        private void StepLog(string stepInfo, bool screenShot)
        {
            if (screenShot)
            {
                //test.Log(Status.Pass, $"(Step : {stepInfo}) " + test.AddScreenCaptureFromPath(SaveScreenShot(stepInfo)));
                Console.WriteLine("Step : " + stepInfo);
                //test.Log(Status.Pass, "Step : " + stepInfo);
            }
            else
            {
                //test.Log(Status.Pass, "Step : " + stepInfo);
                Console.WriteLine("Step : " + stepInfo);
            }

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
    }
}
