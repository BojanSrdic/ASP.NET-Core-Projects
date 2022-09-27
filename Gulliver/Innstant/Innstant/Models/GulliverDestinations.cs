using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.Models
{
	public class GulliverDestinations
	{
		public int GulliverDestinationId { get; set; }
		public string GulliverDestinationName { get; set; }
		public int InnstantDestinationId { get; set; }
		public string InnstantDestinationName { get; set; }
		public string Type { get; set; }
	}
}
