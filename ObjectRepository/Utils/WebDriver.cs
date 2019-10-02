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

            switch (Config.Browser.ToLower())
            {
                case "chrome":
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
                    driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(Int16.Parse(Config.BrowserLoad)));
                    break;
                default:
                    throw new ArgumentException($"Browser Option {Config.Browser} Is Not Valid - Use Chrome, Edge or IE Instead");
            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Int16.Parse(Config.PageLoad));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(Int16.Parse(Config.ImplicitWait));
            return driver;
        }
    }
}