using AutoMapper;
using HaladoProg2Beadandó.Models.DTOs.User;

namespace HaladoProg2Beadandó.MapperConfigs
{
    public class UserMapperConfig : Profile
    {
        public UserMapperConfig()
        {
            CreateMap<Models.User, UserReadDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<UserRegisterDTO, Models.User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.VirtualWallet, opt => opt.Ignore());
        }
    }
}
