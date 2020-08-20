using ApplicationLayer.Queries;
using DataAccessLayer.Context;
using DataAccessLayer.Extensions;
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
        public async Task<Wholesaler[]> GetWholesalers()
        {
            await Task.Delay(0);

            var wholesalers = _context.Wholesaler.ToArray();

            return wholesalers?.Resolve(_context.StockItem.ToArray());
        }

        public async Task<StockItem> GetStockItem(Guid id)
        {
            await Task.Delay(0);

            return _context.StockItem.FirstOrDefault(stockItem => stockItem.Id.Equals(id));
        }

        public async Task<Wholesaler> GetWholesaler(Guid id)
        {
            await Task.Delay(0);

            var wholesaler = _context.Wholesaler.FirstOrDefault(wholesaler => wholesaler.Id.Equals(id));

            return wholesaler?.Resolve(_context.StockItem.ToArray());
        }

        public async Task<Wholesaler[]> GetWholesalersByItemId(Guid itemId)
        {
            await Task.Delay(0);

            var wholesalers = _context.Wholesaler.Where(wholesaler => 
                wholesaler.StockItems.FirstOrDefault(stockItem => stockItem.ItemId.Equals(itemId)) != null)
                .ToArray();

            return wholesalers?.Resolve(_context.StockItem.ToArray());
        }

        public async Task<StockItem[]> GetWholesalerStock(Guid wholesalerId)
        {
            await Task.Delay(0);

            var wholesaler = _context.Wholesaler.FirstOrDefault(wholesaler => wholesaler.Id.Equals(wholesalerId));

            return wholesaler?.Resolve(_context.StockItem.Where(item => item.WholesalerId.Equals(wholesaler.Id)).ToArray())?.StockItems;
        }
    }
}
