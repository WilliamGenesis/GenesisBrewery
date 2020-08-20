using ApplicationLayer.Queries;
using BrandDomain;
using DataAccessLayer.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Queries
{
    public class BrandQuery : IBrandQuery
    {
        private IGenesisBreweryContext _context;

        public BrandQuery(IGenesisBreweryContext context)
        {
            _context = context;
        }

        public async Task<Beer> GetBeer(Guid id)
        {
            await Task.Delay(0);

            return _context.Beer.FirstOrDefault(beer => beer.Id.Equals(id));
        }

        public async Task<Beer[]> GetBeers(Guid BreweryId)
        {
            await Task.Delay(0);

            return _context.Beer.ToArray();
        }

        public async Task<Brewery[]> GetBreweries()
        {
            await Task.Delay(0);
            return _context.Brewery.ToArray();
        }

        public async Task<Brewery> GetBrewery(Guid id)
        {
            await Task.Delay(0);

            return _context.Brewery.FirstOrDefault(brewery => brewery.Id.Equals(id));
        }
    }
}
