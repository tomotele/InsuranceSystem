using InsuranceSystem.ServiceContracts.CoreServicesInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.ServiceContracts
{
    public interface IServiceManager
    {
        IClaimsServices ClaimsServices { get; }
    }
}
