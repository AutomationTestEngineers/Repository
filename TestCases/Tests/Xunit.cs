using Configuration;
using ObjectRepository;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TestCases.Tests
{
    public class Xunit : Common, IDisposable
    {
        //public RemoteWebDriver driver;
        private readonly ITestOutputHelper _output;

        public Xunit(ITestOutputHelper output)
        {
            _output = output;
            Parameter.Add<string>("Client", "DISCOVERY");
            this.CollectSharedParameters();
            var browser = new WebDriver();
            driver = browser.OpenBrowser(_output);
        }

        public void Dispose()
        {
            if(driver!=null)
                driver.Quit();
        }
        
        [Theory]
        [InlineData("membership")]
        [InlineData("LOANS")]
        public void TestCase_123(String type)
        {
            var exception = Record.Exception(() =>
            {
            try
            {
                HomePage.GoToPage(type);
                AgreementsPage.VerifyLinks("E-Sign Agreement");
                AgreementsPage.VerifyLinks("Membership Agreement");
                AgreementsPage.GetStarted();
                ProductsPage.Discovey();
                ApplicantsPage.PopulateData( true, true, true, false);
                ReviewPage.Review();
                driver.Dispose();
             }
             catch (AssertActualExpectedException e)
             {
                _output.WriteLine(e.Message);
                Assert.IsType<FormatException>(e);
                Dispose();
             }
            });
            Assert.Null(exception);
        }
    }
}
