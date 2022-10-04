using Innstant.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.Services
{
	/* This is CSV file converter, converts CSV file to C# object */
	public class InnstantStaticDataReader
	{
        private readonly SaveInnstantStaticData _saveToDatabase;
        public InnstantStaticDataReader(SaveInnstantStaticData saveToDatabase)
		{
            _saveToDatabase = saveToDatabase;
		}

        public List<InnstantDestinations> InnstantDestinationsParser()
        {
            #region Read all destionations from destinations.csv file and filter them by Israel "IL"

            string[] innstantDestionationRows = File.ReadAllLines(@"..\\..\\..\\InnstantStaticData\\destinations.csv");

            var innstantIsraelDestionatins = new List<InnstantDestinations>();

            foreach (string row in innstantDestionationRows.Skip(1))
            {
                string[] prop = row.Split(',');
                if (string.Equals(prop[5].ToString(), "IL"))
                {
                    innstantIsraelDestionatins.Add(new InnstantDestinations()
                    {
                        DestinationId = Convert.ToInt32(prop[0]),
                        DestinationName = prop[1],
                        CountryId = prop[5],
                        Contains = prop[9]
                    });
                }
            }

            #endregion

            return innstantIsraelDestionatins;
        }

        public List<InnstantHotels> InnstantIsraelHotelsParser(List<int> innstantIsraelHotelIdList)
        {
            string[] innstantHotelsArray = File.ReadAllLines(@"..\\..\\..\\InnstantStaticData\\hotels.csv");

            var innstantIsraelHotels = new List<InnstantHotels>();

            foreach (string hotelRow in innstantHotelsArray.Skip(1))
            {
                string[] stringIntoArray = hotelRow.Split(',');

                int hotelIdCastedValue = Convert.ToInt32(stringIntoArray[0]);

                if (innstantIsraelHotelIdList.Contains(hotelIdCastedValue))
                {
                    innstantIsraelHotels.Add(new InnstantHotels
                    {
                        HotelId = Convert.ToInt32(stringIntoArray[0]),
                        HotelName = stringIntoArray[1],
                        Address = stringIntoArray[2],
                        Status = Convert.ToInt32(stringIntoArray[3]),
                        Zip = Convert.ToInt32(stringIntoArray[4]),
                        Phone = stringIntoArray[5],
                        Fax = stringIntoArray[6],
                        Lat = Convert.ToInt32(stringIntoArray[7]),
                        Lon = Convert.ToInt32(stringIntoArray[8]),
                        Stars = Convert.ToInt32(stringIntoArray[9]),
                        Seoname = stringIntoArray[10]
                    });

                }
            }

            // Save to database
            _saveToDatabase.SaveInnstantHotelsIntoDatabase(innstantIsraelHotels);

            return innstantIsraelHotels;
        }

        public List<InnstantHotelDestination> InnstantIsraelHotelDestinationParser(List<InnstantDestinations> innstantIsraelsDestinationList)
        {
            #region Task 2: Receive Israel Hotel_Destination mapping list

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
    }
}
