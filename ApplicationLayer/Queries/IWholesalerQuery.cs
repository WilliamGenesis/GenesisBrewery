using System;
using System.Threading.Tasks;
using WholesalerDomain;

namespace ApplicationLayer.Queries
{
    public interface IWholesalerQuery
    {
        Task<Wholesaler[]> GetWholesalers();
        Task<Wholesaler> GetWholesaler(Guid id);
        Task<StockItem> GetStockItem(Guid id);
        Task<StockItem[]> GetWholesalerStock(Guid wholesalerId);
        Task<Wholesaler[]> GetWholesalersByItemId(Guid itemId);
    }
}
