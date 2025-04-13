namespace HaladoProg2Beadandó.Models
{
    public class VirtualWallet
    {
     public int VirtualWalletId { get; set; }
     public double Amount { get; set; }
     
    public int UserId { get; set; }

    public User User { get; set; }

    public ICollection<CryptoAsset> CryptoAssets { get; set; } = new List<CryptoAsset>();
    }
}
