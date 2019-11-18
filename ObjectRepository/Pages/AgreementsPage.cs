using FluentAssertions;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;

namespace ObjectRepository.Pages
{
    public class AgreementsPage : BasePage
    {
        public AgreementsPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement msa = null;

        [FindsBy(How = How.Id, Using = "e-sign-text")]
        private IWebElement esignText = null;

        [FindsBy(How = How.Id, Using = "esign-agreement")]
        private IWebElement esignAgreemtLink = null;

        [FindsBy(How = How.XPath, Using = "//a[text()='Terms and Conditions']")]
        private IWebElement termsAndConditions = null;

        [FindsBy(How = How.XPath, Using = "//a[text()='Truth in Savings Certificates']")]
        private IWebElement truthInSavingCertificates = null;

        [FindsBy(How = How.XPath, Using = "//a[text()='Truth in Savings Shares']")]
        private IWebElement truthInSavingShares = null;

        [FindsBy(How = How.Id, Using = "ckbLandingAgree-0")]
        private IWebElement iAgreeChk = null;

        [FindsBy(How = How.Id, Using = "ckbLandingAgree-1")]
        private IWebElement membershipChk = null;

        [FindsBy(How = How.Id, Using = "continue")]
        private IWebElement getStarted = null;

        [FindsBy(How = How.ClassName, Using = "navbar-header")]
        private IWebElement header = null;

        public string GetMadatoryText()
        {
            if (esignText.Displayed())
            {
                esignText.HighlightElement(driver);
                return esignText.Text;
            }
            return null;
        }

        public void GetStarted()
        {
            VerifyPage();
            iAgreeChk.ClickCustom("IAgree", driver, false, false);
            if (membershipChk.Displayed())
                membershipChk.ClickCustom("Membership Checkbox", driver);
            getStarted.ClickCustom("Get Started", driver);
        }

        public void VerifyPage()
        {
            header.HighlightElement(driver);
            //Console.WriteLine(header.FindElement(By.TagName("span")).Text+" is displayed");
        }
        public void VerifyLinks(string name)
        {
            switch (name)
            {
                case "Terms And Conditions":
                    termsAndConditions.ClickCustom("Terms And Condition", driver);
                    break;
                case "E-Sign Agreement":
                    esignAgreemtLink.ClickCustom("E-Sign Agreement", driver);
                    break;
                case "Truth Insaving Certificates":
                    truthInSavingCertificates.ClickCustom("Truth Insaving Certificates", driver);
                    break;
                case "Truth Insaving Shares":
                    truthInSavingShares.ClickCustom("Truth Insaving Shares", driver);
                    break;
                case "Membership Agreement":
                    msa.ClickCustom("Membership Agreement", driver);
                    break;
            }
            driver.SwitchToNewWindow();
            driver.Close();
            Sleep(300);
            driver.SwitchWindowUsingWindowCount(1);
        }
    }
}