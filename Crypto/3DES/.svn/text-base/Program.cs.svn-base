/* 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Company: -								 							 *            
 * Assignment: Cyclic Redundancy Check + 3DES + EF + WCF	             *
 * Deadline: -                           	 							 *
 * Programmer: Baran Topal                   							 *
 * Solution: Crypto					 							         *
 * Project Name: 3DES          	        	 							 *
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

namespace _3DES
{
    // DLL
    class Program
    {
        static void Main(string[] args)
        {
            // Test data
            EncDec e = new EncDec();
            EncDec.Encrpyt("loremipsum.txt", "encrypted.enc", "1234-4567-8910-2345");
            EncDec.Decrypt("encrypted.enc", "decrypted.dec", "1234-4567-8910-2345");
        }
    }
}
