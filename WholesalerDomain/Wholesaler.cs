using System;

namespace WholesalerDomain
{
    public class Wholesaler
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public StockItem[] StockItems;
    }
}
