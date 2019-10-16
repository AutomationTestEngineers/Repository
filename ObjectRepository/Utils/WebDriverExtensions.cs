using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ObjectRepository
{
    public static class WebDriverExtensions
    {
        public static void SwitchToNewWindow(this IWebDriver driver)
        {
            string baseWindow = driver.CurrentWindowHandle;
            IList<string> windows = driver.WindowHandles.ToList();
            if (windows.Count > 1)
                windows.Remove(baseWindow);
            driver.SwitchTo().Window(windows.FirstOrDefault());
        }

        public static void SwitchWindowUsingWindowCount(this IWebDriver driver,int number) {
            string[] windows = driver.WindowHandles.ToArray();
            driver.SwitchTo().Window(windows[windows.Count()-1]);
        }
        

        private static IWebElement FindBy(IWebDriver driver, By by, int timeout)
        {
            driver.Wait(ExpectedConditions.ElementIsVisible(by), timeout);
            return driver.FindElement(by);
        }

        public static void Wait<TResult>(this IWebDriver driver, Func<IWebDriver, TResult> condition, int seconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(condition);
        }

        public static void TakeScreenshot(this IWebDriver driver, string fileNameBase)
        {
            try
            {
                var artifactDirectory = Path.Combine("C:\\evidence\\screenshots", "testresults" + DateTime.Now.ToString("yyyyMMdd"));
                if (!Directory.Exists(artifactDirectory)) Directory.CreateDirectory(artifactDirectory);
                ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;
                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();
                    string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + ".jpg");
                    Console.WriteLine($"[Screen Shot Path] {screenshotFilePath}");
                    var screenshotBase64 = screenshot.AsBase64EncodedString;
                    SaveByteArrayAsImage(screenshotFilePath, screenshotBase64);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("[ERROR]: Error while taking screenshot: {0}", ex);
            }
        }

        private static void SaveByteArrayAsImage(string screenshotFilePath, string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            { image = Image.FromStream(ms); }
            image.Save(screenshotFilePath, ImageFormat.Jpeg);
        }

        public static void GetScreenShot(this IWebDriver driver, string testName)
        {
            driver.TakeScreenshot($"Error_{testName}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}");
        }

        public static void ScrollPage(this IWebDriver driver, int x, int y)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollBy({x},{y})");
        }

        public static string ScreenShotBase64(this IWebDriver driver)
        {
            System.Threading.Thread.Sleep(1000);
            string screenshotBase64 = null;
            ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;
            if (takesScreenshot != null)
            {
                driver.Wait(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"),20);
                var screenshot = takesScreenshot.GetScreenshot();                
                screenshotBase64 = screenshot.AsBase64EncodedString;
               // bytes = Convert.FromBase64String(screenshotBase64);
            }
            return screenshotBase64;   
        }
    }
}
