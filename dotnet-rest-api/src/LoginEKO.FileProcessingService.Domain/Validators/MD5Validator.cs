using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LoginEKO.FileProcessingService.Domain.Validators
{
    public static partial class MD5Validator
    {
        public static IEnumerable<byte> ComputeHash(IEnumerable<byte> bytesToHash)
        {
            if (bytesToHash == null)
                throw new ArgumentNullException(nameof(bytesToHash));

            var bytesArray = bytesToHash.ToArray();
            if (bytesArray.Length == 0)
                throw new ArgumentException("Invalid input to compute an MD5 Hash");

            var hashBytes = MD5.HashData(bytesArray);

            return hashBytes;
        }

        public static string CreateHashStringFromHashBytes(IEnumerable<byte> hashBytes)
        {
            if (hashBytes == null)
            {
                throw new ArgumentNullException(nameof(hashBytes));
            }

            var hashArray = hashBytes as byte[] ?? [.. hashBytes];
            if (hashArray is not { Length: 16 })
            {
                throw new ArgumentException("Invalid binary hash value to create hash string", nameof(hashBytes));
            }

            var sb = new StringBuilder();
            foreach (var hashByte in hashArray)
            {
                sb.Append(hashByte.ToString("X2"));
            }

            return sb.ToString();
        }

        public static bool Validate(IEnumerable<byte> hashInputBytes, string supliedHash)
        {
            var hashBytes = CreateHashBytesFromHashString(supliedHash);
            return hashInputBytes.SequenceEqual(hashBytes);
        }

        public static IEnumerable<byte> CreateHashBytesFromHashString(string hashString)
        {
            if (!IsValidMd5HashFormat(hashString))
            {
                throw new ArgumentNullException(nameof(hashString));
            }

            var byteArray = new byte[hashString.Length / 2];

            for (var i = 0; i < hashString.Length; i +=2)
            {
                var hexPair = hashString.Substring(i, 2);
                byteArray[i / 2] = Convert.ToByte(hexPair, 16);
            }

            return byteArray;
        }

        private static bool IsValidMd5HashFormat(string hashString)
        {
            return hashString != null && MD5HashRegex().IsMatch(hashString);
        }

        [GeneratedRegex("^[0-9a-fA-F]{32}$")]
        private static partial Regex MD5HashRegex();
    }
}
