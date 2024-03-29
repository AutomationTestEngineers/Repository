﻿using Configuration;
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

        [TestCase, Category("PERSONAL"), Category("MEMBERSHIP")]
        public void ApplyMemberShip_For_Enrichment_Personal()
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
            RunStep(SelectionPage.SelectAccountType, "PERSONAL", "Select Personal Account Type");
            RunStep(ProductsPage.RandomSelection, "Choose Account");
            RunStep(ApplicantsPage.PopulateData, true, false, true, true, "Populate Fields On Applicants page");
            RunStep(ReviewPage.CheckAllAndContinue, "Check All Checbox and Continue");
            RunStep<string>(FundingPage.Payment, null, "Enter Credit Card Details");
            RunStep(VerificationPage.AnswersForTheGivenQuestions, "Answer For Question");
            RunStep(ConfirmationPage.VeriyfConfirmation, "Verify Confirmation Message");
        }

        [TestCase, Category("TEEN"), Category("MEMBERSHIP")]
        public void ApplyMemberShip_For_Enrichment_Teen()
        {
            RunStep(HomePage.GoToPage, "membership", "Goto MemeberShip Page", true, false);
            RunStep(() =>
             {
                 AgreementsPage.VerifyLinks("Terms And Conditions");
                 AgreementsPage.VerifyLinks("E-Sign Agreement");
                 AgreementsPage.VerifyLinks("Truth Insaving Certificates");
                 AgreementsPage.VerifyLinks("Truth Insaving Shares");
             }, "Verify All HyperLinks are working", true, false);
            RunStep(AgreementsPage.GetStarted, "Click GetStarted Button");
            RunStep(SelectionPage.SelectAccountType, "TEEN", "Select Teen Account Type");
            RunStep(ProductsPage.RandomSelection, "Choose Account");
            RunStep(ApplicantsPage.PopulateData, false, false, true, true, "Populate Fields On Applicants page");
            RunStep(ReviewPage.CheckAllAndContinue, "Check All Checbox and Continue");
            RunStep<string>(FundingPage.Payment, null, "Enter Credit Card Details");
            RunStep(VerificationPage.AnswersForTheGivenQuestions, "Answer For Question");
            RunStep(ConfirmationPage.VeriyfConfirmation, "Verify Confirmation Message");
        }

        [TestCase, Category("YOUTH"), Category("MEMBERSHIP")]
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
            RunStep(SelectionPage.SelectAccountType, "YOUTH", "Select Youth Account Type");
            RunStep(ProductsPage.RandomSelection, "Choose Account");
            RunStep(ApplicantsPage.PopulateData, false, false, true, true, "Populate Fields On Applicants page");
            RunStep(ReviewPage.CheckAllAndContinue, "Check All Checbox and Continue");
            RunStep<string>(FundingPage.Payment, null, "Enter Credit Card Details");
            RunStep(VerificationPage.AnswersForTheGivenQuestions, "Answer For Question");
            RunStep(ConfirmationPage.VeriyfConfirmation, "Verify Confirmation Message");
        }

        #endregion
    }
}
