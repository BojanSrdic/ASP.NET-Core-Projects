using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.Services.InnstantAPI
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

        public Pax(int adults, List<object> children)
        {
            this.adults = adults;
            this.children = children;
        }
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
