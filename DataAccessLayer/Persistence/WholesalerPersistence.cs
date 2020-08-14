using ApplicationLayer.Persistence;
using System;
using System.Threading.Tasks;
using WholesalerDomain;

namespace DataAccessLayer.Persistence
{
    public class WholesalerPersistence : IWholesalerPersistence
    {
        public Task<Guid> CreateStockItem(StockItem item)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateStockItem(StockItem item)
        {
            throw new NotImplementedException();
        }
    }
}
