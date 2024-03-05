using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Entities.Models
{
    public class InsuranceClaims : BaseModel
    {
        [Column("ClaimsId")]
        public string Id { get; set; }
        [ForeignKey(nameof(PolicyHolders))]
        public int PolicyHolderId { get; set; }
        public PolicyHolders PolicyHolders { get; set; }
        public int PolicyHolderNationalID { get; set; } 
        public int ExpenseType { get; set; }
        public string ExpenseName { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ClaimStatus { get; set; }
    }
}
