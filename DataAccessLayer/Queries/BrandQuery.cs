using ApplicationLayer.Queries;
using BrandDomain;
using DataAccessLayer.Context;
using DataAccessLayer.Extentions;
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

            var beer = _context.Beer.FirstOrDefault(beer => beer.Id.Equals(id));

            return beer?.Resolve(_context.Brewery.FirstOrDefault(brewery => brewery.Id.Equals(beer.BreweryId)));
        }

        public async Task<Beer[]> GetBeers(Guid BreweryId)
        {
            await Task.Delay(0);

            var beers =  _context.Beer.Where(beer => beer.IsObsolete == false && beer.BreweryId.Equals(BreweryId)).ToArray();

            return beers?.Resolve(_context.Brewery.ToArray());
        }

        public async Task<Brewery[]> GetBreweries()
        {
            await Task.Delay(0);
            var breweries = _context.Brewery.ToArray();

            return breweries?.Resolve(_context.Beer.ToArray());
        }

        public async Task<Brewery> GetBrewery(Guid id)
        {
            await Task.Delay(0);

            var brewery = _context.Brewery.FirstOrDefault(brewery => brewery.Id.Equals(id));

            return brewery?.Resolve(_context.Beer.ToArray());
        }
    }
}
