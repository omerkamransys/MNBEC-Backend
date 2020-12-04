using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MNBEC.Core.Interface;

namespace MNBEC.Core.Extensions
{
    public class EnzoHttpClientFactory : IEnzoHttpClientFactory
    {
        private readonly IHttpClientFactory _clientFactory;
        public EnzoHttpClientFactory(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// GetAsync makes an async GET request to the passed URI
        /// It returns Deserialized object of Type T.
        /// </summary>
        /// <typeparam name="T">Response deserializes to the passed T type.</typeparam>
        /// <param name="requestUri">HTTP Request endpoint url </param>
        /// <param name="scheme">Default null. Used for authorization. </param>
        /// <param name="schemeToken">Default null. Used for authorization.</param>
        /// <param name="acceptLangauage">Http AcceptLanguage header</param>
        /// <returns></returns>
        public async Task<T> GetData<T>(string requestUri, string scheme= null, string schemeToken = null,string acceptLangauage=null) where T : class
        {
            T data = null;
            using (var client = _clientFactory.CreateClient("EnzoClientFactory"))
            {
                // check if langauage avaialble then add to httpclient request header.
                if (string.IsNullOrEmpty(acceptLangauage) == false)
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLangauage));
                }

                // check if scheme avaialble then add to httpclient request header.
                if (string.IsNullOrEmpty(scheme) == false && string.IsNullOrEmpty(schemeToken) == false)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, schemeToken);
                }
               

                var responseData = await client.GetStringAsync(requestUri);

                if (!string.IsNullOrWhiteSpace(responseData))
                {
                    data = JsonConvert.DeserializeObject<T>(responseData);
                }
            }
            return data;
        }
        /// <summary>
        /// PostAsync makes an async POST request to the passed URI
        /// It deserializes the passed T type contentObject, and returns Deserialized object of Type U
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="contentObject"></param>
        /// <param name="URI"></param>
        /// <param name="token">Default null. Used for authorization. </param>
        /// <returns></returns>
        public async Task<U> PostAsyncReturnsObject<T, U>(T contentObject, string URI, string token =  null) where U : class
        {
            HttpResponseMessage response;
            using (var client = _clientFactory.CreateClient("EnzoClientFactory"))
            {

                // check if Bearer token available then add to httpclient request header.
                if (string.IsNullOrEmpty(token) == false)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                

                var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");
                response = await client.PostAsync(URI, content);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
                }
            }
            return null;
        }

        /// <summary>
        /// PostAsync makes an async POST request to the passed URI
        /// It deserializes the passed T type contentObject, and returns Deserialized object of Type U
        /// </summary>
        /// <typeparam name="T"> type of the content object </typeparam>
        /// <typeparam name="U">type of the expected return object </typeparam>
        /// <param name="contentObject"></param>
        /// <param name="URI"></param>
        /// <returns></returns>
        public async Task<U> PostAsyncReturnsStruct<T, U>(T contentObject, string URI) where U : struct
        {
            U defaultResponse = default(U);

            HttpResponseMessage response;

            using (var client = _clientFactory.CreateClient("EnzoClientFactory"))
            {
                var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");
                response = await client.PostAsync(URI, content);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
                }
            }
            return defaultResponse;
        }
    }
}
