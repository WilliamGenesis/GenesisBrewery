using BrandDomain;
using System.Collections.Generic;
using System.Linq;
using WholesalerDomain;

namespace DataAccessLayer.Context
{
    public class GenesisBreweryFakeContext : IGenesisBreweryContext
    {
        public GenesisBreweryFakeContext()
        {
            Initialize();
        }

        public List<Beer> Beer { get; set; }
        public List<Brewery> Brewery { get; set; }
        public List<Wholesaler> Wholesaler { get; set; }
        public List<StockItem> StockItem { get; set; }

        private void Initialize()
        {
            Beer = new List<Beer>();
            Brewery = new List<Brewery>();
            Wholesaler = new List<Wholesaler>();
            StockItem = new List<StockItem>();

            var mockData = new GenesisBreweryMockData();
            mockData.Seed();

           Beer.AddRange(mockData.Beer);
           Brewery.AddRange(mockData.Brewery);
           Wholesaler.AddRange(mockData.Wholesaler);
           StockItem.AddRange(mockData.StockItem);
        }
    }
}
