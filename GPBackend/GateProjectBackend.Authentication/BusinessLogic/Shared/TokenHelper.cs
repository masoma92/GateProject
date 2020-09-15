using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.Shared
{
    public static class TokenHelper
    {
        private const string ENCRYPTION_KEY = "cZH3k2C5UGWqjc8DGXapT24q4jkgEBwf";

        static public string GenerateToken(byte[] passwordSalt)
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary()); //length: 8
            var input = time.Concat(passwordSalt).ToArray();

            var encrpytedData = Encrypt(input);

            var token = Convert.ToBase64String(encrpytedData).Replace('+', '-').Replace('/', '_');

            return token;
        }


        public static bool IsValidToken(string token, byte[] passwordSalt)
        {
            if (String.IsNullOrEmpty(token)) return false;

            token = token.Replace('-', '+').Replace('_', '/');

            var decryptedData = Decrypt(Convert.FromBase64String(token));

            DateTime time = DateTime.FromBinary(BitConverter.ToInt64(decryptedData.Take(8).ToArray(), 0));

            var temp = decryptedData.Skip(8).ToArray();
            var tempTime = DateTime.UtcNow.AddHours(-24);

            if (tempTime < time && passwordSalt.SequenceEqual(temp))
            {
                return true;
            }

            return false;
        }

        static private byte[] Encrypt(byte[] dataToEncrypt)
        {
            byte[] keybites = Encoding.UTF8.GetBytes(ENCRYPTION_KEY);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(ENCRYPTION_KEY, keybites, 1000);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                        cs.Close();
                    }
                    return ms.ToArray();
                }
            }
        }

        static private byte[] Decrypt(byte[] dataToDecrypt)
        {
            byte[] keybites = Encoding.UTF8.GetBytes(ENCRYPTION_KEY);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(ENCRYPTION_KEY, keybites, 1000);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                        cs.Close();
                    }
                    return ms.ToArray();
                }
            }
        }
    }
}
