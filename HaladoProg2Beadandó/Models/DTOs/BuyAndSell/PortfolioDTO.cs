namespace HaladoProg2Beadandó.Models.DTOs.BuyAndSell
{
    public class PortfolioDTO
    {
        public double WalletPrice { get; set; }
        
        public List<PortfolioItemDTO> CryptoAssets { get; set; }
    }

    public class PortfolioItemDTO
    {
        public string Symbol { get; set; }
        public string CryptoCurrencyName { get; set; }
        public double Amount { get; set; }              
        public double PurchaseValue { get; set; }       
        public double CurrentPricePerUnit { get; set; }      
        public double CurrentValue { get; set; }
    }
}
