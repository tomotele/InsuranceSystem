using AutoMapper;
using InsuranceSystem.Entities.DTOs;
using InsuranceSystem.Entities.Enums;
using InsuranceSystem.Entities.Models;
using InsuranceSystem.Entities.Responses;
using InsuranceSystem.RepositoryContracts.IUnitOfWork;
using InsuranceSystem.ServiceContracts.CoreServicesInterface;
using InsuranceSystem.ServiceContracts.UtilityServiceInterface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Services.CoreServices
{
    internal sealed class ClaimsServices : IClaimsServices
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ClaimsServices(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Task<ServiceResponse> ApproveClaim(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> CreateClaims(InsuranceClaimsDTO insuranceClaims)
        {
            _logger.LogInformation($"About to create claims for expense name : {insuranceClaims.ExpenseName}");
            ServiceResponse response = new ServiceResponse();
            try
            {
                var claimsEntity = _mapper.Map<InsuranceClaims>(insuranceClaims);
                claimsEntity.CreatedDate = DateTime.Now;
                claimsEntity.ClaimStatus = ClaimStatus.Submitted.ToString();
            }
            catch (Exception ex)
            {
                response.Message = "An error occured while submitting offpass formA";
                response.Errors.Add(new ServiceError(Message.ErrorCode.ERROR, $"An error occured while creating claims due to {ex.Message}"));
                _logger.LogError($"Error occured while trying to create claims due to {ex.Message}");
                return response;
            }

            return response;
        }

        public Task<ServiceResponse> DeleteClaimById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<IEnumerable<InsuranceClaims>>> GetAllClaims()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> GetClaimById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> RejectClaim(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateClaim(InsuranceClaimsDTO insuranceClaims)
        {
            throw new NotImplementedException();
        }
    }
}
