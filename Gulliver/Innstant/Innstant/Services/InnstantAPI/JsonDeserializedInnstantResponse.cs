using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.Services.InnstantAPI
{
    // InnstantResponse myDeserializedClass = JsonConvert.DeserializeObject<InnstantResponse>(myJsonResponse);
    public class BarRate
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Cancellation
    {
        public string type { get; set; }
        public List<Frame> frames { get; set; }
    }

    public class Frame
    {
        public string from { get; set; }
        public string to { get; set; }
        public Penalty penalty { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public string category { get; set; }
        public string bedding { get; set; }
        public string board { get; set; }
        public string hotelId { get; set; }
        public PaxResponse paxResponse { get; set; }
        public Quantity quantity { get; set; }
        public bool detailsAvailable { get; set; }
    }

    public class NetPrice
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class PaxResponse
    {
        public int adults { get; set; }
        public List<object> children { get; set; }
    }

    public class Penalty
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Price
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Provider
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Quantity
    {
        public int min { get; set; }
        public int max { get; set; }
    }

    public class Result
    {
        public Price price { get; set; }
        public NetPrice netPrice { get; set; }
        public BarRate barRate { get; set; }
        public string confirmation { get; set; }
        public string paymentType { get; set; }
        public bool packageRate { get; set; }
        public bool commissionable { get; set; }
        public List<Provider> providers { get; set; }
        public List<object> specialOffers { get; set; }
        public List<Item> items { get; set; }
        public Cancellation cancellation { get; set; }
        public string code { get; set; }
    }

    public class InnstantResponse
    {
        public int timestamp { get; set; }
        public string requestTime { get; set; }
        public string status { get; set; }
        public int completed { get; set; }
        public string sessionId { get; set; }
        public int processTime { get; set; }
        public string expireAt { get; set; }
        public List<Result> results { get; set; }
    }
}
