using ApplicationLayer.Persistence;
using ApplicationLayer.Queries;
using System;
using System.Threading.Tasks;
using WholesalerDomain;

namespace ApplicationLayer.Business
{
    public class WholesalerService : IWholesalerService
    {
        private IWholesalerQuery _wholesalerQuery;
        private IWholesalerPersistence _wholesalerPersistence;

        public WholesalerService(IWholesalerQuery wholesalerQuery, IWholesalerPersistence wholesalerPersistence)
        {
            _wholesalerQuery = wholesalerQuery;
            _wholesalerPersistence = wholesalerPersistence;
        }

        public async Task<Guid> CreateStockItem(StockItem item)
        {
            return await _wholesalerPersistence.CreateStockItem(item);
        }

        public async Task<Wholesaler[]> GetWholesalersByItem(Guid itemId)
        {
            return await _wholesalerQuery.GetWholesalersByItemId(itemId);
        }

        public async Task<StockItem[]> GetWholesalerStock(Guid wholesalerId)
        {
            return await _wholesalerQuery.GetWholesalerStock(wholesalerId);
        }

        public async Task<Guid> UpdateStockItem(StockItem item)
        {
            return await _wholesalerPersistence.UpdateStockItem(item);
        }
    }
}
