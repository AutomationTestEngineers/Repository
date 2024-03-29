﻿using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Windows.Forms;
using Configuration;
using SeleniumExtras.WaitHelpers;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ObjectRepository
{
    public static class WebElementExtensions
    {
        public static By GetLocator(this IWebElement element)
        {
            var elementProxy = RemotingServices.GetRealProxy(element);
            var bysFromElement = (IReadOnlyList<object>)elementProxy
                .GetType()
                .GetProperty("Bys", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?
                .GetValue(elementProxy);
            return (By)bysFromElement[0];
        }

        public static By GetLocator(this IList<IWebElement> element)
        {
            var elementProxy = RemotingServices.GetRealProxy(element);
            var bysFromElement = (IReadOnlyList<object>)elementProxy
                .GetType()
                .GetProperty("Bys", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?
                .GetValue(elementProxy);
            return (By)bysFromElement[0];
        }

        public static IWebElement find(this IWebElement element,By by)
        {
            try
            {
                return element.FindElement(by);
            }
            catch { return null; }
        }
       
        public static bool Displayed(this IWebElement element)
        {
            try
            {
                Thread.Sleep(100);
                return element.Displayed;
            }
            catch { return false; }
        }

        public static void SelectDropDown(this IWebElement element, string option,string elementName, IWebDriver driver, bool js = false)
        {
            int count = 0;
            try
            {
                ScreenBusy(driver);
                Thread.Sleep(20);
                bool selected = false;
                if (!js)
                {
                    for (int i = 0; i < 25; i++)
                    {
                        count = i;
                        element.ClickCustom(elementName,driver);
                        var options = element.FindElements(By.TagName("option"));
                        Wait((d => d.FindElements(By.TagName("option")).Count() > 0), driver, 1);
                        foreach (var a in options)
                        {
                            if (a.Text.Trim() == option)
                            {
                                a.Click();
                                selected = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    Thread.Sleep(3000);
                    JavaScriptExecutor(string.Format(JSOperator.DropDown, option), element);
                }

                if (!selected)
                    throw new Exception($"[Root Cause] : Unable to Secting DropDown Option [{option}] On [{element.GetLocator()}] ## [Retry Count] : {count + 1}");

            }
            catch (Exception e)
            {
                Console.WriteLine($"[Root Cause] : Unable to Secting DropDown Option [{option}] On [{element.GetLocator()}]");
                throw new Exception(e.Message);
            }
        }

        public static string SelectComboBox(this IWebElement element, string option,string elementName, IWebDriver driver)
        {
            element.HighlightElement(driver);
            SelectElement select = new SelectElement(element);
            Wait((d => d.FindElements(By.TagName("option")).Count() > 0), driver, 1);
            option = option ?? select.Options[GenericUtils.GetRandomNumber(1, select.Options.Count-1)].Text.ToString().Trim();
            select.SelectByText(option);
            element.ScreenBusy(driver);
            return select.SelectedOption.Text.ToString().Trim();
        }

        public static string GetSelected(this IWebElement element,bool value = true)
        {
            SelectElement select = new SelectElement(element);
            if (value)
            {
                var a = select.SelectedOption.Text.ToString().Trim();
                return element.FindElements(By.TagName("option")).Where(opt => opt.Text.Contains(a)).FirstOrDefault().GetAttribute("value");                
            }
            return select.SelectedOption.Text.ToString().Trim();
        }

        public static IList<string> GetOptions(this IWebElement element, IWebDriver driver)
        {
            var options = element.FindElements(By.TagName("option"));
            Wait((d => d.FindElements(By.TagName("option")).Count() > 0), driver, 1);
            return options.Select(a=>a.Text).ToList();
        }

        public static void ClickCustom(this IWebElement element,string elementName, IWebDriver driver, bool js = false,bool clickable = true)
        {
            try
            {
                element.ScreenBusy(driver);
                if(clickable)
                    element.ElementToBeClickable(driver);
                element.HighlightElement(driver);
                if (!js)
                    element.Click();
                else
                    JavaScriptExecutor(JSOperator.Click, element);
                element.ScreenBusy(driver);
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Root Cause] : While Performing Click On [{element.GetLocator()}]");
                Console.WriteLine(e.StackTrace);
                throw new Exception(e.Message);
            }

        }

        public static void SendKeysWrapper(this IWebElement element, string text, string elementName,IWebDriver driver, bool js = false)
        {
            try
            {
                if (!js)
                {
                    ScreenBusy(driver);
                    element.HighlightElement(driver);
                    element.Clear();
                    Thread.Sleep(50);
                    element.SendKeys(text);
                    ScreenBusy(driver);
                }
                else
                {
                    JavaScriptExecutor(string.Format(JSOperator.SetValue,text), element);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Root Cause] : While Sending Text On [{element.GetLocator()}]");
                throw new Exception(e.Message);
            }
        }

        public static void ScreenBusy(this IWebElement element, IWebDriver driver)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Int16.Parse(Parameter.Get<string>("ScreenTimeOut"))));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(Parameter.Get<string>("ScreenBusy"))));                
            }
            catch { }
        }

        public static void ScreenBusy(IWebDriver driver)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Int16.Parse(Parameter.Get<string>("ScreenTimeOut"))));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(Parameter.Get<string>("ScreenBusy"))));
            }
            catch { }
        }

        public static void HighlightElement(this IWebElement element, IWebDriver driver)
        {
            if (bool.Parse(Parameter.Get<string>("Highlight").ToLower()))
            {
                int count = Int16.Parse( Parameter.Get<string>("HighlightCount"));
                for (int i = 0; i < count; i++)
                {
                    Thread.Sleep(3);
                    (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].setAttribute('style',arguments[1]);", element, "border: 5px solid blue;");
                    (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].setAttribute('style',arguments[1]);", element, "border: 0px solid blue;");
                }
            }
        }

        public static void Wait<TResult>(Func<IWebDriver, TResult> condition, IWebDriver driver, int seconds = 20)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
                wait.PollingInterval = TimeSpan.FromMilliseconds(500);
                wait.IgnoreExceptionTypes(typeof(Exception));
                wait.Until(condition);
            }
            catch { }            
        }

        public static void ElementToBeClickable(this IWebElement element, IWebDriver driver, int timeOut = 10)
        {
            try
            {
                Wait(ExpectedConditions.ElementToBeClickable(element), driver, timeOut);
            }
            catch { Console.WriteLine($"    Element Not Clickable [Locator] : {element.GetLocator()}"); }

        }

        public static IWebDriver GetWrappedDriver(this IWebElement element)
        {
            IWebDriver instance = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    Thread.Sleep(100);
                    instance = (element as IWrapsDriver ?? (IWrapsDriver)((IWrapsElement)element).WrappedElement).WrappedDriver;
                    break;
                }
                catch { continue; }
            }
            if (instance != null)
                return instance;
            else
                throw new Exception("[Info : WebDriver instace is not created]");
        }

        private static void JavaScriptExecutor(string pattern, IWebElement element)
        {
            var js = element.GetWrappedDriver() as IJavaScriptExecutor;
            js.ExecuteScript(pattern, element);
        }

        private static class JSOperator
        {
            public static string Click { get { return "arguments[0].click();"; } }
            public static string Clear { get { return "arguments[0].value = '';"; } }
            public static string SetValue { get { return "arguments[0].value = '{0}';"; } }
            public static string IsDisplayed { get { return "if(parseInt(arguments[0].offsetHeight) > 0 && parseInt(arguments[0].offsetWidth) > 0) return true; return false;"; } }
            public static string ValidateAttribute { get { return "return arguments[0].getAttribute('{0}');"; } }
            public static string ScrollToElement { get { return "arguments[0].scrollIntoView(true);"; } }
            public static string DropDown { get { return "var length = arguments[0].options.length;  for (var i=0; i<length; i++){{  if (arguments[0].options[i].text == '{0}'){{ arguments[0].selectedIndex = i; break; }} }}"; } }
            public static string GetText { get { return "return arguments[0].innerText"; } }
        }

    }
}
