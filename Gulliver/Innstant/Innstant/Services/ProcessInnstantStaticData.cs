using Innstant.Models;
using System.Collections.Generic;

namespace Innstant.Services
{
	public class ProcessInnstantStaticData
	{
        public static List<InnstantDestinations> ProcessInnstantDestinations()
        {
            // Read data from Innstamt
            var innstantDestinationsData = InnstantStaticDataReader.InnstantDestinationsParser(); 
            var innstantHotelsData = InnstantStaticDataReader.InnstantDestinationsParser();
            var innstantDestinationHotelsData = InnstantStaticDataReader.InnstantDestinationsParser();

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
