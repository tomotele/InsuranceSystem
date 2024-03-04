using InsuranceSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.RepositoryContracts.IPersistenceRepository
{
    public interface IPolicyHolderRepository
    {
        void CreatePolicyHolder(PolicyHolders policyHolders);
        Task<PolicyHolders> GetPolicyHolderByIdAsync(int Id, bool trackChanges);
        Task<IEnumerable<PolicyHolders>> GetAllPolicyHoldersAsync(bool trackChanges);
    }
}
