using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using InsuranceSystem.API.Controllers;
using InsuranceSystem.Entities.DTOs;
using InsuranceSystem.Entities;
using InsuranceSystem.Entities.Models;
using InsuranceSystem.Entities.Responses;
using InsuranceSystem.RepositoryContracts.IPersistenceRepository;
using InsuranceSystem.RepositoryContracts.IUnitOfWork;
using InsuranceSystem.RepositoryServices.UnitOfWork;
using InsuranceSystem.ServiceContracts.UtilityServiceInterface;
using InsuranceSystem.Services;
using InsuranceSystem.Services.CoreServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using static InsuranceSystem.API.Extensions.ServiceExtensions;
using InsuranceSystem.API;

namespace InsuranceSystemTest
{
    public class PolicyHolderTest
    {
        protected readonly IFixture _fixture;
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly Mock<ILoggerManager> _loggerManagerMock;
        private readonly Mock<IMapper> _mapperMock;


        public PolicyHolderTest()
        {
            _repositoryMock = new Mock<IRepositoryManager>();
            _loggerManagerMock = new Mock<ILoggerManager>();
            _mapperMock = new Mock<IMapper>();

            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }

        [Fact]
        public void PolicyHolderRepository_GetAllPolicyHolders_ShouldNotBeNull_ShouldBeOfTypeInsuranceClaims_ShouldReturnClaims()
        {
            var mockRepo = new Mock<IPolicyHolderRepository>();
            var policyHolder = new List<PolicyHolders>
                {
                    _fixture.Build<PolicyHolders>().Create(),
                    _fixture.Build<PolicyHolders>().Create()
                };

            mockRepo.Setup(repo => (repo.GetAllPolicyHoldersAsync(false))).ReturnsAsync(policyHolder);
            var result = mockRepo.Object.GetAllPolicyHoldersAsync(false).GetAwaiter().GetResult();

            result.Should().NotBeNull(null);
            result.Should().BeOfType<List<PolicyHolders>>(null);
            result.Should().BeEquivalentTo(policyHolder);

        }

        [Theory]
        [InlineData(1)]
        public void PolicyHolderRepository_GetClaimsByIdAsync_ShouldNotBeNull_ShouldBeOfTypeInsuranceClaims_ShouldReturnClaims(int Id)
        {
            var mockRepo = new Mock<IPolicyHolderRepository>();
            PolicyHolders policyHolder = _fixture.Build<PolicyHolders>().Create();

            mockRepo.Setup(repo => (repo.GetPolicyHolderByIdAsync(Id, false))).ReturnsAsync(policyHolder);

            var result = mockRepo.Object.GetPolicyHolderByIdAsync(Id, false).GetAwaiter().GetResult();

            result.Should().BeOfType<PolicyHolders>();
            result.Should().NotBe(null);
            result.Should().BeEquivalentTo(policyHolder);

        }


        [Theory]
        [InlineData(1)]
        public async Task PolicyHolderRepository_GetClaimById_WhenExceptionThrown_ShouldReturnErrorResponse(int id)
        {
            var mockRepository = new Mock<IPolicyHolderRepository>();
            var service = new PolicyHolderService(_repositoryMock.Object, _loggerManagerMock.Object, _mapperMock.Object);

            mockRepository.Setup(repo => repo.GetPolicyHolderByIdAsync(id, false)).ThrowsAsync(new Exception("Mock exception"));

            var response = await service.GetPolicyHolderById(id);

            response.Should().NotBeNull();
            response.Successful.Should().BeFalse();
            response.Errors.Should().Contain(error => error.Code == Message.ErrorCode.ERROR);
        }


        [Fact]
        public async Task PolicyHolderRepository_GetAllClaims_WhenExceptionThrown_ShouldReturnErrorResponse()
        {
            var repositoryMock = new Mock<IPolicyHolderRepository>();

            repositoryMock.Setup(repo => repo.GetAllPolicyHoldersAsync(false)).ThrowsAsync(new Exception("Mock exception"));
            var service = new PolicyHolderService(_repositoryMock.Object, _loggerManagerMock.Object, _mapperMock.Object);

            var result = await service.GetAllPolicyHolder();

            result.Should().NotBeNull();
            result.Successful.Should().BeFalse();
            result.Errors.Should().Contain(error => error.Code == Message.ErrorCode.ERROR);
        }

        public IMapper GetMapper()
        {
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }

        [Fact]
        public async Task<IActionResult> PolicyHolderController_IntegrationTesting()
        {
            var mapper = GetMapper();
            var logger = LoggerManagerLocator.GetLoggerManager();
            PolicyHolderDTO policyHolder = _fixture.Build<PolicyHolderDTO>().Create();

            DbContextOptionsBuilder<RepositoryContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            IActionResult createPolicyHolderResponse;
            IActionResult getAllPolicyHolderResponse;
            IActionResult getPolicyHolderByIdResponse;
            IActionResult getPolicyHolderByIdResponse_NegativeTest;


            using (RepositoryContext context = new(optionsBuilder.Options))
            {
                var repositoryManager = new RepositoryManager(context);
                var serviceManager = new ServiceManager(repositoryManager, logger, mapper);

                createPolicyHolderResponse = await new PolicyHoldersController(serviceManager).CreatePolicyHolderAsync(policyHolder);
                getAllPolicyHolderResponse = await new PolicyHoldersController(serviceManager).GetAllPolicyHoldersAsync();
                getPolicyHolderByIdResponse = await new PolicyHoldersController(serviceManager).GetPolicyHolderByIdAsync(1);
                getPolicyHolderByIdResponse_NegativeTest = await new PolicyHoldersController(serviceManager).GetPolicyHolderByIdAsync(50);
            }


            createPolicyHolderResponse.Should().BeOfType<OkObjectResult>();
            createPolicyHolderResponse.Should().NotBeNull(null);
            getAllPolicyHolderResponse.Should().BeOfType<OkObjectResult>();
            getAllPolicyHolderResponse.Should().NotBeNull(null);
            getPolicyHolderByIdResponse.Should().BeOfType<OkObjectResult>();
            getPolicyHolderByIdResponse.Should().NotBeNull(null);
            getPolicyHolderByIdResponse_NegativeTest.Should().BeOfType<NotFoundObjectResult>();
            getPolicyHolderByIdResponse_NegativeTest.Should().NotBeNull(null);

            return createPolicyHolderResponse;
        }

    }
}
