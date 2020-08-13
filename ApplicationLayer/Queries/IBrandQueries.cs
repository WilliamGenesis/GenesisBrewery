using BrandDomain;
using System;
using System.Threading.Tasks;

namespace ApplicationLayer.Queries
{
    public interface IBrandQueries
    {
        Task<Brewery> GetBrewery(Guid id);
        Task<Beer> GetBeer(Guid id);
        Task<Brewery[]> GetBreweries();
        Task<Beer[]> GetBeers(Guid BreweryId);
    }
}
