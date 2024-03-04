using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Entities.Models
{
    public class Claims
    {
        public string ClaimID { get; set; }
        public string PolicyholderNationalID { get; set; } 
        public string ExpenseType { get; set; }
        public string ExpenseName { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfExpense { get; set; }
        public string ClaimStatus { get; set; }
    }
}
