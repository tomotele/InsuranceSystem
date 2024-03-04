using InsuranceSystem.Entities.Models;
using InsuranceSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceSystem.RepositoryContracts.IPersistenceRepository;
using InsuranceSystem.RepositoryServices.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace InsuranceSystem.RepositoryServices.PersistenceRepository
{
    public class ClaimsRepository : RepositoryBase<Claims>, IClaimsRepository
    {
        public ClaimsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateClaims(Claims claims) => Create(claims);

        public async Task<Claims> GetClaimsByIdAsync(int Id, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(Id), trackChanges)
        .SingleOrDefaultAsync();

        public async Task<IEnumerable<Claims>> GetAllClaimsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .OrderBy(c => c.CreatedDate)
        .ToListAsync();
    }
}
