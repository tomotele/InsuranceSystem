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
    public interface IPolicyHolderServices
    {
        Task<ServiceResponse> CreatePolicyHolder(PolicyHolderDTO policyHolders);
        Task<ServiceResponse<IEnumerable<PolicyHolders>>> GetAllPolicyHolder();
        Task<ServiceResponse<PolicyHolders>> GetPolicyHolderById(int id);
    }
}
