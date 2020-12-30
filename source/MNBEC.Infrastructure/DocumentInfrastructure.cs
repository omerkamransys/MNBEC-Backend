using MNBEC.Core.Extensions;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.Infrastructure;
using MNBEC.Infrastructure.Extensions;
using MNBEC.InfrastructureInterface;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;


namespace Vitol.Enzo.Infrastructure
{
    /// <summary>
    /// DocumentInfrastructure inherits from BaseDataAccess and implements IDocumentInfrastructure. It performs all required CRUD operations on Document Entity on database.
    /// </summary>
    public class DocumentInfrastructure : BaseInfrastructure, IDocumentInfrastructure
    {
        #region Constructor
        /// <summary>
        /// DocumentInfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public DocumentInfrastructure(IConfiguration configuration, ILogger<DocumentInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants
        private const string FileUploadProcedureName = "DocumentAdd";
        private const string ActivateProcedureName = "DocumentActivate";
        private const string GetStoredProcedureName = "DocumentGet";
        private const string GetConditionReportStoredProcedureName = "DocumentGetConditionReport";

        private const string DocumentIdColumnName = "DocumentId";
        private const string DocumentTypeIdColumnName = "DocumentTypeId";
        private const string DocumentFileColumnName = "DocumentFile";
        private const string DocumentNameColumnName = "DocumentName";
        private const string DocumentExtensionColumnName = "DocumentExtension";
        private const string DocumentPathColumnName = "DocumentPath";
        private const string DisplayNameColumnName = "DisplayName";
        private const string InspectionCheckpointDocumentIdColumnName = "InspectionCheckpointDocumentId";
        private const string InspectionCheckpointIdColumnName = "InspectionCheckpointId";



        private const string DocumentIdParameterName = "PDocumentId";
        private const string DocumentTypeIdParameterName = "PDocumentTypeId";
        private const string DocumentFileParameterName = "PDocumentFile";
        private const string DocumentNameParameterName = "PDocumentName";
        private const string DocumentExtensionParameterName = "PDocumentExtension";
        private const string DocumentPathParameterName = "PDocumentPath";
        private const string DisplayNameParameterName = "PDisplayName";
        private const string AuctionIdParameterName = "PAuctionId";
        private const string InspectionCheckpointDocumentIdParameterName = "PInspectionCheckpointDocumentId";
        private const string InspectionCheckpointIdParameterName = "PInspectionCheckpointId";


        #endregion

        #region Interface IDocumentfrastructure Implementation
        /// <summary>
        /// Upload Document and returns the document Id for reference.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public async Task<int> Add(Documents documents)
        {

            var DocumentIdParamter = base.GetParameterOut(DocumentInfrastructure.DocumentIdParameterName, SqlDbType.Int, documents.DocumentId);
            var parameters = new List<DbParameter>
            {
                DocumentIdParamter,
                base.GetParameter(DocumentInfrastructure.DocumentNameParameterName, documents.DocumentName),
                base.GetParameter(DocumentInfrastructure.DocumentTypeIdParameterName, documents.DocumentTypeId),
                base.GetParameter(DocumentInfrastructure.DocumentExtensionParameterName, documents.DocumentExtension),
                base.GetParameter(DocumentInfrastructure.DocumentPathParameterName, documents.DocumentPath),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, documents.CurrentUserId),
                base.GetParameter(BaseInfrastructure.ActiveParameterName, documents.Active)
            };

            await base.ExecuteNonQuery(parameters, DocumentInfrastructure.FileUploadProcedureName, CommandType.StoredProcedure);

            documents.DocumentId = Convert.ToInt32(DocumentIdParamter.Value);

            return documents.DocumentId;
        }

        public async Task<bool> Activate(Documents document)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(DocumentInfrastructure.DocumentIdParameterName, document.DocumentId),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, document.CurrentUserId)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, DocumentInfrastructure.ActivateProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<Documents> Get(Documents document)
        {
            Documents documentItem = null;

            var parameters = new List<DbParameter>
            {
                base.GetParameter(DocumentInfrastructure.DocumentIdParameterName, document.DocumentId),
                base.GetParameter(BaseInfrastructure.CurrentUserIdParameterName, document.CurrentUserId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, DocumentInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        documentItem = new Documents
                        {
                            DocumentId = dataReader.GetUnsignedIntegerValue(DocumentInfrastructure.DocumentIdColumnName),
                            DocumentName = dataReader.GetStringValue(DocumentInfrastructure.DocumentNameColumnName),
                            DocumentExtension = dataReader.GetStringValue(DocumentInfrastructure.DocumentExtensionColumnName),
                            DocumentPath = dataReader.GetStringValue(DocumentInfrastructure.DocumentPathColumnName),
                            DisplayName = dataReader.GetStringValue(DocumentInfrastructure.DisplayNameColumnName),
                            Active = dataReader.GetBooleanValue(BaseInfrastructure.ActiveColumnName)
                        };
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return documentItem;
        }

        public Task<AllResponse<Documents>> GetAll(AllRequest<Documents> entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Documents>> GetList(Documents entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Documents entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
