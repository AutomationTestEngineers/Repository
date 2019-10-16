using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public static class TestConfig
    {

        private static bool _consoleLogWasInitialized = false;
        public static string AutomatedTestVersionNumber { get; set; } = "0.0.0.0";
        public enum ServerEnvironment
        {
            Local
            , Test
            , Stage
            , Prod
        }
        public enum Company
        {
            Apfcu = 0
            , AwesomeCu = 1
            , AwesomeNb = 2
            , CuWest = 3
            , CyFairFcu = 4
            , Dcfcu = 5
            , Diamondcu = 6
            , Discoveryfcu = 7
            , Dutchpoint = 8
            , Enrichmentfcu = 9
            , Fscu = 10
            , Marinecu = 11
            , Pvfcu = 12
            , PennEastFcu = 13
            , Profedcu = 14
            , PrimewayFcu = 15
            , Tcafcu = 16
            , Tsfcu = 17
            , Uhcu = 18



            , None = 99 // Do this once all tests are finished
        }
        public enum Module
        {
            Membership = 0          //a
            , Loan = 1              //ln
            , Business = 2          //b
            , Trust = 3             //t
        }
        public enum ModuleSubTypes
        {
            MembershipTypeApfcuPersonal = 0        //ages 19+
            , MembershipTypeApfcuYouth = 1         //ages 13 - 18
            , MembershipTypeDcfcuGeneral = 2       //ages 18+
            , MembershipTypeDcfcuGenYouMinor = 3   //ages 14 - 17
            , MembershipTypeDcfcuSenior = 4        //ages 50+
            , MembershipTypeFscuPersonal = 5       //ages 18+
            , MembershipTypeFscuTeens = 6          //ages 15 - 17
            , MembershipTypeFscuYouth = 7          //ages 0 - 14
            , MembershipTypeFscuBusiness = 8
            , MembershipTypeFscuTrust = 9

            , LoanTypeFscuAuto = 10
            , LoanTypeFscuPersonal = 11
            , LoanTypeFscuCreditCard = 12
            , LoanTypeFscuOtherVehicles = 13
            , LoanTypeFscuSecuredLoan = 14
            , LoanTypeFscuMortgage = 15

            , TrustTypeFscuRevocable = 16
            , TrustTypeFscuIrrevocable = 17

            , BusinessTypeFscuSoleProprietorship = 18
            , BusinessTypeFscuPartnership = 19
            , BusinessTypeFscuLimitedLiabilityCompany = 20
            , BusinessTypeFscuCorporation = 21
        }
        public enum MembershipTypes
        {
            ApfcuPersonal = 0        //ages 19+
                , ApfcuYouth = 1         //ages 13 - 18
                , DcfcuGeneral = 2       //ages 18+
                , DcfcuGenYouMinor = 3   //ages 14 - 17
                , DcfcuSenior = 4        //ages 50+
                , FscuPersonal = 5       //ages 18+
                , FscuTeens = 6          //ages 15 - 17
                , FscuYouth = 7          //ages 0 - 14
                , FscuBusiness = 8
                , FscuTrust = 9
        }

        private static void InitializeConsoleLog()
        {
            var AutomatedTestVersionNumber = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        }
        public static void LogData(string dataToLogInConsole)
        {
            if (!_consoleLogWasInitialized)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                //Console.Clear();
                InitializeConsoleLog();
            }
            Console.WriteLine(dataToLogInConsole);
        }


    }
}
