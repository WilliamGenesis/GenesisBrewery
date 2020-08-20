using ApplicationLayer.Persistence;
using BrandDomain;
using DataAccessLayer.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Persistence
{
    public class BrandPersistence : IBrandPersistence
    {
        private IGenesisBreweryContext _context;

        public BrandPersistence(IGenesisBreweryContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateBeer(Beer beer)
        {
            await Task.Delay(0);

            beer.Id = new Guid();
            _context.Beer.Add(beer);

            return beer.Id;
        }

        public async Task<Guid> MarkBeerAsObsolete(Guid beerId)
        {
            await Task.Delay(0);

            _context.Beer.FirstOrDefault(beer => beer.Id == beerId).IsObsolete = true;

            return beerId;
        }
    }
}
