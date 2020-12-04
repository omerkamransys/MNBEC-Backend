using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Infrastructure.Extensions;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// EmailInfrastructure inherits from BaseInfrastructure and implements IEmailfrastructure. It performs all required CRUD operations on Email Entity on database.
    /// </summary>
    public class SMSInfrastructure : BaseInfrastructure, ISMSInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  Emailfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public SMSInfrastructure(IConfiguration configuration, ILogger<EmailInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants

        //Sp Name
        private const string GetStoredProcedureName = "GetSMSTemplatebyCode";

        //Column names
        private const string SMSTemplateIdColumnName = "SMSTemplateId";
        private const string SMSCodeColumnName = "SMSCode";
        private const string MessageColumnName = "Message";
        private const string MessageTranslationColumnName = "MessageTranslation";
        private const string SenderColumnName = "Sender";
        private const string ReceiverColumnName = "Receiver";

        //Parameter Names
        private const string EmailTemplateCodeParameterName = "PSMSTemplateCode";

        #endregion

        #region Interface ISMSInfrastructure Implementation

        public async Task<SMS> Get(SMS sms)
        {
            SMS smsTemplate = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(SMSInfrastructure.EmailTemplateCodeParameterName, sms.SMSCode)
            };

            using (var dataReader = await base.ExecuteReader(parameters, SMSInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        smsTemplate = new SMS
                        {                                                   
                            SMSTemplateId = dataReader.GetUnsignedIntegerValue(SMSInfrastructure.SMSTemplateIdColumnName),
                            SMSCode = dataReader.GetStringValue(SMSInfrastructure.SMSCodeColumnName),
                            Message = dataReader.GetStringValue(SMSInfrastructure.MessageColumnName),
                            MessageTranslation = dataReader.GetStringValue(SMSInfrastructure.MessageTranslationColumnName),
                            Receiver = dataReader.GetStringValue(SMSInfrastructure.ReceiverColumnName),
                            Sender = dataReader.GetStringValue(SMSInfrastructure.SenderColumnName)                           
                        };
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return smsTemplate;
        }
        #endregion
    }
}
