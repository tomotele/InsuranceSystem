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
using System.Transactions;

namespace InsuranceSystem.Services.CoreServices
{
    public sealed class ClaimsServices : IClaimsServices
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ClaimsServices(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        #region CREATE_CLAIMS
        public async Task<ServiceResponse> CreateClaims(InsuranceClaimsDTO insuranceClaims)
        {
            _logger.LogInformation($"About to create claims for policy holder with Id : {insuranceClaims.PolicyHolderId}");
            ServiceResponse response = new ServiceResponse();
            try
            {
                var claimsEntity = _mapper.Map<InsuranceClaims>(insuranceClaims);
                claimsEntity.CreatedDate = DateTime.Now;
                claimsEntity.ClaimStatus = ClaimStatus.Submitted.ToString();

                _repository.ClaimsRepository.CreateClaims(claimsEntity);
                await _repository.SaveAsync();
                _logger.LogInformation($"Successfully created claims for policy holder with Id : {insuranceClaims.PolicyHolderId}");
                response.Message = $"Successfully Created claims for policy holder with Id : {insuranceClaims.PolicyHolderId}";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured while creating claims for policy holder with Id : {insuranceClaims.PolicyHolderId}";
                response.Errors.Add(new ServiceError(Message.ErrorCode.ERROR, $"An error occured while creating claims due to {ex.Message}"));
                _logger.LogError($"Error occured while trying to create claims for policy holder with Id : {insuranceClaims.PolicyHolderId}. due to {ex.Message}");
                return response;
            }
            return response;
        }
        #endregion

        #region GET_CLAIMS_BY_ID
        public async Task<ServiceResponse<InsuranceClaims>> GetClaimById(int id)
        {
            ServiceResponse<InsuranceClaims> response = new ServiceResponse<InsuranceClaims>();
            var logSerializer = new { Id = id };
            try
            {
                _logger.LogInformation($"About to retrive insurance claims with Id : {JsonConvert.SerializeObject(logSerializer.Id)} from the database");
                var claims = await _repository.ClaimsRepository.GetClaimsByIdAsync(id, false);
                if (claims != null)
                {
                    _logger.LogInformation($"Insurance claims  with Id : {JsonConvert.SerializeObject(logSerializer.Id)} retrived from database successfully");
                    response.Message = $"Successfully retrieved Insurance claims  with Id : {JsonConvert.SerializeObject(logSerializer.Id)}";
                    response.Result = claims;
                }
                else
                {
                    response.Message = $"Insurance claims with Id : {JsonConvert.SerializeObject(logSerializer.Id)} not found";
                    response.Errors.Add(new ServiceError(Message.ErrorCode.NOT_FOUND, $"Insurance claims with Id : {JsonConvert.SerializeObject(logSerializer.Id)} not found"));
                    _logger.LogError($"Insurance claims with Id : {JsonConvert.SerializeObject(logSerializer.Id)} not found");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured while getting Insurance claims with Id : {JsonConvert.SerializeObject(logSerializer.Id)}";
                response.Errors.Add(new ServiceError(Message.ErrorCode.ERROR, $"An error occured while getting Insurance claims with Id : {JsonConvert.SerializeObject(logSerializer.Id)} due to {ex.Message}"));
                _logger.LogError($"Error occured while trying to get Insurance claims with Id : {JsonConvert.SerializeObject(logSerializer.Id)} due to {ex.Message}");
                return response;
            }
            return response;
        }
        #endregion

        #region GET_ALL_CLAIMS
        public async Task<ServiceResponse<IEnumerable<InsuranceClaims>>> GetAllClaims()
        {
            ServiceResponse<IEnumerable<InsuranceClaims>> response = new ServiceResponse<IEnumerable<InsuranceClaims>>();
            try
            {
                _logger.LogInformation($"About to get all insurance claims");
                var claims = await _repository.ClaimsRepository.GetAllClaimsAsync(false);
                if (claims.Count() > 0)
                {
                    _logger.LogInformation("Insurance claims retrived from database successfully");
                    response.Message = $"Successfully retrieved all insurance claims";
                    response.Result = claims;
                }
                else
                {
                    response.Message = $"Insurance claims not found";
                    response.Errors.Add(new ServiceError(Message.ErrorCode.NOT_FOUND, $"Insurance claims not found"));
                    _logger.LogError($"Insurance claims not found");
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Message = "An error occured while getting all insurance claims";
                response.Errors.Add(new ServiceError(Message.ErrorCode.ERROR, $"An error occured while getting all insurance claims due to {ex.Message}"));
                _logger.LogError($"An error occured while getting all insurance claims due to {ex.Message}");
            }
            return response;
        }
        #endregion

        #region APPROVE_CLAIM_BY_ID
        public async Task<ServiceResponse> ApproveClaim(UpdateClaimsDTO claims)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                _logger.LogInformation($"About to approve claims with Id : {claims.ClaimsId}");
                var insuranceClaims = await _repository.ClaimsRepository.GetClaimsByIdAsync(claims.ClaimsId, true);
                if (insuranceClaims != null)
                {
                    insuranceClaims.UpdatedDate = DateTime.Now;
                    insuranceClaims.ModifiedBy = claims.User;
                    insuranceClaims.ClaimStatus = ClaimStatus.Approved.ToString();

                    _repository.ClaimsRepository.UpdateClaims(insuranceClaims);
                    await _repository.SaveAsync();
                    _logger.LogInformation($"Successfully approved claims with Id : {insuranceClaims.Id}");
                    response.Message = $"Successfully approved claims with Id : {insuranceClaims.Id}";
                }

                else
                {
                    response.Message = $"Claims Id not found while trying to approve claims";
                    response.Errors.Add(new ServiceError(Message.ErrorCode.NOT_FOUND, $"Claims with Id : {claims.ClaimsId} not found while trying to approve claims"));
                    _logger.LogError($"Claims with Id : {claims.ClaimsId} not found while trying to approve claims");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured while approving Claims with Id : {claims.ClaimsId}";
                response.Errors.Add(new ServiceError(Message.ErrorCode.ERROR, $"An error occured while approving Claims with Id : {claims.ClaimsId} due to {ex.Message}"));
                _logger.LogError($"Error occured while trying to approve Claims with Id : {claims.ClaimsId} due to {ex.Message}");
            }
            return response;
        }
        #endregion

        #region REJECT_CLAIM_BY_ID
        public async Task<ServiceResponse> RejectClaim(UpdateClaimsDTO claims)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                _logger.LogInformation($"About to reject claims with Id : {claims.ClaimsId}");
                var insuranceClaims = await _repository.ClaimsRepository.GetClaimsByIdAsync(claims.ClaimsId, true);
                if (insuranceClaims != null)
                {
                    insuranceClaims.UpdatedDate = DateTime.Now;
                    insuranceClaims.ModifiedBy = claims.User;
                    insuranceClaims.ClaimStatus = ClaimStatus.Declined.ToString();

                    _repository.ClaimsRepository.UpdateClaims(insuranceClaims);
                    await _repository.SaveAsync();
                    _logger.LogInformation($"Successfully rejected claims with Id : {insuranceClaims.Id}");
                    response.Message = $"Successfully rejected claims with Id : {insuranceClaims.Id}";
                }

                else
                {
                    response.Message = $"Claims with Id : {claims.ClaimsId} not found while trying to reject claims";
                    response.Errors.Add(new ServiceError(Message.ErrorCode.NOT_FOUND, $"Claims with Id : {claims.ClaimsId} not found while trying to reject claims"));
                    _logger.LogError($"Claims with Id : {claims.ClaimsId} not found while trying to reject claims");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured while rejecting Claims with Id : {claims.ClaimsId}";
                response.Errors.Add(new ServiceError(Message.ErrorCode.ERROR, $"An error occured while rejecting Claims with Id : {claims.ClaimsId} due to {ex.Message}"));
                _logger.LogError($"Error occured while trying to reject Claims with Id : {claims.ClaimsId} due to {ex.Message}");
            }
            return response;
        }
        #endregion

    }
}
