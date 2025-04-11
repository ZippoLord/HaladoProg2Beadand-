namespace HaladoProg2Beadandó.Models
{
    public class VirtualWallet
    {
     public int VirtualWalletId { get; set; }
     public float Amount { get; set; }
     public string Currency { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    }
}
