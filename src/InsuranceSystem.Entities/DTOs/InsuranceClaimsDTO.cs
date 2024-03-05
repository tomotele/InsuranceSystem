using InsuranceSystem.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Entities.DTOs
{
    public class InsuranceClaimsDTO
    {
        [DataType(DataType.Text)]
        public const string BindProperty = "PolicyHolderNationalID,ExpenseName,Amount,ExpenseType,ExpenseDate";

        [Required(ErrorMessage = "Kindly input the policy holder national identification number")]
        public int PolicyHolderNationalID { get; set; }
        [Required(ErrorMessage = "Kindly input the policy holder Id")]
        public int PolicyHolderId { get; set; }
        public string ExpenseName { get; set; }
        public decimal Amount { get; set; }
        public ExpenseType ExpenseType { get; set; }
        [Required(ErrorMessage = "Kindly input the date of expense")]
        public DateTime ExpenseDate { get; set; }
    }
}
