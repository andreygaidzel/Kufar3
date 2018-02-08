using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Repositories
{
    public class BaseRepository
    {
        public KufarContext Context;

        public BaseRepository()
        {
            Context = new KufarContext();
        }
    }
}