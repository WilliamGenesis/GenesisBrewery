using ApplicationLayer.Queries;
using DataAccessLayer.Context;
using System;
using System.Linq;
using System.Threading.Tasks;
using WholesalerDomain;

namespace DataAccessLayer.Queries
{
    public class WholesalerQuery : IWholesalerQuery
    {
        private IGenesisBreweryContext _context;

        public WholesalerQuery(IGenesisBreweryContext context)
        {
            _context = context;
        }

        public async Task<StockItem> GetStockItem(Guid id)
        {
            await Task.Delay(0);

            return _context.StockItem.FirstOrDefault(stockItem => stockItem.Id.Equals(id));
        }

        public async Task<Wholesaler> GetWholesaler(Guid id)
        {
            await Task.Delay(0);

            return _context.Wholesaler.FirstOrDefault(wholesaler => wholesaler.Id.Equals(id));
        }

        public async Task<Wholesaler[]> GetWholesalersByItemId(Guid itemId)
        {
            await Task.Delay(0);

            return (Wholesaler[])_context.Wholesaler.Where(wholesaler => 
                wholesaler.StockItems.FirstOrDefault(stockItem => stockItem.ItemId.Equals(itemId)) != null);
        }

        public async Task<StockItem[]> GetWholesalerStock(Guid wholesalerId)
        {
            await Task.Delay(0);

            return _context.Wholesaler.FirstOrDefault(wholesaler => wholesaler.Id.Equals(wholesalerId)).StockItems;
        }
    }
}
