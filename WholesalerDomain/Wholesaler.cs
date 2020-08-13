using System;

namespace WholesalerDomain
{
    public class Wholesaler
    {
        public Guid Id { get; set; }

        public StockItem[] StockItems;
    }
}
