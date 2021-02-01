using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.Infrastructure.Extensions;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// QuestionnaireTemplateInfrastructure inherits from BaseDataAccess and implements IQuestionnaireTemplateInfrastructure. It performs all required CRUD operations on QuestionnaireTemplate Entity on database.
    /// </summary>
    public class QuestionnaireTemplateInfrastructure : BaseSQLInfrastructure, IQuestionnaireTemplateInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  QuestionnaireTemplateInfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public QuestionnaireTemplateInfrastructure(IConfiguration configuration, ILogger<QuestionnaireTemplateInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants
        private const string AddStoredProcedureName = "QuestionnaireTemplateAdd";
        private const string GetStoredProcedureName = "QuestionnaireTemplateGet";
        private const string GetListStoredProcedureName = "QuestionnaireTemplateGetList";
        private const string UpdateStoredProcedureName = "QuestionnaireTemplateUpdate";
        private const string ActivateStoredProcedureName = "QuestionnaireTemplateActivate";


        private const string IdColumnName = "Id";
        private const string TitleColumnName = "Title";

        private const string QuestionaireTemplateIdColumnName = "QuestionaireTemplateId";
        private const string AreaColumnName = "Area";
        private const string FourPColumnName = "FourP";
        private const string ResponsibleColumnName = "Responsible";
        private const string LevelColumnName = "Level";
        private const string OrderNumberColumnName = "OrderNumber";
        private const string Level0ColumnName = "Level0";
        private const string Level1ColumnName = "Level1";
        private const string Level2ColumnName = "Level2";
        private const string Level3ColumnName = "Level3";
        private const string Level4ColumnName = "Level4";
        private const string ElementColumnName = "Element";


        private const string TitleParameterName = "PTitle";
        private const string IdParameterName = "PId";
        #endregion

        #region Interface IQuestionnaireTemplateInfrastructure Implementation

        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="questionnaireTemplate"></param>
        /// <returns></returns>
        public async Task<int> Add(QuestionnaireTemplate questionnaireTemplate)
        {
            var questionnaireTemplateIdParamter = base.GetParameterOut(QuestionnaireTemplateInfrastructure.IdParameterName, SqlDbType.Int, questionnaireTemplate.Id);
            var parameters = new List<DbParameter>
            {
                questionnaireTemplateIdParamter,
                base.GetParameter(QuestionnaireTemplateInfrastructure.TitleParameterName, questionnaireTemplate.Title),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, questionnaireTemplate.CurrentUserId)
            };
            

            await base.ExecuteNonQuery(parameters, QuestionnaireTemplateInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            questionnaireTemplate.Id = Convert.ToInt32(questionnaireTemplateIdParamter.Value);

            return questionnaireTemplate.Id;
        }

        /// <summary>
        /// Activate activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="questionnaireTemplate"></param>
        /// <returns></returns>
        public async Task<bool> Activate(QuestionnaireTemplate questionnaireTemplate)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(QuestionnaireTemplateInfrastructure.IdParameterName, questionnaireTemplate.Id),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, questionnaireTemplate.Active),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, questionnaireTemplate.CurrentUserId)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, QuestionnaireTemplateInfrastructure.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return true;
        }

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<QuestionnaireTemplate> Get(QuestionnaireTemplate questionnaireTemplate)
        {
            QuestionnaireTemplate questionnaireTemplateItem = null;
            Question questionItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(QuestionnaireTemplateInfrastructure.IdParameterName, questionnaireTemplate.Id)
            };

            using (var dataReader = await base.ExecuteReader(parameters, QuestionnaireTemplateInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        questionnaireTemplateItem = new QuestionnaireTemplate
                        {
                            Id = dataReader.GetUnsignedIntegerValue(QuestionnaireTemplateInfrastructure.IdColumnName),
                            Title = dataReader.GetStringValue(QuestionnaireTemplateInfrastructure.TitleColumnName),
                            QuestionsList = new List<Question>()
                        };
                    }
                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            questionItem = new Question
                            {
                                Id = dataReader.GetUnsignedIntegerValue(QuestionnaireTemplateInfrastructure.IdColumnName),
                                QuestionaireTemplateId = dataReader.GetUnsignedIntegerValue(QuestionnaireTemplateInfrastructure.QuestionaireTemplateIdColumnName),
                                Area = dataReader.GetUnsignedIntegerValueNullable(QuestionnaireTemplateInfrastructure.AreaColumnName),
                                FourP = dataReader.GetUnsignedIntegerValueNullable(QuestionnaireTemplateInfrastructure.FourPColumnName),
                                Responsible = dataReader.GetUnsignedIntegerValueNullable(QuestionnaireTemplateInfrastructure.ResponsibleColumnName),
                                Level = dataReader.GetUnsignedIntegerValueNullable(QuestionnaireTemplateInfrastructure.LevelColumnName),
                                Level0 = dataReader.GetStringValue(QuestionnaireTemplateInfrastructure.Level0ColumnName),
                                Level1 = dataReader.GetStringValue(QuestionnaireTemplateInfrastructure.Level1ColumnName),
                                Level2 = dataReader.GetStringValue(QuestionnaireTemplateInfrastructure.Level2ColumnName),
                                Level3 = dataReader.GetStringValue(QuestionnaireTemplateInfrastructure.Level3ColumnName),
                                Level4 = dataReader.GetStringValue(QuestionnaireTemplateInfrastructure.Level4ColumnName),
                                QuestionElement = dataReader.GetStringValue(QuestionnaireTemplateInfrastructure.ElementColumnName),
                                OrderNumber = dataReader.GetUnsignedIntegerValueNullable(QuestionnaireTemplateInfrastructure.OrderNumberColumnName),
                                Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName)
                            };

                            questionnaireTemplateItem?.QuestionsList.Add(questionItem);
                        }
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return questionnaireTemplateItem;
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="applicationClaimGroup"></param>
        /// <returns></returns>
        public Task<AllResponse<QuestionnaireTemplate>> GetAll(AllRequest<QuestionnaireTemplate> questionnaireTemplate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="questionnaireTemplate"></param>
        /// <returns></returns>
        public async Task<List<QuestionnaireTemplate>> GetList(QuestionnaireTemplate questionnaireTemplate)
        {
            var questionnaireTemplates = new List<QuestionnaireTemplate>();
            QuestionnaireTemplate questionnaireTemplateItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, questionnaireTemplate.CurrentUserId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, QuestionnaireTemplateInfrastructure.GetListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        questionnaireTemplateItem = new QuestionnaireTemplate
                        {
                            Id = dataReader.GetUnsignedIntegerValue(QuestionnaireTemplateInfrastructure.IdColumnName),
                            Title = dataReader.GetStringValue(QuestionnaireTemplateInfrastructure.TitleColumnName)
                        };

                        questionnaireTemplates.Add(questionnaireTemplateItem);
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return questionnaireTemplates;
        }

        /// <summary>
        /// Update updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="questionnaireTemplate"></param>
        /// <returns></returns>
        public async Task<bool> Update(QuestionnaireTemplate questionnaireTemplate)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(QuestionnaireTemplateInfrastructure.IdParameterName, questionnaireTemplate.Id),
                base.GetParameter(QuestionnaireTemplateInfrastructure.TitleParameterName, questionnaireTemplate.Title),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, questionnaireTemplate.CurrentUserId)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, QuestionnaireTemplateInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);

            return true;
        }

        #endregion
    }
}
