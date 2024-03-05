using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using InsuranceSystem.API;
using InsuranceSystem.API.Controllers;
using InsuranceSystem.Entities;
using InsuranceSystem.Entities.DTOs;
using InsuranceSystem.Entities.Models;
using InsuranceSystem.Entities.Responses;
using InsuranceSystem.RepositoryContracts.IPersistenceRepository;
using InsuranceSystem.RepositoryContracts.IUnitOfWork;
using InsuranceSystem.RepositoryServices.UnitOfWork;
using InsuranceSystem.ServiceContracts.UtilityServiceInterface;
using InsuranceSystem.Services;
using InsuranceSystem.Services.CoreServices;
using InsuranceSystem.Services.UtilityServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using static InsuranceSystem.API.Extensions.ServiceExtensions;

namespace InsuranceSystemTest
{
    public class ClaimsServiceTest
    {
        protected readonly IFixture _fixture;
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly Mock<ILoggerManager> _loggerManagerMock;
        private readonly Mock<IMapper> _mapperMock;



        public ClaimsServiceTest()
        {

            _repositoryMock = new Mock<IRepositoryManager>();
            _loggerManagerMock = new Mock<ILoggerManager>();
            _mapperMock = new Mock<IMapper>();


            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }

        [Fact]
        public void ClaimsRepository_GetAllClaims_ShouldNotBeNull_ShouldBeOfTypeInsuranceClaims_ShouldReturnClaims()
        {
            var mockRepo = new Mock<IClaimsRepository>();
            var claims = new List<InsuranceClaims>
                {
                    _fixture.Build<InsuranceClaims>().Create(),
                    _fixture.Build<InsuranceClaims>().Create()
                };

            mockRepo.Setup(repo => (repo.GetAllClaimsAsync(false))).ReturnsAsync(claims);
            var result = mockRepo.Object.GetAllClaimsAsync(false).GetAwaiter().GetResult();

            result.Should().NotBeNull(null);
            result.Should().BeOfType<List<InsuranceClaims>>(null);
            result.Should().BeEquivalentTo(claims);

        }

        [Theory]
        [InlineData(1)]
        public void ClaimsRepository_GetClaimsByIdAsync_ShouldNotBeNull_ShouldBeOfTypeInsuranceClaims_ShouldReturnClaims(int Id)
        {
            var mockRepo = new Mock<IClaimsRepository>();
            InsuranceClaims claims = _fixture.Build<InsuranceClaims>().Create();

            mockRepo.Setup(repo => (repo.GetClaimsByIdAsync(Id, false))).ReturnsAsync(claims);

            var result = mockRepo.Object.GetClaimsByIdAsync(Id, false).GetAwaiter().GetResult();

            result.Should().BeOfType<InsuranceClaims>();
            result.Should().NotBe(null);
            result.Should().BeEquivalentTo(claims);

        }


        [Theory]
        [InlineData(1)]
        public async Task ClaimsRepository_GetClaimById_WhenExceptionThrown_ShouldReturnErrorResponse(int id)
        {
            var mockRepository = new Mock<IClaimsRepository>();
            var service = new ClaimsServices(_repositoryMock.Object, _loggerManagerMock.Object, _mapperMock.Object);

            mockRepository.Setup(repo => repo.GetClaimsByIdAsync(id, false)).ThrowsAsync(new Exception("Mock exception"));

            var response = await service.GetClaimById(id);

            response.Should().NotBeNull();
            response.Successful.Should().BeFalse();
            response.Errors.Should().Contain(error => error.Code == Message.ErrorCode.ERROR);
        }


        [Fact]
        public async Task ClaimsRepository_GetAllClaims_WhenExceptionThrown_ShouldReturnErrorResponse()
        {
            var repositoryMock = new Mock<IClaimsRepository>();
            var loggerMock = new Mock<ILogger<ClaimsServices>>();

            repositoryMock.Setup(repo => repo.GetAllClaimsAsync(false)).ThrowsAsync(new Exception("Test exception"));
            var service = new ClaimsServices(_repositoryMock.Object, _loggerManagerMock.Object, _mapperMock.Object);

            var result = await service.GetAllClaims();

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
        public async Task<IActionResult> ClaimsController_IntegrationTesting()
        {
            var mapper = GetMapper();
            var logger = LoggerManagerLocator.GetLoggerManager();
            InsuranceClaimsDTO claims = _fixture.Build<InsuranceClaimsDTO>().Create();
            UpdateClaimsDTO Updateclaims = _fixture.Build<UpdateClaimsDTO>().Create();
            Updateclaims.ClaimsId = 1;
            Updateclaims.User = "Tosin";
            Updateclaims.Comment = "Bad Request";


            DbContextOptionsBuilder<RepositoryContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);

            IActionResult createClaimsResponse;
            IActionResult getAllClaimsResponse;
            IActionResult getClaimsByIdResponse;
            IActionResult getClaimsByIdResponse_NegativeTest;
            IActionResult approveClaimResponse;
            IActionResult rejectClaimResponse;


            using (RepositoryContext context = new(optionsBuilder.Options))
            {
                var repositoryManager = new RepositoryManager(context);
                var serviceManager = new ServiceManager(repositoryManager, logger, mapper);

                createClaimsResponse = await new ClaimsController(serviceManager).CreateClaimsAsync(claims);
                getAllClaimsResponse = await new ClaimsController(serviceManager).GetAllClaimsAsync();
                getClaimsByIdResponse = await new ClaimsController(serviceManager).GetClaimsByIdAsync(1);
                getClaimsByIdResponse_NegativeTest = await new ClaimsController(serviceManager).GetClaimsByIdAsync(50);
                approveClaimResponse = await new ClaimsController(serviceManager).ApproveClaimsByIdAsync(Updateclaims);
                rejectClaimResponse = await new ClaimsController(serviceManager).RejectClaimsByIdAsync(Updateclaims);
            }


            createClaimsResponse.Should().BeOfType<OkObjectResult>();
            createClaimsResponse.Should().NotBeNull(null);
            getAllClaimsResponse.Should().BeOfType<OkObjectResult>();
            getAllClaimsResponse.Should().NotBeNull(null);
            getClaimsByIdResponse.Should().BeOfType<OkObjectResult>();
            getClaimsByIdResponse.Should().NotBeNull(null);
            getClaimsByIdResponse_NegativeTest.Should().BeOfType<NotFoundObjectResult>();
            getClaimsByIdResponse_NegativeTest.Should().NotBeNull(null);
            approveClaimResponse.Should().BeOfType<OkObjectResult>();
            approveClaimResponse.Should().NotBeNull(null);
            rejectClaimResponse.Should().BeOfType<OkObjectResult>();
            rejectClaimResponse.Should().NotBeNull(null);

            return createClaimsResponse;
        }

    }
}
