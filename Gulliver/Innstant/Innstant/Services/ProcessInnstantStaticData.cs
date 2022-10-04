using Innstant.Models;
using System.Collections.Generic;

namespace Innstant.Services
{
	public class ProcessInnstantStaticData
	{
        private readonly InnstantStaticDataReader _innstantStaticDataReader;

        public ProcessInnstantStaticData(InnstantStaticDataReader innstantStaticDataReader)
		{
            _innstantStaticDataReader = innstantStaticDataReader;
        }

        public List<InnstantDestinations> ProcessInnstantDestinations()
        {
            // Read data from Innstamt
            var innstantDestinationsData = _innstantStaticDataReader.InnstantDestinationsParser(); 
            //var innstantHotelsData = _innstantStaticDataReader.InnstantDestinationsParser();
            //var innstantDestinationHotelsData = _innstantStaticDataReader.InnstantDestinationsParser();

            #region Filter all Israel destinations by area or city

            var innstantIzraelDestinations = new List<InnstantDestinations>();

            foreach (InnstantDestinations destination in innstantDestinationsData)
            {
                #region Way one is to create new list!
                if (!string.IsNullOrEmpty(destination.Contains))
                {
                    innstantIzraelDestinations.Add(destination);

                    // Set Type property of Destinaton class
                    destination.Type = destination.Contains.Contains(";") ? "Area" : "City";
                }
                #endregion

                #region Way two is to remove elements from existing list!
                /*
                if (string.IsNullOrEmpty(destination.Contains))
				{
                    innstantDestinationsData.Remove(destination);
				}

                // Set Type property of Destinaton class
                destination.Type = destination.Contains.Contains(";") ? "Area" : "City";
                */
                #endregion
            }

            #endregion

            return innstantIzraelDestinations;
        }
    }
}
