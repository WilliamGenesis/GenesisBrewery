using System;
using System.Threading.Tasks;
using WholesalerDomain;

namespace ApplicationLayer.Business
{
    public interface IWholesalerService
    {
        Task<Wholesaler[]> GetWholesalers();
        Task<StockItem[]> GetWholesalerStock(Guid wholesalerId);
        Task<Wholesaler[]> GetWholesalersByItem(Guid itemId);
        Task<Guid> CreateStockItem(StockItem item);
        Task<Guid> UpdateStockItem(StockItem item);
        Task<Quote> GenerateQuote(QuoteRequest request);

    }
}
