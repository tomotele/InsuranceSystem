using InsuranceSystem.API.Filters;
using InsuranceSystem.Entities.DTOs;
using InsuranceSystem.Entities.Responses;
using InsuranceSystem.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceSystem.API.Controllers
{
    [Route("api/claims")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IServiceManager _service;
        public ClaimsController(IServiceManager service)
        {
            _service = service;
        }

        #region CREATE_CLAIMS
        [HttpPost("CreateClaims", Name = "CreateClaims")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateOffpassFormAAsync([Bind(InsuranceClaimsDTO.BindProperty)][FromBody] InsuranceClaimsDTO claims)
        {
            ServiceResponse response = new ServiceResponse();

            ServiceResponse serviceResponse = await _service.ClaimsServices.CreateClaims(claims);
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
    }
}
