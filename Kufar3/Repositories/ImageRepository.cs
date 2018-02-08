using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kufar3.Models;

namespace Kufar3.Repositories
{
    public class ImageRepository : BaseRepository
    {
        public void Add(Image image)
        {
            Context.Images.Add(image);
            Context.SaveChanges();
        }
    }
}