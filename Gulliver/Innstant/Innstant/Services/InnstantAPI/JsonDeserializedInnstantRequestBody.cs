using System.Collections.Generic;

namespace Innstant.InnstantAPI
{
	public class JsonDeserializedInnstantRequestBody
	{
        // InnstantRequestBody myDeserializedClass = JsonConvert.DeserializeObject<InnstantRequestBody>(myJsonResponse);
        public class Dates
        {
            public string from { get; set; }
            public string to { get; set; }

			public Dates(string from, string to)
			{
                this.from = from;
                this.to = to;
			}
        }

        public class Destination
        {
            public int id { get; set; }
            public string type { get; set; }
        }

        public class Pax
        {
            public int adults { get; set; }
            public List<object> children { get; set; }
        }

        public class InnstantRequestBody
        {
            public List<string> currencies { get; set; }
            public string customerCountry { get; set; }
            public List<object> customFields { get; set; }
            public Dates dates { get; set; }
            public List<Destination> destinations { get; set; }
            public List<object> filters { get; set; }
            public List<Pax> pax { get; set; }
            public int timeout { get; set; }
            public string service { get; set; }
        }
    }
}
