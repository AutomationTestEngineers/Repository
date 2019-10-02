using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRepository.Pages
{
    public class FundingPage : BasePage
    {
        public FundingPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement Credit = null, number=null, expiry=null, cvc=null;

        [FindsBy(How = How.Name, Using = "submitApplicationButton")]
        private IWebElement submitApplicationButton = null;

        public void EnterCreditCard()
        {
            Credit.ClickCustom(driver);
            number.SendKeysWrapper("4111111111111111", driver);
            expiry.SendKeysWrapper("12/22",driver,true);
            cvc.SendKeysWrapper("123", driver);
            submitApplicationButton.ClickCustom(driver);
        }
    }
}
