using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Stock.Services.Api
{
    public class ApiRequest
    {
        private HttpClient _client;

        string api_key = "cebcd21a80313d6b192920e4594216c4";

        private string base_url = "https://marketdata.websol.barchart.com/getHistory.json?apikey=";


        public ApiRequest()
        {
            _client = new HttpClient();
            
        }

        public async Task<object> SendRequest(string startDate, string symbol)
        {

            HttpClient hTTPClient = new HttpClient();

            Debug.WriteLine(base_url + api_key + "&symbol=" + symbol + "&type=daily&startDate=" + startDate);

            var response = await hTTPClient.GetAsync(new Uri(base_url + api_key + "&symbol=" + symbol + "&type=daily&startDate=" + startDate));

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var teste = JsonConvert.DeserializeObject(content);

                Debug.WriteLine(teste);
                //Debug.WriteLine(content);


                return teste;
            }
            else
            {
                return null;
            }

        }
    }
}
