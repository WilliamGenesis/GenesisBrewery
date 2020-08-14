using ApplicationLayer.Queries;
using BrandDomain;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer.Queries
{
    public class BrandQuery : IBrandQuery
    {
        public Task<Beer> GetBeer(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Beer[]> GetBeers(Guid BreweryId)
        {
            throw new NotImplementedException();
        }

        public Task<Brewery[]> GetBreweries()
        {
            throw new NotImplementedException();
        }

        public Task<Brewery> GetBrewery(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
