using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ArtApp.Services.Interfaces;
using Newtonsoft.Json;

namespace ArtApp.Services
{
    /*
        * The T, identifies the class i want to work with, such as, work, condition report, etc;
        * The TResourceIdentifier, identifies the specific resource i want to acces, such as, Id or username
        */
    public class RestApiService<T, TResourceIdentifier> : IDisposable, IRestApiService<T, TResourceIdentifier> where T : class
    {
        private bool disposed = false;
        private HttpClient httpClient;
        protected readonly string serviceBaseAddress;
        private readonly string addressSuffix;
        private readonly string jsonMediaType = "application/json";


        public RestApiService(string serviceBaseAddress, string addressSuffix)
        {
            this.serviceBaseAddress = serviceBaseAddress;
            this.addressSuffix = addressSuffix;
            this.httpClient = MakeHttpClient(this.serviceBaseAddress);

        }

        protected virtual HttpClient MakeHttpClient(string serviceBaseAddress)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serviceBaseAddress);

            //----------------APENAS PARA TESTE--------------
            httpClient.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
            //-----------------------------------------------

            httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(jsonMediaType));
            //CONFIRMAR
            //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("defalte"));
            //httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue()));
            return httpClient;
        }


        public async Task<List<T>> GetManyAsync()
        {
            var responseMessage = await httpClient.GetAsync(addressSuffix);
            //Throws exception VER DEPOIS!
            //responseMessage.EnsureSuccessStatusCode();

            string data = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<T>>(data);
            }
            else
            {
                return new List<T>();
            }

        }

        public async Task<T> GetAsync(TResourceIdentifier identifier)
        {
            var responseMessage = await httpClient.GetAsync(addressSuffix + identifier.ToString());

            string data = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                return null;
                //Null ou vazio?
                //return new T();
            }
        }

        public async Task<T> PostAsync(T model)
        {
            //var requestMessage = new HttpRequestMessage();
            string content = JsonConvert.SerializeObject(model);
            StringContent body = new StringContent(content, Encoding.UTF8, jsonMediaType);
            var responseMessage = await httpClient.PostAsync(addressSuffix, body);

            string data = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                return null;
            }

        }

        public async Task<T> PutAsync(TResourceIdentifier identifier, T model)
        {
            //var requestMessage = new HttpRequestMessage();
            string content = JsonConvert.SerializeObject(model);
            StringContent body = new StringContent(content, Encoding.UTF8, jsonMediaType);
            var responseMessage = await httpClient.PutAsync(addressSuffix + identifier.ToString(), body);

            string data = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteAsync(TResourceIdentifier identifier)
        {
            var responseMessage = await httpClient.DeleteAsync(addressSuffix + identifier.ToString());

            //COMPLETE
            if (responseMessage.IsSuccessStatusCode)
            {

            }
        }


        public void Dispose()
        {

        }
    }
}
