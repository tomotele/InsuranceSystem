using InsuranceSystem.Entities;
using InsuranceSystem.Entities.Models;
using InsuranceSystem.RepositoryContracts.IPersistenceRepository;
using InsuranceSystem.RepositoryServices.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.RepositoryServices.PersistenceRepository
{
    public class PolicyHolderRepository : RepositoryBase<PolicyHolders>, IPolicyHolderRepository
    {
        public PolicyHolderRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreatePolicyHolder(PolicyHolders policyHolders) => Create(policyHolders);

        public async Task<PolicyHolders> GetPolicyHolderByIdAsync(int Id, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(Id), trackChanges)
        .SingleOrDefaultAsync();

        public async Task<IEnumerable<PolicyHolders>> GetAllPolicyHoldersAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .OrderBy(c => c.CreatedDate)
        .ToListAsync();
    }
}
