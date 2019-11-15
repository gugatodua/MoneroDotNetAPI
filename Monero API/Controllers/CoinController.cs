﻿using System;
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

namespace Monero_API.Controllers
{

    public class CoinController : Controller
    {

        public CoinController() { }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        //getblock უნდა გადავაკეთო getTxHashesFromBlock-ად. ჰეშები მინდა მარტო
        [Route("coin/getb/{height}")]
        public string[] GetTransactionHashFromBlock(string height)
        {
            var str = "{\"jsonrpc\":\"2.0\",\"id\":\"0\",\"method\":\"get_block\",\"params\":{\"height\":" + height + "}";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:18081/json_rpc");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = str;
                streamWriter.Write(json);
            }
            string result;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            var x = result.ToString();

            //აქ ვიღებ ბლოკის მთლიან დატას
            string block = x.Replace("\\n", string.Empty).Replace("\\", "");

            //შემიძლია ეს ცალკე მეთოდად გავიტანო
            String s1 = block.Substring(block.IndexOf("\"tx_hashes\":") + 12);
            string s2 = s1.Trim().Substring(s1.IndexOf("\"tx_hashes\":"));
            string s3 = s2.Remove(0, 13);
            string final = "";

            foreach (var f in s3)
            {
                if (f == ']')
                {
                    final = s3.Remove(s3.IndexOf(f));
                }
            }

            string[] tx_hashes = final.Split(',');
            foreach (var s in tx_hashes)
            {
                System.Console.WriteLine(s);
            }

            return tx_hashes;
        }

        [Route("coin/gettx/{tx_hash}")]
        //ეს უნდა აბრუნებდეს txpubkey-ს და output-ს ანუ extra და vout/target/key
        public string GetTransactionDetails(string tx_hash)
        {
            //get_transactions
            var tx_str = "{\"txs_hashes\":[\"" + tx_hash + "\"],\"decode_as_json\":true}";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:18081/get_transactions");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = tx_str;
                streamWriter.Write(json);
            }
            string result;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            var x = result.ToString();

            var jsonstr = x.IndexOf("\"txs_as_json\":");
            //s2 - სრული JSON სტრინგი
            string s2 = x.Trim().Substring(jsonstr);
            s2 = s2.Remove(0, 16);
            s2 = s2.Replace("\\n", "");
            s2 = s2.Replace("\\", "");

            s2 = s2.Remove(0, 1);
            System.Console.WriteLine(s2);

            // if(s2.Contains("\"untrusted\"")){
            //     var ind = s2.IndexOf("\"untrusted\"");
            //     s2 = s2.Remove(ind);
            //     s2= s2.Remove(s2.Length-9);
            //     //s2=s2.Insert(s2.Length,"}");
            // }

            // // RootObject tr = JsonConvert.DeserializeObject<RootObject> (s2);
            // // System.Console.WriteLine(tr);
            // // System.Console.WriteLine("VIN : ",tr.vin);
            // // System.Console.WriteLine ("EXTRA:",tr.extra);

            int lastindex = 0;
            while (true)
            {
                var indexofextra = s2.IndexOf("\"extra", lastindex + 1);
                System.Console.WriteLine("INDEX OF EXTRA IS : {0}",indexofextra);
                if (indexofextra == -1)
                    break;

                lastindex = indexofextra;

                var indexOfStartExtra = s2.IndexOf("[", lastindex + 1);
                if (indexOfStartExtra == -1)
                    break;

                var indexOfEndExtra = s2.IndexOf("]", lastindex + 1);
                if (indexOfEndExtra == -1)
                    break;

                var dataOfExtra = s2.Substring(indexOfStartExtra, (indexOfEndExtra - indexOfStartExtra) + 1);
                dataOfExtra = dataOfExtra.Replace("\n", "");

                var extraData = JsonConvert.DeserializeObject<List<int>>(dataOfExtra);
                foreach(var item in extraData){
                    System.Console.WriteLine("POSSIBLE OUTPUT : {0}", item);

                }
            }

            return s2;

        }

        [Route("coin/checktx")]
        public string CheckTransaction()
        {

            //საბოლოოდ აქ ვინახავ ტრანზაქციებს
            string[] myTX;
            //ესენი ორობითში უნდა გადავიყვანო
            var a = "ec68d01a7a41165a967344786a239c3f57eaca93818787e0c127da466e14a604"; //private view key
            var b = "21af15b0478be723d6323f5cae179b1d6225f6ad79f8a07bd66163502eead20f"; //private spend key
            var B = "8d02abba1e6759405169e081fd80ec7c1903df455bfc6c3129c187347e1c83f3"; //public spend key
            var A = "cf1f1e621dd5679e7d219e906f30c894347472575d05b2945177bda0002393ab"; //public view key

            string[] G = { "0x58", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66", "0x66" };

            var tx_hash_from_block = GetTransactionHashFromBlock("height_goes_here");
            var tx = GetTransactionDetails("hash_goes_here");
            foreach (var tx_hash in tx_hash_from_block)
            {
                var R = ""; //აქ მეთოდი უნდა მიენიჭოს. მეთოდი რომელიც public key-ს დააბრუნებს 
                foreach (var i in tx)
                {
                    // i is output index you are checking
                    //output is something you compare to see if it's yours
                    //if( is_mine(output, R, i) ) { myTX.append(tx,output)} 
                }
            }
            return "OK";
        }

    }
}