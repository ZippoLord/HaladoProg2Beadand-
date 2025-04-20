using AutoMapper;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Models.DTOs;
using HaladoProg2Beadandó.Models.DTOs.Wallet;

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
