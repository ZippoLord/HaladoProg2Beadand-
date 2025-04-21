using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using HaladoProg2Beadandó.Models.DTOs.Transaction;

namespace HaladoProg2Beadandó.MapperConfigs
{
    public class TransactionsByUserIdConfig : Profile
    {
        public TransactionsByUserIdConfig() {
            CreateMap<TransactionDTO, TransactionDetailsDTO>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy.MM.dd HH:mm")));
        
        }
    }
}
