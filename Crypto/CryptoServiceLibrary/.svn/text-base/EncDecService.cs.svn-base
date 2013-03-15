/* 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Company: -								 							 *            
 * Assignment: Cyclic Redundancy Check + 3DES + EF + WCF	             *
 * Deadline: -                           	 							 *
 * Programmer: Baran Topal                   							 *
 * Solution: Crypto					 							         *
 * Project Name: CryptoServiceLibrary          	               	 		 *
 * File name: EncDecService.cs                       					 *
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
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using CRC32;
using EDMDAL;
namespace CryptoServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class EncDecService : IEncDecService
    {
        private static byte[] randomBytes;

        // Random byte generation
        public EncDecService()
        {
            byte[] array = new byte[8];
            Random random = new Random();
            random.NextBytes(array);

            // Test
            /*
            foreach (byte value in array)
            {
                Console.WriteLine(value);
                Console.Write(' ');
            }
            Console.WriteLine();
             */
            randomBytes = array;
        }

        // Please check 3DES project in this solution for comments, code is the same
        public byte[] Encrypt(byte[] clearData, byte[] key, byte[] IV)
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

        public string Encrypt(string clearText, string trivia)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);

            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(8), pdb.GetBytes(8));

            return Convert.ToBase64String(encryptedData);
        }

        public byte[] Encrypt(byte[] clearData, string trivia)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);
            return Encrypt(clearData, pdb.GetBytes(8), pdb.GetBytes(8));
        }

        public void Debug()
        {
            string text = "In";
            System.IO.File.WriteAllText(@"C:\debug.txt", text);
        }

        public byte[] Decrypt(byte[] cipherData, byte[] key, byte[] IV)
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

        public string Decrypt(string cipherText, string trivia)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);

            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(8), pdb.GetBytes(8));
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        public byte[] Decrypt(byte[] cipherData, string trivia)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);
            return Decrypt(cipherData, pdb.GetBytes(8), pdb.GetBytes(8));
        }

        public bool Decrypt(string fileIn, string fileOut, string trivia)
        {
            FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(trivia, randomBytes);
            TripleDES alg = TripleDES.Create();

            alg.Key = pdb.GetBytes(alg.KeySize / 8);
            alg.IV = pdb.GetBytes(8);
            alg.Padding = PaddingMode.None;
            using (CryptoStream cs = new CryptoStream(fsOut, alg.CreateDecryptor(), CryptoStreamMode.Write))
            {
                int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int bytesRead;
                lock (this)
                {
                    do
                    {
                        bytesRead = fsIn.Read(buffer, 0, bufferLen);
                        cs.Write(buffer, 0, bytesRead);
                        cs.Flush();
                    } while (bytesRead != 0);
                }
            }
            fsIn.Flush();
            fsIn.Close();

            return true;
        }

        public bool Encrpyt(string fileIn, string fileOut, string trivia)
        {
            Debug();
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
                lock (this)
                {
                    do
                    {
                        bytesRead = fsIn.Read(buffer, 0, bufferLen);
                        cs.Write(buffer, 0, bytesRead);
                        cs.Flush();
                    } while (bytesRead != 0);
                }
            }
            byte[] allIn = File.ReadAllBytes(fileIn);
            byte[] allOut = File.ReadAllBytes(fileOut);

            fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            fsOut = new FileStream(fileOut, FileMode.Open, FileAccess.Read);

            Crc32 crc32Input = new Crc32();
            string hashInput = string.Empty;
            foreach (byte b in crc32Input.ComputeHash(fsIn)) hashInput += b.ToString("x2").ToLower();

            //Console.WriteLine("CRC-32 is {0}", hashInput);

            Crc32 crc32Output = new Crc32();
            string hashOutput = string.Empty;
            foreach (byte b in crc32Output.ComputeHash(fsOut)) hashOutput += b.ToString("x2").ToLower();

            //Console.WriteLine("CRC-32 is {0}", hashOutput);

            // EF-ORM
            EDM.AddNewRecord(fileIn, allIn, fileOut, allOut, hashInput, hashOutput, trivia);

            fsOut.Flush();
            fsOut.Close();
            fsIn.Flush();
            fsIn.Close();
            return true;
        }
    }
}
