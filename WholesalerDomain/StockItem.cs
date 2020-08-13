using SharedDomain;
using System;

namespace WholesalerDomain
{
    public class StockItem
    {
        public Guid Id { get; set; }
        public Guid WholesalerId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }

        public IItem Item { get; set; }
    }
}
