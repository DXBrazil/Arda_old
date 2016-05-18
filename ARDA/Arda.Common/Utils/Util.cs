using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arda.Common.Utils
{
    public class Util
    {
        public static byte[] GetBytes(string obj)
        {
            return Encoding.UTF8.GetBytes(obj);
        }

        public static string GetString(byte[] obj)
        {
            return Encoding.UTF8.GetString(obj);
        }
    }
}
