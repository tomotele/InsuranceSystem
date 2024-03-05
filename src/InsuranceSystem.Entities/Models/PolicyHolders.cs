using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Entities.Models
{
    public class PolicyHolders : BaseModel
    {
        [Column("PolicyHolderId")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidlleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PolicyNumber { get; set; }
        public int NationalIdentificationNumber { get; set; }

    }
}
