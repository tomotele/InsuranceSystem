using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Entities.DTOs
{
    public class PolicyHolderDTO
    {
        [DataType(DataType.Text)]
        public const string BindProperty = "FirstName,LastName,MidlleName,DateOfBirth,PolicyNumber,NationalIdentificationNumber";

        [Required(ErrorMessage = "Kindly input the first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Kindly input the last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Kindly input the middle name")]
        public string MidlleName { get; set; }
        [Required(ErrorMessage = "Kindly input the date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Kindly input the policy number")]
        public string PolicyNumber { get; set; }
        [Required(ErrorMessage = "Kindly input the policy holder national identification number")]
        public int NationalIdentificationNumber { get; set; }
    }
}
