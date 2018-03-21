using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        public static byte[] ConvertImageinByte(HttpPostedFileBase file)
        {
            var b = new BinaryReader(file.InputStream);
            var binData = b.ReadBytes(file.ContentLength);

            return binData;
        }

        public static string ConvertByteToImage(byte[] data)
        {
            return data != null && data.Length > 0
                ? $"data:image;base64,{Convert.ToBase64String(data)}"
                : string.Empty;
        }
    } 
}