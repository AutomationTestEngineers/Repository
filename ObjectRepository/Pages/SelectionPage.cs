using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRepository.Pages
{
    public class SelectionPage : BasePage
    {
        public SelectionPage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.CssSelector, Using = "div[id^='account-sub-type'] td[id^='account-sub-type-icon']")]
        private IList<IWebElement> accountType = null;

        public void SelectAccountType(string type)
        {
            switch (type.ToLower())
            {
                case "personal":
                    accountType[0].ClickCustom(driver);
                    break;
                case "teen":
                    accountType[1].ClickCustom(driver);
                    break;
                case "youth":
                    accountType[2].ClickCustom(driver);
                    break;
            }
        }
    }
}
