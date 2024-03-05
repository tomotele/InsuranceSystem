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
        Task<ServiceResponse> GetClaimById(int id);
        Task<ServiceResponse> ApproveClaim(int Id);
        Task<ServiceResponse> RejectClaim(int Id);
        Task<ServiceResponse> UpdateClaim(InsuranceClaimsDTO insuranceClaims);
        Task<ServiceResponse> DeleteClaimById(int Id);
    }
}
