using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MNBEC.Core.Interface
{
    public interface IEnzoHttpClientFactory
    {
        Task<U> PostAsyncReturnsStruct<T, U>(T contentObject, string URI) where U : struct;
        Task<U> PostAsyncReturnsObject<T, U>(T contentObject, string URI, string token = null) where U : class;
        Task<T> GetData<T>(string requestUri, string scheme = null, string schemeToken = null, string acceptLangauage = null) where T : class;
    }
}
