using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Security
{
    public class ClsSecurity
    {
        public static string Encriptar(string text)
        {
            UnicodeEncoding Code = new UnicodeEncoding();
            byte[] textToByte = Code.GetBytes(text);
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            byte[] byteHash = SHA1.ComputeHash(Code.GetBytes(text));

            return Convert.ToBase64String(byteHash);
        }

    }
}
