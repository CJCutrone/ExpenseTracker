using System.Security.Cryptography;
using System.Text;

namespace Expenses.API.Utilities.Credentials
{
    public static class PlainTextHasher
    {
        public static byte[] Hash(string text, string salt)
            => Hash(Encoding.Default.GetBytes(text), Encoding.Default.GetBytes(salt));

        public static byte[] Hash(byte[] text, byte[] salt)
        {
            var algorithim = new SHA256Managed();
            
            var bytes = new byte[text.Length + salt.Length];
            text.CopyTo(bytes, 0);
            salt.CopyTo(bytes, text.Length);

            return algorithim.ComputeHash(bytes);
        }
    }
}
