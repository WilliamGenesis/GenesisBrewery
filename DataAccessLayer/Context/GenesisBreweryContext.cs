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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mockData = new GenesisBreweryMockData();
            mockData.Seed();

            modelBuilder.Entity<Beer>().HasData(mockData.Beer);
            modelBuilder.Entity<Brewery>().HasData(mockData.Brewery);
            modelBuilder.Entity<Wholesaler>().HasData(mockData.Wholesaler);
            modelBuilder.Entity<StockItem>().HasData(mockData.StockItem);
        }
    }
}
