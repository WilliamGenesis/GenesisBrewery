using ApplicationLayer.Persistence;
using DataAccessLayer.Context;
using System;
using System.Linq;
using System.Threading.Tasks;
using WholesalerDomain;

namespace DataAccessLayer.Persistence
{
    public class WholesalerPersistence : IWholesalerPersistence
    {
        private IGenesisBreweryContext _context;

        public WholesalerPersistence(IGenesisBreweryContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateStockItem(StockItem item)
        {
            await Task.Delay(0);

            item.Id = Guid.NewGuid();
            
            _context.StockItem.Add(item);

            return item.Id;
        }

        public async Task<Guid> UpdateStockItem(StockItem item)
        {
            await Task.Delay(0);

            var itemToUpdate = _context.StockItem.FirstOrDefault(stockItem => stockItem.Id == item.Id);

            _context.StockItem.Remove(itemToUpdate);
            _context.StockItem.Add(item);

            return item.Id;
        }
    }
}
