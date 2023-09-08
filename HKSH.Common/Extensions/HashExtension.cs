using System.Security.Cryptography;
using System.Text;

namespace HKSH.Common.Extensions
{
    /// <summary>
    /// Hash Extension
    /// </summary>
    public static class HashExtension
    {
        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha256(this string input)
        {
            if (input.IsMissing())
            {
                return string.Empty;
            }

            using SHA256 sha = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Creates a SHA256 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash.</returns>
        public static byte[] Sha256(this byte[] input)
        {
            if (input == null)
            {
                return Array.Empty<byte>();
            }

            using SHA256 sha = SHA256.Create();
            return sha.ComputeHash(input);
        }

        /// <summary>
        /// Creates a SHA512 hash of the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A hash</returns>
        public static string Sha512(this string input)
        {
            if (input.IsMissing())
            {
                return string.Empty;
            }

            using SHA512 sha = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}