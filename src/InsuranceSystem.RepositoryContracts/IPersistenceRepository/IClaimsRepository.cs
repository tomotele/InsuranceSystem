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
        void CreateClaims(InsuranceClaims claims);
        void UpdateClaims(InsuranceClaims claims);
        Task<InsuranceClaims> GetClaimsByIdAsync(int Id, bool trackChanges);
        Task<IEnumerable<InsuranceClaims>> GetAllClaimsAsync(bool trackChanges);
    }
}
