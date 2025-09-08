using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Domain.Validators
{
    public static class MD5Validator
    {
        private const string InvalidInputToHash = "Invalid input to compute an MD5 Hash";
        public static string ComputeHash(IEnumerable<byte> bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            var bytesArray = bytes.ToArray();
            if (bytesArray.Length == 0)
                throw new ArgumentException(InvalidInputToHash);
            var hashBytes = MD5.HashData(bytesArray);

            return CreateHashStringFromHashBytes(hashBytes);
        }

        private static string CreateHashStringFromHashBytes(IEnumerable<byte> hashBytes)
        {
            var hashBytesArray = hashBytes.ToArray();
            if (hashBytesArray.Length == 0)
                throw new ArgumentException(InvalidInputToHash);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytesArray)
            {
                sb.Append(hashByte.ToString("X2"));
            }

            return sb.ToString();
        }

        //public static bool Validate(IEnumerable<byte> inputBytes, string supliedHash)
        //{

        //}
    }
}
