using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRepository.Pages
{
    public class ReviewPage : BasePage
    {
        public ReviewPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement referral_source = null;

        [FindsBy(How = How.Name, Using = "checkAllButton")]
        private IWebElement checkAll = null;

        [FindsBy(How = How.Name, Using = "continueButton")]
        private IWebElement continueButton = null;

        [FindsBy(How = How.Name, Using = "confirmAndContinueButton")]
        private IWebElement confirmAndContinueButton = null;

        [FindsBy(How = How.Id, Using = "speedbump-checkbox")]
        private IWebElement checkBox = null;


        public void CheckAll()
        {
            referral_source.SelectDropDown(driver, "Dealership");
            checkAll.ClickCustom(driver);
            continueButton.ClickCustom(driver);

            Signature();
            checkBox.ClickCustom(driver);
            confirmAndContinueButton.ClickCustom(driver);
        }

        public void ReviewConfirmation()
        {

        }

        public void Signature()
        {
            try
            {
                Sleep(200);
                var elements = driver.FindElements(By.TagName("canvas"));
                foreach(IWebElement e in elements)
                    actions.MoveToElement(e).ClickAndHold().MoveByOffset(165, 15).MoveByOffset(185, 15)
                        .Release().Build().Perform();
            }
            catch { }
        }

        public void GetPrimaryDetails()
        {

        }        

        public void GetJointOwnerDetails()
        {


        }

        public void GetBeneficiaryDetails()
        {

        }
    }
}
