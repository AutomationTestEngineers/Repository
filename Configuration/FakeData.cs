using Bogus;
using Configuration.SerializableParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class FakeData
    {
        static string Prefix = "Auto_";
        public static string FirstName
        {
            get
            {
                return new Bogus.DataSets.Name().FirstName().ToString();
            }
        }
        public static string LastName
        {
            get
            {
                return new Bogus.DataSets.Name().LastName().ToString();
            }
        }
        public static string MiddleName
        {
            get
            {
                return new Bogus.DataSets.Name().ToString();
            }
        }
        
        public static string FullName
        {
            get
            {
                return $"{Prefix} {FirstName} {LastName}";
            }
        }
        public static string Word
        {
            get
            {
                return $"{Prefix}{new Bogus.DataSets.Name().Random.Words(10)}";
            }
        }
        public static string FullAddress
        {
            get { return new Bogus.DataSets.Address().FullAddress(); }
        }
        public static string Zip
        {
            get { return new Bogus.DataSets.Address().ZipCode(); }
        }
        public static string StreetAddress
        {
            get { return new Bogus.DataSets.Address().StreetAddress(); }
        }
        public static string Number(int min = 1, int max = 1000)
        {
            return $"{new Bogus.Randomizer().Number(min, max)}";
        }
        
        
        public static Employee RandomEmployee()
        {
            return new Faker<Employee>()
                 .RuleFor(pi => pi.Employer, a =>"Test "+ a.Person.FirstName)
                 .RuleFor(pi => pi.Occupation, a => a.Name.JobType())
                 .RuleFor(pi => pi.Address, a => a.Address.StreetAddress())
                 .RuleFor(pi => pi.Suite, a => a.Address.StreetName())
                 .RuleFor(pi => pi.Zip, a => "99501")
                 .RuleFor(pi => pi.Phone, a => Parameter.Get<string>("Cell")).Generate();

        }
        
    }
}
