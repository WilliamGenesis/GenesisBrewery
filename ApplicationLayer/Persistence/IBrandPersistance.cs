using BrandDomain;
using System;
using System.Threading.Tasks;

namespace ApplicationLayer.Persistence
{
    public interface IBrandPersistance
    {
        Task<Guid> CreateBeer(Beer beer);
        Task<Guid> MarkBeerAsObsolete(Guid beerId)
    }
}
