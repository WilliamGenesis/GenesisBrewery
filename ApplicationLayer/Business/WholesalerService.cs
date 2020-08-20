using ApplicationLayer.Persistence;
using ApplicationLayer.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Wholesaler[]> GetWholesalers()
        {
            return await _wholesalerQuery.GetWholesalers();
        }

        public async Task<Guid> CreateStockItem(StockItem item)
        {
            return await _wholesalerPersistence.CreateStockItem(item);
        }

        public async Task<Quote> GenerateQuote(QuoteRequest request)
        {
            var wholesalerStock = await _wholesalerQuery.GetWholesalerStock(request.WholesalerId);

            var quote = new Quote
            {
                WholesalerId = request.WholesalerId,
                QuotedItems = GetQuotedItems(request, wholesalerStock),
            };
            quote.RawPrice = quote.QuotedItems.Sum(item => item.UnitPrice * item.Quantity);
            quote.Discount = GetDiscount(quote.RawPrice, quote.QuotedItems.Sum(item => item.Quantity));
            quote.Price = quote.RawPrice - quote.Discount;

            return quote;
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

        private StockItem[] GetQuotedItems(QuoteRequest request, StockItem[] wholesalerStock)
        {
            var requestedItems = new List<StockItem>();

            foreach (var requestedItem in request.RequestItems)
            {
                var wholesalerStockItem = wholesalerStock.FirstOrDefault(item => item.ItemId.Equals(requestedItem.ItemId));

                requestedItems.Add(
                    new StockItem
                    {
                        ItemId = requestedItem.ItemId,
                        UnitPrice = wholesalerStockItem.UnitPrice,
                        Quantity = requestedItem.Quantity
                    });
            }

            return requestedItems.ToArray();
        }

        private float GetDiscount(float price, int quantity)
        {
            if (quantity <= 10)
                return 0;

            return quantity <= 20
                ? price * 0.1f
                : price * 0.2f;
        }
    }
}
