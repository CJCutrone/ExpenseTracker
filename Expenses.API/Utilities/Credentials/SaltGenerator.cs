using System.Security.Cryptography;

namespace Expenses.API.Utilities.Credentials
{
    public static class SaltGenerator
    {
        public static byte[] Generate(int length)
        {
            var salt = new byte[length];

            using var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(salt);

            return salt;
        }
    }
}
