using ApplicationLayer.Queries;
using System;
using System.Threading.Tasks;
using WholesalerDomain;

namespace DataAccessLayer.Queries
{
    public class WholesalerQuery : IWholesalerQuery
    {
        public Task<StockItem> GetStockItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Wholesaler> GetWholesaler(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Wholesaler[]> GetWholesalersByItemId(Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<StockItem[]> GetWholesalerStock(Guid wholesalerId)
        {
            throw new NotImplementedException();
        }
    }
}
