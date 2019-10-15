using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Configuration
{
    public class ParameterDeserializer
    {
        private readonly string _namespace = "Configuration.SerializableParameters";
        public virtual IParameterCreator Deserialize(XmlNode node)
        {
            IParameterCreator param = null;
            var serializer = new XmlSerializer(Type.GetType(_namespace + "." + node.Name));
            using (var reader = new XmlTextReader(new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(node.OuterXml)))))
            {
                if (serializer.CanDeserialize(reader))
                    param = (IParameterCreator)serializer.Deserialize(reader);
            }
            return param;
        }
        public virtual bool IsDeserializable(string className)
        {
            if (Type.GetType(_namespace + "." + className, false) == null)
                return false;

            return true;
        }
    }
}
