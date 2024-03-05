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

        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper,
            IConfiguration configuration)
        {
            _claimsServices = new Lazy<IClaimsServices>(() => new ClaimsServices(repositoryManager, logger, mapper, configuration));
        }

        public IClaimsServices ClaimsServices => _claimsServices.Value;
    }
}
