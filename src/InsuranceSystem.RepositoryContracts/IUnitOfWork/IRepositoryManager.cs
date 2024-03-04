using InsuranceSystem.RepositoryContracts.IPersistenceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.RepositoryContracts.IUnitOfWork
{
    public interface IRepositoryManager
    {
        IPolicyHolderRepository PolicyHolderRepository { get; }
        IClaimsRepository ClaimsRepository { get; }
        Task SaveAsync();
    }
}
