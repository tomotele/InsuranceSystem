using InsuranceSystem.API.Filters;
using InsuranceSystem.Entities.DTOs;
using InsuranceSystem.Entities.Models;
using InsuranceSystem.Entities.Responses;
using InsuranceSystem.ServiceContracts;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceSystem.API.Controllers
{
    public class PolicyHoldersController : ControllerBase
    {
        private readonly IServiceManager _service;
        public PolicyHoldersController(IServiceManager service)
        {
            _service = service;
        }

        #region CREATE_POLICY_HOLDER
        [HttpPost("CreatePolicyHolder", Name = "CreatePolicyHolder")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePolicyHolderAsync([Bind(PolicyHolderDTO.BindProperty)][FromBody] PolicyHolderDTO policyHolder)
        {
            ServiceResponse response = new ServiceResponse();

            ServiceResponse serviceResponse = await _service.PolicyHolderService.CreatePolicyHolder(policyHolder);
            if (serviceResponse.Successful)
            {
                response.Message = serviceResponse.Message;
                return Ok(response);
            }
            else
            {
                if (serviceResponse.Errors[0].Code == "404")
                {
                    response.Message = serviceResponse.Message;
                    response.Errors = serviceResponse.Errors;
                    return NotFound(response);
                }
                response.Message = serviceResponse.Message;
                response.Errors = serviceResponse.Errors;
                return BadRequest(response);
            }
        }
        #endregion

        #region GET_POLICY_HOLDER_BY_ID
        [HttpGet("PolicyHolderByID/{Id}", Name = "PolicyHolderByID")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetPolicyHolderByIdAsync(int Id)
        {
            ServiceResponse<PolicyHolders> response = new ServiceResponse<PolicyHolders>();

            ServiceResponse<PolicyHolders> serviceResponse = await _service.PolicyHolderService.GetPolicyHolderById(Id);
            if (serviceResponse.Successful)
            {
                response.Message = serviceResponse.Message;
                response.Result = serviceResponse.Result;
                return Ok(response);
            }
            else
            {
                if (serviceResponse.Errors[0].Code == "404")
                {
                    response.Message = serviceResponse.Message;
                    response.Errors = serviceResponse.Errors;
                    return NotFound(response);
                }
                response.Message = serviceResponse.Message;
                response.Errors = serviceResponse.Errors;
                return BadRequest(response);
            }
        }
        #endregion

        #region GET_ALL_POLICY_HOLDERS
        [HttpGet("GetAllPolicyHolders", Name = "GetAllPolicyHolders")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetAllPolicyHoldersAsync()
        {
            ServiceResponse<IEnumerable<PolicyHolders>> response = new ServiceResponse<IEnumerable<PolicyHolders>>();

            ServiceResponse<IEnumerable<PolicyHolders>> serviceResponse = await _service.PolicyHolderService.GetAllPolicyHolder();
            if (serviceResponse.Successful)
            {
                response.Message = serviceResponse.Message;
                response.Result = serviceResponse.Result;
                return Ok(response);
            }
            else
            {
                if (serviceResponse.Errors[0].Code == "404")
                {
                    response.Message = serviceResponse.Message;
                    response.Errors = serviceResponse.Errors;
                    return NotFound(response);
                }
                response.Message = serviceResponse.Message;
                response.Errors = serviceResponse.Errors;
                return BadRequest(response);
            }
        }
        #endregion
    }
}
