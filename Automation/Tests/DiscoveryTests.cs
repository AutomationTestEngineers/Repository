using Configuration;
using NUnit.Framework;
using ObjectRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation
{
    [TestFixture]
    public class DiscoveryTests : Common
    {
        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
            Parameter.Add<string>("Client", "DISCOVERY");
            this.CollectSharedParameters();
            driver = (new WebDriver()).InitDriver(Parameter.Get<string>("Url"));
        }

        #region TestCases
        [Test, Category("MEMBERSHIP"), Category("DISCOVERY")]
        public void ApplyMemberShip()
        {
            RunStep(HomePage.GoToPage, "membership", "Goto MemeberShip Page");
            RunStep(() =>
            {
                AgreementsPage.VerifyLinks("E-Sign Agreement");
                AgreementsPage.VerifyLinks("Membership Agreement");
            }, "Verify All HyperLinks are working", true, false);
            RunStep(AgreementsPage.GetStarted, "Click GetStarted Button");
            RunStep<string>(ProductsPage.ChooseAccountForDiscovery, "CHECKING", "BAsic", "Choose Savings Account ");
            RunStep(ApplicantsPage.PopulateData, true, true, true, false, "Populate Fields On Applicants page");
            //RunStep(ReviewPage.CheckAll, "Check All Checbox and Continue");
            //RunStep(FundingPage.EnterCreditCard, "Enter Credit Card Details");
            //RunStep(VerificationPage.GiveAnswersForQuestions, "Answer For Question");
            //RunStep(ConfirmationPage.VeriyfConfirmation, "Verify Confirmation Message");
        }


        [Test, Category("LOAN"), Category("DISCOVERY")]
        public void Auto_Loan()
        {
            RunStep(HomePage.GoToPage, "loans", "Goto MemeberShip Page");
            RunStep(SelectionPage.SelectAccountType, "TEEN", "Select Personal Account Type");

        }
        #endregion
    }

}
    