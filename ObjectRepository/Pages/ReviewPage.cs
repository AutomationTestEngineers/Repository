using Configuration;
using Configuration.SerializableParameters;
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
        private IWebElement referral_source = null, fullName = null, dateOfBirth = null, address_without_address_2 = null,
            cellPhone = null, email = null, idNumber = null, idIssuingState = null, idIssueDate = null, idExpirationDate = null, ssn = null;

        [FindsBy(How = How.Name, Using = "checkAllButton")]
        private IWebElement checkAll = null;

        [FindsBy(How = How.Name, Using = "continueButton")]
        private IWebElement continueButton = null;

        [FindsBy(How = How.Name, Using = "confirmAndContinueButton")]
        private IWebElement confirmAndContinueButton = null;

        [FindsBy(How = How.Id, Using = "speedbump-checkbox")]
        private IWebElement checkBox = null;


        string primarySections = "//h3[text()='Primary Applicant']/../div[contains(@class,'review-section')]";


        public void CheckAllAndContinue()
        {
            referral_source.SelectComboBox(null,driver);
            checkAll.ClickCustom(driver);
            continueButton.ClickCustom(driver);

            Signature();
            checkBox.ClickCustom(driver);
            confirmAndContinueButton.ClickCustom(driver);
        }

        public void ReviewInformation()
        {
            var primaryDetials = Parameter.Get<ICollection>("PrimaryDetails_Selected");            
            var jointDetails = Parameter.Get<ICollection>("JoinDetails_Selected");
            var benificiaryDetails = Parameter.Get<ICollection>("BenificiaryDetails_Selected"); 
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
