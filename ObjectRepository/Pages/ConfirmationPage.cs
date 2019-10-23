using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRepository.Pages
{
    public class ConfirmationPage : BasePage
    {
        public ConfirmationPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement MembershipXWillContactSecondaryHeader = null;


        public void VeriyfConfirmation()
        {
            MembershipXWillContactSecondaryHeader.HighlightElement(driver);
            //Assert.True(MembershipXWillContactSecondaryHeader.Displayed,"Confirmation Message Not Displayed");
        }
    }
}
