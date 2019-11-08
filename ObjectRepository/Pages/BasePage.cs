using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using Configuration;
using System.Collections.Generic;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using FluentAssertions;

namespace ObjectRepository.Pages
{

    public class  BasePage
    {        
        protected IWebDriver driver;
        protected Actions actions;
        protected int minTimeOut = 10;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this.driver, this);
            actions = new Actions(driver);            
            this.ScreenBusy();
        }
        public WebDriverWait WebDriverWait
        {
            get
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(minTimeOut));
                wait.PollingInterval = TimeSpan.FromMilliseconds(500);
                wait.IgnoreExceptionTypes(typeof(Exception));
                return wait;
            }
        }

        public void Wait<TResult>(Func<IWebDriver, TResult> condition, int seconds = 15)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
                wait.PollingInterval = TimeSpan.FromMilliseconds(500);
                wait.IgnoreExceptionTypes(typeof(Exception));
                wait.Until(condition);
            }catch(Exception e) { }            
        }
        public IWebElement FindBy(By by, int i = 5, bool exist = false)
        {
            try
            {
                if (exist) { Sleep(i * 300); return driver.FindElement(by); }
                Wait(ExpectedConditions.ElementExists(by), i);
                return driver.FindElement(by);
            }
            catch (StaleElementReferenceException ex)
            {
                return FindBy(by);
            }
            catch (NoSuchElementException e)
            {
                return null;
            }
        }
        public IList<IWebElement> FindElements(By by, int i = 5, bool exist = false)
        {
            try
            {
                if (exist) { Sleep(i * 500); return driver.FindElements(by); }
                Wait(ExpectedConditions.ElementExists(by), i);
                return driver.FindElements(by);
            }
            catch (StaleElementReferenceException ex)
            {
                return FindElements(by);
            }
            catch (NoSuchElementException e)
            {
                return null;
            }
        }
        public void ScreenBusy(int timeout = 120)
        {
            Sleep(200);
            Wait(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(Parameter.Get<string>("ScreenBusy"))),timeout);
        }

        public void Sleep(int timeout = 1000)
        {
            Thread.Sleep(timeout);
        }

        public void Verify(bool condition,string name,string actual,string expected)
        {
            condition.Should().BeTrue($"[{name}] => Actual [{actual}]  Expected[{expected}]");
            Logger.LogVerify($"{name} : Actual [{actual}]  Expected [{expected}]");
        }
    }
}
