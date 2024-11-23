using Application.Dtos;
using AutoMapper;
using Persistence.Dtos;

namespace Application.Profiles
{
    public class ServicePersistanceProfiles : Profile
    {
        public ServicePersistanceProfiles()
        {
            DtoProfiles();
        }

        private void DtoProfiles()
        {
            CreateMap<DepositPlanDtoDomain, DepositPlanDto>();
        }
    }
}
