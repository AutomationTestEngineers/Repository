using OpenQA.Selenium;
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
