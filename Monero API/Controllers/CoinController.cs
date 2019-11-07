using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Monero_API.Controllers {

    public class CoinController : Controller {

        public CoinController () { }

        public static string Reverse (string s) {
            char[] charArray = s.ToCharArray ();
            Array.Reverse (charArray);
            return new string (charArray);
        }
        //getblock უნდა გადავაკეთო getTxHashesFromBlock-ად. ჰეშები მინდა მარტო
        [Route ("coin/getb/{height}")]
        public string[] GetTransactionHashFromBlock (string height) {
            var str = "{\"jsonrpc\":\"2.0\",\"id\":\"0\",\"method\":\"get_block\",\"params\":{\"height\":" + height + "}";

            var httpWebRequest = (HttpWebRequest) WebRequest.Create ("http://127.0.0.1:18081/json_rpc");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter (httpWebRequest.GetRequestStream ())) {
                string json = str;
                streamWriter.Write (json);
            }
            string result;
            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse ();
            using (var streamReader = new StreamReader (httpResponse.GetResponseStream ())) {
                result = streamReader.ReadToEnd ();
            }

            var x = result.ToString ();

            //აქ ვიღებ ბლოკის მთლიან დატას
            string block = x.Replace ("\\n", string.Empty).Replace ("\\", "");

            //შემიძლია ეს ცალკე მეთოდად გავიტანო
            String s1 = block.Substring (block.IndexOf ("\"tx_hashes\":") + 12);
            string s2 = s1.Trim ().Substring (s1.IndexOf ("\"tx_hashes\":"));
            string s3 = s2.Remove (0, 13);
            string final = "";

            foreach (var f in s3) {
                if (f == ']') {
                    final = s3.Remove (s3.IndexOf (f));
                }
            }

            string[] tx_hashes = final.Split (',');
            foreach (var s in tx_hashes) {
                System.Console.WriteLine (s);
            }

            return tx_hashes;
        }

        [Route ("coin/gettx/{tx_hash}")]
        //ეს უნდა აბრუნებდეს txpubkey-ს და output-ს
        public string GetTransactionDetails (string tx_hash) {
            //get_transactions
            var tx_str = "{\"txs_hashes\":[\"" + tx_hash + "\"],\"decode_as_json\":true}";

            var httpWebRequest = (HttpWebRequest) WebRequest.Create ("http://127.0.0.1:18081/get_transactions");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter (httpWebRequest.GetRequestStream ())) {
                string json = tx_str;
                streamWriter.Write (json);
            }
            string result;
            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse ();
            using (var streamReader = new StreamReader (httpResponse.GetResponseStream ())) {
                result = streamReader.ReadToEnd ();
            }

            var x = result.ToString ();

            var jsonstr = x.IndexOf ("\"txs_as_json\":");
            //s2 - სრული JSON სტრინგი
            string s2 = x.Trim ().Substring (jsonstr);
            s2 = s2.Remove (0, 16);
            s2 = s2.Replace ("\\n", "");
            s2 = s2.Replace ("\\", "");

            s2 = s2.Remove (0, 1);
            System.Console.WriteLine(s2);
            // if(s2.Contains("\"untrusted\"")){
            //     var ind = s2.IndexOf("\"untrusted\"");
            //     s2 = s2.Remove(ind);
            //     s2= s2.Remove(s2.Length-7);
            // }

            // RootObject tr = JsonConvert.DeserializeObject<RootObject> (s2);
            // System.Console.WriteLine(tr);
            // System.Console.WriteLine("VIN : ",tr.vin);
            // System.Console.WriteLine ("EXTRA:",tr.extra);

            return x;

        }

        [Route ("coin/checktx")]
        public string CheckTransaction () {

            //საბოლოოდ აქ ვინახავ ტრანზაქციებს
            string[] myTX;
            //ესენი ორობითში უნდა გადავიყვანო
            var a = "6cb502c74cd7d93e441b5c04f482613e8c9623d79282ab22bacdce831cb02a05"; //private view key
            var b = "117d466a6a3c67507d0913c37aab73c7481870beadef6135d9ae2c9b330cb603"; //private spend key
            var B = "16b94297c334518487b7709c5c446808d446707a5608e68a3cf93b3589a2638e"; //public spend key
            var A = "e93bf43c6eac1d9796e32bf8eb01c0a7b3112a5c5d1c27b550a30aa5aed5a779"; //public view key

            string[] G = { "0x58", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66" };

            var tx_hash_from_block = GetTransactionHashFromBlock ("height_goes_here");
            var tx = GetTransactionDetails ("hash_goes_here");
            foreach (var tx_hash in tx_hash_from_block) {
                var R = ""; //აქ მეთოდი უნდა მიენიჭოს. მეთოდი რომელიც public key-ს დააბრუნებს 
                foreach (var i in tx) {
                    // i is output index you are checking
                    //output is something you compare to see if it's yours
                    //if( is_mine(output, R, i) ) { myTX.append(tx,output)} 
                }
            }
            return "OK";
        }

    }
}