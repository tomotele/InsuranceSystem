using AutoMapper;
using InsuranceSystem.RepositoryContracts.IUnitOfWork;
using InsuranceSystem.ServiceContracts;
using InsuranceSystem.ServiceContracts.CoreServicesInterface;
using InsuranceSystem.ServiceContracts.UtilityServiceInterface;
using InsuranceSystem.Services.CoreServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IClaimsServices> _claimsServices;
        private readonly Lazy<IPolicyHolderServices> _policyHolderServices;

        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper)
        {
            _claimsServices = new Lazy<IClaimsServices>(() => new ClaimsServices(repositoryManager, logger, mapper));
            _policyHolderServices = new Lazy<IPolicyHolderServices>(() => new PolicyHolderService(repositoryManager, logger, mapper));
        }

        public IClaimsServices ClaimsServices => _claimsServices.Value;
        public IPolicyHolderServices PolicyHolderService => _policyHolderServices.Value;
    }
}
