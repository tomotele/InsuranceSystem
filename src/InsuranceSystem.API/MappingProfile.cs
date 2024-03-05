using AutoMapper;
using InsuranceSystem.Entities.DTOs;
using InsuranceSystem.Entities.Models;
using Microsoft.Extensions.DependencyModel;

namespace InsuranceSystem.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InsuranceClaims, InsuranceClaimsDTO>().ReverseMap();
            CreateMap<PolicyHolders, PolicyHolderDTO>().ReverseMap();

        }
    }
}
