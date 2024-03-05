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

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidlleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PolicyNumber { get; set; }
        public int NationalIdentificationNumber { get; set; }
    }
}
