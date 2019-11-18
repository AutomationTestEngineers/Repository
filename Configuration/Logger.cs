using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class Logger
    {
        static string currentLogFile;
        static DateTime startTime;        
        
        public static void StepLog(string stepName)
        {
            Log("===================================================================================");
            Log("Step Info : {0}", stepName);
            Log("Start Time: {0}", DateTime.Now.ToString());
        }
        public static void BeginTestIteration(string testName)
        {
            startTime = DateTime.Now;
            Log("***********************************************************************************");            
            Log("**** Test Name : {0}", testName);
            Log("***********************************************************************************");
        }

        public static void EndTestIteration(AventStack.ExtentReports.Status outcome)
        {
            Log(Environment.NewLine);
            Log("Test Duration : {0}", (DateTime.Now- startTime).ToString());
            Log("***********************************************************************************");
            Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>       End Test        <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            Log("***********************************************************************************");  
            TestOutcome(outcome);
        }

        private static void TestOutcome(AventStack.ExtentReports.Status outcome)
        {
            switch (outcome)
            {
                case AventStack.ExtentReports.Status.Fail:
                    Log(@"           __  _____ _____ ____ _____   _____ _    ___ _     _____ ____  ");
                    Log(@"  _       / / |_   _| ____/ ___|_   _| |  ___/ \  |_ _| |   | ____|  _ \ ");
                    Log(@" (_)_____| |    | | |  _| \___ \ | |   | |_ / _ \  | || |   |  _| | | | |");
                    Log(@"  _|_____| |    | | | |___ ___) || |   |  _/ ___ \ | || |___| |___| |_| |");
                    Log(@" (_)     | |    |_| |_____|____/ |_|   |_|/_/   \_\___|_____|_____|____/ ");
                    Log(@"          \_\                                                            ");
                    break;
                case AventStack.ExtentReports.Status.Pass:
                    Log(@"        __    _____ _____ ____ _____   ____   _    ____  ____  _____ ____  ");
                    Log(@"  _     \ \  |_   _| ____/ ___|_   _| |  _ \ / \  / ___|/ ___|| ____|  _ \ ");
                    Log(@" (_)_____| |   | | |  _| \___ \ | |   | |_) / _ \ \___ \\___ \|  _| | | | |");
                    Log(@"  _|_____| |   | | | |___ ___) || |   |  __/ ___ \ ___) |___) | |___| |_| |");
                    Log(@" (_)     | |   |_| |_____|____/ |_|   |_| /_/   \_\____/|____/|_____|____/ ");
                    Log(@"        /_/                                                                ");
                    break;
            }
        }

        public static void CreateTestLogFile(string fullPath)
        {
            currentLogFile = fullPath;
            using (var tw = new StreamWriter(fullPath, false))
            {
                tw.WriteLine("Genearted On :"+DateTime.Now);
                Console.WriteLine("LogFilePath :"+fullPath);
            }
        }
        public static void Log(string message, params string[] args)
        {
            string msg = string.Format(message, args);
            using (FileStream fs = new FileStream(currentLogFile, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(msg);
            }
        }
    }
}
