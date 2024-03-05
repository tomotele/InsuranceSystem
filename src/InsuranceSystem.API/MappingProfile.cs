using AutoMapper;
using InsuranceSystem.Entities.DTOs;
using InsuranceSystem.Entities.Models;

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
