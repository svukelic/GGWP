using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GGWP.Security
{
    public class PWSHasher
    {
        public HashWithSaltResult HashWithGivenSalt(string password, int saltLength, byte[] saltBytes)
        {
            //RNG rng = new RNG();
            HashAlgorithm hashAlgo = SHA512.Create();
            //byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(digestBytes));
        }

        public HashWithSaltResult HashWithRandomSalt(string password, int saltLength, HashAlgorithm hashAlgo)
        {
            RNG rng = new RNG();
            byte[] saltBytes = rng.GenerateRandomCryptographicBytes(saltLength);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(digestBytes));
        }
    }
}
