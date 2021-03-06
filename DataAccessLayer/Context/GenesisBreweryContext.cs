﻿using BrandDomain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WholesalerDomain;

namespace DataAccessLayer.Context
{
    public class GenesisBreweryContext : DbContext
    {
        public GenesisBreweryContext(DbContextOptions<GenesisBreweryContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=GenesisBrewery.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Beer> Beer { get; set; }
        public virtual DbSet<Brewery> Brewery { get; set; }
        public virtual DbSet<Wholesaler> Wholesaler { get; set; }
        public virtual DbSet<StockItem> StockItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>().ToTable("Beer");
            modelBuilder.Entity<Brewery>().ToTable("Brewery");
            modelBuilder.Entity<Wholesaler>().ToTable("Wholesaler");
            modelBuilder.Entity<StockItem>().ToTable("StockItem");

            var mockData = new GenesisBreweryMockData();
            mockData.Seed();

            modelBuilder.Entity<Beer>().HasData(mockData.Beer);
            modelBuilder.Entity<Brewery>().HasData(mockData.Brewery);
            modelBuilder.Entity<Wholesaler>().HasData(mockData.Wholesaler);
            modelBuilder.Entity<StockItem>().HasData(mockData.StockItem);

            base.OnModelCreating(modelBuilder);
        }
    }
}
