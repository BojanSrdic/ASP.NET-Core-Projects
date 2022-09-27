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
	public static class InnstantStaticDataReader
	{
        public static List<InnstantDestinations> InnstantDestinationsParser()
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
                        Id = Convert.ToInt32(prop[0]),
                        Name = prop[1],
                        CountryId = prop[5],
                        Contains = prop[9]
                    });
                }
            }

            #endregion

            return innstantIsraelDestionatins;
        }
    }
}
