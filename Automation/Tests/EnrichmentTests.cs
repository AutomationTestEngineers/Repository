using Configuration;
using NUnit.Framework;
using ObjectRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation
{
    [TestFixture(Category = "ENRICHMENT")]
    public class EnrichmentTests : Common
    {
        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
            Parameter.Add<string>("Client", "ENRICHMENT");
            this.CollectSharedParameters();
            driver = (new WebDriver()).InitDriver(Parameter.Get<string>("Url"));
        }

        #region TestCases

        [Test, Category("PERSONAL"), Category("MEMBERSHIP")]
        public void ApplyMemberShip_For_Enrichment_Personal()
        {
            RunStep(HomePage.GoToPage, "membership", "Goto MemeberShip Page");
            //RunStep(() =>
            //{
            //    AgreementsPage.VerifyLinks("Terms And Conditions");
            //    AgreementsPage.VerifyLinks("E-Sign Agreement");
            //    AgreementsPage.VerifyLinks("Truth Insaving Certificates");
            //    AgreementsPage.VerifyLinks("Truth Insaving Shares");
            //}, "Verify All HyperLinks are working", true, false);
            RunStep(AgreementsPage.GetStarted, "Click GetStarted Button");
            RunStep(SelectionPage.SelectAccountType, "PERSONAL", "Select Personal Account Type");
            RunStep(ProductsPage.ChooseAccount, "savings", 1, "Choose Savings Account With Option 2");
            RunStep(ApplicantsPage.UploadDrivingLicense, "");
            RunStep(ApplicantsPage.PopulateData, false, true, true, "Populate Fields On Applicants page");
            RunStep(ReviewPage.CheckAll, "Check All Checbox and Continue");
            RunStep(FundingPage.EnterCreditCard, "Enter Credit Card Details");
            RunStep(VerificationPage.GiveAnswersForQuestions, "Answer For Question");
            RunStep(ConfirmationPage.VeriyfConfirmation, "Verify Confirmation Message");
        }

        [Test,Category("TEEN") ,Category("MEMBERSHIP")]
        public void ApplyMemberShip_For_Enrichment_Teen()
        {
           RunStep(HomePage.GoToPage, "membership", "Goto MemeberShip Page");
            RunStep(() =>
             {
                 AgreementsPage.VerifyLinks("Terms And Conditions");
                 AgreementsPage.VerifyLinks("E-Sign Agreement");
                 AgreementsPage.VerifyLinks("Truth Insaving Certificates");
                 AgreementsPage.VerifyLinks("Truth Insaving Shares");
             }, "Verify All HyperLinks are working", true, false);
            RunStep(AgreementsPage.GetStarted, "Click GetStarted Button");
            RunStep(SelectionPage.SelectAccountType, "TEEN", "Select Personal Account Type");
            RunStep(ProductsPage.ChooseAccount, "savings", 1, "Choose Savings Account With Option 2");
            RunStep(ApplicantsPage.PopulateData, false, true, true, "Populate Fields On Applicants page");
            RunStep(ReviewPage.CheckAll, "Check All Checbox and Continue");
            RunStep(FundingPage.EnterCreditCard, "Enter Credit Card Details");
            RunStep(VerificationPage.GiveAnswersForQuestions, "Answer For Question");
            RunStep(ConfirmationPage.VeriyfConfirmation, "Verify Confirmation Message");
        }
        

        [Test, Category("YOUTH"), Category("MEMBERSHIP")]
        public void ApplyMemberShip_For_Enrichment_Youth()
        {
            RunStep(HomePage.GoToPage, "membership", "Goto MemeberShip Page");
            RunStep(() =>
            {
                AgreementsPage.VerifyLinks("Terms And Conditions");
                AgreementsPage.VerifyLinks("E-Sign Agreement");
                AgreementsPage.VerifyLinks("Truth Insaving Certificates");
                AgreementsPage.VerifyLinks("Truth Insaving Shares");
            }, "Verify All HyperLinks are working", true, false);
            RunStep(AgreementsPage.GetStarted, "Click GetStarted Button");
            RunStep(SelectionPage.SelectAccountType, "YOUTH", "Select Personal Account Type");
            RunStep(ProductsPage.ChooseAccount, "savings", 1, "Choose Savings Account With Option 2");
            RunStep(ApplicantsPage.PopulateData, false, true, true, "Populate Fields On Applicants page");
            RunStep(ReviewPage.CheckAll, "Check All Checbox and Continue");
            RunStep(FundingPage.EnterCreditCard, "Enter Credit Card Details");
            RunStep(VerificationPage.GiveAnswersForQuestions, "Answer For Question");
            RunStep(ConfirmationPage.VeriyfConfirmation, "Verify Confirmation Message");
        }
        #endregion
    }
}
