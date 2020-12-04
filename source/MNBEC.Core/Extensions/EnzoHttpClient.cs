using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MNBEC.Core.Extensions
{
    /// <summary>
    /// EnzoHttpClient inherites HttpClient class to provides implementation for HttpClient.
    /// </summary>
    public class EnzoHttpClient : HttpClient
    {
        #region Constructor
        /// <summary>
        /// EnzoHttpClient initializes class object.
        /// </summary>
        public EnzoHttpClient()
        {
        }
        #endregion

        #region Properties and Data Members
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="parameter"></param>
        public void AddAuthorizationRequestHeader(string scheme, string parameter)
        {
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void AddBearerAuthorizationRequestHeader(string parameter)
        {
            this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="parameter"></param>
        public void AddAcceptLangauageRequestHeader(string acceptLangauage)
        {
            if (!string.IsNullOrWhiteSpace(acceptLangauage))
            {
                this.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLangauage));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public async Task<T> GetData<T>(string requestUri) where T : class
        {
            T data = null;

            var responseData = await this.GetStringAsync(requestUri);

            if (!string.IsNullOrWhiteSpace(responseData))
            {
                data = JsonConvert.DeserializeObject<T>(responseData);
            }

            return data;
        }

        /// <summary>
        /// PostAsync makes an async POST request to the passed URI
        /// It deserializes the passed T type contentObject, and returns Deserialized object of Type U
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentObject"></param>
        /// <param name="URI"></param>
        /// <returns></returns>
        public async Task<U> PostAsyncReturnsObject<T,U>(T contentObject, string URI) where U : class
        {
            HttpResponseMessage response;

            var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");
            response = await this.PostAsync(URI, content);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
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

            var content = new StringContent(JsonConvert.SerializeObject(contentObject), Encoding.UTF8, "application/json");
            response = await this.PostAsync(URI, content);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
            }

            return defaultResponse;

        }
        #endregion
    }
}