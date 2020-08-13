using BrandDomain;
using System;
using System.Threading.Tasks;

namespace ApplicationLayer.Business
{
    public interface IBrandService
    {
        Task<Brewery[]> GetBreweries();
        Task<Beer[]> GetBeers(Guid breweryId);
        Task<Guid> CreateBeer(Beer beer);
        Task<bool> MarkBeerAsObsolete(Guid beerId);
    }
}
