using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.ServiceConnectorInterface;
using MNBEC.ServiceProviderInterface;

namespace MNBEC.ServiceConnector
{
    /// <summary>
    /// ExternalStorageConnector class inherits BaseServiceConector and provide Connector implementation for External Storage.
    /// </summary>
    public class ExternalStorageConnector : BaseServiceConnector, IExternalStorageConnector
    {
        #region Constructor
        /// <summary>
        ///  ExternalStorageConnector initializes class object.
        /// </summary>
        /// <param name="externalStorageProvider"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ExternalStorageConnector(IExternalStorageProvider externalStorageProvider, IConfiguration configuration, ILogger<ExternalStorageConnector> logger) : base(configuration, logger)
        {
            this.ExternalStorageProvider = externalStorageProvider;
        }
        #endregion

        #region Properties and Data Members
        public IExternalStorageProvider ExternalStorageProvider { get; }
        #endregion

        #region Methods
        ///// <summary>
        ///// UploadFile uploads provided files stream to related container.
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="containerName"></param>
        ///// <returns></returns>
        //public async Task<string> UploadFile(IFormFile file, string containerName)
        //{
        //    return await this.ExternalStorageProvider.UploadFile(file, containerName);
        //}

        /// <summary>
        /// UploadFile uploads provided files stream to related container.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public async Task<string> UploadFile(Documents document, string containerName)
        {
            return await this.ExternalStorageProvider.UploadFile(document, containerName);
        }



        /// <summary>
        /// UploadFile downloads file bytes from related container.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public async Task<byte[]> DownloadFile(string fileName, string containerName)
        {
            return await this.ExternalStorageProvider.DownloadFile(fileName, containerName);
        }

        public async Task<string> UploadImage(Documents document, string containerName)
        {
            return await this.ExternalStorageProvider.UploadImage(document, containerName);
        }
        #endregion
    }
}
