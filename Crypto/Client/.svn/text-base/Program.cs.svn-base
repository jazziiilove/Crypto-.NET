/* 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Company: -								 							 *            
 * Assignment: Cyclic Redundancy Check + 3DES + EF + WCF	             *
 * Deadline: -                           	 							 *
 * Programmer: Baran Topal                   							 *
 * Solution: Crypto					 							         *
 * Project Name: Client          	        	 						 *
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
using System.ServiceModel;
using System.IO;
using System.Threading;
using CryptoServiceLibrary;

// Web service client
namespace Client
{
    class Program
    {
        private static IEncDecService httpChannel = null;
        static void Main(string[] args)
        {
            // Prevent possible exception by giving a delay
            Thread.Sleep(5000);
            var httpEndPoint = new EndpointAddress("http://localhost:8000/EncDecService");

            // You can add a service reference and just use a proxy
            // mex in config is a relative address, so you have to add a base address in config
            // service behavior for publishing metadata

            httpChannel = ChannelFactory<IEncDecService>.CreateChannel(new BasicHttpBinding(), httpEndPoint);

            // Web service client sends the data and retrieves the result
            bool enc = httpChannel.Encrpyt("loremipsum_inp.txt", "loremipsum_enc.enc", "1234-4567-8910-2345");
            bool dec = httpChannel.Decrypt("loremipsum_enc.enc", "loremipsum_dec.dec", "1234-4567-8910-2345");

            if (enc && dec)
                if (enc)
                    Console.WriteLine("Encryption and decryption successful!");
                else
                    Console.WriteLine("Hopefully you are not seeing this...");
        }
    }
}
