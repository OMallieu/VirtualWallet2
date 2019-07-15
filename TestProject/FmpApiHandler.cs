using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VirtualWallet.DataObjects;
using VirtualWallet.JsonObjects;

namespace VirtualWallet
{
    /// <summary>
    /// Handles everything related to the financialmodelingprep.com api, in order to load stock prices hystory into DB.
    ///
    /// </summary>
    // I am aware that my object model is currently too heavily coupled with the api responses. 
    // I need a new set of models for deserializing the json responses of the api, and a bunch of converters to my own models.
    // So some of these methods need to be moved elsewhere (like LoadAllCompaniesInDB who doesn't belong here) and/or reworked.
    static class FmpApiHandler
    {
       private static JsonCompanyInfo [] GetSymbolsArray()
        {
            string symbolListRequest = "https://financialmodelingprep.com/api/v3/company/stock/list";
            JsonSymbolsList myJsonSymbolList = (JsonSymbolsList)ReadJson<JsonSymbolsList>(ExecuteRestRequest(symbolListRequest));
            return myJsonSymbolList.symbolsList.ToArray();
        } 
        public static void LoadAllCompaniesInDB(int pstart, int pbatchSize)
        {
            var companyInfos = GetSymbolsArray();

            int firstOfCurrentBatch = pstart;
            List<Company> companies = new List<Company>();
            while (firstOfCurrentBatch<=companyInfos.Count()-pbatchSize)
            {
                companies = GetNextCompaniesHistory(companyInfos.Skip(firstOfCurrentBatch).ToArray(), pbatchSize);
                Repo.LoadCompaniesHystoryInDB(companies);
                Console.WriteLine("Companies from "+firstOfCurrentBatch+" to "+(firstOfCurrentBatch+pbatchSize)+" succesfully loaded into DB");
                firstOfCurrentBatch += pbatchSize;
            }
            companies = GetNextCompaniesHistory(companyInfos.Skip(firstOfCurrentBatch).ToArray(), companyInfos.Count()-firstOfCurrentBatch);
            Repo.LoadCompaniesHystoryInDB(companies);
        }
       private static List<Company> GetNextCompaniesHistory(JsonCompanyInfo[] pcompInfos, int pbatchSize)
        {
            List<Company> companies = new List<Company>();
            //Stopwatch watch1 = new Stopwatch();
            string fullHistoryRequest = "https://financialmodelingprep.com/api/v3/historical-price-full/";
            foreach (var compInfo in pcompInfos.Take(pbatchSize))
            {
                //watch1.Restart();
                Company myCompany = GetCompanyHistory(fullHistoryRequest + compInfo.symbol);
                //Console.WriteLine("time for restRequest and jsonparser : "+watch1.Elapsed);
                companies.Add(myCompany);
            }
            return companies;
        }
        private static Company GetCompanyHistory(string prestRequest)
        {
            return (Company)ReadJson<Company>(ExecuteRestRequest(prestRequest));
        }
        private static Object ReadJson<Object>(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var reader = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(reader))
                {
                    try
                    {
                        return serializer.Deserialize<Object>(jsonReader);
                    }
                    catch (Exception)
                    {
                        return default(Object);
                    }
                }
            }

        }
        private static Stream ExecuteRestRequest(string prestRequest)
        {
            var response = ExecuteRestRequestAsync(prestRequest);
            var myResult = response.Result.Content.ReadAsStreamAsync();
            return myResult.Result;
        }
        private static async Task<HttpResponseMessage> ExecuteRestRequestAsync(string prestRequest)
        {
            using (var httpClient = new HttpClient())

            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), prestRequest))

                {
                    var response = await httpClient.SendAsync(request);
                    return response;
                }
            }
        }
        public static void WriteRestResultToTxt(string prestRequest, string pfileName)
        {
            WriteStreamToTxt(ExecuteRestRequest(prestRequest), pfileName);
        }
        private static void WriteStreamToTxt(Stream pInput, string pfileName)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\programmation\c#\VirtualWallet\"+pfileName+".txt"))
            {
                string sLine = "";
                using (StreamReader sr = new StreamReader(pInput))
                {
                    while (sLine != null)
                    {

                        sLine = sr.ReadLine();
                        sw.WriteLine(sLine);

                    }
                }

            }
        }
    }
}
