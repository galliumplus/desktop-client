
using System.Security.Cryptography;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace Couche_Métier.Utilitaire
{
    /// <summary>
    /// Permet de crypter un string en SHA256
    /// </summary>
    public static class CryptStringToSHA256
    {
        /// <summary>
        /// Hash un message
        /// </summary>
        /// <param name="message"> message à hashé </param>
        /// <returns> le message hashé </returns>
        public static string Hash(string message)
        {
            return BC.HashPassword(message);
        }


        public static bool Verify(string message,string encoded)
        {
            return BC.Verify(message, encoded); 
        }
    }
}
