using ApplicationLayer.Persistence;
using ApplicationLayer.Queries;
using BrandDomain;
using System;
using System.Threading.Tasks;

namespace ApplicationLayer.Business
{
    public class BrandService : IBrandService
    {
        private IBrandQuery _brandQuery;
        private IBrandPersistence _brandPeristence;

        public BrandService(IBrandQuery brandQuery, IBrandPersistence brandPersistence)
        {
            _brandPeristence = brandPersistence;
            _brandQuery = brandQuery;
        }
        public async Task<Guid> CreateBeer(Beer beer)
        {
            return await _brandPeristence.CreateBeer(beer);
        }

        public async Task<Beer[]> GetBeers(Guid breweryId)
        {
            return await _brandQuery.GetBeers(breweryId);
        }

        public async Task<Brewery[]> GetBreweries()
        {
            return await _brandQuery.GetBreweries();
        }

        public async Task<bool> MarkBeerAsObsolete(Guid beerId)
        {
            try
            {
                await _brandPeristence.MarkBeerAsObsolete(beerId);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
