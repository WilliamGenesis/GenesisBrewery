using ApplicationLayer.Queries;
using BrandDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ApplicationLayer.Validations
{
    public class BrandValidation : IBrandValidation
    {
        IBrandQuery _brandQuery;

        public BrandValidation(IBrandQuery brandQuery)
        {
            _brandQuery = brandQuery;
        }

        public async Task<bool> BeerExists(Guid id)
        {
            return await _brandQuery.GetBeer(id) != null;
        }

        public async Task<bool> BreweryExists(Guid id)
        {
            return await _brandQuery.GetBrewery(id) != null;
        }

        public async Task<ValidationResult[]> ValidateBeer(Beer beer)
        {
            var results = new List<ValidationResult>();
            results.Add(await ValidateBeerBrewery(beer.BreweryId));

            return results.ToArray();
        }

        #region Beer Validation

        private async Task<ValidationResult> ValidateBeerBrewery(Guid breweryId)
        {
            if (breweryId.Equals(Guid.Empty))
                return new ValidationResult("A beer must always be linked to a brewery");

            if (await _brandQuery.GetBrewery(breweryId) is null)
                return new ValidationResult("A beer must always be linked to an existing brewery");

            return ValidationResult.Success;
        }

        #endregion
    }
}
