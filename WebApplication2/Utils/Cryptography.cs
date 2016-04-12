using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication2.Utils
{
    class Cryptography
    {
        private static byte[] SALT = { 34, 65, 11, 12, 16, 0, 65, 128, 92, 72, 65, 23, 87, 11, 10, 8 };
        private enum CryptProc { ENCRYPT, DECRYPT };

        /// <summary>
        /// Performs either an encryption or decrytion
        /// </summary>
        /// <param name="plain">Unencrypted byte array
        /// <param name="password">Password to be used
        /// <param name="iterations">Number of iterations hash algorithm uses
        /// <param name="cryptproc">Process to be performed
        /// <returns>Results of process in the form of a byte array</returns>
        private static byte[] CryptBytes(byte[] plain, string password, int iterations, CryptProc cryptproc)
        {
            //Create our key from the Password provided
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, SALT, "SHA512", iterations);

            //We'll be using 3DES
            TripleDES des = TripleDES.Create();
            des.Key = pdb.GetBytes(24);
            des.IV = pdb.GetBytes(8);

            MemoryStream memstream = new MemoryStream();

            ICryptoTransform cryptor = (cryptproc == CryptProc.ENCRYPT) ? des.CreateEncryptor() : des.CreateDecryptor();

            CryptoStream cryptostream = new CryptoStream(memstream, cryptor, CryptoStreamMode.Write);
            cryptostream.Write(plain, 0, plain.Length); //write finished product to our MemoryStream

            cryptostream.Close();

            return memstream.ToArray();
        }

        /// <summary>
        /// Encrypts byte arrays
        /// </summary>
        /// <param name="plain">Unencrypted byte array
        /// <param name="password">Password to be used
        /// <param name="iterations">Number of iterations hash algorithm uses
        /// <returns>Encypted byte array</returns>
        public static byte[] EncryptBytes(byte[] plain, string password, int iterations)
        {
            return CryptBytes(plain, password, 2, CryptProc.ENCRYPT);
        }

        /// <summary>
        /// Decrypts byte arrays
        /// </summary>
        /// <param name="plain">Unencrypted byte array
        /// <param name="password">Password to be used
        /// <param name="iterations">Number of iterations hash algorithm uses
        /// <returns>Decrypted byte array</returns>
        public static byte[] DecryptBytes(byte[] plain, string password, int iterations)
        {
            return CryptBytes(plain, password, 2, CryptProc.DECRYPT);
        }
        public static string Encrypt(string str)
        {
            var EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        public static string Decrypt(string str)
        {
            str = str.Replace(" ", "+");
            string DecryptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[str.Length];

            byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(str.Replace(" ", "+"));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
    }
}