using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations.ImageConverter
{
    public static class ImageConverter
    {
        public static byte[] Encryption(IFormFile file)
        {
            if(file.Length > 1)
            {
                using(var ms = new MemoryStream())
                {
                    file.CopyTo(ms);

                    return ms.ToArray();
                }
            }
            return null;
        }

        public static IFormFile Decrypt(byte[] imageByte)
        {
            if (imageByte != null)
            {
                var stream = new MemoryStream(imageByte);
                return new FormFile(stream, 0, imageByte.Length, "", "");
            }
            return null;
        }


    }
}
