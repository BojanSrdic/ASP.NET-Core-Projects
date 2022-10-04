using Innstant.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.Services
{
	public class SaveInnstantStaticData
	{
        #region Save Data to SQL database -- SaveDestinationsIntoDatabase
        public void SaveDataIntoDatabase(List<GulliverDestinations> list)
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

        #region Save Innstant Hotels to SQL database
        public void SaveInnstantHotelsIntoDatabase(List<InnstantHotels> list)
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
