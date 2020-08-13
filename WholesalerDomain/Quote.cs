using System.Collections.Generic;

namespace WholesalerDomain
{
    public class Quote
    {
        public float Price { get; set; }
        public float RawPrice { get; set; }
        public float Discount { get; set; }
        public string WholesalerName { get; set; }
        public StockItem[] QuotedItems { get; set; }
    }
}
