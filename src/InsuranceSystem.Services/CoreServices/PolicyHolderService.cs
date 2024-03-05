using AutoMapper;
using InsuranceSystem.Entities.DTOs;
using InsuranceSystem.Entities.Enums;
using InsuranceSystem.Entities.Models;
using InsuranceSystem.Entities.Responses;
using InsuranceSystem.RepositoryContracts.IUnitOfWork;
using InsuranceSystem.ServiceContracts.CoreServicesInterface;
using InsuranceSystem.ServiceContracts.UtilityServiceInterface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSystem.Services.CoreServices
{
    internal sealed class PolicyHolderService : IPolicyHolderServices
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public PolicyHolderService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        #region CREATE_POLICY_HOLDER
        public async Task<ServiceResponse> CreatePolicyHolder(PolicyHolderDTO policyHolders)
        {
            _logger.LogInformation($"About to create policy holder with first name : {policyHolders.FirstName}");
            ServiceResponse response = new ServiceResponse();
            try
            {
                var policyHolderEntity = _mapper.Map<PolicyHolders>(policyHolders);
                policyHolderEntity.CreatedDate = DateTime.Now;

                _repository.PolicyHolderRepository.CreatePolicyHolder(policyHolderEntity);
                await _repository.SaveAsync();
                _logger.LogInformation($"Successfully created policy holder with first name : {policyHolders.FirstName}");
                response.Message = $"Successfully created policy holder with first name : {policyHolders.FirstName}";
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured while creating policy holder with first name : {policyHolders.FirstName}";
                response.Errors.Add(new ServiceError(Message.ErrorCode.ERROR, $"An error occured while creating policy holder due to {ex.Message}"));
                _logger.LogError($"Error occured while trying to create policy holder with first name : {policyHolders.FirstName}. due to {ex.Message}");
                return response;
            }
            return response;
        }
        #endregion

        #region GET_POLICY_HOLDER_BY_ID
        public async Task<ServiceResponse<PolicyHolders>> GetPolicyHolderById(int id)
        {
            ServiceResponse<PolicyHolders> response = new ServiceResponse<PolicyHolders>();
            var logSerializer = new { Id = id };
            try
            {
                _logger.LogInformation($"About to retrive policy holder with Id : {JsonConvert.SerializeObject(logSerializer.Id)} from the database");
                var policyHolder = await _repository.PolicyHolderRepository.GetPolicyHolderByIdAsync(id, false);
                if (policyHolder != null)
                {
                    _logger.LogInformation($"Policy holder with Id : {JsonConvert.SerializeObject(logSerializer.Id)} retrived from database successfully");
                    response.Message = $"Successfully retrieved policy holder with Id : {JsonConvert.SerializeObject(logSerializer.Id)}";
                    response.Result = policyHolder;
                }
                else
                {
                    response.Message = $"Policy holder with Id : {JsonConvert.SerializeObject(logSerializer.Id)} not found";
                    response.Errors.Add(new ServiceError(Message.ErrorCode.NOT_FOUND, $"Policy holder with Id : {JsonConvert.SerializeObject(logSerializer.Id)} not found"));
                    _logger.LogError($"Policy holder with Id : {JsonConvert.SerializeObject(logSerializer.Id)} not found");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"An error occured while getting policy holder with Id : {JsonConvert.SerializeObject(logSerializer.Id)}";
                response.Errors.Add(new ServiceError(Message.ErrorCode.ERROR, $"An error occured while getting policy holder with Id : {JsonConvert.SerializeObject(logSerializer.Id)} due to {ex.Message}"));
                _logger.LogError($"Error occured while trying to get policy holder with Id : {JsonConvert.SerializeObject(logSerializer)} due to {ex.Message}");
                return response;
            }
            return response;
        }
        #endregion

        #region GET_ALL_POLICY_HOLDERS
        public async Task<ServiceResponse<IEnumerable<PolicyHolders>>> GetAllPolicyHolder()
        {
            ServiceResponse<IEnumerable<PolicyHolders>> response = new ServiceResponse<IEnumerable<PolicyHolders>>();
            try
            {
                _logger.LogInformation($"About to get all policy holders");
                var policyHolders = await _repository.PolicyHolderRepository.GetAllPolicyHoldersAsync(false);
                if (policyHolders.Count() > 0)
                {
                    _logger.LogInformation("Policy holders retrived from database successfully");
                    response.Message = $"Successfully retrieved all policy holders";
                    response.Result = policyHolders;
                }
                else
                {
                    response.Message = $"Policy holders not found";
                    response.Errors.Add(new ServiceError(Message.ErrorCode.NOT_FOUND, $"Policy holders not found"));
                    _logger.LogError($"Policy holders not found");
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Message = "An error occured while getting all policy holders";
                response.Errors.Add(new ServiceError(Message.ErrorCode.ERROR, $"An error occured while getting all policy holders due to {ex.Message}"));
                _logger.LogError($"An error occured while getting all policy holders due to {ex.Message}");
            }
            return response;
        }
        #endregion
    }
}
