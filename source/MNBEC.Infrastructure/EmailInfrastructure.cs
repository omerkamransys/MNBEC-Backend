using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.Infrastructure.Extensions;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// EmailInfrastructure inherits from BaseInfrastructure and implements IEmailfrastructure. It performs all required CRUD operations on Email Entity on database.
    /// </summary>
    public class EmailInfrastructure : BaseInfrastructure, IEmailInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  Emailfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public EmailInfrastructure(IConfiguration configuration, ILogger<EmailInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants
        private const string AddStoredProcedureName = "EmailAdd";
        private const string ActivateStoredProcedureName = "EmailActivate";
        private const string GetStoredProcedureName = "GetEmailTemplatebyEmailCode";
        private const string GetAllStoredProcedureName = "EmailGetAll";
        private const string GetListStoredProcedureName = "EmailGetList";
        private const string UpdateStoredProcedureName = "EmailUpdate";
        private const string EmailSearchStoredProcedureName = "EmailSearch";

        private const string EmailTemplateIdColumnName = "EmailTemplateId";
        private const string SubjectTranslationColumnName = "SubjectTranslation";
        private const string SubjectColumnName = "Subject";
        private const string MessageColumnName = "Message";
        private const string MessageTranslationColumnName = "MessageTranslation";
        private const string TOEmailColumnName = "TOEmail";
        private const string CCEmailColumnName = "CCEmail";
        private const string BCCEmailColumnName = "BCCEmail";
        private const string FromEmailColumnName = "FromEmail";
        private const string FromNameColumnName = "FromName";
        private const string EmailTemplateCodeColumnName = "EmailTemplateCode";

        private const string EmailIdParameterName = "PEmailId";
        private const string EmailNameParameterName = "PEmailName";
        private const string EmailNameTranslationParameterName = "PEmailNameTranslation";
        private const string EmailTemplateCodeParameterName = "PEmailTemplateCode";


        private const string GetAllEmailTemplateStoredProcedureName = "EmailTemplateGetAll";

        #endregion

        #region Interface IEmailfrastructure Implementation

        public async Task<EmailTemplate> Get(EmailTemplate emailtemplate)
        {
            EmailTemplate emailTemplate = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(EmailInfrastructure.EmailTemplateCodeParameterName, emailtemplate.EmailTemplateCode)

            };

            using (var dataReader = await base.ExecuteReader(parameters, EmailInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        emailTemplate = new EmailTemplate
                        {
                            EmailTemplateId = dataReader.GetUnsignedIntegerValue(EmailInfrastructure.EmailTemplateIdColumnName),
                            EmailTemplateCode = dataReader.GetStringValue(EmailInfrastructure.EmailTemplateCodeColumnName),
                            Subject = dataReader.GetStringValue(EmailInfrastructure.SubjectColumnName),
                            SubjectTranslation = dataReader.GetStringValue(EmailInfrastructure.SubjectTranslationColumnName),
                            Message = dataReader.GetStringValue(EmailInfrastructure.MessageColumnName),
                            MessageTranslation = dataReader.GetStringValue(EmailInfrastructure.MessageTranslationColumnName),
                            To = dataReader.GetStringValue(EmailInfrastructure.TOEmailColumnName),
                            CC = dataReader.GetStringValue(EmailInfrastructure.CCEmailColumnName),
                            BCC = dataReader.GetStringValue(EmailInfrastructure.BCCEmailColumnName)
                        };
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return emailTemplate;
        }

        public async Task<List<EmailTemplate>> GetAllEmailTemplate()
        {
            var result = new List<EmailTemplate>();
            EmailTemplate template = default(EmailTemplate);

            using (var dataReader = await base.ExecuteReader(new List<DbParameter>(), GetAllEmailTemplateStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        template = new EmailTemplate
                        {
                            EmailTemplateId = dataReader.GetUnsignedIntegerValue(EmailInfrastructure.EmailTemplateIdColumnName),
                            EmailTemplateCode = dataReader.GetStringValue(EmailInfrastructure.EmailTemplateCodeColumnName),
                            Subject = dataReader.GetStringValue(EmailInfrastructure.SubjectColumnName),
                            SubjectTranslation = dataReader.GetStringValue(EmailInfrastructure.SubjectTranslationColumnName),
                            Message = dataReader.GetStringValue(EmailInfrastructure.MessageColumnName),
                            MessageTranslation = dataReader.GetStringValue(EmailInfrastructure.MessageTranslationColumnName),
                            To = dataReader.GetStringValue(EmailInfrastructure.TOEmailColumnName),
                            CC = dataReader.GetStringValue(EmailInfrastructure.CCEmailColumnName),
                            BCC = dataReader.GetStringValue(EmailInfrastructure.BCCEmailColumnName)
                        };
                        result.Add(template);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }

            return result;
        }
        #endregion
    }
}
