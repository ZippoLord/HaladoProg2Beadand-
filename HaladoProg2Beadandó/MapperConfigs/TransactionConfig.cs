namespace HaladoProg2Beadandó.MapperConfigs;
using AutoMapper;
using HaladoProg2Beadandó.Models.DTOs.Transaction;

public class TransactionConfig : Profile
{
    public TransactionConfig()
    {
        CreateMap<TransactionDTO, TransactionListDTO>()
          .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
          .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
          .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
          .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy.MM.dd HH:mm")));

        CreateMap<TransactionDTO, TransactionDetailsDTO>()
           .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy.MM.dd HH:mm")));

    }

}    


