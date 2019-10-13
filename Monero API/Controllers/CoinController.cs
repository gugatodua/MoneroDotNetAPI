using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Monero_API.Controllers
{
    public class CoinController : Controller
    {

        public CoinController()
        {
        }

        [Route("coin/stats")]
        public IActionResult CoinStats()
        {
            var webclient = new WebClient();
            var json = webclient.DownloadString("http://moneroblocks.info/api/get_stats/");

            return Json(json);
        }

        
        [Route("coin/blockheaderbyheight/{height}")]
        public IActionResult BlockHeaderByHeight(string height)
        {
            var webclient = new WebClient();
            var requestString = String.Concat("http://moneroblocks.info/api/get_block_header/", height);
            var json = webclient.DownloadString(requestString);

            return Json(json);
        }

        [Route("coin/blockheaderbyhash/{hash}")]
        public IActionResult BlockHeaderByHash(string hash)
        {
            var webclient = new WebClient();
            var requestString = String.Concat("http://moneroblocks.info/api/get_block_header/", hash);
            var json = webclient.DownloadString(requestString);

            return Json(json);
        }

        [Route("coin/blockdatabyheight/{height}")]
        public IActionResult BlockDataByHeight(string height)
        {
            var webclient = new WebClient();
            var requestString = String.Concat("http://moneroblocks.info/api/get_block_data/", height);
            var json = webclient.DownloadString(requestString);

            return Json(json);
        }

        [Route("coin/blockdatabyhash/{hash}")]
        public IActionResult BlockDataByHash(string hash)
        {
            var webclient = new WebClient();
            var requestString = String.Concat("http://moneroblocks.info/api/get_block_data/", hash);
            var json = webclient.DownloadString(requestString);

            return Json(json);
        }


    }
}