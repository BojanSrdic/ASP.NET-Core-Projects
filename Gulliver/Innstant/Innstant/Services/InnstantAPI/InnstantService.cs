using Innstant.DataAccess;
using Innstant.Models;
using Innstant.Services;
using Innstant.Services.InnstantAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Innstant.InnstantAPI
{
    public interface IInnstantService
	{
        void HitInnstantAPI();
    }

    public class InnstantService : IInnstantService
    {
        private readonly IInnstantDataAccessLayer _innstantDataAccessLayer;
        private readonly ISaveInnstantStaticData _saveInnstantStaticData;
        public InnstantService(IInnstantDataAccessLayer innstantDataAccessLayer, ISaveInnstantStaticData saveInnstantStaticData)
		{
            _innstantDataAccessLayer = innstantDataAccessLayer;
            _saveInnstantStaticData = saveInnstantStaticData;
        }

        //private static readonly string searchApiUrl = ConfigurationManager.AppSettings["InnstantSearchAPIUrl"];
        //private static readonly string poolApiUrl = ConfigurationManager.AppSettings["InnstantPoolAPIUrl"];

        private static readonly string searchApiUrl = "https://api-search.mishor5.innstant-servers.com/hotels/search";
        private static readonly string poolApiUrl = "https://api-search.mishor5.innstant-servers.com/hotels/poll";

        public void HitInnstantAPI()
		{
            var allInnstantHotels = GetAllIsraelInnstantHotels();
            var innstantHotelIdList = Separator(allInnstantHotels);

            var rooms = new List<InnstantRooms>();

            foreach (List<int> requestBodyInnstantHotelIdList in innstantHotelIdList) 
            {
                var innstantRequestBody = InnstantRequestBody(requestBodyInnstantHotelIdList);

                // Get hotels from Innstant - Search
                InnstantSearchPoolRequest(innstantRequestBody, searchApiUrl);

                // Get hotels from Innstant - Poll
                var returnPoll = InnstantSearchPoolRequest(innstantRequestBody, poolApiUrl).ToString();
                var response = JsonSerializer.Deserialize<InnstantResponse>(returnPoll);
                
                foreach (Result result in response.results)
				{
                    rooms.Add(new InnstantRooms { 
                        HotelId = Convert.ToInt32(result.items[0].hotelId),
                        RoomName = result.items[0].name,
                        RoomCategory = result.items[0].category,
                        Bedding = result.items[0].bedding
                    });

                    // Check the board task: GLV-12049
                    List<string> boards = new List<string> { "RO", "BB", "HB", "FB", "AL" };

                    if (!boards.Contains(result.items[0].board))
                    {
                        boards.Add(result.items[0].board);
                    }
                }
            }

            _saveInnstantStaticData.SaveInnstantRooms(rooms);


        }

        // Rate limit is set to 250 hotelIds per request and we have 2004 i guess
        private List<int> GetAllIsraelInnstantHotels()
		{
            var innstantHotelIdList = new List<int>();
            var innstantHotels = _innstantDataAccessLayer.GetIsraelInnstantHotels();

            foreach (InnstantHotels item in innstantHotels)
            {
                innstantHotelIdList.Add(item.HotelId);
            }

            return innstantHotelIdList;
		}

        private List<List<int>> Separator(List<int> innstantHotelIdList)
		{
            var innstantHotelIdRateLimit = new List<List<int>>();

            int count = innstantHotelIdList.Count();

            for(int i = 0; i >= 0 && i <= count; i = i + 250)
			{
                List<int> element = innstantHotelIdList.Skip(i).Take(250).ToList();
                innstantHotelIdRateLimit.Add(element);
            }

            return innstantHotelIdRateLimit;
        }

        private InnstantRequestBody InnstantRequestBody(List<int> hotelIdList)
		{
			var innstantRequestBody = new InnstantRequestBody();

			innstantRequestBody.currencies = new List<string> { "EUR" };
			innstantRequestBody.customerCountry = "IL";
			innstantRequestBody.customFields = new List<object>();
			innstantRequestBody.dates = new Dates("2022-10-25", "2022-10-26");

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

            var childrenList = new List<object>();
            int numberOfAdults = 2;

            innstantRequestBody.pax = new List<Pax> { new Pax(numberOfAdults, childrenList) };
            
			innstantRequestBody.timeout = 19000;
			innstantRequestBody.service = "hotels";
			
			return innstantRequestBody;
		}

        private static string InnstantSearchPoolRequest(InnstantRequestBody body, string apiUrl)
        {
            string jsonRequest = JsonSerializer.Serialize(body);

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
