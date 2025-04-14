namespace HaladoProg2Beadandó.Models
{
    public class CryptoAsset
    {
       public int CryptoAssetId { get; set; }
       public string CryptoCurrencyName { get; set; }

        public string Symbol { get; set; }

        public double Price { get; set; }

        public double Amount { get; set; }
        public int VirtualWalletId { get; set; }
       public VirtualWallet VirtualWallet { get; set; }
    }
}
