using Innstant.Models;
using Innstant.Services;
using System;
using System.Collections.Generic;

namespace Innstant
{
	class Program
	{
		static void Main(string[] args)
		{
            GulliverService.CreateGulliverDestinationTable();

            #region Task 2: Receive Israel hotel list

            // Read all destinations from israel "IL"
            var israelsDestinationList = InnstantStaticDataReader.InnstantDestinationsParser();

            // Create tabels in the database

            // find all hotel ID for this step create Stored procedure

            // Search in hotels.csv file for all hotels by isael list ID

            #endregion
        }



        #region Save Data to CSV file
        public void SaveDataIntoCSV()
        {

        }
        #endregion

        #region Ideas and Learning

        public void NotUsed()
        {
            var innstantDestinations = ProcessInnstantStaticData.ProcessInnstantDestinations();

            foreach (InnstantDestinations destination in innstantDestinations)
            {
                string a = destination.Contains;
                string[] destionationProperties = a.Split(";");

                int[] ints = Array.ConvertAll(destionationProperties, s => int.Parse(s));

                if (ints.Length != 1)
                {
                    // Set Type prop to city or area
                    destination.Type = "Area";

                }
                else { destination.Type = "City"; }

                bool v = a.Contains(";");
                destination.Type = destination.Contains.Contains(";") ? "Area" : "City";
            }
        }

        public void PrintListMethode()
        {
            var list = ProcessInnstantStaticData.ProcessInnstantDestinations();
            // Lisata
            Console.WriteLine(list); // necu dobiti listu 
            // da bi ispisao listu u terminalu potrebno je da napravim foreach pelju

            foreach (var a in list)
            {
                Console.WriteLine(a.Name);
            }
        }

        #endregion
    }
}

// Question: City or area? what we do not have anything written in last Contains
// Proverio sam yaya provider Yaya ID, name Guliver ID gulier Name