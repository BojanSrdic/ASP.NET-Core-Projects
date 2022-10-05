using Innstant.DataAccess;
using Innstant.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using static Innstant.InnstantAPI.JsonDeserializedInnstantRequestBody;

namespace Innstant.InnstantAPI
{
    public interface IInnstantService
	{
        List<int> HitInnstantAPI();

    }

    public class InnstantService : IInnstantService
    {
        private readonly IInnstantDataAccessLayer _innstantDataAccessLayer;
        public InnstantService(IInnstantDataAccessLayer innstantDataAccessLayer)
		{
            _innstantDataAccessLayer = innstantDataAccessLayer;
        }

        //private static readonly string searchApiUrl = ConfigurationManager.AppSettings["InnstantSearchAPIUrl"];
        //private static readonly string poolApiUrl = ConfigurationManager.AppSettings["InnstantPoolAPIUrl"];

        private static readonly string searchApiUrl = "https://api-search.mishor5.innstant-servers.com/hotels/search";
        private static readonly string poolApiUrl = "https://api-search.mishor5.innstant-servers.com/hotels/poll";

        public List<int> HitInnstantAPI()
		{
            var innstantHotelIdList = new List<int>();
            var innstantHotels = _innstantDataAccessLayer.GetIsraelInnstantHotels();

            foreach (InnstantHotels item in innstantHotels)
			{
                innstantHotelIdList.Add(item.HotelId);
            }

            var innstantRequestBody = InnstantRequestBody(innstantHotelIdList);

            // Get hotels from Innstant - Search
            InnstantSearchPoolRequest(innstantRequestBody, searchApiUrl);

            // Get hotels from Innstant - Poll
            var returnPoll = InnstantSearchPoolRequest(innstantRequestBody, poolApiUrl);


            //GetHotelsFromInnstant(innstantRequest, searchApiUrl);
            //var returnPoll = GetHotelsFromInnstant(innstantRequest, poolApiUrl);
            //var response = BLHelper.DataConvertorHelper.DeserializeJson<InnstantSearchResponse>(returnPoll);

            //return GetInstantDomesticHotelsResults(response);
            return null;

		}

		private InnstantRequestBody InnstantRequestBody(List<int> hotelIdList)
		{
			var innstantRequestBody = new InnstantRequestBody();

			innstantRequestBody.currencies = new List<string> { "EUR" };    // moze i new string[] { "EUR" };
			innstantRequestBody.customerCountry = "IL";
			innstantRequestBody.customFields = new List<object>();
			innstantRequestBody.dates = new Dates("2022-10-25", "2022-10-26");
			//innstantRequestBody.destinations = new List<Destination> { new Destination { id = 12795, type = "hotel" }, new Destination { id = 12795, type = "hotel" } };

            var destinationList = new List<Destination>();
            foreach (int i in hotelIdList)
			{
                destinationList.Add(new Destination
                {
                    id = i,
                    type = "hotel"
                });			
            }

            innstantRequestBody.destinations = destinationList;
            innstantRequestBody.filters = new List<object>();

            var childrenList = new List<object> { 3, 6 };
            int numberOfAdults = 2;

            innstantRequestBody.pax = new List<Pax> { new Pax(numberOfAdults, childrenList) };
            
			innstantRequestBody.timeout = 19000;
			innstantRequestBody.service = "hotels";
			
			return innstantRequestBody;
		}

        private static string InnstantSearchPoolRequest(InnstantRequestBody body, string apiUrl)
        {
            string jsonRequest = JsonSerializer.Serialize(body);
            //var jsonRequest = Serializer.SerializeToJson<InnstantRequestBody>(body);

            var httpSearchWebReq = (HttpWebRequest)WebRequest.Create(apiUrl);
            byte[] byteData = Encoding.UTF8.GetBytes(jsonRequest);

            httpSearchWebReq.Method = "POST";
            httpSearchWebReq.ContentType = "application/json";
            httpSearchWebReq.Timeout = 19000;
            httpSearchWebReq.ContentLength = byteData.Length;
            //httpSearchWebReq.Headers.Add(ConfigurationManager.AppSettings["AetherAppKey"], ConfigurationManager.AppSettings["AetherAppKeyValue"]);
            //httpSearchWebReq.Headers.Add(ConfigurationManager.AppSettings["AetherAccessTokenKey"], ConfigurationManager.AppSettings["AetherAccessTokenValue"]);
            httpSearchWebReq.Headers.Add("aether-application-key", "$2y$10$PQhR0V5xRTDAkV3T1Dbw0eimsI504qge6NKSlyQDx1Dm.ZewF1MzW");
            httpSearchWebReq.Headers.Add("aether-access-token", "$2y$10$pEBTSXZG6lNM6zoRVLmMUefMb6/7CB3t8N81sU0mPqET2XG0V6fLG");

            using (Stream stream = httpSearchWebReq.GetRequestStream())
            {
                stream.Write(byteData, 0, byteData.Length);
            }

            string responseString = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)httpSearchWebReq.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseString = streamReader.ReadToEnd();
                }
            }

            return responseString;
        }
    }
}
