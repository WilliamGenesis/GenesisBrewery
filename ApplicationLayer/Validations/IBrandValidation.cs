using BrandDomain;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ApplicationLayer.Validations
{
    public interface IBrandValidation
    {
        Task<ValidationResult[]> ValidateBeer(Beer beer);
    }
}
