using BrandDomain;
using System;
using System.Linq;
using WholesalerDomain;


namespace DataAccessLayer.Context
{
    public static class SeedData
    {
        public static void Initialize(GenesisBreweryContext context)
        {
            var breweries = new[]
                {
                    new Brewery{Name = "William Brew"},
                    new Brewery{Name = "Genesis Brew"},
                    new Brewery{Name = "Supreme Brew"},
                    new Brewery{Name = "Baguette Brew"},
                    new Brewery{Name = "Zuzu Pils"},
                };

            context.Brewery.AddRange(breweries);
            context.SaveChanges();

            var beers = new[]
                {
                    new Beer {Name = "Willy Blond", BreweryId = GetBreweryIdOrDefault(context, "William Brew")},
                    new Beer {Name = "Genesis IPA", BreweryId = GetBreweryIdOrDefault(context, "Genesis Brew")},
                    new Beer {Name = "Supreme Red", BreweryId = GetBreweryIdOrDefault(context, "Supreme Brew")},
                    new Beer {Name = "Du vin", BreweryId = GetBreweryIdOrDefault(context, "Baguette Brew")},
                    new Beer {Name = "Toujours du vin", BreweryId = GetBreweryIdOrDefault(context, "Baguette Brew")},
                    new Beer {Name = "Cara", BreweryId = GetBreweryIdOrDefault(context, "Zuzu Pils")},
                    new Beer {Name = "Heineken", BreweryId = GetBreweryIdOrDefault(context, "Zuzu Pils")},
                };

            context.Beer.AddRange(beers);
            context.SaveChanges();

            var wholesalers = new[]
            {
                new Wholesaler{Name = "Genesis Shop"},
                new Wholesaler{Name = "Zuzu Shop"},
                new Wholesaler{Name = "Pizza Beer"},
            };

            context.Wholesaler.AddRange(wholesalers);
            context.SaveChanges();

            var stockItems = new[]
            {
                new StockItem{Quantity = 2, UnitPrice = 1.5f, ItemId = GetBeerIdOrDefault(context, "Willy Blond"), WholesalerId = GetWholesalerIdOrDefault(context, "Genesis Shop")},
                new StockItem{Quantity = 3, UnitPrice = 2.5f, ItemId = GetBeerIdOrDefault(context, "Genesis IPA"), WholesalerId = GetWholesalerIdOrDefault(context, "Pizza Beer")},
                new StockItem{Quantity = 1, UnitPrice = 1.6f, ItemId = GetBeerIdOrDefault(context, "Heineken"), WholesalerId = GetWholesalerIdOrDefault(context, "Genesis Shop")},
                new StockItem{Quantity = 4, UnitPrice = 1.9f, ItemId = GetBeerIdOrDefault(context, "Supreme Red"), WholesalerId = GetWholesalerIdOrDefault(context, "Pizza Beer")},
                new StockItem{Quantity = 20, UnitPrice = 3.2f, ItemId = GetBeerIdOrDefault(context, "Du vin"), WholesalerId = GetWholesalerIdOrDefault(context, "Genesis Shop")},
                new StockItem{Quantity = 6, UnitPrice = 0.5f, ItemId = GetBeerIdOrDefault(context, "Cara"), WholesalerId = GetWholesalerIdOrDefault(context, "Zuzu Shop")},
                new StockItem{Quantity = 1, UnitPrice = 1.9f, ItemId = GetBeerIdOrDefault(context, "Willy Blond"), WholesalerId = GetWholesalerIdOrDefault(context, "Zuzu Shop")},
            };

            context.StockItem.AddRange(stockItems);
            context.SaveChanges();
        }

        private static Guid GetBreweryIdOrDefault(GenesisBreweryContext context, string name)
        {
            var brewery = context.Brewery.FirstOrDefault(brewery => brewery.Name.Equals(name));

            return (brewery is null)
                ? Guid.NewGuid()
                : brewery.Id;
        }

        private static Guid GetBeerIdOrDefault(GenesisBreweryContext context, string name)
        {
            var beer = context.Beer.FirstOrDefault(beer => beer.Name.Equals(name));

            return (beer is null)
                ? Guid.NewGuid()
                : beer.Id;
        }

        private static Guid GetWholesalerIdOrDefault(GenesisBreweryContext context, string name)
        {
            var wholesaler = context.Wholesaler.FirstOrDefault(wholesaler => wholesaler.Name.Equals(name));

            return (wholesaler is null)
                ? Guid.NewGuid()
                : wholesaler.Id;
        }
    }
}
