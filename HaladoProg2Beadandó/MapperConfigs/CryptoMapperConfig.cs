using AutoMapper;

namespace HaladoProg2Beadandó.MapperConfigs
{
    public class CryptoMapperConfig : Profile
    {
        public CryptoMapperConfig()
        {
            CreateMap<Models.CryptoCurrency, Models.DTOs.CryptoCurrencyDTO>().ReverseMap();
        }
    
    }
}
