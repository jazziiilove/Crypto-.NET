/* 
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Company: -								 							 *            
 * Assignment: Cyclic Redundancy Check + 3DES + EF + WCF	             *
 * Deadline: -                           	 							 *
 * Programmer: Baran Topal                   							 *
 * Solution: Crypto					 							         *
 * Project Name: EDMDAL                       	               	 		 *
 * File name: EDM.cs                                  					 *
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
using System.Data.EntityClient;
using System.Data;

// Entity Framework ORM
namespace EDMDAL
{
    public class EDM
    {
        public static void ReadContents()
        {
            using (EntityConnection cn = new EntityConnection("name=DESEDMEntities"))
            {
                cn.Open();

                string query = "SELECT VALUE DESContent FROM DESEDMEntities.DESContents AS DESContent";

                using (EntityCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = query;

                    // Finally, get the data reader and process records.
                    using (EntityDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        Console.WriteLine("Count: " + dr.FieldCount);
                        Console.WriteLine("Row:" + dr.HasRows);

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Console.WriteLine("****RECORD*****");
                                Console.WriteLine("ID: {0}", dr["Id"]);
                                Console.WriteLine("Input File Name: {0}", dr["InputFileName"]);
                                Console.WriteLine("Output File Name: {0}", dr["OutputFileName"]);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adding a new record to database via EF
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <param name="inputFileContent"></param>
        /// <param name="outputFileName"></param>
        /// <param name="outputFileContent"></param>
        /// <param name="hashInput"></param>
        /// <param name="hashOutput"></param>
        /// <param name="trivia"></param>
        public static void AddNewRecord(string inputFileName, byte[] inputFileContent, string outputFileName, byte[] outputFileContent, string hashInput, string hashOutput, string trivia)
        {

            using (DESEDMEntities context = new DESEDMEntities())
            {
                try
                {
                    context.DESContents.AddObject(new DESContent() { InputFileName = inputFileName, InputFileContent = inputFileContent, OutputFileName = outputFileName, OutputFileContent = outputFileContent, HashInput = hashInput, HashOutput = hashOutput, Trivia = trivia });
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
        }

        /// <summary>
        /// Print database content
        /// </summary>
        public static void PrintAllContent()
        {
            using (DESEDMEntities context = new DESEDMEntities())
            {
                foreach (DESContent c in context.DESContents)
                    Console.WriteLine(c.InputFileName);
            }
        }

        /// <summary>
        /// Remove a row via id
        /// </summary>
        public static void RemoveRecord()
        {
            using (DESEDMEntities context = new DESEDMEntities())
            {
                EntityKey key = new EntityKey("DESEDMEntities.DESContents", "Id", 2222);
                DESContent contentToDelete = (DESContent)context.GetObjectByKey(key);
                if (contentToDelete != null)
                {
                    context.DeleteObject(contentToDelete);
                    context.SaveChanges();
                }

            }
        }

        /// <summary>
        /// Remove via link
        /// </summary>
        public static void RemoveRecordWithLINQ()
        {
            using (DESEDMEntities context = new DESEDMEntities())
            {
                // Check whether we have it or not
                var contentToDelete = (from c in context.DESContents where c.Id == 2222 select c).FirstOrDefault();
            }
        }

        /// <summary>
        /// Update a row via id
        /// </summary>
        private static void UpdateRecord()
        {
            using (DESEDMEntities context = new DESEDMEntities())
            {
                EntityKey key = new EntityKey("DESEDMEntities.DESContents", "Id", 2222);

                DESContent contentToUpdate = (DESContent)context.GetObjectByKey(key);
                Console.WriteLine(contentToUpdate.EntityState);

                if (contentToUpdate != null)
                {
                    contentToUpdate.InputFileName = "Updated.txt";
                    context.SaveChanges();
                }
            }
        }

        // DLL
        /// <summary>
        /// Test data
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ReadContents();
            string inputFileName = "loremipsum.txt";
            byte[] inputFileContent = null;
            string outputFileName = string.Empty;
            byte[] outputFileContent = null;
            string hashInput = string.Empty;
            string hashOutput = string.Empty;
            string trivia = string.Empty;
            AddNewRecord(inputFileName, inputFileContent, outputFileName, outputFileContent, hashInput, hashOutput, trivia);
            PrintAllContent();
        }
    }
}
