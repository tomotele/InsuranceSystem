using InsuranceSystem.Entities;
using InsuranceSystem.RepositoryContracts.IPersistenceRepository;
using InsuranceSystem.RepositoryContracts.IUnitOfWork;
using InsuranceSystem.RepositoryServices.PersistenceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.RepositoryServices.UnitOfWork
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IClaimsRepository> _claimsRepository;
        private readonly Lazy<IPolicyHolderRepository> _policyHolderRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _claimsRepository = new Lazy<IClaimsRepository>(() => new ClaimsRepository(repositoryContext));
            _policyHolderRepository = new Lazy<IPolicyHolderRepository>(() => new PolicyHolderRepository(repositoryContext));
        }

        public IClaimsRepository ClaimsRepository => _claimsRepository.Value;
        public IPolicyHolderRepository PolicyHolderRepository => _policyHolderRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
