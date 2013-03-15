/* 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Company: -								 							 *            
 * Assignment: Cyclic Redundancy Check + 3DES + EF + WCF	             *
 * Deadline: -                           	 							 *
 * Programmer: Baran Topal                   							 *
 * Solution: Crypto					 							         *
 * Project Name: ConsoleHost          	        	 					 *
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
using System.ServiceModel.Description;
using System.ServiceModel;
using CryptoServiceLibrary;
using System.Threading;


// Host that keeps the web service, client connects here
namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = "http://localhost:8000/EncDecService";
            using (var host = new ServiceHost(typeof(EncDecService)))
            {
                ServiceEndpoint endPoint = host.AddServiceEndpoint(typeof(IEncDecService), new BasicHttpBinding(), address);
                host.Open();
                foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
                {
                    Console.WriteLine("{0} ({1})",
                      endpoint.Address.ToString(), endpoint.Binding.Name);
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to stop the service.");
                Console.ReadKey();
                //Service stopped
            }
        }
    }
}
