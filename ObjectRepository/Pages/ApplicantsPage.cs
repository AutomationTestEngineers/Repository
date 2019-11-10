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
        private IWebElement pri_first_name = null, pri_gender = null, pri_last_name = null, pri_date_of_birth = null, pri_street_address = null //,pri_street_address2 = null
            , pri_zip = null, pri_cell_phone = null, pri_primary_phone = null, pri_phone_type = null, pri_email_address = null, pri_contact_method = null, pri_ssn = null
            , pri_idtype = null, pri_state_id = null, pri_identificaton_number = null, pri_idissue_date = null, pri_id_exp_date = null,
            /* $$$$$$$ Employment Controls $$$$$$$$$*/
            pri_monthly_salary = null, pri_employment_start_date = null, pri_employer_phone = null,
            pri_employer_street_address = null, eligibility_zip = null, eligibility_employer_church_work = null,
            pri_employer_zip = null, pri_years_at_address = null, pri_months_at_address = null, pri_rent = null, pri_maiden = null,
            co_2_monthly_salary=null, co_2_employment_start_date=null,
            co_2_employer_street_address=null, co_2_employer_zip=null, co_2_employer_phone=null;

        [FindsBy(How = How.XPath, Using = "//input[@id='pri_employerloantxt' or @id='pri_employertxt']")]
        public IWebElement pri_employertxt = null;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'pri_occupation')]")]
        public IWebElement pri_occupation = null;

        [FindsBy(How = How.XPath, Using = "//input[@id='co_2_employerloantxt' or @id='co_2_employertxt']")]
        public IWebElement co_2_employertxt = null;

        [FindsBy(How = How.XPath, Using = "//input[contains(@id,'co_2_occupation')]")]
        public IWebElement co_2_occupation = null;

        [FindsBy(How = How.CssSelector, Using = "div[id='add-co-applicant-button-holder'] button")]
        private IWebElement addJoinOwner = null;

        [FindsBy(How = How.CssSelector, Using = "div #add-bene-button")]
        private IWebElement addBenificiary = null;

        [FindsBy(How = How.CssSelector, Using = "div[class='button-container multi-button-container'] #continue-button")]
        private IWebElement next = null;

        /* $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$   Joint Owner   $$SS$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*/

        [FindsBy]
        private IWebElement co_2_first_name = null, co_2_last_name = null, co_2_gender = null, co_2_date_of_birth = null, co_2_street_address = null,
            co_2_zip = null, co_2_primary_phone = null, co_2_phone_type = null, co_2_email_address = null, co_2_contact_method = null,
            co_2_ssn = null, co_2_idtype = null, co_2_state_id = null, co_2_identificaton_number = null, co_2_idissue_date = null,
            co_2_cell_phone = null, co_2_home_phone = null, co_2_id_exp_date = null, co_2_maiden = null, co_2_years_at_address=null, co_2_months_at_address=null, co_2_rent=null;

        [FindsBy(How = How.XPath, Using = "//b[@id='co_2_required-text']/..")]
        private IWebElement jointOwner_DL = null;

        [FindsBy(How = How.XPath, Using = "//div[@id='co_2_image-capture-modal']//button[@id='upload-front-image']")]
        private IWebElement uploadFront1 = null;

        [FindsBy(How = How.XPath, Using = "//div[@id='co_2_image-capture-modal']//button[@id='upload-back-image']")]
        private IWebElement uploadBack1 = null;

        [FindsBy(How = How.XPath, Using = "//div[@id='co_2_image-capture-modal']//button[@id='submit-alert']")]
        private IWebElement submitLicenseImage1 = null;

        /* $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$    Beneficiary   $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$*/
        [FindsBy]
        private IWebElement bene_first_name_0 = null, txt_bene_last_name_0 = null, beneficiaryDob_0 = null, bene_relation_0 = null,
            pay_on_death_ratio_0 = null, bene_ssn_0 = null, eligibility = null;

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


        public void PopulateData(bool license = true, bool employment = false, bool jointOwner = false, bool benificiary = false)
        {
            var primary = Parameter.Get<List<ICollection>>("Primary");            
            var primaryDetials = primary.Where(a => a.FirstName.Trim()=="Dave" && a.LastName.Trim()== "Simpson").FirstOrDefault();
            Parameter.Add<ICollection>("PrimaryDetails_Selected", primaryDetials);
            var jointDetails = primary.Where(a => a.FirstName != primaryDetials.FirstName).FirstOrDefault();
            Parameter.Add<ICollection>("JoinDetails_Selected", jointDetails);
            var benificiaryDetails = primary.Where(a => a.FirstName != primaryDetials.FirstName && a.FirstName != jointDetails.FirstName).FirstOrDefault();
            Parameter.Add<ICollection>("BenificiaryDetails_Selected", benificiaryDetails);

            if (license)
                PrimaryDetailsWithdDrivingLicense(primaryDetials);
            else
                this.PrimaryDetails(primaryDetials);

            primaryDetials.StateSelected = pri_state_id.GetSelected();
            Parameter.Add<ICollection>("PrimaryDetails_Selected", primaryDetials);

            if (employment)
            {
                var emp = FakeData.RandomEmployee();
                Parameter.Add<Employee>("PrimaryEmployee", emp);
                this.PrimaryEmploymentInfo(emp);
            }

            if (jointOwner)
            {
                this.AddJointOwner(jointDetails);
                jointDetails.StateSelected = co_2_state_id.GetSelected();
                Parameter.Add<ICollection>("JoinDetails_Selected", jointDetails);

                var emp = FakeData.RandomEmployee();
                Parameter.Add<Employee>("JointEmployee", emp);
                this.JointEmploymentInfo(emp);
            }   

            if (benificiary)
                this.AddBeneficiary(benificiaryDetails);

            next.ClickCustom("Next",driver);

            this.Eligibility();
        }

        public void JointEmploymentInfo(Employee emp)
        {
            if (co_2_employertxt.Displayed())
                co_2_employertxt.SendKeysWrapper(emp.Employer,"Employee Name", driver);
            if (co_2_occupation.Displayed())
                co_2_occupation.SendKeysWrapper(emp.Occupation, "Occupation", driver);
            if (co_2_monthly_salary.Displayed())
                co_2_monthly_salary.SendKeysWrapper("500", "Salary", driver);
            if (co_2_employment_start_date.Displayed())
                co_2_employment_start_date.SendKeysWrapper("12/12/2012", "Employee Start Date", driver, true);
            if (co_2_employer_street_address.Displayed())
                co_2_employer_street_address.SendKeysWrapper(emp.Address, "Employee Street Address", driver);
            if (co_2_employer_zip.Displayed())
            {
                co_2_employer_zip.SendKeysWrapper(emp.Zip, "Employee Zip", driver);
                co_2_employer_zip.SendKeys(Keys.Tab);
            }               
            
            if (co_2_employer_phone.Displayed())
                co_2_employer_phone.SendKeysWrapper(Parameter.Get<string>("Cell"), "Employee Cell", driver, true);
            Sleep(200);
        }

        public void PrimaryEmploymentInfo(Employee emp)
        {
            if(pri_employertxt.Displayed())
                pri_employertxt.SendKeysWrapper(emp.Employer, "Primary Employee Name", driver);
            if (pri_occupation.Displayed())
                pri_occupation.SendKeysWrapper(emp.Occupation, "Primary Employee Occupation", driver);
            if (pri_monthly_salary.Displayed())
                pri_monthly_salary.SendKeysWrapper("500", "Primary Employee Salary", driver);
            if (pri_employment_start_date.Displayed())
                pri_employment_start_date.SendKeysWrapper("12/12/2012", "Primary Employee Start Date", driver,true);
            if (pri_employer_street_address.Displayed())
                pri_employer_street_address.SendKeysWrapper(emp.Address, "Primary Employee Address", driver);
            if (pri_employer_zip.Displayed())
            {
                pri_employer_zip.SendKeysWrapper(emp.Zip, "Primary Employee Zip", driver);
                pri_employer_zip.SendKeys(Keys.Tab);
            }                
            if (pri_employer_phone.Displayed())
                pri_employer_phone.SendKeysWrapper(Parameter.Get<string>("Cell"), "Primary Employee Cell", driver, true);
            Sleep(200);
        }

        public void PrimaryDetails(ICollection details)
        {
            if (!string.IsNullOrEmpty(details.FirstName))
                pri_first_name.SendKeysWrapper(details.FirstName, "Primary First Name", driver);
            if (!string.IsNullOrEmpty(details.LastName))
                pri_last_name.SendKeysWrapper(details.LastName, "Primary Last Name", driver);
            if (!string.IsNullOrEmpty(details.DOB))
            {
                pri_date_of_birth.SendKeysWrapper(details.DOB, "Primary Date Of Birth", driver, true);
                pri_date_of_birth.SendKeys(Keys.Tab);
                if (FindBy(By.Id("pri_date_of_birth-error"), 3, true) != null)
                {
                    var a = Int16.Parse(new String(FindBy(By.Id("pri_date_of_birth-error")).Text.Where(Char.IsDigit).ToArray()));
                    pri_date_of_birth.SendKeysWrapper(GenericUtils.GenerateDate(0, 0, -a + 2), "Primary Date Of Birth" ,driver, true);
                    pri_date_of_birth.SendKeys(Keys.Tab);
                }
            }
            if (pri_gender.Displayed())
                pri_gender.SelectComboBox(null, "Primary Gender", driver);
            if (!string.IsNullOrEmpty(details.Address))
                pri_street_address.SendKeysWrapper(details.Address, "Primary Address", driver);
            if (!string.IsNullOrEmpty(details.Zip))
                pri_zip.SendKeysWrapper(details.Zip, "Primary Zip", driver);

            if (pri_cell_phone.Displayed())
                pri_cell_phone.SendKeysWrapper(Parameter.Get<string>("Cell"), "Primary Phone", driver, true);
            if (pri_primary_phone.Displayed())
                pri_primary_phone.SendKeysWrapper(Parameter.Get<string>("Cell"), "Primary Phone", driver, true);
            if (pri_years_at_address.Displayed())
                pri_years_at_address.SendKeysWrapper("5", "Primary Years At Address", driver, true);
            if (pri_months_at_address.Displayed())
                pri_months_at_address.SendKeysWrapper("5", "Primary Months at Address", driver, true);
            if (pri_rent.Displayed())
                pri_rent.SelectComboBox(Parameter.Get<string>("Resisency"), "Primary Resisency Type", driver);
            if (pri_phone_type.Displayed())
                pri_phone_type.SelectComboBox(Parameter.Get<string>("PhoneType"), "Primary PhoneType",driver);
            pri_email_address.SendKeysWrapper(Parameter.Get<string>("Email"), "Primary Email", driver,true);
            if (pri_contact_method.Displayed())
                pri_contact_method.SelectComboBox(Parameter.Get<string>("ContactMethod"), "Primary Email",driver);

            if (!string.IsNullOrEmpty(details.SSN))
            {
                pri_ssn.SendKeysWrapper(details.SSN,"Primary SSN", driver, true);
                pri_ssn.SendKeys(Keys.Tab);
            }
            if (pri_maiden.Displayed())
                pri_maiden.SendKeysWrapper(Parameter.Get<string>("MaidenName"), "Primary MaidenName", driver, true);
            this.IdentificationType("Driver's License", details);
        }

        public void AddJointOwner(ICollection details)
        {
            if (addJoinOwner.Displayed())
                addJoinOwner.ClickCustom("Joint Owner",driver);
            // if joint owner required drivers license 
            Sleep(1000);
            if (jointOwner_DL.Displayed())
            {
                jointOwner_DL.ClickCustom("Joint Driver License", driver);
                continuePoppup.ClickCustom("Continue", driver);
                uploadFront1.ClickCustom("Joint Owner Upload Front", driver);
                var licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "DrivingLicence_2_1.jpg");
                Sleep(5000);
                SendKeys.SendWait(licensePath);
                SendKeys.SendWait(@"{ENTER}");
                uploadBack1.ClickCustom("Joint Owner Upload Back", driver);
                licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "DrivingLicence_2_2.jpg");
                Sleep(5000);
                SendKeys.SendWait(licensePath);
                SendKeys.SendWait(@"{ENTER}");
                submitLicenseImage1.ClickCustom("Joint Owner Submit", driver);
                Wait(ExpectedConditions.InvisibilityOfElementLocated(By.Id("co_2_image-capture-modal")), 20);
                VerifyDriverLicenseUploaded();

            }

            if (co_2_first_name.Displayed())
                co_2_first_name.SendKeysWrapper(details.FirstName, "Joint Owner First Name", driver);

            if (co_2_last_name.Displayed())
                co_2_last_name.SendKeysWrapper(details.LastName, "Joint Owner Last Name", driver);
            if (co_2_date_of_birth.Displayed())
                co_2_date_of_birth.SendKeysWrapper(details.DOB, "Joint Owner Date Of Birth", driver, true);
            if (co_2_gender.Displayed())
                co_2_gender.SelectComboBox(null,"Joint Owner Gender", driver);

            if (co_2_street_address.Displayed())
                co_2_street_address.SendKeysWrapper(details.Address, "Joint Owner Address", driver);
            if (co_2_zip.Displayed())
            {
                co_2_zip.SendKeysWrapper(details.Zip, "Joint Owner Zip", driver);
                co_2_zip.SendKeys(Keys.Tab);
            }  
            if (co_2_years_at_address.Displayed())
                co_2_years_at_address.SendKeysWrapper("5", "Joint Owner Years At Address", driver, true);
            if (co_2_months_at_address.Displayed())
                co_2_months_at_address.SendKeysWrapper("5", "Joint Owner Months At Address", driver, true);
            if (co_2_rent.Displayed())
                co_2_rent.SelectComboBox(Parameter.Get<string>("Resisency"),"Joint Owner Residenct Type", driver);
            if (co_2_primary_phone.Displayed())
            {
                co_2_primary_phone.SendKeysWrapper(Parameter.Get<string>("Cell"), "Joint Owner Phone", driver, true);
                co_2_phone_type.SelectComboBox(Parameter.Get<string>("PhoneType"), "Joint Owner Phone Type",driver);
            }
            if (co_2_cell_phone.Displayed())
            {
                co_2_cell_phone.SendKeysWrapper(Parameter.Get<string>("Cell"), "Joint Owner Cell", driver, true);
                co_2_home_phone.SendKeysWrapper(Parameter.Get<string>("Cell"), "Joint Owner Phone", driver, true);
            }
            co_2_email_address.SendKeysWrapper(Parameter.Get<string>("Email"), "Joint Owner Email", driver,true);
            if (co_2_contact_method.Displayed())
                co_2_contact_method.SelectComboBox(Parameter.Get<string>("ContactMethod"), "Joint Owner Contact Method",driver);
            if (co_2_ssn.Displayed())
            {
                co_2_ssn.SendKeysWrapper(details.SSN, "Joint Owner SSN", driver, true);
                co_2_ssn.SendKeys(Keys.Tab);
            }
            if (co_2_idtype.Displayed())
                co_2_idtype.SelectComboBox(Parameter.Get<string>("IdType"), "Joint Owner IdTy[e",driver);
            Sleep(500);
            if (co_2_state_id.Displayed())
                co_2_state_id.SelectComboBox(details.DLState, "Joint Owner DL State", driver);
            if (co_2_identificaton_number.Displayed())
                co_2_identificaton_number.SendKeysWrapper(details.DLNumber, "Joint Owner Identification Number", driver);
            if (co_2_idissue_date.Displayed())
                co_2_idissue_date.SendKeysWrapper(details.IssueDate, "Joint Owner DL Issue Date", driver, true);
            if (co_2_id_exp_date.Displayed())
                co_2_id_exp_date.SendKeysWrapper(details.ExpDate, "Joint Owner DL Exp Date", driver, true);
            if (co_2_maiden.Displayed())
                co_2_maiden.SendKeysWrapper("Maiden Name", "Joint Owner Maiden Name", driver, true);
        }

        public void AddBeneficiary(ICollection details)
        {
            addBenificiary.ClickCustom("Benificiary", driver);
            bene_first_name_0.SendKeysWrapper(details.FirstName, "Benificiary First Name", driver);
            txt_bene_last_name_0.SendKeysWrapper(details.LastName, "Benificiary last Name", driver);
            beneficiaryDob_0.SendKeysWrapper(details.DOB, "Benificiary DOB", driver, true);
            bene_relation_0.SelectComboBox(null,"" ,driver);
            pay_on_death_ratio_0.SendKeysWrapper("100", "Benificiary Death Ratio", driver);
            bene_ssn_0.SendKeysWrapper(details.SSN, "Benificiary SSN", driver, true);

        }

        public void PrimaryDetailsWithdDrivingLicense(ICollection details)
        {
            // Uploading the Driver License,
            uploadDocument_Link.ClickCustom("Primary Upload Document",driver);
            continuePoppup.ClickCustom("Primart Continue",driver);

            uploadFront.ClickCustom("Primart Upload Front",driver);
            var licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"TestData","DrivingLicence_1.jpeg");
            Sleep(5000);
            SendKeys.SendWait(licensePath);
            SendKeys.SendWait(@"{ENTER}");
            uploadBack.ClickCustom("Primary Upload DL Back",driver);
            licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "DrivingLicence_2.jpeg");
            Sleep(2000);
            SendKeys.SendWait(licensePath);
            SendKeys.SendWait(@"{ENTER}");
            submitLicenseImage.ClickCustom("Submit",driver);
            Wait(ExpectedConditions.InvisibilityOfElementLocated(By.Id("pri_image-capture-modal")), 20);
            VerifyDriverLicenseUploaded();

            // Primary Details
            if (pri_first_name.GetAttribute("value").Contains("D")) pri_first_name.SendKeysWrapper("Dave","Primary First Name", driver);
            if (pri_cell_phone.Displayed())
                pri_cell_phone.SendKeysWrapper(details.Phone,"Primary Phone", driver, true);
            if (pri_primary_phone.Displayed())
            {
                pri_primary_phone.SendKeysWrapper(details.Phone,"Primary Phone", driver, true);
                pri_phone_type.SelectComboBox(Parameter.Get<string>("PhoneType"), "Primary PhoneType", driver);
            }
            
            if (pri_years_at_address.Displayed())
                pri_years_at_address.SendKeysWrapper("5", "Primary Years At Address",driver, true);
            if (pri_months_at_address.Displayed())
                pri_months_at_address.SendKeysWrapper("5", "Primary Months At Address", driver, true);
            if (pri_rent.Displayed())
                pri_rent.SelectComboBox(Parameter.Get<string>("Resisency"), "Primary Residency Type", driver);

            if(pri_email_address.Displayed())
                pri_email_address.SendKeysWrapper(Parameter.Get<string>("Email"), "Primary EMail", driver);
            if (pri_contact_method.Displayed())
                pri_contact_method.SelectComboBox(Parameter.Get<string>("ContactMethod"), "Primary ContactMethod",driver);
            if (!string.IsNullOrEmpty(details.SSN))
            {
                pri_ssn.SendKeysWrapper(details.SSN, "Primary SSN", driver, true);
                pri_ssn.SendKeys(Keys.Tab);
            }
            if (pri_maiden.Displayed())
                pri_maiden.SendKeysWrapper(Parameter.Get<string>("MaidenName"), "Primary Maiden Name", driver, true);
        }

        public void VerifyDriverLicenseUploaded()
        {
            Sleep(2000);
            successMsgDLFront.HighlightElement(driver);
            successMsgDLBack.HighlightElement(driver);
        }

        private void IdentificationType(string type, ICollection details = null)
        {
            string selectedType = pri_idtype.SelectComboBox(type, "Primary ID Type", driver);
            pri_identificaton_number.SendKeysWrapper(details.DLNumber, "Primary DL Number", driver, true);
            switch (selectedType)
            {
                case "Student Identification Card":
                    break;
                case "Driver's License":
                    pri_state_id.SelectComboBox(details.DLState, "Primary DL State", driver);
                    pri_idissue_date.SendKeysWrapper(Parameter.Get<string>("IssueDate"),"Primary IssueDate", driver, true);
                    pri_id_exp_date.SendKeysWrapper(Parameter.Get<string>("ExpDate"), "Primary Expiry Date", driver, true);
                    break;
                case "State ID Card":
                case "US Passport":
                case "Military Identification Card":
                case "Resident Alien Card":
                    pri_idissue_date.SendKeysWrapper(Parameter.Get<string>("IssueDate"), "Primary IssueDate", driver, true);
                    pri_id_exp_date.SendKeysWrapper(Parameter.Get<string>("ExpDate"), "Primary Expiry Date", driver, true);
                    break;
            }
        }

        private void Eligibility()
        {
            eligibility.SelectComboBox(null,"Eligibility",  driver);
            try
            {
                if (FindElements(selectEligibilty.GetLocator(),1) != null)
                    selectEligibilty[GenericUtils.GetRandomNumber(0, selectEligibilty.Count - 1)].ClickCustom("Eligibility Selection",driver);
            }
            catch { }            
            if (eligibility_zip.Displayed())
                eligibility_zip.SendKeysWrapper(Parameter.Get<string>("County"), "County", driver);
            if (eligibility_employer_church_work.Displayed())
                eligibility_employer_church_work.SendKeysWrapper(Parameter.Get<string>("Test"),"Employer Church", driver);
            if (continu.Displayed())
                continu.ClickCustom("Continue",driver);
            if (FindBy(By.Id("mfa-submit"), 1).Displayed())
                FindBy(By.Id("mfa-submit"), 1).ClickCustom("Submit",driver);
        }
    }
}
