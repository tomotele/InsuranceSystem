using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.ServiceContracts.UtilityServiceInterface
{
    public interface ILoggerManager
    {

        public void LogDebug(string message, params object[] args);

        public void LogError(string message, params object[] args);

        public void LogInformation(string message, params object[] args);

        public void LogWarn(string message, params object[] args);
    }
}
