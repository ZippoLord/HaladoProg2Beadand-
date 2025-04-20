namespace HaladoProg2Beadandó.Models.DTOs.Wallet
{
    public class GetWalletDTO
    {
        public double Amount { get; set; }
        public List<CryptoAssetDTO> CryptoAssets { get; set; } = new List<CryptoAssetDTO>();

    }
}
