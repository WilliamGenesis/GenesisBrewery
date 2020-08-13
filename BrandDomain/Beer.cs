using SharedDomain;
using System;

namespace BrandDomain
{
    public class Beer : IItem
    {
        public Guid Id { get; set; }
        public Guid BreweryId { get; set; }
        public string Name { get; set; }
        public Brewery Brewery { get; set; }
    }
}
