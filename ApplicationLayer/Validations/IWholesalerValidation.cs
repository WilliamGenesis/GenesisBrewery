using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WholesalerDomain;

namespace ApplicationLayer.Validations
{
    public interface IWholesalerValidation
    {
        Task<ValidationResult[]> ValidateStockItem(StockItem item);
    }
}
