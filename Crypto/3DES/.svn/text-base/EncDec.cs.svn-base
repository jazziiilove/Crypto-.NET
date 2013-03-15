/* 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Company: -								 							 *            
 * Assignment: Cyclic Redundancy Check + 3DES + EF + WCF	             *
 * Deadline: -                           	 							 *
 * Programmer: Baran Topal                   							 *
 * Solution: Crypto					 							         *
 * Project Name: 3DES          	        	 							 *
 * File name: EncDes.cs                       							 *
 *                                           							 *      
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *	                                                                                         *
 *  LICENSE: This source file is subject to have the protection of GNU General               *
 *	Public License. You can distribute the code freely but storing this license information. *
 *	Contact Baran Topal if you have any questions. barantopal@barantopal.com                 *
 *	                                                                                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace _3DES
{
    public class EncDec
    {
        private static byte[] randomBytes;

        /// <summary>
        /// Byte randomization
        /// </summary>

        public EncDec()
        {
            byte[] array = new byte[8];
            Random random = new Random();
            random.NextBytes(array);

            // Test
            foreach (byte value in array)
            {
                Console.WriteLine(value);
                Console.Write(' ');
            }
            Console.WriteLine();
            randomBytes = array;
        }

        /// <summary>
        /// Encrypt the clear byte data with key and IV
        /// </summary>
        /// <param name="clearData"></param>
        /// <param name="key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] clearData, byte[] key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();


            TripleDES alg = TripleDES.Create();

            alg.Key = key;
            alg.IV = IV;
            using (CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearData, 0, clearData.Length);
            }

            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        /// <summary>
        /// Encrypt the clear string data with a password
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="trivia"></param>
        /// <returns></returns>
        public static string Encrypt(string clearText, string trivia)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);

            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(8), pdb.GetBytes(8));

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// Encrypt the clear byte data with a password
        /// </summary>
        /// <param name="clearData"></param>
        /// <param name="trivia"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] clearData, string trivia)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);
            return Encrypt(clearData, pdb.GetBytes(8), pdb.GetBytes(8));
        }

        /// <summary>
        /// Encrypt the file input data with a password to a file output
        /// </summary>
        /// <param name="fileIn"></param>
        /// <param name="fileOut"></param>
        /// <param name="trivia"></param>
        public static void Encrpyt(string fileIn, string fileOut, string trivia)
        {
            FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);


            TripleDES alg = TripleDES.Create();
            alg.Key = pdb.GetBytes(alg.KeySize / 8);
            alg.IV = pdb.GetBytes(8);

            using (CryptoStream cs = new CryptoStream(fsOut, alg.CreateEncryptor(), CryptoStreamMode.Write))
            {
                int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];

                int bytesRead;

                do
                {
                    bytesRead = fsIn.Read(buffer, 0, bufferLen);
                    cs.Write(buffer, 0, bytesRead);
                } while (bytesRead != 0);
            }
        }

        /// <summary>
        /// Similar with above definitions but Decrypt
        /// </summary>
        /// <param name="cipherData"></param>
        /// <param name="key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] cipherData, byte[] key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            TripleDES alg = TripleDES.Create();

            alg.Key = key;
            alg.IV = IV;

            using (CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherData, 0, cipherData.Length);
            }

            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }


        public static string Decrypt(string cipherText, string trivia)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);

            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(8), pdb.GetBytes(8));
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        public static byte[] Decrypt(byte[] cipherData, string trivia)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);
            return Decrypt(cipherData, pdb.GetBytes(8), pdb.GetBytes(8));
        }

        public static void Decrypt(string fileIn, string fileOut, string trivia)
        {
            FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);
            TripleDES alg = TripleDES.Create();

            alg.Key = pdb.GetBytes(alg.KeySize / 8);
            alg.IV = pdb.GetBytes(8);

            using (CryptoStream cs = new CryptoStream(fsOut, alg.CreateDecryptor(), CryptoStreamMode.Write))
            {
                int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int bytesRead;

                do
                {
                    bytesRead = fsIn.Read(buffer, 0, bufferLen);
                    cs.Write(buffer, 0, bytesRead);

                } while (bytesRead != 0);
            }
        }
    }
}
