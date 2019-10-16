using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Configuration
{
    public static class Report
    {
        #region Report variables

        public static ExtentReports Extent;
        public static ExtentTest Test;

        public static string TestName { get; set; } = null;
        public static string CreatedFolderName { get; set; } = null;
        public static string ExtentConfigFileName { get; set; } = null;
        public static string TestDescription { get; set; } = null;
        public static string ReportName { get; set; } = null;
        //public static string ReportDirectory { get; set; } = TestConfig.PathOfSavedReport;
        public static string ReportNameAndDirectory { get; set; } = null;
        public static int ScreenShotNumber { get; set; } = 0;
        public static string ScreenShotName { get; set; } = "screenshot";
        public static StringBuilder DisplayMessage = new StringBuilder();
        private static bool _reportIsInitialized = false;
        private static bool _reportIsOpen = false;

        #endregion
        public static string[] SplitCamelCase(string source)
        {
            return Regex.Split(source, $"(?<!^)(?=[A-Z])");
        }
        public static string GetThisMethodsName([CallerMemberName]string memberName = "")
        {
            var split = SplitCamelCase(memberName);
            var splitMemberName = new StringBuilder();
            foreach (var s in split)
            {
                splitMemberName.Append($"{s} ");
                //Console.Write($"{s} ");
            }
            //TestConfig.LogData("");
            return splitMemberName.ToString().Trim(); //output name of calling method
        }//-GetThisMethodsName
        private static void InitializeTestReport()
        {
            try
            {
                //TestConfig.LogData($"- {Report.GetThisMethodsName()} ...");
                var weekDay = DateTime.Now.DayOfWeek;
                var dateMonth = DateTime.Now.Month;
                var dateDay = DateTime.Now.Day;
                var dateYear = DateTime.Now.Year;
                var dateTimeHours = DateTime.Now.TimeOfDay.Hours;
                var dateTimeMinutes = DateTime.Now.TimeOfDay.Minutes;

                // Set the unique name for Today's report folder.
                CreatedFolderName = ($"{weekDay}_{dateMonth}_{dateDay}_" +
                    $"{dateYear}");
               var ReportDirectory = "C:\\Automation";

                // Update the report directory with the new folder name
                ReportDirectory = $"{ReportDirectory}\\{CreatedFolderName}";

                // If the folder doesn't exist, create it!
                if (!System.IO.Directory.Exists(ReportDirectory))
                    System.IO.Directory.CreateDirectory(ReportDirectory);


                ReportName = ($"\\TestResults_{weekDay}_{dateMonth}_{dateDay}_" +
                    $"{dateYear}_{dateTimeHours}_{dateTimeMinutes}.html");
                // Set the unique name for future screenshot images.
                ScreenShotName = ($"ScreenShot_{weekDay}_{dateMonth}_{dateDay}_" +
                    $"{dateYear}_{dateTimeHours}_{dateTimeMinutes}");

                ReportNameAndDirectory = ($"{ReportDirectory}{ReportName}");

                _reportIsInitialized = true;
                Report.Open(ReportNameAndDirectory);
            }
            catch (Exception ex)
            {
                TestConfig.LogData("");
                TestConfig.LogData($"- {Report.GetThisMethodsName()} Failed | \n Full Error: {ex}");
            }
        }
        public static void Start(string testName, string companyToTest, string serverEnvironment = (@"Prod"))
        {
            TestName = ($"{testName}");

            if (!_reportIsInitialized)
            {
                InitializeTestReport();
            }
            TestConfig.LogData($"- Report.{Report.GetThisMethodsName()} is starting {TestName}");
            
            Test = Extent.CreateTest(TestName);

            Test.Log(Status.Pass, ($"- Report.{Report.GetThisMethodsName()} & Extent Report " +
                $"successfully started | {TestName}"));
            TestConfig.LogData($"- {Report.GetThisMethodsName()} successfully " +
                $"started Extent Report: {TestName}");
        }//-StartTest
        private static void Open(string reportDirectoryAndReportName = (@"TestResults.html"))
        {
            // Make sure the report was initialized before we attempt to open it.
            if (!_reportIsInitialized)
            {
                _reportIsOpen = false;
                InitializeTestReport();
            }

            try
            {
                TestConfig.LogData($"- {Report.GetThisMethodsName()} is opening report named: {ReportName}");
                ReportName = reportDirectoryAndReportName;
                var currentDirectory = Directory.GetCurrentDirectory();
                //ReportDirectory = currentDirectory;
                ExtentConfigFileName = currentDirectory + "\\Utilities\\extent-config.xml";

                TestConfig.LogData($"- {Report.GetThisMethodsName()} is opening the Extent " +
                    $"Config File named: {ExtentConfigFileName}");
                //Extent = new ExtentReports(ReportName,true);
                Extent = new ExtentReports();
                Extent.AddSystemInfo("Test Creator", "eCU Technology's SQA Team");
                Extent.AddSystemInfo("Purpose", "Automated UI Testing");
                Extent.AddSystemInfo("Creation Date", DateTime.Now.ToShortDateString());
                Extent.AddSystemInfo("Automated Test Version #: ", TestConfig.AutomatedTestVersionNumber);
                //Extent.LoadConfig(ExtentConfigFileName);

                _reportIsOpen = true;
                TestConfig.LogData($"- {Report.GetThisMethodsName()} successfully opened the Extent " +
                    $"Config File named: {ExtentConfigFileName}");
            }
            catch (Exception)
            {
                TestConfig.LogData($"- {Report.GetThisMethodsName()} FAILED TO OPEN");
            }

        }//-Open

    }
}
