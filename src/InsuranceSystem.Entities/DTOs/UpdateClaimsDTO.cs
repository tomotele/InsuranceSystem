using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Entities.DTOs
{
    public class UpdateClaimsDTO
    {
        [DataType(DataType.Text)]
        public const string BindProperty = "ClaimsId,User,Comment";

        [Required(ErrorMessage = "Kindly input the claims Id")]
        public int ClaimsId { get; set; }
        [Required(ErrorMessage = "Kindly input user")]
        public string User { get; set; }
        public string Comment { get; set; }
    }
}
