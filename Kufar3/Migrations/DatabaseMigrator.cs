using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Migrations
{
    public static class DatabaseMigrator
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<KufarContext, Configuration>());
        }
    }
}