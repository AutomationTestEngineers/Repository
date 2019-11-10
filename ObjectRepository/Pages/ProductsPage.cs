using Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
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

        [FindsBy(How = How.CssSelector, Using = "button[id^='open-now-item']")]
        private IList<IWebElement> openNow = null;

        [FindsBy(How = How.Id, Using = "courtesy-pay-checkbox")]
        private IWebElement courtesy_Checkbox = null;

        [FindsBy(How = How.Id, Using = "courtesy-pay-accepted-modal-i-agree-button-without-post")]
        private IWebElement iAgree = null;

        [FindsBy(How = How.XPath, Using = "//button[@title='Add Certificate' or @title='Add Share Certificate']")]
        private IWebElement certificate = null;

        [FindsBy(How = How.XPath, Using = "//div[@class='dropdown-menu show']/a")]
        private IList<IWebElement> dropDowns = null;

        [FindsBy(How = How.CssSelector, Using = "button[class='button_pn btnNext btn btn-primary']")]
        private IWebElement next = null;        

        [FindsBy(How = How.CssSelector, Using = "button[title = 'Add Money Market']")]
        private IWebElement moneyMarket = null;

        [FindsBy(How = How.CssSelector, Using = "button[title = 'Add Credit Card']")]
        private IWebElement creaditCard = null;

        [FindsBy(How = How.XPath, Using = "(//div[@class='modal' and @id='disclosure-for-cc-modal']//button[@title='I agree' ])[1]")]
        private IWebElement disclousure_iAgree = null;


        private bool Populate { get { return GenericUtils.IsValueOdd; } }

        public void Discovey()
        {
            var selection = new List<string>();         
            //CHECKING
            if (Populate)
            {
                checking.ClickCustom("Checking",driver);
                openNow[GenericUtils.GetRandomNumber(0, openNow.Count-1)].ClickCustom("Checking Option", driver);
                Wait(ExpectedConditions.ElementToBeClickable(moneyMarket));
            }
            Sleep(200);            
            //Monet Market
            if (Populate)
            {                
                moneyMarket.ClickCustom("Monet Market",driver);
                dropDowns[0].ClickCustom(dropDowns[0].Text,driver);
                Wait(ExpectedConditions.ElementToBeClickable(certificate));
            }

            //CERTIFICATE
            if (Populate)
                certificate.ClickCustom("Certificate", driver);

            //Credit Card
            if (Populate)
                creaditCard.ClickCustom("CreaditCard", driver);
            Sleep(1000);
            var section = FindElements(By.XPath("//div[@class='product-header']/h3/span | //div[@class='product-header']/h3/sup/..")).Select(a=>a.Text.Trim()).ToList();
            Parameter.Add("Products", section);
            next.ClickCustom("Next",driver);
            if (disclousure_iAgree.Displayed())
                disclousure_iAgree.ClickCustom("Disclousure iAgree", driver);
        }

        public void RandomSelection()
        {
            Sleep(500);
            //SAVINGS
            if (Populate)
                if(saving.Displayed())
                    saving.ClickCustom("Saving",driver);
            Sleep(300);
            //CHECKING
            if (Populate)
            {
                if (checking.Displayed())
                    checking.ClickCustom("Checking", driver);
                if (FindBy(By.XPath("(//div[@class='dropdown-menu show']/a)[1]"),2).Displayed())
                {
                    dropDowns[1].ClickCustom(dropDowns[1].Text, driver);
                    courtesy_Checkbox.ClickCustom("Courtesy Checkbox", driver);
                    iAgree.ClickCustom("I Agree",driver);
                }
            }
            Sleep(200);
            //CERTIFICATE
            if (Populate)
            {
                certificate.ClickCustom("Certificate", driver);
                int[] numbers = new int[] {3, 6, 12,24,36, 48, 60 };
                int random = numbers[GenericUtils.GetRandomNumber(0, numbers.Length-1)];
                dropDowns[GenericUtils.GetRandomNumber(0, numbers.Length - 1)].ClickCustom("Certificate Option",driver);
            }
            Sleep(200);
            next.ClickCustom("Next",driver);
        }

        public void ChooseAccount(string type,int index)
        {
            switch (type.ToUpper())
            {
                case "SAVINGS":
                    saving.ClickCustom("SAVINGS",driver);
                    break;
                case "CHECKING":
                    checking.ClickCustom("CHECKING",driver);
                    break;
                case "CERTIFICATE":
                    certificate.ClickCustom("CERTIFICATE",driver);
                    break;
                default:
                    throw new ArgumentException("Please Give Correct Argument While Selecting Account Type instead["+type+"]");
            }
            dropDowns[index].ClickCustom(dropDowns[index].Text,driver);
            next.ClickCustom("Next",driver);
        } 
        
        public void ChooseAccountForDiscovery(string type,string subselection=null)
        {
            switch (type.ToUpper())
            {                
                case "CHECKING":
                    checking.ClickCustom("CHECKING",driver);
                    FindBy(By.XPath($"//div/h4[contains(text(),'{(new CultureInfo("en-US", false).TextInfo).ToTitleCase(subselection)}')]/../button")).ClickCustom("CHECKING",driver);
                    break;
                case "MONEY MARKET":
                    moneyMarket.ClickCustom("MONEY MARKET",driver);
                    dropDowns[0].ClickCustom("MONEY MARKET",driver);
                    break;
                case "SHARE CERTIFICATE":
                    certificate.ClickCustom("SHARE CERTIFICATE",driver);
                    break;
                case "CREDIT CARD":
                    creaditCard.ClickCustom("CREDIT CARD",driver);
                    break;
                default:
                    throw new ArgumentException("Please Give Correct Argument While Selecting Account Type instead["+type+"]");
            }
            next.ClickCustom("Next",driver);
        }
        
    }
}
