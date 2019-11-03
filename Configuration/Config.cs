using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Configuration
{
    public class  Config
    {       
        //public static string Browser => ConfigurationManager.AppSettings["Browser"];
        //public static string BrowserLoad => ConfigurationManager.AppSettings["BrowserLoad"];
        //public static string ImplicitWait => ConfigurationManager.AppSettings["ImplicitWait"];
        //public static string ScreenTimeOut => ConfigurationManager.AppSettings["ScreenTimeOut"]; 
        //public static string PageLoad => ConfigurationManager.AppSettings["PageLoad"];        
        //public static string ScreenBusy => ConfigurationManager.AppSettings["ScreenBusy"];
        //public static bool Highlight => bool.Parse(ConfigurationManager.AppSettings["Highlight"]);

        public static void LogInfo(string message)
        {
            Console.WriteLine("[Info] : "+message);
        }
        public static void LogVerify(string message)
        {
            Console.WriteLine("[Verify] : " + message);
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

    }     

    public class Questions
    {
        private static Dictionary<string, object> _parametersDictionary = new Dictionary<string, object>();

        public static void Add<T>(string key, T value) where T : class
        {
            if (_parametersDictionary.ContainsKey(key))
                _parametersDictionary[key] = value;
            else
                _parametersDictionary.Add(key, value);
        }

        public static T Get<T>(string key) where T : class
        {
            object value = null;
            _parametersDictionary.TryGetValue(key, out value);
            if (value != null)
                return value as T;
            Console.WriteLine($"[Null Value] - Question[{key}] does not Question.xml file");
            return null;
        }

        public static void Collect(string parametersFileName, List<string> section)
        {
            string filePath = string.Empty;
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            filePath = directory + parametersFileName;
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            foreach (var sectionXpath in section)
            {
                var xmlNode = xmlDoc.SelectSingleNode(sectionXpath);
                if (xmlNode != null && xmlNode.HasChildNodes)
                {
                    foreach (XmlNode node in xmlNode.ChildNodes)
                    {
                        if ((node.NodeType != (XmlNodeType.Comment)) && (node.NodeType != XmlNodeType.Whitespace))
                            Questions.Add<string>(node.Name, node.InnerText.ToString());
                    }
                }
                else
                    Console.WriteLine($"[Skipping collection] - Parameters file: [{parametersFileName}]. Section xpath: [{sectionXpath}], there are no parameters or xpath is wrong.");
            }
        }        
    }

    public class Parameter
    {
        private static Dictionary<string, object> _parametersDictionary = new Dictionary<string, object>();
        // such as $, then those need to be doubled escaped such as $$.  
        private static readonly Regex _paramsRegex = new Regex("(?<!%)%[a-zA-Z0-9_-]+?%"); // defined parameter format 

        public static void Add<T>(string key, T value, bool shouldLog = false) where T : class
        {
            if (_parametersDictionary.ContainsKey(key))
                _parametersDictionary[key] = value;
            else
                _parametersDictionary.Add(key, value);


            if (shouldLog)
            {
                var stringValue = value as string;
                Console.WriteLine("[Parameter Created] - Name: [{0}]. Value: [{1}].", key, stringValue ?? typeof(T).ToString());
            }
        }

        public static T Get<T>(string key, bool shouldLog = false) where T : class
        {
            object value = null;
            _parametersDictionary.TryGetValue(key, out value);

            if (value != null)
            {
                if (typeof(T) == typeof(string) && IsEmbedded((string)value))
                {
                    value = GetEmbedded((string)value);
                }
                if (shouldLog)
                {
                    var stringValue = value as string;
                    Console.WriteLine("[Parameter Info] - Name: [{0}]. Value: [{1}].", key, stringValue ?? typeof(T).ToString());
                }
                return value as T;
            }
            Console.WriteLine("[Null Value] - Parameter collection does not contain key: [{0}]", key);
            return null;
        }

        //private bool IsEmbedded(string paramValue)
        private static bool IsEmbedded(string paramValue)
        {
            if (_paramsRegex.Match(paramValue).Success)
                return true;
            return false;
        }

        public static string GetEmbedded(string value, List<string> variableNames = null)
        {
            if (variableNames == null)
            {
                variableNames = new List<string>() { };
            }

            // parse the value to see if we are using existing parameter values
            MatchCollection embeddedParams = _paramsRegex.Matches(value);
            foreach (Match paramsMatch in embeddedParams)
            {
                string paramKey = paramsMatch.Value.Trim();
                var doublePercent = new Regex("^%|%?");
                paramKey = doublePercent.Replace(paramKey, "");

                //have to check if infinite loop will happend
                if (variableNames.Contains(paramKey))
                    throw new Exception(String.Format("Possible infinite loop for value {0} and parameter name {1}", value, paramKey));

                variableNames.Add(paramKey);
                object tmpValue;
                _parametersDictionary.TryGetValue(paramKey, out tmpValue); // support only string type parameter
                string paramValue = tmpValue as string;
                if (!string.IsNullOrEmpty(paramValue) && IsEmbedded(paramValue))
                {
                    paramValue = GetEmbedded(paramValue, variableNames);
                }

                // It is safe to remove key after recursion ended
                variableNames.Remove(paramKey);

                if (paramValue == null)
                    throw new Exception(String.Format("[ERROR] - Parameter [{0}] of type [{1}] has null value", paramKey, typeof(string)));

                value = Regex.Replace(value, paramsMatch.Value, paramValue);
            }
            return value;
        }        

        public static void Clear()
        {
            _parametersDictionary.Clear();
        }
    }

    public class ParameterType
    {
        public static bool IsXML(string parameter)
        {
            var xmlParameter = new XmlDocument();
            try
            {
                xmlParameter.LoadXml(parameter);
                return true;
            }
            catch { return false; }
        }
    }

    public class XmlParameterCollector
    {
        /// <summary>
        /// To collect the parmeters
        /// </summary>
        /// <param name="parametersFileName"></param>
        /// <param name="collectionCriteria"></param>
        public virtual void Collect(string parametersFileName, List<string> collectionCriteria)
        {
            string filePath = string.Empty;
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"TestData\");
            filePath = directory + parametersFileName;
            var parameterDeserilizer = new ParameterDeserializer();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            foreach (var sectionXpath in collectionCriteria)
            {
                var xmlNode = xmlDoc.SelectSingleNode(sectionXpath);
                if (xmlNode != null && xmlNode.HasChildNodes)
                {
                    foreach (XmlNode node in xmlNode.ChildNodes)
                    {
                        if (ParameterType.IsXML(node.InnerXml))
                        {
                            if (parameterDeserilizer.IsDeserializable(node.FirstChild.Name))
                            {
                                Parameter.Add<object>(node.Name, parameterDeserilizer.Deserialize(node.FirstChild).Create(), false);
                            }
                            else
                            {
                                Parameter.Add<XmlNode>(node.Name, node.FirstChild);
                            }
                        }
                        else if ((node.NodeType != (XmlNodeType.Comment)) && (node.NodeType != XmlNodeType.Whitespace))
                        {
                            Parameter.Add<string>(node.Name, node.InnerText.ToString());
                        }
                    }
                    // Log appropriate collect summary info message.
                    Console.WriteLine($"[Collected] - Parameters file: [{parametersFileName}]. Section xpath: [{sectionXpath}]. Parameters collected: [{xmlNode.ChildNodes.Count}].");
                }
                else
                {
                    Console.WriteLine($"[Skipping collection] - Parameters file: [{parametersFileName}]. Section xpath: [{sectionXpath}], there are no parameters or xpath is wrong.");
                }
            }
        }
    }

}
