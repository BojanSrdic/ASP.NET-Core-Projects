using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.Models
{
	public class InnstantDestinations
	{
		public int DestinationId { get; set; }
		public string DestinationName { get; set; }
		public string DestinationType { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public string CountryId { get; set; }
		public int Searchable { get; set; }
		public string Seoname { get; set; }
		public string State { get; set; }
		public string Contains { get; set; }
	}
}