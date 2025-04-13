namespace HaladoProg2Beadandó.Models
{
    public class CryptoAsset
    {
       public int CryptoAssetId { get; set; }
       public string Name { get; set; } 
       
       public double Price { get; set; }

       public int VirtualWalletId { get; set; }
       public VirtualWallet VirtualWallet { get; set; }
    }
}
