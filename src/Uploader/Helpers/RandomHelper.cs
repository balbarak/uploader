using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uploader.Helpers
{
    public class RandomHelper
    {
        public static Random Random => new Random();

        public static string GenerateFileName()
        {
            return Random.Next(10000, 999999).ToString();
        }
    }
}
