﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRepository.Pages
{
    public class ProductsPage : BasePage
    {
        public ProductsPage(IWebDriver driver) : base(driver) { }


        [FindsBy(How = How.CssSelector, Using = "button[title='Add Savings']")]
        private IWebElement saving = null;

        [FindsBy(How = How.CssSelector, Using = "button[title='Add Checking']")]
        private IWebElement checking = null;

        [FindsBy(How = How.Id, Using = "courtesy-pay-checkbox")]
        private IWebElement courtesy_Checkbox = null;

        [FindsBy(How = How.Id, Using = "courtesy-pay-accepted-modal-i-agree-button-without-post")]
        private IWebElement iAgree = null;

        [FindsBy(How = How.CssSelector, Using = "button[title='Add Certificate']")]
        private IWebElement certificate = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='dropdown-menu show']/a")]
        private IList<IWebElement> dropDowns = null;

        [FindsBy(How = How.CssSelector, Using = "button[class='button_pn btnNext btn btn-primary']")]
        private IWebElement next = null;
        

        [FindsBy(How = How.CssSelector, Using = "button[title = 'Add Money Market']")]
        private IWebElement moneyMarket = null;

        [FindsBy(How = How.CssSelector, Using = "button[title = 'Add Share Certificate']")]
        private IWebElement shareCertificate = null;

        [FindsBy(How = How.CssSelector, Using = "button[title = 'Add Credit Card']")]
        private IWebElement creaditCard = null;


        private bool Populate { get { return GenericUtils.IsValueOdd; } }


        public void RandomSelection()
        {
            //SAVINGS
            if (Populate)
                if(saving.Displayed())
                    saving.ClickCustom(driver);
            Sleep(300);
            //CHECKING
            if (Populate)
            {
                checking.ClickCustom(driver);
                if (dropDowns[1].Displayed())
                {
                    dropDowns[1].ClickCustom(driver);
                    courtesy_Checkbox.ClickCustom(driver);
                    iAgree.ClickCustom(driver);
                }
            }
            Sleep(200);
            //CERTIFICATE
            if (Populate)
            {
                certificate.ClickCustom(driver);
                int[] numbers = new int[] {3, 6, 12,24,36, 48, 60 };
                int random = numbers[GenericUtils.GetRandomNumber(0, numbers.Length-1)];
                dropDowns[GenericUtils.GetRandomNumber(0, numbers.Length - 1)].ClickCustom(driver);
            }
            Sleep(200);
            next.ClickCustom(driver);
        }

        public void ChooseAccount(string type,int index)
        {
            switch (type.ToUpper())
            {
                case "SAVINGS":
                    saving.ClickCustom(driver);
                    break;
                case "CHECKING":
                    checking.ClickCustom(driver);
                    break;
                case "CERTIFICATE":
                    certificate.ClickCustom(driver);
                    break;
                default:
                    throw new ArgumentException("Please Give Correct Argument While Selecting Account Type instead["+type+"]");
            }
            dropDowns[index].ClickCustom(driver);
            next.ClickCustom(driver);
        } 
        
        public void ChooseAccountForDiscovery(string type,string subselection=null)
        {
            switch (type.ToUpper())
            {                
                case "CHECKING":
                    checking.ClickCustom(driver);
                    FindBy(By.XPath($"//div/h4[contains(text(),'{(new CultureInfo("en-US", false).TextInfo).ToTitleCase(subselection)}')]/../button")).ClickCustom(driver);
                    break;
                case "MONEY MARKET":
                    moneyMarket.ClickCustom(driver);
                    dropDowns[0].ClickCustom(driver);
                    break;
                case "SHARE CERTIFICATE":
                    shareCertificate.ClickCustom(driver);
                    break;
                case "CREDIT CARD":
                    creaditCard.ClickCustom(driver);
                    break;
                default:
                    throw new ArgumentException("Please Give Correct Argument While Selecting Account Type instead["+type+"]");
            }
            next.ClickCustom(driver);
        }


    }
}
