using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkAPI.Security
{
    public class Encryption
    {
        public static string Encrypt(string str)
        {
            string result = string.Empty;

            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] < 120)
                    result += Convert.ToChar(str[i] + 7);
                else
                    result += str[i];
            }
            return result;
            //return new string(str.Select(x =>
            //    char.IsLetter(x)
            //    && (x >= 'a' && x <= 'm')
            //    || (x >= 'A' && x <= 'M') ?
            //    Convert.ToChar((x + 13)) :
            //    char.IsLetter(x)
            //    && (x >= 'n' && x <= 'z')
            //    || (x >= 'N' && x <= 'Z') ?
            //    Convert.ToChar((x - 13)) : x)
            //    .ToArray());
        }
    }
}
