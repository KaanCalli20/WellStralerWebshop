using System;
using System.Security.Cryptography;
using crypto;

namespace WellStralerWebshop.Models.Domain
{
    public class Encryption
    {
        public static string Encrypt(string text, string password)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();
            string encrypted = "";
            byte[] hash = new byte[32];
            byte[] temp = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password));
            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);
            AES.Key = hash;
            AES.Mode = System.Security.Cryptography.CipherMode.ECB;
            ICryptoTransform DESEncrypter = AES.CreateEncryptor();

            byte[] Buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));
            
            return encrypted;
        }

        /*
        public static string Decrypt(string text, string password)
        {
            RijndaelManaged AES = new RijndaelManaged();
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.ECB;

            MD5CryptoServiceProvider Hash_AES = new MD5CryptoServiceProvider();
            string decrypted = " ";

            byte[] hash = new byte[32];
            byte[] temp = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(password));

            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);
            AES.Key = hash;
            
            ICryptoTransform DESDecrypter = AES.CreateDecryptor();
            byte[] Buffer = Convert.FromBase64String(text);
            AES.Clear();

            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length));

            return decrypted;
        }*/
    }
}
