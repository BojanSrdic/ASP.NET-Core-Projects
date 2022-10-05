using Innstant.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.DataAccess
{
	public interface IInnstantDataAccessLayer
	{
		IEnumerable<InnstantDestinations> GetInnstantDestinations();
		IEnumerable<InnstantHotels> GetIsraelInnstantHotels();
	}

	public class InnstantDataAccessLayer : IInnstantDataAccessLayer
	{
		public IEnumerable<InnstantDestinations> GetInnstantDestinations()
		{
			List<InnstantDestinations> innstantDestinationsList = new List<InnstantDestinations>();

			using (SqlConnection connection = new SqlConnection(@"Data Source = BSRDIC; Initial Catalog = Test; Integrated Security = True; Encrypt=False"))
			{
				SqlCommand cmd = new SqlCommand("select * from [Innstant_Destinations]", connection);
				cmd.CommandType = CommandType.Text;

				connection.Open();
				SqlDataReader rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					InnstantDestinations innstantDestination = new InnstantDestinations();
					innstantDestination.DestinationId = Convert.ToInt32(rdr["DestinationId"]);
					innstantDestination.DestinationName = rdr["DestinationName"].ToString();
					innstantDestination.DestinationType = rdr["DestinationType"].ToString();

					innstantDestinationsList.Add(innstantDestination);
				}
				connection.Close();
			}

			return innstantDestinationsList;
		}

		public IEnumerable<InnstantHotels> GetIsraelInnstantHotels()
		{
			List<InnstantHotels> innstantHotelsList = new List<InnstantHotels>();

			using (SqlConnection connection = new SqlConnection(@"Data Source = BSRDIC; Initial Catalog = Test; Integrated Security = True; Encrypt=False"))
			{
				SqlCommand cmd = new SqlCommand("select * from [Innstant_Hotels]", connection);
				cmd.CommandType = CommandType.Text;

				connection.Open();
				SqlDataReader rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					InnstantHotels innstantHotel = new InnstantHotels();
					innstantHotel.HotelId = Convert.ToInt32(rdr["HotelId"]);
					innstantHotel.HotelName = rdr["HotelName"].ToString();

					innstantHotelsList.Add(innstantHotel);
				}
				connection.Close();
			}

			return innstantHotelsList;
		}
	}
}
