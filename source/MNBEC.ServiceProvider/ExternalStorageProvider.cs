using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;
using MNBEC.Core.Extensions;
using MNBEC.Domain;
using MNBEC.ServiceProviderInterface;

namespace MNBEC.ServiceProvider
{
    /// <summary>
    /// ExternalStorageProvider class inherits BaseServiceProvider and provide implementation to manage azure blob storage.
    /// </summary>
    public class ExternalStorageProvider : BaseServiceProvider, IExternalStorageProvider
    {
        #region Constructor
        /// <summary>
        /// ExternalStorage initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ExternalStorageProvider(IConfiguration configuration, ILogger<ExternalStorageProvider> logger) : base(configuration, logger)
        {

            this.Logger?.LogEnterConstructor(this.GetType());
        }
        #endregion

        #region Properties and Data Members
        #endregion

        #region Private Methods
        /// <summary>
        /// Create Connection between Azure Clound storage
        /// </summary>
        /// <returns>CloudStorageAccount</returns>
        private CloudStorageAccount GetAccount()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(this.Configuration["ConnectionStrings:ExternalStorageConnection"]);
            return storageAccount;
        }

        private CloudStorageAccount GetAccountForImages()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(this.Configuration["ConnectionStrings:ExternalStorageConnectionImages"]);
            return storageAccount;
        }

        /// <summary>
        /// After successfull connection with Azure cloud storage account creates an object of client.
        /// </summary>
        /// <param name="CloudStorageAccount"></param>
        /// <returns></returns>
        private CloudBlobClient GetClient(CloudStorageAccount storageAccount)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            return blobClient;
        }

        /// <summary>
        /// Gets the reference of the Container where we want to upload the file
        /// </summary>
        /// <param name="blobClient"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        private async Task<CloudBlobDirectory> GetContainer(CloudBlobClient blobClient, string containerName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            CloudBlobDirectory directory = container.GetDirectoryReference("data-set-1");

            return directory;
        }

        private async Task<CloudBlobContainer> GetImagesContainer(CloudBlobClient blobClient, string containerName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            await container.CreateIfNotExistsAsync();

            //TODO: Validate permissions
            //await container.SetPermissionsAsync(new BlobContainerPermissions
            //{
            //    PublicAccess = BlobContainerPublicAccessType.Blob
            //});

            return container;
        }

        /// <summary>
        /// Creates a blob container to be stored in cloud 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private CloudBlockBlob GetBlockBlob(CloudBlobDirectory container, string fileName)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            return blockBlob;
        }

        private CloudBlockBlob GetBlockBlobForImages(CloudBlobContainer container, string fileName)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            return blockBlob;
        }

        /// <summary>
        /// Uploads file to Azure Cloud storage
        /// </summary>
        /// <param name="blockBlob"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        private async Task UploadFile(CloudBlockBlob blockBlob, Documents document)
        {
            await blockBlob.UploadFromByteArrayAsync(document.DocumentByte, 0, document.DocumentByte.Length);
        }

        /// <summary>
        /// DownloadFile from azure cloud storage
        /// </summary>
        /// <param name="blockBlob"></param>
        /// <returns></returns>
        private async Task<byte[]> DownloadFile(CloudBlockBlob blockBlob)
        {

            using (MemoryStream stream = new MemoryStream())
            {

                await blockBlob.DownloadToStreamAsync(stream);

                return stream.ToArray();
            }
        }



        #endregion

        #region Public Interface Methods
        ///// <summary>
        ///// UploadFile uploads provided files stream to related container.
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="containerName"></param>
        ///// <returns></returns>
        //public async Task<string> UploadFile(IFormFile file, string containerName)
        //{
        //    string response = "";

        //        CloudStorageAccount storageAccount = this.GetAccount();
        //        CloudBlobClient blobClient = this.GetClient(storageAccount);
        //        CloudBlobContainer container = await this.GetContainer(blobClient, containerName);
        //        CloudBlockBlob blockBlob = this.GetBlockBlob(container, file.FileName);

        //         await this.UploadFile(blockBlob, file);
        //         response = blockBlob.StorageUri.PrimaryUri.GetStringValue();           

        //    return response;
        //}

        /// <summary>
        /// UploadFile uploads provided files stream to related container.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public async Task<string> UploadFile(Documents document, string containerName)
        {
            CloudStorageAccount storageAccount = this.GetAccount();
            CloudBlobClient blobClient = this.GetClient(storageAccount);
            CloudBlobDirectory container = await this.GetContainer(blobClient, containerName);
            CloudBlockBlob blockBlob = this.GetBlockBlob(container, document.DocumentName);

            await this.UploadFile(blockBlob, document);

            return blockBlob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        public async Task<string> UploadImage(Documents document, string containerName)
        {
            CloudStorageAccount storageAccount = this.GetAccountForImages();
            CloudBlobClient blobClient = this.GetClient(storageAccount);
            CloudBlobContainer container = await this.GetImagesContainer(blobClient, containerName);
            CloudBlockBlob blockBlob = this.GetBlockBlobForImages(container, document.DocumentName);

            await this.UploadFile(blockBlob, document);

            return blockBlob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        /// <summary>
        /// UploadFile downloads file stram from related container.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public async Task<byte[]> DownloadFile(string fileName, string containerName)
        {
            CloudStorageAccount storageAccount = this.GetAccount();
            CloudBlobClient blobClient = this.GetClient(storageAccount);
            CloudBlobDirectory container = await this.GetContainer(blobClient, containerName);
            CloudBlockBlob blockBlob = this.GetBlockBlob(container, fileName);

            return await this.DownloadFile(blockBlob);

        }

        ///// <summary>
        ///// Gets all files from blob
        ///// </summary>
        ///// <param name="containerName"></param>
        ///// <returns></returns>
        //public async Task<List<string>> GetAllFilesInContainer(string containerName)
        //{
        //    CloudStorageAccount storageAccount = this.GetAccount();
        //    CloudBlobClient blobClient = this.GetClient(storageAccount);
        //    CloudBlobContainer cloudBlobContainer = await this.GetContainer(blobClient, containerName);
        //    // CloudBlockBlob blockBlob = this.GetBlockBlob(cloudBlobContainer, fileName);
        //    List<string> items = null;
        //      BlobContinuationToken blobContinuationToken = null;
        //    do
        //    {
        //        var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
        //        // Get the value of the continuation token returned by the listing call.
        //        blobContinuationToken = results.ContinuationToken;
        //        foreach (IListBlobItem item in results.Results)
        //        {
        //            Console.WriteLine(item.Uri);
        //            items.Add(item.Uri.GetStringValue());
        //        }
        //    } while (blobContinuationToken != null);

        //    return items;
        //}

        ///// <summary>
        ///// Get Single blob item from the storage
        ///// </summary>
        ///// <param name="containerName"></param>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //public async Task<string> GetFile(string containerName, string fileName)
        //{
        //    CloudStorageAccount storageAccount = this.GetAccount();
        //    CloudBlobClient blobClient = this.GetClient(storageAccount);
        //    CloudBlobContainer cloudBlobContainer = await this.GetContainer(blobClient, containerName);
        //    CloudBlockBlob blockBlob = this.GetBlockBlob(cloudBlobContainer, fileName);

        //    return blockBlob.StorageUri.PrimaryUri.GetStringValue();
        //}

        ///// <summary>
        ///// File converter from stream into Bytes
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public byte[] ConvertFileStreamIntoByte(Stream input)
        //{
        //    byte[] buffer = new byte[input.Length];
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        int read;
        //        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            ms.Write(buffer, 0, read);
        //        }
        //        return ms.ToArray();
        //    }
        //}

        #endregion

    }
}
