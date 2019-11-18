using Configuration;
using Configuration.SerializableParameters;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
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



        string primary = "//h3[contains(text(),'Primary')]/..";
        string joint = "//h3[text()='Joint Owner']/..";

        public void CheckAllAndContinue()
        {
            referral_source.SelectComboBox(null, "Referal Source", driver);
            checkAll.ClickCustom("Check All", driver);
            continueButton.ClickCustom("Continue", driver);

            Signature();
            if (checkBox.Displayed())
                checkBox.ClickCustom("Signature Check box", driver);
            if (confirmAndContinueButton.Displayed())
                confirmAndContinueButton.ClickCustom("Confirm", driver);
        }

        public void Review()
        {
            ICollection primaryDetials = Parameter.Get<ICollection>("PrimaryDetails_Selected");
            ICollection jointDetails = Parameter.Get<ICollection>("JoinDetails_Selected");
            ICollection benificiaryDetails = Parameter.Get<ICollection>("BenificiaryDetails_Selected");
            var selection = Parameter.Get<List<string>>("Products");

            VerifyProduct(selection);
            Logger.Log("Primary Details Verification");
            VerifyDetails(primary, primaryDetials);
            VeriyfyEmployement(primary, Parameter.Get<Employee>("PrimaryEmployee"));
            Logger.Log("JointOwner Details Verification");
            VerifyDetails(joint, jointDetails);
            VeriyfyEmployement(joint, Parameter.Get<Employee>("JointEmployee"));
        }

        public void Signature()
        {
            try
            {
                Sleep(200);
                var elements = driver.FindElements(By.TagName("canvas"));
                foreach (IWebElement e in elements)
                    actions.MoveToElement(e).ClickAndHold().MoveByOffset(165, 15).MoveByOffset(185, 15)
                        .Release().Build().Perform();
            }
            catch { }
        }

        public void VerifyDetails(string xpath, ICollection details)
        {
            var section = FindBy(By.XPath(xpath), 1, true);
            if (section != null)
            {
                section.HighlightElement(driver);
                var fullName = section.find(By.Id("fullName"));
                if (fullName != null && details.FirstName != null)
                    Verify(fullName.Text.Contains(details.FirstName), "Name", fullName.Text, details.FirstName);

                var dateOfBirth = section.find(By.Id("dateOfBirth"));
                var expectedDOB = DateTime.Parse(dateOfBirth.Text).ToString("M/d/yyyy", CultureInfo.InvariantCulture);
                if (dateOfBirth != null)
                    Verify(dateOfBirth.Text.Contains(expectedDOB), "Date Of Birth", dateOfBirth.Text, details.DOB);

                var gender = section.find(By.Id("gender"));
                if (gender != null && details.Gender != null)
                    Verify(details.Gender.ToString().StartsWith(gender.Text), "Gender", gender.Text, details.Gender);

                var address = section.find(By.Id("address_without_address_2"));
                if (address != null && details.Address != null)
                    Verify(address.Text.Contains(details.Address), "Address", address.Text, details.Address);

                var phone = section.find(By.XPath("//div[contains(@id,'Phone')]"));
                if (phone != null && details.Phone != null)
                    Verify(phone.Text.Contains(details.Phone), "Phone", phone.Text, details.Phone);

                var idtype = section.find(By.XPath("//*[@id='idType']/a"));
                if (idtype != null && details.IDType != null)
                    Verify(idtype.Text.Contains(details.IDType), "ID Type", idtype.Text, details.IDType);

                var idNumber = section.find(By.Id("idNumber"));
                if (idNumber != null && details.DLNumber != null)
                    Verify(idNumber.Text.Contains(details.DLNumber), "Identification Number", idNumber.Text, details.DLNumber);

                var issueState = section.find(By.Id("idIssuingState"));
                if (issueState != null && details.StateSelected != null)
                    Verify(issueState.Text.Contains(details.StateSelected), "Issued State", issueState.Text, details.DLState);

                var issueDate = section.find(By.Id("idIssueDate"));
                if (issueDate != null)
                    Verify(issueDate.Text.Contains(details.IssueDate), "Issue date", issueDate.Text, details.IssueDate);

                var expDate = section.find(By.Id("idExpirationDate"));
                if (expDate != null)
                    Verify(expDate.Text.Contains(details.ExpDate), "Expiry Date", expDate.Text, details.ExpDate);

                var ssn = section.find(By.Id("ssn"));
                if (ssn != null)
                    Verify(details.SSN.Contains(ssn.Text.Split('-').LastOrDefault()), "SSN", ssn.Text, details.SSN);

                var maidenName = section.find(By.Id("mothersMaidenName"));
                if (maidenName != null && details.MaidenName != null)
                    Verify(maidenName.Text.Contains(details.MaidenName), "Maiden Name", maidenName.Text, details.MaidenName);
            }
        }

        public void VeriyfyEmployement(string xpath, Employee emp)
        {
            var section = FindBy(By.XPath(xpath), 1, true);
            if (section != null)
            {
                section.HighlightElement(driver);
                var currentEmployment = section.find(By.Id("current-employer"));
                if (currentEmployment != null && emp.Employer != null)
                    Verify(currentEmployment.Text.Contains(emp.Employer), "Employment", currentEmployment.Text, emp.Employer);

                var address = FindBy(By.XPath($"{xpath}//h3[text()='Employment Information']/../div[2]"), 1);
                if (address != null && emp.Address != null)
                    Verify(address.Text.Contains(emp.Address), "Employee Address", address.Text, emp.Address);

                var empPhone = section.find(By.Id("idIssuingCountry"));
                if (empPhone != null && emp.Phone != null)
                    Verify(empPhone.Text.Contains(emp.Phone), "Employee Phone", empPhone.Text, emp.Phone);
            }

        }

        public void VerifyProduct(List<string> selection)
        {
            Logger.Log("Prodcuts Verification");
            var list = FindElements(By.XPath("//div[@class='panel-section' or @name='panel-section']/div[1]/span"));
            foreach (IWebElement e in list)
            {
                e.HighlightElement(driver);
                var product = selection.Where(a => a.Contains(e.Text.Trim())).FirstOrDefault();
                Verify(product.Contains(e.Text), "Product Name", e.Text, product);
            }
        }
    }
}