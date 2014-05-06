using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBAY_PHOTO_HARVESTER
{
    class Utilities
    {
        public static string GenerateRandomString()
        {
            Random random = new Random();
            string randomString = "";

            // letters
            for(int i = 0; i < 6; i++)
            {
                int num = random.Next(0, 26);
                char let = (char)('a' + num);
                randomString += let;
            }

            // numbers
            for (int j = 0; j < 6; j++)
            {
                int num = random.Next(0, 9);
                randomString += "" + num;
            }

            return randomString;
        }
    }
}
