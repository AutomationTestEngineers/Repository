using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration.SerializableParameters
{
    [XmlRoot("JointOwnerCreator")]
    public class JointOwnerCreator
    {
        [XmlElement("collection")]
        public List<JointOwnerCollections> Collections { get; set; }

        public JointOwnerCreator()
        {
            Collections = new List<JointOwnerCollections>();
        }

        public object Create()
        {
            var collections = new List<JointOwnerCollections>();
            foreach (var c in Collections)
            {
                var collection = new JointOwnerCollections()
                {
                    Employer = c.Employer,
                    Occupation = c.Occupation,
                    Address = c.Address,
                    Apt = c.Apt,
                    Zip = c.Zip,
                    City = c.City,
                    State = c.State,
                    EmployerPhone = c.EmployerPhone
                };
                collections.Add(collection);
            }
            return collections;
        }
    }
    public class JointOwnerCollections
    {
        #region properties
        [XmlAttribute("employer")]
        public string Employer { get; set; }

        [XmlAttribute("occupation")]
        public string Occupation { get; set; }

        [XmlAttribute("address")]
        public string Address { get; set; }

        [XmlAttribute("apt")]
        public string Apt { get; set; }

        [XmlAttribute("zip")]
        public string Zip { get; set; }

        [XmlAttribute("city")]
        public string City { get; set; }

        [XmlAttribute("state")]
        public string State { get; set; }

        [XmlAttribute("employerPhone")]
        public string EmployerPhone { get; set; }
        #endregion
    }
}
