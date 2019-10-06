using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Parameter
{
    public class Applicants
    {
        public string PrimaryFirstName { get; set; }
        public string PrimaryLastname { get; set; }
        public string PrimaryDOB{ get; set; }
        public string PrimaryStreetAddress { get; set; }
        public string PrimaryZip { get; set; }
        public string PrimaryPhone { get; set; }
        public string PrimaryPhoneType { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryContactMethod { get; set; }
        public string PrimarySSN { get; set; }
        public string PrimaryIdType { get; set; }
        public string PrimaryStateId { get; set; }
        public string PrimaryIdentificationNumber { get; set; }
        public string PrimaryIdIssueDate { get; set; }
        public string PrimaryIdExpDate { get; set; }


        public string JointFirstName { get; set; }
        public string JointLastName { get; set; }
        public string JointDOB { get; set; }
        public string JointStreetAddress { get; set; }
        public string JointZip { get; set; }
        public string JointPrimaryPhone { get; set; }
        public string JointPhoneType { get; set; }
        public string JointEmail { get; set; }
        public string JointContactMethod { get; set; }
        public string JointSSN { get; set; }
        public string JointIdType { get; set; }
        public string JointStateId { get; set; }
        public string JointIdentificationNumber { get; set; }
        public string JointIdIssueDate { get; set; }
        public string JointIdExpDate { get; set; }


        public string BeneficiaryFirstName { get; set; }
        public string BeneficiaryLastName { get; set; }
        public string BeneficiaryDOB { get; set; }
        public string BeneficiaryRelation { get; set; }
        public string BeneficiaryDeathRatio { get; set; }
        public string BeneficiarySSN { get; set; }
    }
}
