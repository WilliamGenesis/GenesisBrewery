using BrandDomain;
using System.Linq;

namespace DataAccessLayer.Extentions
{
    public static class BeerExension
    {
        public static Beer Resolve(this Beer beer, Brewery brewery)
        {
            beer.Brewery = brewery;

            return beer;
        }

        public static Beer[] Resolve(this Beer[] beers, Brewery[] breweries)
        {
            return (Beer[])beers.Select(beer => beer.Resolve(breweries.FirstOrDefault(brewery => brewery.Id.Equals(beer.BreweryId))));
        }

        public static Brewery Resolve(this Brewery brewery, Beer[] beers)
        {
            brewery.Beers = beers;

            return brewery;
        }

        public static Brewery[] Resolve(this Brewery[] breweries, Beer[] beers)
        {
            return (Brewery[])breweries.Select(brewery => brewery.Resolve(beers.Where(beer => beer.BreweryId.Equals(brewery.Id)).ToArray()));
        }
    }
}
