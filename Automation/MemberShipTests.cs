using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation
{
    [TestFixture]
    public class MemberShipTests : Common
    { 
        #region TestCases
        [Test]
        public void ApplyMemberShip_For_Enrichment()
        {            
            // To Verify All Hyper Links
            //RunStep(() =>
            //{
            //    AgreementsPage.VerifyLinks("Terms And Conditions");
            //    AgreementsPage.VerifyLinks("E-Sign Agreement");
            //    AgreementsPage.VerifyLinks("Truth Insaving Certificates");
            //    AgreementsPage.VerifyLinks("Truth Insaving Shares");
            //}, "Verify All HyperLinks are working", true, false);
            RunStep(AgreementsPage.GetStarted, "Click GetStarted Button");
            RunStep(SelectionPage.SelectAccountType, "personal", "Select Personal Account Type");
            RunStep(ProductsPage.ChooseAccount, "savings",1, "Choose Savings Account With Option 2");
            RunStep(ApplicantsPage.PopulateData, "Populate Fields On Applicants page");
            RunStep(ReviewPage.CheckAll, "Check All Checbox and Continue");
            RunStep(FundingPage.EnterCreditCard, "Enter Credit Card Details");
            RunStep(VerificationPage.GiveAnswersForQuestions, "Answer For Question");
            RunStep(ConfirmationPage.VeriyfConfirmation, "Verify Confirmation Message");
        }      

        #endregion
    }
}
