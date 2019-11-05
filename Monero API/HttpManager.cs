using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Monero_API {
    public class HttpManager {
        public static HttpClient httpClient = new HttpClient ();

        public static HttpResponseMessage PostValuesAsJson (string url, Dictionary<string, string> values) {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject (values);
            StringContent content = new StringContent (json, Encoding.UTF8, "application/json");
            HttpResponseMessage result = HttpManager.httpClient.PostAsync (url, content).Result;
            return result;
        }

    }
}