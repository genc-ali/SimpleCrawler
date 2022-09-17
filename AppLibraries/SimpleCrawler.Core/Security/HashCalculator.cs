using System.Text;

namespace SimpleCrawler.Core.Security
{
    public static class StringExtension
    {
        public static string GetMd5Hash(this string stringContent)
        {
            // Use input string to calculate MD5 hash
            using var md5 = System.Security.Cryptography.MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(stringContent);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("X2"));
            }
            
            return sb.ToString();
        }
        
    }
}