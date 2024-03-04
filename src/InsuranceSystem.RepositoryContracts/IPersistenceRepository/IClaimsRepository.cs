using InsuranceSystem.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.RepositoryContracts.IPersistenceRepository
{
    public interface IClaimsRepository
    {
        void CreateClaims(Claims claims);
        Task<Claims> GetClaimsByIdAsync(int Id, bool trackChanges);
        Task<IEnumerable<Claims>> GetAllClaimsAsync(bool trackChanges);
    }
}
