﻿using ApplicationLayer.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WholesalerDomain;

namespace ApplicationLayer.Validations
{
    public class WholesalerValidation : IWholesalerValidation
    {
        private IWholesalerQuery _wholesalerQuery;

        public WholesalerValidation(IWholesalerQuery wholesalerQuery)
        {
            _wholesalerQuery = wholesalerQuery;
        }

        public async Task<bool> StockItemExist(Guid id)
        {
            return await _wholesalerQuery.GetStockItem(id) != null;
        }

        public async Task<bool> WholesalerExists(Guid id)
        {
            return await _wholesalerQuery.GetWholesaler(id) != null;
        }

        public async Task<ValidationResult[]> ValidateStockItem(StockItem item)
        {
            var results = new List<ValidationResult>();
            results.Add(await ValidateWholesalerId(item.WholesalerId));

            if (results.Any(result => result != ValidationResult.Success))
                return results.ToArray();

            results.Add(ValidateStockItemPrice(item.UnitPrice));
            results.Add(ValidateStockItemQuantity(item.Quantity));

            return results.ToArray();
        }

        public async Task<ValidationResult[]> ValidateQuoteRequest(QuoteRequest request)
        {
            var results = new List<ValidationResult>();

            results.Add(ValidateQuoteRequestNotEmpty(request));
            results.Add(await ValidateWholesalerId(request.WholesalerId));

            if (results.Any(result => result != ValidationResult.Success))
                return results.ToArray();

            results.Add(ValidateQuoteRequestAllUnique(request));
            results.Add(await ValidateWholesalerStock(request));

            return results.ToArray();
        }

        #region Stock Item Validation


        private ValidationResult ValidateStockItemPrice(float price)
        {
            return price >= 0
                ? ValidationResult.Success
                : new ValidationResult("The price of a stock item cannot be negative");
        }

        private ValidationResult ValidateStockItemQuantity(int quantity)
        {
            return quantity >= 0
                ? ValidationResult.Success
                : new ValidationResult("The quantity of a stock item cannot be negative");
        }

        #endregion

        #region Quote Request Validation

        private ValidationResult ValidateQuoteRequestNotEmpty(QuoteRequest request)
        {
            if (request is null || request.RequestItems is null ||request.RequestItems.Length == 0)
                return new ValidationResult("Quote request cannot be empty");

            return ValidationResult.Success;
        }

        private ValidationResult ValidateQuoteRequestAllUnique(QuoteRequest request)
        {
            var allUnique = request.RequestItems.GroupBy(x => x.ItemId).All(g => g.Count() == 1);

            return allUnique 
                ? ValidationResult.Success
                : new ValidationResult("All request items should be unique");
        }

        private async Task<ValidationResult> ValidateWholesalerStock(QuoteRequest request)
        {
            var wholesalerStock = await _wholesalerQuery.GetWholesalerStock(request.WholesalerId);

            foreach(var requestItem in request.RequestItems)
            {
                var requestedItemInStock = wholesalerStock.FirstOrDefault(stockItem => stockItem.ItemId.Equals(requestItem.ItemId));

                if (requestedItemInStock is null) 
                    return new ValidationResult("Requested Item is not sold by this wholesaler");
                if (requestedItemInStock.Quantity < requestItem.Quantity)
                    return new ValidationResult($"Requested quantity for item {requestItem.ItemId} is not available at this wholesaler");
            }

            return ValidationResult.Success;
        }

        #endregion

        private async Task<ValidationResult> ValidateWholesalerId(Guid wholesalerId)
        {
            if (wholesalerId.Equals(Guid.Empty))
                return new ValidationResult("The wholesaler cannot be null or empty");

            if (await _wholesalerQuery.GetWholesaler(wholesalerId) is null)
                return new ValidationResult("The wholesaler does not exist");

            return ValidationResult.Success;
        }
    }
}
