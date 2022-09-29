using Innstant.Models;
using Innstant.Services;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Innstant
{
	class Program
	{
		static void Main(string[] args)
		{
            #region Task 1: Create Gulliver Destination tabel
            //GulliverService.CreateGulliverDestinationTable();
            #endregion

            #region Task 2: Receive Israel hotel list

            // Read all destinations from israel "IL"
            var innstantIsraelsDestinationList = InnstantStaticDataReader.InnstantDestinationsParser();

            // Create mapping table between Israel Hotels and Destinations
            var israelHotelDestinationMappingTable = IsraelHotelDestinationMappingTable(innstantIsraelsDestinationList);
            var innstantHotelsList = new List<InnstantHotelDestination>(); // ovo je lista Svih hotela iz izraela

            var innstantIsraelHotelIdList = new List<int>();

			foreach (InnstantHotelDestination row in israelHotelDestinationMappingTable)
			{
                innstantIsraelHotelIdList.Add(row.HotelId);
			}

			InnstantIsraelHotelsList(innstantIsraelHotelIdList);

            /*	foreach (InnstantHotels hotel in israelHotels)
			{
				if (innstantHotelsList.Contains(hotels.HotelId))
				{
                    // namapiraj
				}
			}*/

            #endregion

            #region Task 3: Craete Innstant_Gulliver_Hotels_Conversion table
            #endregion

        }

        List<int> TestList = new List<int> { 123, 1323, 141, 13214, 51, 123 };
        public static List<InnstantHotelDestination> InnstantIsraelHotelsList(List<int> innstantIsraelHotelIdList)
		{
			string[] innstantHotelsArray = File.ReadAllLines(@"..\\..\\..\\InnstantStaticData\\hotels.csv");

            var innstantIsraelHotels = new List<InnstantHotels>();

			foreach (string hotelRow in innstantHotelsArray.Skip(1))
			{
                string[] stringIntoArray = hotelRow.Split(',');
                int hotelIdCastedValue = Convert.ToInt32(stringIntoArray[0]);

                if (innstantIsraelHotelIdList.Contains(hotelIdCastedValue))
				{
                    innstantIsraelHotels.Add(new InnstantHotels { 
                        HotelId = Convert.ToInt32(stringIntoArray[0]),
                        HotelName = stringIntoArray[1],
                        Address = stringIntoArray[2]
                    });

                }
			}

            return null;
		}

		public static List<InnstantHotelDestination> IsraelHotelDestinationMappingTable(List<InnstantDestinations> innstantIsraelsDestinationList)
        {
            #region Task 2: Receive Israel hotel list

            // Read Hotels_Destination file and merge two string arrays in one
            string[] innstantHotelDestionationRows = File.ReadAllLines(@"..\\..\\..\\InnstantStaticData\\hotel_destinations.csv");
            string[] innstantHotelDestionationRows1 = File.ReadAllLines(@"..\\..\\..\\InnstantStaticData\\hotel_destinations.part1.csv");
            // innstantHotelDestionationRows = innstantHotelDestionationRows1.Concat(innstantHotelDestionationRows1).ToArray();
            // kada se spajaju dva niza imamo problem sto se onaj prvi red ponavlaj i u rugom dokumentu a nismo ga skipovali

            var israelDestinationIdList = new List<int>(innstantIsraelsDestinationList.Select(s => s.DestinationId));

            // Create Israel Hotel_Destination list and fill the list
            var israelHotelDestionatin = new List<InnstantHotelDestination>();
            foreach (string destinationRow in innstantHotelDestionationRows.Skip(1))
            {
                string[] stringIntoArray = destinationRow.Split(',');

                // Cast Row destinationId from string to int
                var destinationRowIdCastedValue = Convert.ToInt32(stringIntoArray[1]);

                if (israelDestinationIdList.Contains(destinationRowIdCastedValue))
                {
                    israelHotelDestionatin.Add(new InnstantHotelDestination()
                    {
                        DestinationId = Convert.ToInt32(stringIntoArray[0]),
                        HotelId = Convert.ToInt32(stringIntoArray[1])
                    });
                }
            }

            return israelHotelDestionatin;

            // check if this can be done using stored procedure 

            #endregion
        }

        #region Bojan komplikovanije resenje :D Preko DataTable
        public static void ConvertCSVtoSQL()
        {
            try
			{
                // Ovde postoji jedna velika mana a to je komplikovanost koda
                // Da bi citali podatke iz dataTable treba nam cela kompleksa logika sto se vidi u primeru kreiramo celu metodu koja cita redove pa dok pronadjemo sve te redove malo je komplikovano
                // string[] hotelDestionationLines = File.ReadAllLines(@"..\\..\\..\\InnstantStaticData\\hotel_destinations.csv");
                // preskocili smo ceo kompleksan proces i smanjili kolicinu koda :D
                // Tako da pogledati dusanovo resenje
                string csv_file_path = @"C:\Users\b.srdic\Desktop\ASP.NET Core\Git\ASP.NET-Core-Projects\Gulliver\Innstant\Innstant\InnstantStaticData\hotel_destinations.csv";
                DataTable csvData = GetDataTabletFromCSVFile(csv_file_path);
                InsertDataIntoSQLServerUsingSQLBulkCopy(csvData);
            }
			catch(Exception ex)
			{
                Console.WriteLine(ex);
			}
            
        }

        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return null;
            }
            return csvData;
        }

        // Copy the DataTable to SQL Server using SqlBulkCopyReceived an invalid column 
        public static void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            using (SqlConnection dbConnection = new SqlConnection(@"Data Source = BSRDIC; Initial Catalog = Test; Integrated Security = True"))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = "Destinations";

                    foreach (var column in csvFileData.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());

                    s.WriteToServer(csvFileData);
                }
            }
        }
        #endregion


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
                Console.WriteLine(a.DestinationName);
            }
        }

        #endregion
    }
}

// Question: City or area? what we do not have anything written in last Contains
// Proverio sam yaya provider Yaya ID, name Guliver ID gulier Name

// TAsk 2:
// Read about HashSet

// Prepraviti static metode dok je jos vreme kako bi mogli testirati aplikaciju
