using Innstant.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Innstant.Services
{
	public interface ISaveInnstantStaticData
	{
        void SaveInnstantDestinations(List<InnstantDestinations> list);
        void SaveInnstantHotelDestinations(List<InnstantHotelDestination> list);
        void SaveInnstantHotelDestinationsMappingTable(List<GulliverDestinations> list);
        void SaveInnstantHotels(List<InnstantHotels> list);
        void SaveInnstantRooms(List<InnstantRooms> list);
    }

    public class SaveInnstantStaticData : ISaveInnstantStaticData
	{
        string connectionString = "Data Source = BSRDIC; Initial Catalog = Test; Integrated Security = True; Encrypt=False";

        #region ## SaveInnstantDestinations ##
        public void SaveInnstantDestinations(List<InnstantDestinations> list)
        {
            using (SqlConnection dbConnection = new SqlConnection(@"Data Source = BSRDIC; Initial Catalog = Test; Integrated Security = True; Encrypt=False"))
            {
                dbConnection.Open();

                foreach (InnstantDestinations row in list)
                {
                    string query = string.Format("INSERT INTO [Innstant_Destinations] values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', {9})", 
                        row.DestinationId, row.DestinationName, row.DestinationType, row.Latitude, row.Longitude, row.CountryId, row.Searchable, row.Seoname, row.State, row.Contains);

                    SqlCommand cmd = new SqlCommand(query, dbConnection);
                    cmd.ExecuteNonQuery();
                }

                dbConnection.Close();
            }
        }
        #endregion

        #region ## SaveInnstantHotelDestinations ##
        public void SaveInnstantHotelDestinations(List<InnstantHotelDestination> list)
        {
            using (SqlConnection dbConnection = new SqlConnection(@"Data Source = BSRDIC; Initial Catalog = Test; Integrated Security = True; Encrypt=False"))
            {
                dbConnection.Open();

                foreach (InnstantHotelDestination row in list)
                {
                    string query = string.Format("INSERT INTO [Innstant_Hotel_Destination] values ('{0}', '{1}', '{2}')", row.HotelId,
                        row.DestinationId, row.Surroundings);

                    SqlCommand cmd = new SqlCommand(query, dbConnection);
                    cmd.ExecuteNonQuery();
                }

                dbConnection.Close();
            }
        }
        #endregion

        #region ## SaveInnstantHotelDestinationsMappingTable ##
        public void SaveInnstantHotelDestinationsMappingTable(List<GulliverDestinations> list)
        {
            using (SqlConnection dbConnection = new SqlConnection(@"Data Source = BSRDIC; Initial Catalog = Test; Integrated Security = True; Encrypt=False"))
            {
                dbConnection.Open();

                foreach (GulliverDestinations row in list)
                {
                    string query = string.Format("INSERT INTO [INNSTANT_GULLIVER_DESTINATION_CONVERSIONS] values ('{0}', '{1}', '{2}', '{3}', '{4}')", row.GulliverDestinationId,
                        row.GulliverDestinationName, row.InnstantDestinationId, row.InnstantDestinationName, row.Type);

                    SqlCommand cmd = new SqlCommand(query, dbConnection);
                    cmd.ExecuteNonQuery();
                }

                dbConnection.Close();
            }
        }
        #endregion

        #region ## SaveInnstantRooms ##
        public void SaveInnstantRooms(List<InnstantRooms> list)
        {
            using (SqlConnection dbConnection = new SqlConnection(@"Data Source = BSRDIC; Initial Catalog = Test; Integrated Security = True; Encrypt=False"))
            {
                dbConnection.Open();

                foreach (InnstantRooms row in list)
                {
                    SqlCommand cmd = new SqlCommand("[Innstant_Insert_Room_Types]", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoomTypeId", row.RoomTypeId);
                    cmd.Parameters.AddWithValue("@HotelId", row.HotelId);
                    cmd.Parameters.AddWithValue("@RoomName", row.RoomName);
                    cmd.Parameters.AddWithValue("@RoomCategory", row.RoomCategory);
                    cmd.Parameters.AddWithValue("@Bedding", row.Bedding);


                    cmd.ExecuteNonQuery();
                }                

                dbConnection.Close();
            }
        }
        #endregion

        #region ## SaveInnstantHotels ##
        public void SaveInnstantHotels(List<InnstantHotels> list)
        {
            using (SqlConnection dbConnection = new SqlConnection(@"Data Source = BSRDIC; Initial Catalog = Test; Integrated Security = True; Encrypt=False"))
            {
                dbConnection.Open();

                foreach (InnstantHotels row in list)
                {
                    string query = string.Format("INSERT INTO [Innstant_Hotels] values ('{0}', '{1}')", row.HotelId,
                        row.HotelName);

                    SqlCommand cmd = new SqlCommand(query, dbConnection);
                    cmd.ExecuteNonQuery();
                }

                dbConnection.Close();
            }
        }
        #endregion


        #region Save Innstant Hotels-Destinations to database

        #region My Way for creating HotelsDestiantion table

        #endregion

        #region Dusans way for creating HotelsDestiantion table
        #endregion
        #endregion
    }
}
