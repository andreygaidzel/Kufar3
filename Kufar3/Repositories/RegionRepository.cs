using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Repositories
{
    public class RegionRepository : BaseRepository
    {
        public IQueryable<City> GetCitiesByRegionId(long? id)
        {
            return Context.Cities.Where(x => x.RegionId == id);
        }

        public IQueryable<Region> GetAllRegions()
        {
            return Context.Regions;
        }
    }
}