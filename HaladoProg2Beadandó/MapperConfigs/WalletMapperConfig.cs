using AutoMapper;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Models.DTOs;

namespace HaladoProg2Beadandó.MapperConfigs
{
    public class WalletMapperConfig : Profile
    {

        public WalletMapperConfig()
        {
            CreateMap<VirtualWallet, GetWalletDTO>();
            CreateMap<CryptoAsset, CryptoAssetDTO>();
        }

    }
}
