using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
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
        

        public void ChooseAccount(string type,int index)
        {
            switch (type.ToLower())
            {
                case "savings":
                    saving.ClickCustom(driver);
                    break;
                case "checking":
                    checking.ClickCustom(driver);
                    break;
                case "certificate":
                    certificate.ClickCustom(driver);
                    break;
                default:
                    throw new ArgumentException("Please Give Correct Argument While Selecting Account Type instead["+type+"]");
            }
            dropDowns[index].ClickCustom(driver);
            next.ClickCustom(driver);
        }        
    }
}
