using InsuranceSystem.API.Filters;
using InsuranceSystem.Entities.DTOs;
using InsuranceSystem.Entities.Models;
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
        public async Task<IActionResult> CreateClaimsAsync([Bind(InsuranceClaimsDTO.BindProperty)][FromBody] InsuranceClaimsDTO claims)
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

        #region GET_CLAIMS_BY_ID
        [HttpGet("ClaimsByID/{Id}", Name = "ClaimsByID")]
        public async Task<IActionResult> GetClaimsByIdAsync(int Id)
        {
            ServiceResponse<InsuranceClaims> response = new ServiceResponse<InsuranceClaims>();

            ServiceResponse<InsuranceClaims> serviceResponse = await _service.ClaimsServices.GetClaimById(Id);
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

        #region GET_ALL_CLAIMS
        [HttpGet("GetAllClaims", Name = "GetAllClaims")]
        public async Task<IActionResult> GetAllClaimsAsync()
        {
            ServiceResponse<IEnumerable<InsuranceClaims>> response = new ServiceResponse<IEnumerable<InsuranceClaims>>();

            ServiceResponse<IEnumerable<InsuranceClaims>> serviceResponse = await _service.ClaimsServices.GetAllClaims();
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

        #region APPROVE_CLAIMS_BY_ID
        [HttpPost("ApproveClaims", Name = "ApproveClaims")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ApproveClaimsByIdAsync([Bind(UpdateClaimsDTO.BindProperty)][FromBody] UpdateClaimsDTO claims)
        {
            ServiceResponse response = new ServiceResponse<InsuranceClaims>();

            ServiceResponse serviceResponse = await _service.ClaimsServices.ApproveClaim(claims);
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

        #region REJECT_CLAIMS_BY_ID
        [HttpPost("RejectClaims", Name = "RejectClaims")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RejectClaimsByIdAsync([Bind(UpdateClaimsDTO.BindProperty)][FromBody] UpdateClaimsDTO claims)
        {
            ServiceResponse response = new ServiceResponse<InsuranceClaims>();

            ServiceResponse serviceResponse = await _service.ClaimsServices.RejectClaim(claims);
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
