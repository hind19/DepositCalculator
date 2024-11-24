using Application.Dtos;
using AutoMapper;
using Shared.Enums;
using WpfApp2.Helpers;
using WpfApp2.Models;

namespace WPFClient.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles() 
        {
            CreateMainVmProfiles();
        }

        private void CreateMainVmProfiles()
        {
            CreateMap<DepositDto, DepositModel>()
                .ReverseMap();
            CreateMap<DepositPlanDto, DepositPlanModel>().
                ReverseMap();
            CreateMap<Int32, Currencies>();
            CreateMap<Currencies, NameValuePair<int>>()
                .ForMember(v => v.Value, o => o.MapFrom(x => x))
                .ForMember(n => n.Name, o => o.MapFrom(x => x.ToString()))
                .ReverseMap();
        }
    }
}
