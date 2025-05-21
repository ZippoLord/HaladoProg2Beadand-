using AutoMapper;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Models.DTOs.User;

namespace HaladoProg2Beadandó.MapperConfigs
{
    public class UserMapperConfig : Profile
    {
        public UserMapperConfig()
        {
            CreateMap<UserRegisterDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<int, UserDTO>();
            CreateMap<int, EditUserDTO>();
        }
    }
}
