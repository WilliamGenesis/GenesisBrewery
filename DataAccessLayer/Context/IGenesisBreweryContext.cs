using BrandDomain;
using System.Collections.Generic;
using WholesalerDomain;

namespace DataAccessLayer.Context
{
    public interface IGenesisBreweryContext
    {
        List<Beer> Beer { get; set; }
        List<Brewery> Brewery { get; set; }
        List<Wholesaler> Wholesaler { get; set; }
        List<StockItem> StockItem { get; set; }

    }
}
