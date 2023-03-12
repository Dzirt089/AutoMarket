using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoMarket.Services
{
    public class Hach_md5
    {
        /// <summary>
        /// Вариант первый, шифрование по MD5
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPasswordMD5(string password)
        {
            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] hash = MD5.HashData(b);

            StringBuilder stringBuilder = new();
            foreach (var sb in hash)
            {
                stringBuilder.Append(sb.ToString("X2"));
            }

            return Convert.ToString(stringBuilder);
        }
        /// <summary>
        /// Вариант второй, шифрование по SHA256. 
        /// Здесь на половину длиннее hash-code, в сравнении с MD5
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return hash;
        }


    }
    
}
