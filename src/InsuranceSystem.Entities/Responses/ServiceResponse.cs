using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Entities.Responses
{
    public class ServiceResponse
    {
        public bool Successful => !Errors.Any();
        public string Message { get; set; }
        public List<ServiceError> Errors { get; set; } = new List<ServiceError>();
    }
    public class ServiceResponse<T> : ServiceResponse
    {
        public T Result { get; set; }
    }
    public class ServiceError
    {
        public ServiceError(string code, string error)
        {
            Code = code;
            Error = error;
            TimeStamp = DateTime.Now;
        }
        public string Error { get; set; }
        public string Code { get; set; }
        public DateTime TimeStamp { get; } = DateTime.Now;

    }
}
