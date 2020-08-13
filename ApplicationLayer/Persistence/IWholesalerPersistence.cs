using System;
using System.Threading.Tasks;
using WholesalerDomain;

namespace ApplicationLayer.Persistence
{
    public interface IWholesalerPersistence
    {
        Task<Guid> CreateStockItem(StockItem item);
        Task<Guid> UpdateStockItem(StockItem item);
    }
}
