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
        /// <summary>
        /// Вариант второй, шифрование по SHA256. 
        /// Здесь на половину длиннее hash-code, в сравнении с MD5
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-","").ToLower();

                return hash;
            }
        }


    }
    
}
