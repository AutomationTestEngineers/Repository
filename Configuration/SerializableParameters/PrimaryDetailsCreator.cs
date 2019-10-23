using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration.SerializableParameters
{
    [XmlRoot("PrimaryDetailsCreator")]
    public class PrimaryDetailsCreator : IParameterCreator
    {

        [XmlElement("collection")]
        public List<Collection> Collections { get; set; }

        public PrimaryDetailsCreator()
        {
            Collections = new List<Collection>();
        }

        public object Create()
        {
            var collections = new List<ICollection>();
            foreach (var c in Collections)
            {
                var collection = new Collection()
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    DOB = c.DOB,
                    Address = c.Address,
                    City = c.City,
                    State = c.State,
                    Zip = c.Zip,
                    Country = c.Country,
                    SSN = c.SSN,
                    DLNumber = c.DLNumber,
                    DLState = c.DLState,
                };

                collections.Add(collection);
            }
            return collections;
        }
    }

    public class Collection : ICollection
    {
        #region properties
        [XmlAttribute("firstname")]
        public string FirstName { get; set; }

        [XmlAttribute("lastname")]
        public string LastName { get; set; }

        [XmlAttribute("dob")]
        public string DOB { get; set; }

        [XmlAttribute("address")]
        public string Address { get; set; }

        [XmlAttribute("city")]
        public string City { get; set; }

        [XmlAttribute("state")]
        public string State { get; set; }

        [XmlAttribute("zip")]
        public string Zip { get; set; }

        [XmlAttribute("country")]
        public string Country { get; set; }

        [XmlAttribute("ssn")]
        public string SSN { get; set; }

        [XmlAttribute("dlnumber")]
        public string DLNumber { get; set; }

        [XmlAttribute("dlstate")]
        public string DLState { get; set; }
        #endregion
    }

    public interface ICollection
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string DOB { get; set; }
        string Address { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Zip { get; set; }
        string Country { get; set; }
        string SSN { get; set; }
        string DLNumber { get; set; }
        string DLState { get; set; }
    }
}
