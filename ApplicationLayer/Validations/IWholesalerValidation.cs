using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WholesalerDomain;

namespace ApplicationLayer.Validations
{
    public interface IWholesalerValidation
    {
        Task<bool> WholesalerExists(Guid id);
        Task<bool> StockItemExist(Guid item);
        Task<ValidationResult[]> ValidateStockItem(StockItem item);
    }
}
