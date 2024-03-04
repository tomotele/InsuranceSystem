
using InsuranceSystem.ServiceContracts.UtilityServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Services.UtilityServices
{
    public class LoggerManager : ILoggerManager
    {

        private readonly Serilog.ILogger _logger;

        public LoggerManager(Serilog.ILogger logger)
        {
            _logger = logger;
        }
        public void LogDebug(string message, params object[] args) => _logger.Debug(message, args);
        public void LogError(string message, params object[] args) => _logger.Error(message, args);
        public void LogInformation(string message, params object[] args) => _logger.Information(message, args);
        public void LogWarn(string message, params object[] args) => _logger.Warning(message, args);
    }
}
