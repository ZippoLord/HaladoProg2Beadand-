using AutoMapper;
using HaladoProg2Beadandó.Models.DTOs.CryptoCurrency;

namespace HaladoProg2Beadandó.MapperConfigs
{
    public class CryptoMapperConfig : Profile
    {
        public CryptoMapperConfig()
        {
            CreateMap<Models.CryptoCurrency, AddCryptoCurrencyDTO>().ReverseMap();
        }
    
    }
}
