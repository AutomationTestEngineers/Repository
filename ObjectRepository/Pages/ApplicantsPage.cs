using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ObjectRepository.Pages
{
    public class ApplicantsPage : BasePage
    {
        public ApplicantsPage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.XPath, Using = "//form[@id='pri_applicant_form']//a[@href='#'][text()]")]
        private IWebElement uploadDocument_Link = null;

        [FindsBy]
        private IWebElement pri_first_name = null, pri_last_name = null, pri_date_of_birth = null, pri_street_address = null, pri_street_address2 = null
            , pri_zip = null, pri_primary_phone=null, pri_phone_type=null, pri_email_address=null, pri_contact_method=null, pri_ssn
            , pri_idtype=null, pri_state_id=null, pri_identificaton_number=null, pri_idissue_date=null, pri_id_exp_date=null;        

        
        [FindsBy(How = How.CssSelector, Using = "div[id='add-co-applicant-button-holder'] button")]
        private IWebElement addJoinOwner = null;

        [FindsBy(How = How.CssSelector, Using = "div #add-bene-button")]
        private IWebElement addBenificiary = null;

        [FindsBy(How = How.CssSelector, Using = "div[class='button-container multi-button-container'] #continue-button")]
        private IWebElement next = null;

        /* $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$   Joint Owner   $$SS$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*/

        [FindsBy]
        private IWebElement co_2_first_name = null, co_2_last_name=null , co_2_date_of_birth=null, co_2_street_address=null,
            co_2_zip=null, co_2_primary_phone=null, co_2_phone_type=null, co_2_email_address=null, co_2_contact_method=null,
            co_2_ssn=null, co_2_idtype=null, co_2_state_id=null, co_2_identificaton_number=null, co_2_idissue_date=null, co_2_id_exp_date=null;

        /* $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$    Beneficiary   $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*/
        [FindsBy]
        private IWebElement bene_first_name_0 = null, txt_bene_last_name_0 = null, beneficiaryDob_0 = null, bene_relation_0=null,
            pay_on_death_ratio_0=null, bene_ssn_0=null, eligibility=null;

        [FindsBy(How = How.CssSelector, Using = "#how-qualify-content #continue-button")]
        private IWebElement continu = null;

        [FindsBy(How = How.CssSelector, Using = "#county_eligible input")]
        private IList<IWebElement> selectEligibilty = null;

        /* $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$    Upload   $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*/

        [FindsBy(How = How.CssSelector, Using = "div[id='license-upload-modal-continue-button']")]
        private IWebElement continuePoppup = null;

        [FindsBy(How = How.CssSelector, Using = "button[id='upload-front-image']")]
        private IWebElement uploadFront = null;

        [FindsBy(How = How.CssSelector, Using = "button[id='upload-back-image']")]
        private IWebElement uploadBack = null;

        [FindsBy(How = How.CssSelector, Using = "button[id='submit-alert']")]
        private IWebElement submitLicenseImage = null;


        public void PopulateData()
        {
            //UploadDrivingLicense();

            pri_first_name.SendKeysWrapper("Dave", driver);
            pri_last_name.SendKeysWrapper("Simpson", driver);
            pri_date_of_birth.SendKeysWrapper("03/01/1980", driver,true);
            pri_street_address.SendKeysWrapper("083 Prospect Dr", driver);
            pri_zip.SendKeysWrapper("06511", driver);
            pri_primary_phone.SendKeysWrapper("2104268147", driver);
            pri_phone_type.SelectDropDown(driver,"Mobile");
            pri_email_address.SendKeysWrapper("amina@gmail.com", driver);
            pri_contact_method.SelectDropDown(driver,"Email");
            pri_ssn.SendKeysWrapper("666-99-0425", driver);
            pri_idtype.SelectDropDown(driver, "Driver's License");
            pri_state_id.SelectDropDown(driver, "CONNECTICUT");
            pri_identificaton_number.SendKeysWrapper("110000077", driver);
            pri_idissue_date.SendKeysWrapper("12/12/2012", driver,true);
            pri_id_exp_date.SendKeysWrapper("12/12/2022", driver,true);

            this.AddJointOwner();
            this.AddBeneficiary();
            next.ClickCustom(driver);

            eligibility.SelectDropDown(driver, "I work, worship, or attend school in one of the following counties");
            selectEligibilty[1].ClickCustom(driver);
            continu.ClickCustom(driver);
        }

        public void AddJointOwner()
        {
            addJoinOwner.ClickCustom(driver);

            co_2_first_name.SendKeysWrapper("Aurelie", driver);
            co_2_last_name.SendKeysWrapper("Dylan", driver);
            co_2_date_of_birth.SendKeysWrapper("11/11/1968", driver,true);
            co_2_street_address.SendKeysWrapper("14 Freemont St", driver);
            co_2_zip.SendKeysWrapper("89702", driver);
            co_2_primary_phone.SendKeysWrapper("9033609003", driver);
            co_2_phone_type.SelectDropDown(driver, "Mobile");
            co_2_email_address.SendKeysWrapper("amina@gmail.com", driver);
            co_2_contact_method.SelectDropDown(driver, "Email");
            co_2_ssn.SendKeysWrapper("666-99-3205", driver);
            co_2_idtype.SelectDropDown(driver, "Driver's License");
            co_2_state_id.SelectDropDown(driver, "MARYLAND");
            co_2_identificaton_number.SendKeysWrapper("M12100000087", driver);
            co_2_idissue_date.SendKeysWrapper("12/12/2012", driver,true);
            co_2_id_exp_date.SendKeysWrapper("12/12/2022", driver,true);
        }

        public void AddBeneficiary()
        {
            addBenificiary.ClickCustom(driver);

            bene_first_name_0.SendKeysWrapper("Cammille", driver);
            txt_bene_last_name_0.SendKeysWrapper("Alysssa", driver);
            beneficiaryDob_0.SendKeysWrapper("03/01/1978", driver,true);
            bene_relation_0.SelectDropDown(driver, "Spouse");
            pay_on_death_ratio_0.SendKeysWrapper("100", driver);
            bene_ssn_0.SendKeysWrapper("666-99-3194", driver);
        }

        public void UploadDrivingLicense()
        {
            uploadDocument_Link.ClickCustom(driver);
            continuePoppup.ClickCustom(driver);

            uploadFront.ClickCustom(driver);
            var licensePath =Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "TestData\\DrivingLicence_1.jpeg";

            Sleep(5000);
            Thread thread = new Thread(() => Clipboard.SetText(licensePath));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            Sleep(5000);
            uploadFront.SendKeys(OpenQA.Selenium.Keys.Control + "v");
            
            
            uploadBack.ClickCustom(driver);
            licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "TestData\\DrivingLicence_2.jpeg";
            Clipboard.SetDataObject("control+v", true);
            submitLicenseImage.ClickCustom(driver);
        }

    }
}
