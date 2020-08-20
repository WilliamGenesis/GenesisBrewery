using System.Linq;
using WholesalerDomain;

namespace DataAccessLayer.Extensions
{
    public static class WholesalerExtensions
    {
        public static Wholesaler Resolve(this Wholesaler wholesaler, StockItem[] stockItems)
        {
            return new Wholesaler
            {
                Id = wholesaler.Id,
                Name = wholesaler.Name,
                StockItems = stockItems
            };
        }

        public static Wholesaler[] Resolve(this Wholesaler[] wholesalers, StockItem[] stockItems)
        {
            return wholesalers.Select(wholesaler => wholesaler.Resolve(stockItems.Where(item => item.WholesalerId.Equals(wholesaler.Id)).ToArray()))
                .ToArray();
        }
    }
}
