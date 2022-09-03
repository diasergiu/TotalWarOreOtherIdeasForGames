using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TotalWarOreOtherIdeasForGames.Services
{
    public static class EncryptDecryptService
    {
        private const string key = "E546C8DF278CD5931069B522E695D4F2";
        public static byte[] EncryptString(string stringToEncrypt)
        {
            var keyEncrypted = Encoding.UTF8.GetBytes(key);

            using (var aesAlg = Aes.Create())
            {
                aesAlg.IV = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20};
                using (var encryptor = aesAlg.CreateEncryptor(keyEncrypted, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(stringToEncrypt);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return result;
                    }
                }
            }
        }


        public static string DectyptByte(byte[] cipherByte)
        {
            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(cipherByte, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(cipherByte, iv.Length, cipher, 0, iv.Length);
            var keyForDecryption = Encoding.UTF8.GetBytes(key);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(keyForDecryption, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }
    }
}
