using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Entities.Models
{
    public class Claims : BaseModel
    {
        public string Id { get; set; }
        public string PolicyHolderNationalID { get; set; } 
        public string ExpenseType { get; set; }
        public string ExpenseName { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ClaimStatus { get; set; }
    }
}
