using BrandDomain;
using System;
using System.Threading.Tasks;

namespace ApplicationLayer.Queries
{
    public interface IBrandQueries
    {
        Task<Brewery[]> GetBreweries();
        Task<Beer[]> GetBeers(Guid BreweryId);
    }
}
