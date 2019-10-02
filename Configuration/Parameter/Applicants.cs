using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Parameter
{
    interface IApplicants
    {
        string PrimaryFirstName { get; set; }
        string PrimaryLastname { get; set; }
        string PrimaryDOB{ get; set; }
        string PrimaryStreetAddress { get; set; }
        string PrimaryZip { get; set; }
        string PrimaryPhone { get; set; }
        string PrimaryPhoneType { get; set; }
        string PrimaryEmail { get; set; }
        string PrimaryContactMethod { get; set; }
        string PrimarySSN { get; set; }
        string PrimaryIdType { get; set; }
        string PrimaryStateId { get; set; }
        string PrimaryIdentificationNumber { get; set; }
        string PrimaryIdIssueDate { get; set; }
        string PrimaryIdExpDate { get; set; }


        string JointFirstName { get; set; }
        string JointLastName { get; set; }
        string JointDOB { get; set; }
        string JointStreetAddress { get; set; }
        string JointZip { get; set; }
        string JointPrimaryPhone { get; set; }
        string JointPhoneType { get; set; }
        string JointEmail { get; set; }
        string JointContactMethod { get; set; }
        string JointSSN { get; set; }
        string JointIdType { get; set; }
        string JointStateId { get; set; }
        string JointIdentificationNumber { get; set; }
        string JointIdIssueDate { get; set; }
        string JointIdExpDate { get; set; }


        string BeneficiaryFirstName { get; set; }
        string BeneficiaryLastName { get; set; }
        string BeneficiaryDOB { get; set; }
        string BeneficiaryRelation { get; set; }
        string BeneficiaryDeathRatio { get; set; }
        string BeneficiarySSN { get; set; }
    }
}
