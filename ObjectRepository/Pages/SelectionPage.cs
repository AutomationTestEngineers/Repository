﻿using OpenQA.Selenium;
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

        [FindsBy(How = How.Id, Using = "Auto-Loan")]
        private IWebElement autoLoan = null;

        [FindsBy(How = How.Id, Using = "loan-product-6")]
        private IWebElement personalLoan = null;

        [FindsBy(How = How.Id, Using = "icon-Credit flavor-icon")]
        private IWebElement creditcard = null;

        [FindsBy(How = How.Id, Using = "loan-product-4")]
        private IWebElement otherVehicles = null;

        [FindsBy(How = How.Id, Using = "Secured-Loan")]
        private IWebElement securedLoan = null;

        [FindsBy(How = How.Id, Using = "Home-Equity-Loan")]
        private IWebElement homeEquityLoan = null;

        [FindsBy(How = How.Id, Using = "First-Mortgage-Loan")]
        private IWebElement firstMortgageLoan = null;

        [FindsBy(How = How.Id, Using = "Student-Loan")]
        private IWebElement studentLoan = null;

        public void SelectAccountType(string type)
        {
            switch (type.ToUpper())
            {
                case "PERSONAL":
                    accountType[0].ClickCustom(driver);
                    break;
                case "TEEN":
                    accountType[1].ClickCustom(driver);
                    break;
                case "YOUTH":
                    accountType[2].ClickCustom(driver);
                    break;
                default:
                    throw new ArgumentException($"Please Provide Proper Account Type Instead [{type}]");
            }
        }

        public void SelectLoanType(string type=null)
        {
            switch (type.ToLower())
            {
                //case "":
                //    break;
                //case "":
                //    break;
                //case "":
                //    break;
                //case "":
                //    break;
                //case "":
                //    break;
                //case "":
                //    break;
                //case "":
                //    break;
                //case "":
                //    break;
                //case "":
                //    break;
                default:
                    break;
            }
        }
    }
}
