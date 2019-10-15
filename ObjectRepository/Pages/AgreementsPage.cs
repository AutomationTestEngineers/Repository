using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace ObjectRepository.Pages
{
    public class AgreementsPage : BasePage
    {
        public AgreementsPage(IWebDriver driver) : base(driver) { }

        [FindsBy]
        private IWebElement msa = null;

        [FindsBy(How =How.Id,Using = "esign-agreement")]
        private IWebElement esignAgreemtLink= null;

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


        public void GetStarted()
        {
            iAgreeChk.ClickCustom(driver,false,false);
            if (membershipChk.Displayed())
                membershipChk.ClickCustom(driver);
            getStarted.ClickCustom(driver);
        }

        public void VerifyLinks(string name)
        {
            switch (name)
            {
                case "Terms And Conditions":
                    termsAndConditions.ClickCustom(driver);
                    break;
                case "E-Sign Agreement":
                    esignAgreemtLink.ClickCustom(driver);
                    break;
                case "Truth Insaving Certificates":
                    truthInSavingCertificates.ClickCustom(driver);
                    break;
                case "Truth Insaving Shares":
                    truthInSavingShares.ClickCustom(driver);
                    break;
                case "Membership Agreement":
                    msa.ClickCustom(driver);
                    break;
            }
            
            driver.SwitchToNewWindow();
            driver.Close();
            Sleep(300);
            driver.SwitchWindowUsingWindowCount(1);
        }   
    }
}
