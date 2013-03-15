/* 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Company: -								 							 *            
 * Assignment: Cyclic Redundancy Check + 3DES + EF + WCF	             *
 * Deadline: -                           	 							 *
 * Programmer: Baran Topal                   							 *
 * Solution: Crypto					 							         *
 * Project Name: CRC32          	               	 					 *
 * File name: Program.cs                       							 *
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

namespace CRC32
{
    // DLL
    class Program
    {
        /// <summary>
        /// Test data
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Crc32 crc32 = new Crc32();
            string hash = string.Empty;
            using (FileStream fs = File.Open("loremipsum.txt", FileMode.Open))
                foreach (byte b in crc32.ComputeHash(fs)) hash += b.ToString("x2").ToLower();
            Console.WriteLine("CRC-32 is {0}", hash);
        }
    }
}
