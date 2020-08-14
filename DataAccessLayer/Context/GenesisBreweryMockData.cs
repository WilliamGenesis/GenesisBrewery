using BrandDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WholesalerDomain;


namespace DataAccessLayer.Context
{
    public class GenesisBreweryMockData
    {
        public List<Beer> Beer { get; set; }
        public List<Brewery> Brewery { get; set; }
        public List<Wholesaler> Wholesaler { get; set; }
        public List<StockItem> StockItem { get; set; }

        public void Seed()
        {
            var breweries = new[]
                {
                    new Brewery{Id = Guid.NewGuid(), Name = "William Brew"},
                    new Brewery{Id = Guid.NewGuid(), Name = "Genesis Brew"},
                    new Brewery{Id = Guid.NewGuid(), Name = "Supreme Brew"},
                    new Brewery{Id = Guid.NewGuid(), Name = "Baguette Brew"},
                    new Brewery{Id = Guid.NewGuid(), Name = "Zuzu Pils"},
                };

            var beers = new[]
                {
                    new Beer {Id = Guid.NewGuid(), Name = "Willy Blond", BreweryId = breweries[0].Id},
                    new Beer {Id = Guid.NewGuid(), Name = "Genesis IPA", BreweryId = breweries[1].Id},
                    new Beer {Id = Guid.NewGuid(), Name = "Supreme Red", BreweryId = breweries[2].Id},
                    new Beer {Id = Guid.NewGuid(), Name = "Du vin", BreweryId = breweries[3].Id},
                    new Beer {Id = Guid.NewGuid(), Name = "Toujours du vin", BreweryId = breweries[3].Id},
                    new Beer {Id = Guid.NewGuid(), Name = "Cara", BreweryId = breweries[4].Id},
                    new Beer {Id = Guid.NewGuid(), Name = "Heineken", BreweryId = breweries[4].Id},
                };

            var wholesalers = new[]
            {
                new Wholesaler{Id = Guid.NewGuid(), Name = "Genesis Shop"},
                new Wholesaler{Id = Guid.NewGuid(), Name = "Zuzu Shop"},
                new Wholesaler{Id = Guid.NewGuid(), Name = "Pizza Beer"},
            };

            var stockItems = new[]
            {
                new StockItem{Id = Guid.NewGuid(), Quantity = 2, UnitPrice = 1.5f, ItemId =beers[0].Id, WholesalerId = wholesalers[0].Id},
                new StockItem{Id = Guid.NewGuid(), Quantity = 3, UnitPrice = 2.5f, ItemId =beers[1].Id, WholesalerId = wholesalers[1].Id},
                new StockItem{Id = Guid.NewGuid(), Quantity = 1, UnitPrice = 1.6f, ItemId =beers[2].Id, WholesalerId = wholesalers[0].Id},
                new StockItem{Id = Guid.NewGuid(), Quantity = 4, UnitPrice = 1.9f, ItemId =beers[3].Id, WholesalerId = wholesalers[2].Id},
                new StockItem{Id = Guid.NewGuid(), Quantity = 2, UnitPrice = 3.2f, ItemId =beers[4].Id, WholesalerId = wholesalers[0].Id},
                new StockItem{Id = Guid.NewGuid(), Quantity = 6, UnitPrice = 0.5f, ItemId =beers[5].Id, WholesalerId = wholesalers[2].Id},
                new StockItem{Id = Guid.NewGuid(), Quantity = 1, UnitPrice = 1.9f, ItemId =beers[6].Id, WholesalerId = wholesalers[1].Id}
            };

            Beer = beers.ToList();
            Brewery = breweries.ToList();
            Wholesaler = wholesalers.ToList();
            StockItem = stockItems.ToList();
        }
    }
}
