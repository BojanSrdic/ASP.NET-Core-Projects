using Innstant.DataAccess;
using Innstant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.Services
{
    public interface IGulliverService
	{
        void CreateGulliverDestinationTable();
        List<GulliverDestinations> GulliverDestinationMappingTable(List<InnstantDestinations> innstantIzraelDestinations);
    }


    public class GulliverService : IGulliverService
    {
        private readonly IProcessInnstantStaticData _innstantStaticData;
        private readonly ISaveInnstantStaticData _saveInnstantStaticData;
        private readonly IInnstantStaticDataReader _innstantStaticDataReader;
		private readonly IInnstantDataAccessLayer _innstantDataAccessLayer;

		public GulliverService(IProcessInnstantStaticData innstantStaticData, ISaveInnstantStaticData saveInnstantStaticData, IInnstantStaticDataReader innstantStaticDataReader,
            IInnstantDataAccessLayer innstantDataAccessLayer)
		{
            _innstantStaticData = innstantStaticData;
            _saveInnstantStaticData = saveInnstantStaticData;
            _innstantStaticDataReader = innstantStaticDataReader;
            _innstantDataAccessLayer = innstantDataAccessLayer;

        }

        #region Main Service Functionalities

        public void CreateGulliverDestinationTable()
        {
            // Process Innstant Data
            var innstantIzraelDestinations = _innstantStaticData.ProcessInnstantDestinations();

            // Create result tables
            var gulliverMappingDbTable = GulliverDestinationMappingTable(innstantIzraelDestinations);

            // Save data to the SQL table
            //_saveInnstantStaticData.SaveInnstantDestinationsMappingTable(gulliverMappingDbTable);
        }

       /* public void CreateGulliverHotelTable()
		{
            // Read all destinations from israel "IL"
            var innstantIsraelsDestinationList = _innstantDataAccessLayer.GetInnstantDestinations();

            // Create mapping table between Israel Hotels and Destinations
            var israelHotelDestinationMappingTable = _innstantStaticDataReader.InnstantIsraelHotelDestinationParser(innstantIsraelsDestinationList);

            // Extract Israel Hotel Id from Hotels_Destinations mapping table
            var innstantIsraelHotelIdList = new List<int>();

            foreach (InnstantHotelDestination row in israelHotelDestinationMappingTable)
            {
                innstantIsraelHotelIdList.Add(row.HotelId);
            }

            // Read all Hotels
            _innstantStaticDataReader.InnstantIsraelHotelsParser(innstantIsraelHotelIdList);

            // Save Hotels to the database
        }*/

        #endregion

        // Mapp Gulliver destinations and Innstant destinations
        public List<GulliverDestinations> GulliverDestinationMappingTable(List<InnstantDestinations> innstantIzraelDestinations)
        {
            var gulliverIsraelDestinationTable = new List<GulliverDestinations>();

            foreach (InnstantDestinations destination in innstantIzraelDestinations)
            {
                gulliverIsraelDestinationTable.Add(new GulliverDestinations()
                {
                    InnstantDestinationId = destination.DestinationId,
                    InnstantDestinationName = destination.DestinationName,
                    Type = destination.DestinationType
                });
            }

            return gulliverIsraelDestinationTable;
        }
	}
}
