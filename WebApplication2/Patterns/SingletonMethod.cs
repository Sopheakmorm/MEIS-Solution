using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebApplication2.Utils;

namespace MEIS.Patterns
{
    public static class SingletonMethod
    {
        public static byte[] GetUserPassword(this string str)
        {
            return Cryptography.EncryptBytes(new byte[] {12, 12, 12, 12}, str, 22);
        }
    }
}