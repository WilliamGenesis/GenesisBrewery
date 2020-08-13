using System;

namespace BrandDomain
{
    public class Brewery
    {
        public Guid Id { get; set; }
        public Beer[] Beers { get; set; }
    }
}
