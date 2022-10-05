using Innstant.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.Services
{
	/* This is CSV file converter, converts CSV file to C# object */
    public interface IInnstantStaticDataReader
	{
        List<InnstantDestinations> InnstantDestinationsParser();
        List<InnstantHotelDestination> InnstantIsraelHotelDestinationParser(List<InnstantDestinations> innstantIsraelsDestinationList);
        void InnstantIsraelHotelsParser(List<InnstantHotelDestination> innstantIsraelHotelIdList);
    }

    public class InnstantStaticDataReader : IInnstantStaticDataReader
    {
        private readonly ISaveInnstantStaticData _saveInnstantStaticData;
        public InnstantStaticDataReader(ISaveInnstantStaticData saveInnstantStaticData)
		{
            _saveInnstantStaticData = saveInnstantStaticData;
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
                    if (string.IsNullOrEmpty(prop[9]))
                        prop[9] = "''";
                    else prop[9] = $"'{prop[9]}'";

                    innstantIsraelDestionatins.Add(new InnstantDestinations()
                    {
                        DestinationId = Convert.ToInt32(prop[0]),
                        DestinationName = prop[1],
                        DestinationType = prop[2],
                        Latitude = prop[3],
                        Longitude = prop[4],
                        CountryId = prop[5],
                        Searchable = Convert.ToInt32(prop[6]),
                        Seoname = prop[7],
                        State = prop[8],
                        Contains = prop[9]
                    });
                }
            }

            #endregion

            // Have problem with saveing List into the table. We can use DataRow

            // Convert List to DataTable
            /*var dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Add("DestinationId");

            foreach (InnstantDestinations item in innstantIsraelDestionatins)
            {
                DataRow workRow = dataTable.NewRow();
                dataTable.Rows.Add(item);
            }*/

            _saveInnstantStaticData.SaveInnstantDestinations(innstantIsraelDestionatins);
            return innstantIsraelDestionatins;
        }

        public List<InnstantHotelDestination> InnstantIsraelHotelDestinationParser(List<InnstantDestinations> innstantIsraelsDestinationList)
        {
            
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
                        HotelId = Convert.ToInt32(stringIntoArray[1]),
                        Surroundings = Convert.ToInt32(stringIntoArray[2])
                    });
                }
            }

            _saveInnstantStaticData.SaveInnstantHotelDestinations(israelHotelDestionatin);
            return israelHotelDestionatin;
        }


        // The logic is good but hotels.csv file has lot of mistakes we use phiton to extract hotels
        public void InnstantIsraelHotelsParser(List<InnstantHotelDestination> innstantIsraelHotelDestinationList)
        {
            string[] innstantHotelsArray = File.ReadAllLines(@"..\\..\\..\\InnstantStaticData\\hotels.csv");

            var innstantIsraelHotels = new List<InnstantHotels>();
            var innstantIsraelHotelIdList = new List<int>(innstantIsraelHotelDestinationList.Select(x => x.HotelId));

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
                        Latitude = stringIntoArray[7],
                        Longitude = stringIntoArray[8],
                        Stars = Convert.ToInt32(stringIntoArray[9]),
                        Seoname = stringIntoArray[10]
                    });

                }
            }

            _saveInnstantStaticData.SaveInnstantHotels(innstantIsraelHotels);
        }
    }
}
