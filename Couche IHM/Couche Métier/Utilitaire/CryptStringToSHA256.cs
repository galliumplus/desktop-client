using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Couche_Métier.Utilitaire
{
    /// <summary>
    /// Permet de crypter un string en SHA256
    /// </summary>
    public static class CryptStringToSHA256
    {
        /// <summary>
        /// Hash un string en 256
        /// </summary>
        /// <param name="message"> message à hashé </param>
        /// <returns> le message hashé </returns>
        public static string HashTo256(string message)
        {
            using(SHA256 hash256 = SHA256.Create())
            {
                byte[] hashBytes = hash256.ComputeHash(Encoding.UTF8.GetBytes(message));

                // Convertirle les bytes en hexadécimale
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
