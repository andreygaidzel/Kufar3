using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kufar3.Helpers
{
    public class FilesHelper
    {
        private static string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static void DeleteImage(string url)
        {
            var templatePath = _baseDirectory + url;
            System.IO.File.Delete(templatePath);
        }
    } 
}