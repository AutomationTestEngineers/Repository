using Configuration;
using Configuration.SerializableParameters;
using ObjectRepository;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

namespace ObjectRepository.Pages
{
    public class ApplicantsPage : BasePage
    {
        public ApplicantsPage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.XPath, Using = "//form[@id='pri_applicant_form']//a[@href='#'][text()]")]
        private IWebElement uploadDocument_Link = null;

        [FindsBy]
        private IWebElement pri_first_name = null, pri_last_name = null, pri_date_of_birth = null, pri_street_address = null //,pri_street_address2 = null
            , pri_zip = null, pri_cell_phone = null, pri_primary_phone=null, pri_phone_type=null, pri_email_address=null, pri_contact_method=null, pri_ssn=null
            , pri_idtype=null, pri_state_id=null, pri_identificaton_number=null, pri_idissue_date=null, pri_id_exp_date=null,
            /* $$$$$$$ Employment Controls $$$$$$$$$*/
            pri_employertxt=null, pri_occupation=null, pri_employer_street_address=null, pri_employer_zip=null;        

        
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

        [FindsBy(How = How.Id, Using = "pri_front-dl-status")]
        private IWebElement successMsgDLFront = null;

        [FindsBy(How = How.Id, Using = "pri_back-dl-status")]
        private IWebElement successMsgDLBack = null;


        public void PopulateData(bool license = true,bool employment = false,bool jointOwner = false,bool benificiary = false)
        {
            var primary = Parameter.Get<List<ICollection>>("Primary");
            int random = GenericUtils.GetRandomNumber(0, primary.Count);
            var primaryDetials = primary[random];
            Parameter.Add<ICollection>("PrimaryDetails_Selected",primaryDetials);
            var jointDetails = primary.Where(a=>a.FirstName!= primaryDetials.FirstName).FirstOrDefault();
            Parameter.Add<ICollection>("JoinDetails_Selected", jointDetails);
            var benificiaryDetails = primary.Where(a => a.FirstName != primaryDetials.FirstName && a.FirstName != jointDetails.FirstName).FirstOrDefault();
            Parameter.Add<ICollection>("BenificiaryDetails_Selected", benificiaryDetails);

            if (license)
                PrimaryDetailsWithdDrivingLicense(primaryDetials);
            else
                this.PrimaryDetails(primaryDetials);

            if (employment)
            {
                var emp = FakeData.RandomEmployee();
                Parameter.Add<Employee>("Employee", emp);
                this.EmploymentInfo(emp);
            }                

            if (jointOwner) 
                this.AddJointOwner(jointDetails);

            if (benificiary)
                this.AddBeneficiary(benificiaryDetails);

            next.ClickCustom(driver);

            this.Eligibility();
        }

        public void EmploymentInfo(Employee emp)
        {            
            pri_employertxt.SendKeysWrapper(emp.Employer, driver);
            pri_occupation.SendKeysWrapper(emp.Occupation, driver);
            pri_employer_street_address.SendKeysWrapper(emp.Address, driver);
            pri_employer_zip.SendKeysWrapper(emp.Zip, driver);
            pri_employer_zip.SendKeys(Keys.Tab);
            Sleep(200);
        }

        public void PrimaryDetails(ICollection details)
        {
            if (details != null)
            {
                if(!string.IsNullOrEmpty(details.FirstName))
                    pri_first_name.SendKeysWrapper(details.FirstName, driver);
                if (!string.IsNullOrEmpty(details.LastName))
                    pri_last_name.SendKeysWrapper(details.LastName, driver);
                if (!string.IsNullOrEmpty(details.DOB))
                {
                    pri_date_of_birth.SendKeysWrapper(details.DOB, driver, true);
                    pri_date_of_birth.SendKeys(Keys.Tab);
                    if (FindBy(By.Id("pri_date_of_birth-error"),3,true)!=null)
                    {
                       var a = Int16.Parse(new String(FindBy(By.Id("pri_date_of_birth-error")).Text.Where(Char.IsDigit).ToArray()));
                        pri_date_of_birth.SendKeysWrapper(GenericUtils.GenerateDate(0, 0, -a+2), driver, true);
                        pri_date_of_birth.SendKeys(Keys.Tab);
                    }

                }                    
                if (!string.IsNullOrEmpty(details.Address))
                    pri_street_address.SendKeysWrapper(details.Address, driver);
                if (!string.IsNullOrEmpty(details.Zip))
                    pri_zip.SendKeysWrapper(details.Zip, driver);

                if (pri_cell_phone.Displayed())
                    pri_cell_phone.SendKeysWrapper(Parameter.Get<string>("PrimaryCell"), driver);
                if (pri_primary_phone.Displayed())
                    pri_primary_phone.SendKeysWrapper(Parameter.Get<string>("PrimaryPhone"), driver);

                pri_phone_type.SelectDropDown(driver, Parameter.Get<string>("PrimaryPhoneType"));
                pri_email_address.SendKeysWrapper(Parameter.Get<string>("PrimaryEmail"), driver);
                pri_contact_method.SelectDropDown(driver, Parameter.Get<string>("PrimaryContactMethod"));
                if (!string.IsNullOrEmpty(details.SSN))
                    pri_ssn.SendKeysWrapper(details.SSN, driver);
                this.IdentificationType("Driver's License", details);
            }
            else
            {
                pri_first_name.SendKeysWrapper(Parameter.Get<string>("PrimaryFirstName"), driver);
                pri_last_name.SendKeysWrapper(Parameter.Get<string>("PrimaryLastName"), driver);
                pri_date_of_birth.SendKeysWrapper(Parameter.Get<string>("PrimaryDOB"), driver, true);
                pri_street_address.SendKeysWrapper(Parameter.Get<string>("PrimaryStreetAddress"), driver);
                pri_zip.SendKeysWrapper(Parameter.Get<string>("PrimaryZIP"), driver);

                if (pri_cell_phone.Displayed())
                    pri_cell_phone.SendKeysWrapper(Parameter.Get<string>("PrimaryCell"), driver);
                if (pri_primary_phone.Displayed())
                    pri_primary_phone.SendKeysWrapper(Parameter.Get<string>("PrimaryPhone"), driver);

                pri_phone_type.SelectDropDown(driver, Parameter.Get<string>("PrimaryPhoneType"));
                pri_email_address.SendKeysWrapper(Parameter.Get<string>("PrimaryEmail"), driver);
                pri_contact_method.SelectDropDown(driver, Parameter.Get<string>("PrimaryContactMethod"));
                pri_ssn.SendKeysWrapper(Parameter.Get<string>("PrimarySSN"), driver);
                this.IdentificationType("Driver's License");
            }
        }

        public void AddJointOwner(ICollection details)
        {
            if(addJoinOwner.Displayed())
                addJoinOwner.ClickCustom(driver);                
            co_2_first_name.SendKeysWrapper(details.FirstName, driver);
            co_2_last_name.SendKeysWrapper(details.LastName, driver);
            co_2_date_of_birth.SendKeysWrapper(details.DOB, driver,true);
            co_2_street_address.SendKeysWrapper(details.Address, driver);
            co_2_zip.SendKeysWrapper(details.Zip, driver);
            co_2_primary_phone.SendKeysWrapper(Parameter.Get<string>("PrimaryPhone"), driver);
            co_2_phone_type.SelectDropDown(driver, Parameter.Get<string>("PrimaryPhoneType"));
            co_2_email_address.SendKeysWrapper(Parameter.Get<string>("PrimaryEmail"), driver);
            co_2_contact_method.SelectDropDown(driver, Parameter.Get<string>("PrimaryContactMethod"));
            co_2_ssn.SendKeysWrapper(details.SSN, driver,true);
            co_2_idtype.SelectDropDown(driver, Parameter.Get<string>("IdType"));
            co_2_state_id.SelectDropDown(driver, details.DLState);
            co_2_identificaton_number.SendKeysWrapper(details.DLNumber, driver);
            co_2_idissue_date.SendKeysWrapper(details.IssueDate, driver,true);
            co_2_id_exp_date.SendKeysWrapper(details.ExpDate, driver,true);
        }

        public void AddBeneficiary(ICollection details)
        {
            addBenificiary.ClickCustom(driver);
            bene_first_name_0.SendKeysWrapper(details.FirstName, driver);
            txt_bene_last_name_0.SendKeysWrapper(details.LastName, driver);
            beneficiaryDob_0.SendKeysWrapper(details.DOB, driver, true);
            bene_relation_0.SelectComboBox(null, driver);
            pay_on_death_ratio_0.SendKeysWrapper("100", driver);
            bene_ssn_0.SendKeysWrapper(details.SSN, driver);

        }

        public void PrimaryDetailsWithdDrivingLicense(ICollection details)
        {
            // Uploading the Driver License
            uploadDocument_Link.ClickCustom(driver);
            continuePoppup.ClickCustom(driver);

            uploadFront.ClickCustom(driver);
            var licensePath =Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "TestData\\DrivingLicence_1.jpeg";
            Sleep(5000);
            SendKeys.SendWait(licensePath);
            SendKeys.SendWait(@"{ENTER}");            
            uploadBack.ClickCustom(driver);
            licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "TestData\\DrivingLicence_2.jpeg";
            Sleep(2000);
            SendKeys.SendWait(licensePath);
            SendKeys.SendWait(@"{ENTER}");
            submitLicenseImage.ClickCustom(driver);
            Wait(ExpectedConditions.InvisibilityOfElementLocated(By.Id("pri_image-capture-modal")),20);
            VerifyDriverLicenseUploaded();

            // Primary Details
            if(pri_first_name.GetAttribute("value").Contains("D")) pri_first_name.SendKeysWrapper("Dave",driver);
            if (pri_cell_phone.Displayed())
            {
                pri_cell_phone.SendKeysWrapper(Parameter.Get<string>("PrimaryCell"), driver,true);
            }                
            if (pri_primary_phone.Displayed())
            {
                pri_primary_phone.SendKeysWrapper(Parameter.Get<string>("PrimaryPhone"), driver,true);
                pri_phone_type.SelectDropDown(driver, Parameter.Get<string>("PrimaryPhoneType"));
            }            
            pri_email_address.SendKeysWrapper(Parameter.Get<string>("PrimaryEmail"), driver);
            if (pri_contact_method.Displayed())
                pri_contact_method.SelectDropDown(driver, Parameter.Get<string>("PrimaryContactMethod"));
            if (!string.IsNullOrEmpty(details.SSN))
                pri_ssn.SendKeysWrapper(details.SSN, driver);
        }

        public void VerifyDriverLicenseUploaded()
        {
            Sleep(2000);
            successMsgDLFront.HighlightElement(driver);
            successMsgDLBack.HighlightElement(driver);
        }

        private void IdentificationType(string type,ICollection details = null)
        {
            string selectedType = pri_idtype.SelectComboBox(type,driver);
            pri_identificaton_number.SendKeysWrapper(details.DLNumber, driver);
            switch (selectedType)
            {
                case "Student Identification Card":
                    break;
                case "Driver's License":
                    pri_state_id.SelectComboBox(details.DLState, driver);
                    pri_idissue_date.SendKeysWrapper(Parameter.Get<string>("PrimaryDLIssueDate"), driver, true);
                    pri_id_exp_date.SendKeysWrapper(Parameter.Get<string>("PrimaryDLExpDate"), driver, true);
                    break;
                case "State ID Card":
                case "US Passport":
                case "Military Identification Card":
                case "Resident Alien Card":
                    pri_idissue_date.SendKeysWrapper("12/12/2012", driver, true);
                    pri_id_exp_date.SendKeysWrapper("12/12/2022", driver, true);
                    break;
            }            
        }

        private void Eligibility()
        {
            eligibility.SelectComboBox(null,driver);            
            if (FindElements(selectEligibilty.GetLocator())!=null)
                selectEligibilty[GenericUtils.GetRandomNumber(0, selectEligibilty.Count-1)].ClickCustom(driver);
            continu.ClickCustom(driver);
        }
    }
}
