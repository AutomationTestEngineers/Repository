using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class Logger
    {
        private DateTime startTime = DateTime.Now;
        public static void LogInfo(string message)
        {
            Console.WriteLine("[Info] : " + message);
        }
        public static void LogVerify(string message)
        {
            Console.WriteLine("[Verify] : " + message);
        }
        public static void Log(string message, params string[] args)
        {
            Console.WriteLine(string.Format(message,args));
        }

        public static void LogMessage(string message)
        {
            Console.WriteLine("[Message] : " + message);
        }
        public static void Log(string message)
        {
            string log = ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>";
            Console.WriteLine(log);
            Console.WriteLine(message);
            Console.WriteLine(log);
        }

        public static void BeginTestIteration(string testName)
        {
            Log("***********************************************************************************");
            Log("**  [Test Iteration]");
            Log("**  Iteration Name : {0}",testName);
            Log("***********************************************************************************");
        }

        public static void EndTestIteration()
        {

        }
    }
}
