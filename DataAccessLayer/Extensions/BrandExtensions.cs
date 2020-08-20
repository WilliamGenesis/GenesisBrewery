using BrandDomain;
using System.Linq;

namespace DataAccessLayer.Extentions
{
    public static class BrandExtensions
    {
        public static Beer Resolve(this Beer beer, Brewery brewery)
        {
            return new Beer
            {
                Id = beer.Id,
                Name = beer.Name,
                IsObsolete = beer.IsObsolete,
                BreweryId = beer.BreweryId,
                Brewery = brewery
            };
        }

        public static Beer[] Resolve(this Beer[] beers, Brewery[] breweries)
        {
            return beers.Select(beer => beer.Resolve(breweries.FirstOrDefault(brewery => brewery.Id.Equals(beer.BreweryId))))
                .ToArray();
        }

        public static Brewery Resolve(this Brewery brewery, Beer[] beers)
        {
            return new Brewery
            {
                Id = brewery.Id,
                Name = brewery.Name,
                Beers = beers
            };
        }

        public static Brewery[] Resolve(this Brewery[] breweries, Beer[] beers)
        {
            return breweries.Select(brewery => brewery.Resolve(beers.Where(beer => beer.BreweryId.Equals(brewery.Id)).ToArray()))
                .ToArray();
        }
    }
}
