using AutoMapper;

namespace HaladoProg2Beadandó.MapperConfigs
{
    public class UserMapperConfig : Profile
    {
        public UserMapperConfig()
        {
            CreateMap<Models.User, Models.DTOs.UserReadDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Models.DTOs.UserRegisterDTO, Models.User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.VirtualWallet, opt => opt.Ignore());
        }
    }
}
