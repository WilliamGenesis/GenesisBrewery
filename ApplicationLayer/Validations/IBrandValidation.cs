using BrandDomain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ApplicationLayer.Validations
{
    public interface IBrandValidation
    {
        Task<ValidationResult[]> ValidateBeer(Beer beer);
        Task<bool> BreweryExists(Guid id);
        Task<bool> BeerExists(Guid id);
    }
}
