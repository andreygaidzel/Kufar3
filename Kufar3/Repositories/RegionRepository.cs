using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Repositories
{
    public class RegionRepository : BaseRepository
    {
        public List<City> GetCitiesByRegionId(long? id)
        {
            return Context.Cities.Where(x => x.RegionId == id).ToList();
        }

        public List<Region> GetAllRegions()
        {
            return Context.Regions.ToList();
        }
    }
}