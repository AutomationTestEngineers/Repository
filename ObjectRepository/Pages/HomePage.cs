using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRepository.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.LinkText, Using = "MembershipX")]
        private IWebElement memberShip = null;

        [FindsBy(How = How.LinkText, Using = "LoansX")]
        private IWebElement loans = null;

        [FindsBy(How = How.LinkText, Using = "Client Portal")]
        private IWebElement client = null;


        public void GoToPage(string type)
        {
            switch (type.ToUpper())
            {
                case "MEMBERSHIP":
                    memberShip.ClickCustom("MEMBERSHIP",driver);
                    break;
                case "LOANS":
                    loans.ClickCustom("LOANS",driver);
                    break;
                case "CLIENT":
                    client.ClickCustom("CLIENT",driver);
                    break;                
            }
            Console.Write(type+" : Page is opened");
        }
    }
}
