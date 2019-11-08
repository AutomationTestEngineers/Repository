using Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace ObjectRepository
{
    public class WebDriver
    {
        public virtual IWebDriver InitDriver(string url)
        {
            IWebDriver driver;
            var browserType = Parameter.Get<string>("Browser");

            switch (browserType)
            {
                case "chrome":
                    //ChromeDriverService service = ChromeDriverService.CreateDefaultService();
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
                    //driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(Int16.Parse(Parameter.Get<string>("BrowserLoad"))));
                    driver = new ChromeDriver(options);
                    break;
                default:
                    throw new ArgumentException($"Browser Option {browserType} Is Not Valid - Use Chrome, Edge or IE Instead");
            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Int16.Parse(Parameter.Get<string>("PageLoad")));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(Int16.Parse(Parameter.Get<string>("ImplicitWait")));
            return driver;
        }
    }
}