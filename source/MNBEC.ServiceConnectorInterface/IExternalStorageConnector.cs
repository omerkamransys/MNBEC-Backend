using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using MNBEC.Domain;

namespace MNBEC.ServiceConnectorInterface
{
    /// <summary>
    /// IExternalStorageConnector inhertis IBaseServiceConnector for Azure Blob Storage Connectors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IExternalStorageConnector : IBaseServiceConnector<object>
    {
        #region Methods
        ///// <summary>
        ///// UploadFile uploads provided files stream to related container.
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="containerName"></param>
        ///// <returns></returns>
        //Task<string> UploadFile(IFormFile file, string containerName);

        /// <summary>
        /// UploadFile uploads provided files stream to related container.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<string> UploadFile(Documents document, string containerName);
        Task<string> UploadImage(Documents document, string containerName);


        /// <summary>
        /// UploadFile downloads file in bytes from related container.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<byte[]> DownloadFile(string fileName, string containerName);
        #endregion
    }
}