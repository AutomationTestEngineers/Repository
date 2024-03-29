﻿using OpenQA.Selenium;
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
        private IWebElement Credit = null, number=null, expiry=null, cvc=null,True=null, InternalTransfer=null, 
            acctNumber=null, FiRoutingNumber=null;

        [FindsBy(How = How.Name, Using = "submitApplicationButton")]
        private IWebElement submitApplicationButton = null;

        public void EnterCreditCard()
        {
            Credit.ClickCustom("Credit Card",driver);
            number.SendKeysWrapper("4111111111111111","Card Number", driver);
            expiry.SendKeysWrapper("12/22","Card Expiry",driver,true);
            cvc.SendKeysWrapper("123","Card CVV", driver);            
        }

        public void Payment(String type=null)
        {
            List<string> temp = new List<string> { "credit","transfer","internaltransfer" };
            int index = (type == null)? GenericUtils.GetRandomNumber(0, temp.Count()-1) :temp.FindIndex(s => s.ToLower().Contains(type));
            switch (temp[index])
            {
                case "credit":
                    EnterCreditCard();
                    break;
                case "transfer":
                    True.ClickCustom("Transfer",driver);
                    FiRoutingNumber.SendKeysWrapper(Configuration.Parameter.Get<string>("RoutingNumber"), "RoutingNumber", driver);
                    acctNumber.SendKeysWrapper(Configuration.Parameter.Get<string>("AccountNumber"),"AccountNumber", driver);
                    break;
                case "internaltransfer":
                    InternalTransfer.ClickCustom("InternalTransfer", driver);
                    acctNumber.SendKeysWrapper(Configuration.Parameter.Get<string>("AccountNumber"), "AccountNumber", driver);
                    break;
            }
            submitApplicationButton.ClickCustom("Submit",driver);
        }
    }
}
