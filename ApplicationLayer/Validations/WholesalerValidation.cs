using ApplicationLayer.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            results.Add(await ValidateStockItemWholesaler(item.WholesalerId));
            results.Add(ValidateStockItemPrice(item.UnitPrice));
            results.Add(ValidateStockItemQuantity(item.Quantity));

            return results.ToArray();
        }

        #region Stock Item Validation

        private async Task<ValidationResult> ValidateStockItemWholesaler(Guid wholesalerId)
        {
            if (wholesalerId.Equals(Guid.Empty))
                return new ValidationResult("A stock item must always be linked to a wholesaler");

            if (await _wholesalerQuery.GetWholesaler(wholesalerId) is null)
                return new ValidationResult("A stock item must always be linked to an existing wholesaler");

            return ValidationResult.Success;
        }

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
                : new ValidationResult("The price of a stock item cannot be negative");
        }

        #endregion
    }
}
