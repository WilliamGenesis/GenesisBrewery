using System;
using System.Threading.Tasks;
using WholesalerDomain;

namespace ApplicationLayer.Queries
{
    public interface IWholesalerQueries
    {
        Task<StockItem[]> GetWholesalerStock(Guid wholesalerId);
        Task<Wholesaler[]> GetWholesalersByItemId(Guid itemId);
    }
}
