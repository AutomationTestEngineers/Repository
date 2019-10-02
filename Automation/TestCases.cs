using System;
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using System.Data;
using OpenQA.Selenium.Chrome;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;

namespace Automation
{
    [TestFixture]
    public class TestCases : Common
    {
        [SetUp]
        public void Setup()
        {
            base.Initialize();
        }
        [TearDown]
        public void TearDown()
        {
            base.CleanUp();
        }

        #region TestCases
        [Test]
        public void Gmail_1()
        {
            try
            {
                string url = _testData[1].ToString();
                string username = _testData[2].ToString();
                string password = _testData[3].ToString();

                test.Log(Status.Pass, $"Step : Read Excel File");
                LaunchWebDriver(url);
                test.Log(Status.Pass, "Step : Launching Chrome Browser With Url: " + url);
                test.Log(Status.Pass, "Step : Page Title : " + driver.Title);
                Assert.IsTrue(driver.Title.Equals("Gmail"), "Title not Matched");    // Aseertion
                Gmail_Login(username, password);
                test.Log(Status.Pass, "Step : Page Title : " + driver.Title);
                Assert.IsTrue(driver.Title.Equals("Gmail"),"Title not Matched");    // Aseertion
                test.Log(Status.Pass, $"Step : Logged in With UserName : '{username}, Password: '{password}'");
                //LogoutFrom_Gmail();
                //test.Log(Status.Pass, "Step : Log Out From Gmail");
            }
            catch(Exception e)
            {
                throw new Exception("Exception : " + e.Message);
            }  
        }

        [Test]
        public void Gmail_2()
        {
            try
            {
                string url = _testData[1].ToString();
                string username = _testData[2].ToString();
                string password = _testData[3].ToString();

                test.Log(Status.Pass, $"Step : Read Excel File");
                LaunchWebDriver(url);
                test.Log(Status.Pass, "Step : Launching Chrome Browser With Url: " + url);
                test.Log(Status.Pass, "Step : Page Title : " + driver.Title);
                Assert.IsTrue(driver.Title.Equals("Gmail"), "Title not Matched");    // Aseertion
                Gmail_Login(username, password);
                test.Log(Status.Pass, "Step : Page Title : " + driver.Title);
                Assert.IsTrue(driver.Title.Equals("Gmail"), "Title not Matched");    // Aseertion
                test.Log(Status.Pass, $"Step : Logged in With UserName : '{username}, Password: '{password}'");
                //LogoutFrom_Gmail();
                //test.Log(Status.Pass, "Step : Log Out From Gmail");
            }
            catch (Exception e)
            {
                throw new Exception("Exception : " + e.Message);
            }
        }

        [Test]
        public void Facebook_1()
        {
            try
            {
                string url = _testData[1].ToString();
                string username = _testData[2].ToString();
                string password = _testData[3].ToString();

                //test.Log(Status.Pass, $"Step : Read Excel File");
                //LaunchWebDriver(url);
                //test.Log(Status.Pass, "Step : Launching Chrome Browser With Url: " + url);
                //test.Log(Status.Pass, "Step : Page Title : " + driver.Title);
                //Assert.IsTrue(driver.Title.ToLower().Equals("Facebook – log in or sign up".ToLower()), "Title not Matched");// Aseertion
                //Facebook_Login(username,password);
                //test.Log(Status.Pass, "Step : Page Title : " + driver.Title);
                //Assert.IsTrue(driver.Title.Equals("Facebook"), "Title not Matched");    // Aseertion
                //test.Log(Status.Pass, $"Step : Logged in With UserName : '{username}, Password: '{password}'");
                //LogoutFrom_Facebook();
                //test.Log(Status.Pass,"Step : Log Out From Facebook");
            }
            catch (Exception e)
            {
                throw new Exception("Exception : " + e.Message);
            }
        }

        [Test]
        public void Facebook_2()
        {
            try
            {                
                string url = _testData[1].ToString();
                string username = _testData[2].ToString();
                string password = _testData[3].ToString();

                test.Log(Status.Pass, $"Step : Read Excel File");
                LaunchWebDriver(url);
                test.Log(Status.Pass, "Step : Launching Chrome Browser With Url: " + url);
                test.Log(Status.Pass, "Step : Page Title : " + driver.Title);
                Assert.IsTrue(driver.Title.ToLower().Equals("Facebook – log in or sign up".ToLower()), "Title not Matched");// Aseertion
                Facebook_Login(username, password);
                test.Log(Status.Pass, "Step : Page Title : " + driver.Title);
                Assert.IsTrue(driver.Title.Equals("Facebook"), "Title not Matched");    // Aseertion
                test.Log(Status.Pass, $"Step : Logged in With UserName : '{username}, Password: '{password}'");
                LogoutFrom_Facebook();
                test.Log(Status.Pass, "Step : Log Out From Facebook");
            }
            catch (Exception e)
            {
                throw new Exception("Exception : " + e.Message);
            }
        }

        [Test]
        public void Facebook_3()
        {
            try
            {                
                string url = _testData[1].ToString();
                string username = _testData[2].ToString();
                string password = _testData[3].ToString();

                test.Log(Status.Pass, $"Step : Read Excel File");
                LaunchWebDriver(url);
                test.Log(Status.Pass, "Step : Launching Chrome Browser With Url: " + url);
                test.Log(Status.Pass, "Step : Page Title : " + driver.Title);
                Assert.IsTrue(driver.Title.ToLower().Equals("Facebook – log in or sign up".ToLower()), "Title not Matched");// Aseertion
                Facebook_Login(username, password);
                test.Log(Status.Pass, "Step : Page Title : " + driver.Title);
                Assert.IsTrue(driver.Title.Equals("Facebook"), "Title not Matched");    // Aseertion
                test.Log(Status.Pass, $"Step : Logged in With UserName : '{username}, Password: '{password}'");
                LogoutFrom_Facebook();
                test.Log(Status.Pass, "Step : Log Out From Facebook");
            }
            catch (Exception e)
            {
                throw new Exception("Exception : " + e.Message);
            }
        }

        #endregion

        

        #region Selenium Stuff
        public void LaunchWebDriver(string url)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-notifications"); // to disable notification
            options.AddArguments("--disable-application-cache"); // to disable cache
            options.AddArguments("test-type");
            options.AddArguments("no-sandbox");
            options.AddArguments("--disable-plugins");
            options.AddArguments("--enable-precise-memory-info");
            options.AddArguments("--disable-popup-blocking");
            options.AddArguments("test-type=browser");
            options.AddAdditionalCapability("useAutomationExtension", false);
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddExcludedArguments(new List<string>() { "enable-automation" });
            driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(120));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
        }

        public void Facebook_Login(string userName, string password)
        {
            Find(By.Id("email")).SendKeys(userName);
            Find(By.Id("pass")).SendKeys(password);
            Find(By.Id("loginbutton")).Click();
        }

        public void Gmail_Login(string userName, string password)
        {
            Find(By.Id("identifierId")).SendKeys(userName);
            Wait(ExpectedConditions.ElementToBeClickable(By.Id("identifierNext")));
            Find(By.Id("identifierNext")).Click();
            Find(By.Name("password")).SendKeys(password);
            Find(By.Id("passwordNext")).Click();
            if(Find(By.XPath("//div[text()='Confirm your recovery email']"))!=null)
            {
                Find(By.XPath("//div[text()='Confirm your recovery email']")).Click();
            }
        }

        public void LogoutFrom_Gmail()
        {
            Find(By.XPath("(//header[@id='gb']//a)[5]")).Click();
            Find(By.XPath("//a[text()='Sign out']")).Click();

        }

        public void LogoutFrom_Facebook()
        {
            Find(By.CssSelector("a[aria-labelledby='userNavigationLabel']")).Click();
            Find(By.XPath("//ul[@role='menu']/li[last()]/a")).Click();
        }

        public IWebElement Find(By by)
        {
            try
            {
                return driver.FindElement(by);
            }
            catch (StaleElementReferenceException e) { return Find(by); }
            catch(NoSuchElementException e) {return null;}
            
        }

        public void Wait<TResult>(Func<IWebDriver, TResult> condition, int seconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(condition);
        }

        #endregion
    }
}
