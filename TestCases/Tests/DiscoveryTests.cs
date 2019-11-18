using Configuration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectRepository;
using System;


namespace TestCases.Tests
{
    [TestClass]
    public class DiscoveryTests : Common
    {
        [TestInitialize]
        public override void Initialize()
        {            
            base.Initialize();
            Parameter.Add<string>("Client", "DISCOVERY");            
            RunStep(() => { CollectAllParameters(); }, "Collect Parameters");
            RunStep(() => { driver = WebDriver.InitDriver(Parameter.Get<string>("Url")); }, "Launch webdriver");
        }

        #region TestCases
        [TestMethod]
        public void ApplyMemberShip_For_Discovery()
        {
            RunStep(HomePage.GoToPage, "membership", "Goto MemeberShip Page");
            RunStep(() =>
            {
                AgreementsPage.GetMadatoryText().Should().NotBeNullOrEmpty();
                AgreementsPage.VerifyLinks("E-Sign Agreement");
                AgreementsPage.GetMadatoryText().Should().BeNullOrEmpty();
                AgreementsPage.VerifyLinks("Membership Agreement");
            }, "Verify Required E-sign Agreement", true, false);
            RunStep(AgreementsPage.GetStarted, "Click GetStarted Button");
            RunStep(ProductsPage.Discovey, "Choose Account");
            RunStep(ApplicantsPage.PopulateData, true, true, true, false, "Populate Fields On Applicants page");
            RunStep(ReviewPage.Review, "ReviewPage");
            RunStep(ReviewPage.CheckAllAndContinue, "Check All Checbox and Continue");
            RunStep<string>(FundingPage.Payment, null, "Enter Payment Details");
            RunStep(VerificationPage.AnswersForTheGivenQuestions, "Answer For Question");
            RunStep(ConfirmationPage.VeriyfConfirmation, "Verify Confirmation Message");
        }


        [TestMethod]
        public void Auto_Loan()
        {
            RunStep(HomePage.GoToPage, "loans", "Goto MemeberShip Page");
            RunStep(SelectionPage.SelectLoanType, "TEEN", "Select Personal Account Type");

        }
        #endregion
    }
}
