using Innstant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.Services
{
	public class GulliverService
	{
        #region Main Service Functionalities
        public static void CreateGulliverDestinationTable()
        {
            // Process Innstant Data
            var innstantIzraelDestinations = ProcessInnstantStaticData.ProcessInnstantDestinations();

            // Create result tables
            var gulliverMappingDbTable = GulliverDestinationMappingTable(innstantIzraelDestinations);

            // Save data to the SQL table
            DataAccessLayer.SaveDataIntoDatabase(gulliverMappingDbTable);
        }

        public static void CreateHotelTable()
		{
            // To Do: ...
		}

        #endregion

        // Mapp Gulliver destinations and Innstant destinations
        public static List<GulliverDestinations> GulliverDestinationMappingTable(List<InnstantDestinations> innstantIzraelDestinations)
        {
            var gulliverIsraelDestinationTable = new List<GulliverDestinations>();

            foreach (InnstantDestinations destination in innstantIzraelDestinations)
            {
                gulliverIsraelDestinationTable.Add(new GulliverDestinations()
                {
                    InnstantDestinationId = destination.DestinationId,
                    InnstantDestinationName = destination.DestinationName,
                    Type = destination.Type
                });
            }

            return gulliverIsraelDestinationTable;
        }
	}
}
