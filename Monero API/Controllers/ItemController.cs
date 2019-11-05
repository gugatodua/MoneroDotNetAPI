using AustinHarris.JsonRpc;
using Microsoft.AspNetCore.Mvc;

namespace Monero_API.Controllers {
    public class ItemController : JsonRpcService {
        [Route("item/index")]
        [JsonRpcMethod]
        [HttpPost]
        public string Index (string message) {
            return "Done";
        }

    }
}