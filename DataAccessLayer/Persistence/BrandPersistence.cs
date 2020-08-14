using ApplicationLayer.Persistence;
using BrandDomain;
using DataAccessLayer.Context;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer.Persistence
{
    public class BrandPersistence : IBrandPersistence
    {
        private GenesisBreweryContext _context;

        public BrandPersistence(GenesisBreweryContext context)
        {
            _context = context;
        }

        public Task<Guid> CreateBeer(Beer beer)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> MarkBeerAsObsolete(Guid beerId)
        {
            throw new NotImplementedException();
        }
    }
}
