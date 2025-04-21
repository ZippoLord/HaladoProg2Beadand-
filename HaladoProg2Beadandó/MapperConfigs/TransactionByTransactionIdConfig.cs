using AutoMapper;
using HaladoProg2Beadandó.Models.DTOs.Transaction;

namespace HaladoProg2Beadandó.MapperConfigs
{
    public class TransactionByTransactionIdConfig : Profile
    {
        public TransactionByTransactionIdConfig()
        {
            CreateMap<TransactionDTO, TransactionListDTO>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy.MM.dd HH:mm")));
        }

    }
}
