using BrandDomain;
using Microsoft.EntityFrameworkCore;
using WholesalerDomain;

namespace DataAccessLayer.Context
{
    public class GenesisBreweryContext : DbContext
    {
        public GenesisBreweryContext(DbContextOptions<GenesisBreweryContext> options) : base(options)
        {

        }

        public virtual DbSet<Beer> Beer { get; set; }
        public virtual DbSet<Brewery> Brewery { get; set; }
        public virtual DbSet<Wholesaler> Wholesaler { get; set; }
        public virtual DbSet<StockItem> StockItem { get; set; }

        //public override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
    }
}
