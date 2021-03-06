﻿using BrandDomain;
using System;
using System.Threading.Tasks;

namespace ApplicationLayer.Persistence
{
    public interface IBrandPersistence
    {
        Task<Guid> CreateBeer(Beer beer);
        Task<Guid> MarkBeerAsObsolete(Guid beerId);
    }
}
