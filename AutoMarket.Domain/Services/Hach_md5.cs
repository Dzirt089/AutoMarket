using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoMarket.Services
{
    public class Hach_md5
    {
        public static string hashPassword(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] hash= md5.ComputeHash(b);

            StringBuilder stringBuilder= new StringBuilder();
            foreach(var sb in hash)
            {
                stringBuilder.Append(sb.ToString("X2"));
            }

            return Convert.ToString(stringBuilder);
        }
    }
}
