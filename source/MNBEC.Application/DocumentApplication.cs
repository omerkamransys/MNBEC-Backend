using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MNBEC.ApplicationInterface;
using MNBEC.CacheInterface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.InfrastructureInterface;
using MNBEC.ServiceConnectorInterface;

namespace MNBEC.Application
{
    /// <summary>
    /// MakeApplication inherits from BaseApplication and implements IMakeApplication. It provides the implementation for Make related operations.
    /// </summary>
    public class DocumentApplication : BaseApplication, IDocumentApplication
    {
        #region Constructor
        /// <summary>
        /// DocumentApplication initailizes object instance.
        /// </summary>
        /// <param name="DocumentInfrastructure"></param>
        /// <param name="DocumentCache"></param>
        public DocumentApplication(IDocumentInfrastructure DocumentInfrastructure, IExternalStorageConnector externalStorageConnector, IConfiguration configuration, ILogger<DocumentApplication> logger) : base(configuration, logger)
        {
            this.DocumentInfrastructure = DocumentInfrastructure;


        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// DocumentInfrastructure holds the Infrastructure object.
        /// </summary>
        public IDocumentInfrastructure DocumentInfrastructure { get; }

        public IExternalStorageConnector ExternalStorageConnector { get; }
        #endregion

        #region Interface IDocumentApplication Implementation
        /// <summary>
        /// Add calls DocumentInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="Document"></param>
        /// <returns></returns>
        public async Task<int> Add(Documents document)
        {
            document.DocumentPath = await this.UploadFile(document);

            var response = await this.DocumentInfrastructure.Add(document);

            return response;
        }

        /// <summary>
        /// Activate calls DocumentInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="Document"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Documents Document)
        {
            return await this.DocumentInfrastructure.Activate(Document);
        }

        /// <summary>
        /// Get calls DocumentInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="Document"></param>
        /// <returns></returns>
        public async Task<Documents> Get(Documents Document)
        {
            return await this.DocumentInfrastructure.Get(Document);
        }

        /// <summary>
        /// GetAll calls DocumentInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="Document"></param>
        /// <returns></returns>
        public async Task<AllResponse<Documents>> GetAll(AllRequest<Documents> Document)
        {
            return await this.DocumentInfrastructure.GetAll(Document);


        }

        /// <summary>
        /// GetAll calls DocumentInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="Document"></param>
        /// <returns></returns>
        public async Task<List<Documents>> GetList(Documents Document)
        {
            return await this.DocumentInfrastructure.GetList(Document);
        }



        /// <summary>
        /// Update calls DocumentInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="Document"></param>
        /// <returns></returns>
        public async Task<bool> Update(Documents Document)
        {
            //  return await this.DocumentInfrastructure.Update(Document);
            var response = await this.DocumentInfrastructure.Update(Document);

            return response;
        }


        private async Task<string> UploadFile(Documents document)
        {
            string externalStoreDirectory = GetStorageContainerName(document);

            return await this.ExternalStorageConnector.UploadFile(document, externalStoreDirectory);
        }

        private string GetStorageContainerName(Documents document)
        {
            return base.Configuration["General:ExternalStoreDirectory"];
        }
        #endregion
    }
}