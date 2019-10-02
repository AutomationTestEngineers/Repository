using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;

namespace Configuration
{
    public class  Config
    {       
        public static string Browser => ConfigurationManager.AppSettings["Browser"];
        public static string BrowserLoad => ConfigurationManager.AppSettings["BrowserLoad"];
        public static string ImplicitWait => ConfigurationManager.AppSettings["ImplicitWait"];
        public static string ScreenTimeOut => ConfigurationManager.AppSettings["ScreenTimeOut"]; 
        public static string PageLoad => ConfigurationManager.AppSettings["PageLoad"];        
        public static string ScreenBusy => ConfigurationManager.AppSettings["ScreenBusy"];
        public static bool Highlight => bool.Parse(ConfigurationManager.AppSettings["Highlight"]);
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
            Console.WriteLine("[Null Value] - Parameter collection does not contain key: [{0}]", key);
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
}
