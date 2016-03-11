using System;
using System.Security.Cryptography;
using System.Text;

namespace VERT.Core
{
    public static class EncriptionUtil
    {
        /// <summary>
        /// Create salt key
        /// Recommended: CreateSaltKey(5)
        /// </summary>
        /// <param name="size">Key size</param>
        /// <returns>Salt key</returns>
        public static string GenerateSaltKey(int size = 5)
        {
            // Generate a cryptographic random number
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Create a password hash
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="saltkey">Salk key</param>
        /// <param name="hashAlgorithm">Password format (hash algorithm)</param>
        /// <returns>Password hash</returns>
        public static string GeneratePasswordHash(string password, string saltkey, string hashAlgorithm = "SHA1")
        {
            if (string.IsNullOrEmpty(hashAlgorithm))
                hashAlgorithm = "SHA1";

            var algorithm = HashAlgorithm.Create(hashAlgorithm);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            string saltAndPassword = string.Concat(password, saltkey);
            byte[] saltAndPasswordBytes = Encoding.UTF8.GetBytes(saltAndPassword);
            byte[] hashByteArray = algorithm.ComputeHash(saltAndPasswordBytes);
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }
    }
}
