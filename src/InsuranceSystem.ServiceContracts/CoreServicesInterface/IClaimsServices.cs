using InsuranceSystem.Entities.DTOs;
using InsuranceSystem.Entities.Models;
using InsuranceSystem.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.ServiceContracts.CoreServicesInterface
{
    public interface IClaimsServices
    {
        Task<ServiceResponse> CreateClaims(InsuranceClaimsDTO insuranceClaims);
        Task<ServiceResponse<IEnumerable<InsuranceClaims>>> GetAllClaims();
        Task<ServiceResponse<InsuranceClaims>> GetClaimById(int id);
        Task<ServiceResponse> ApproveClaim(UpdateClaimsDTO claims);
        Task<ServiceResponse> RejectClaim(UpdateClaimsDTO claims);
    }
}
