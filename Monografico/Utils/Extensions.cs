using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monografico.Utils
{
    public static class Extensions
    {
        public static string ToBase64String(this IFormFile file)
        {
            byte[] bytes = new byte[0];
        
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                bytes = stream.ToArray();
            }
            return Convert.ToBase64String(bytes);
        }
    }
}
