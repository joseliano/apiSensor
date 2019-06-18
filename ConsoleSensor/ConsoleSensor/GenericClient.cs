using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Script.Serialization;

namespace ConsoleSensor
{
    public class GenericClient<T> where T : class, new()
    {
        //private static readonly string UrlApi = ConfigurationManager.AppSettings["URLApiServer"];
        /*rotina que envia dados aleatórios para webservice*/     
        public String _Cadastrar(T item)
        {
            string UrlApi = ConfigurationManager.AppSettings["URLApiServer"];
            String NomeClass = item.GetType().Name;

            WebAPIClient client = new WebAPIClient(UrlApi);
            String Url = String.Format("{0}/{1}/{2}", UrlApi, NomeClass, "AddSensor");
            var response = client.HttpClient.PostAsync(Url, new StringContent(new JavaScriptSerializer()
                                                              .Serialize(item),
                                                              Encoding.UTF8, "application/json"
                                                              )
                                      ).Result;
           
            var result = client.Deserialize<T>(response);

            return "";

        } 

    }

  
    public class WebAPIClient
    {
        public WebAPIClient(string baseAddress)
        {
            BaseAddress = baseAddress;
            HttpClient = GetHttpClient();
        }

        private static string BaseAddress { get; set; }

        public HttpClient HttpClient { get; private set; }


        public static HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public T Deserialize<T>(HttpResponseMessage response) where T : class
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();

            var result = response.Content;
            var content = result.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content.Result);
        }
        public StringContent Serialize<T>(T input)
        {
            var folderAsJosn = JsonConvert.SerializeObject(input);
            return new StringContent(folderAsJosn, Encoding.UTF8, "application/json");
        }
    }
}
