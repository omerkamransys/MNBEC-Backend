using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.Core.Extensions;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.Infrastructure.Extensions;
using MNBEC.InfrastructureInterface;
using MNBEC.ViewModel;
using MNBEC.ViewModel.Answer;
using MNBEC.ViewModel.ReportResponseVM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace MNBEC.Infrastructure
{
    public class AnswerInfrastructure : BaseSQLInfrastructure, IAnswerInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  AnswerInfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public AnswerInfrastructure(IConfiguration configuration, ILogger<AnswerInfrastructure> logger) : base(configuration, logger)
        {

        }

        #endregion

        #region Constants
        
        private const string LevelTypeLookUpGetListProcedureName = "LevelTypeLookUpGetList";
        private const string AddStoredProcedureName = "StakeholderAnswerAdd";
        private const string UpdateStoredProcedureName = "StakeholderAnswerUpdate";
        private const string GetListByStakeholderIdStoredProcedureName = "StakeholderAnswerGetListByStakeholderId";
        private const string GetReportListStoredProcedureName = "SP_GetReportList";
        private const string StakeholderQuestionnaireStatusAddStoredProcedureName = "StakeholderQuestionnaireStatusAdd";

        private const string IdColumnName = "Id";
        private const string TitleColumnName = "Title";
        private const string QuestionIdColumnName = "QuestionId";
        private const string AnswerValueColumnName = "AnswerValue";
        private const string LevelTypeColumnName = "LevelType";
        private const string StakeholderIdColumnName = "StakeholderId";
        private const string LevelIdIdColumnName = "LevelId";
        private const string QuestionaireTemplateIdColumnName = "QuestionaireTemplateId";
        private const string Level0ColumnName = "Level0";
        private const string Level1ColumnName = "Level1";
        private const string Level2ColumnName = "Level2";
        private const string Level3ColumnName = "Level3";
        private const string Level4ColumnName = "Level4";
        private const string ElementColumnName = "Element";
        private const string AreaLookUpTitleColumnName = "AreaLookUpTitle";

        private const string IdParameterName = "PId";
        private const string QuestionIdParameterName = "PQuestionId";
        private const string AnswerValueParameterName = "PAnswerValue";
        private const string LevelTypeParameterName = "PLevelType";
        private const string StakeholderIdParameterName = "PStakeholderId";
        private const string LevelIdParameterName = "PLevelId";
        private const string QuestionaireTemplateIdParameterName = "PQuestionaireTemplateId";

        #endregion
        #region Interface IQuestionInfrastructure Implementation

        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<int> Add(Answer answer)
        {
            var answerIdParamter = base.GetParameterOut(AnswerInfrastructure.IdParameterName, SqlDbType.Int, answer.Id);
            var parameters = new List<DbParameter>
            {
                answerIdParamter,
                base.GetParameter(AnswerInfrastructure.QuestionIdParameterName, answer.QuestionId),
                base.GetParameter(AnswerInfrastructure.AnswerValueParameterName, answer.AnswerValue),
                base.GetParameter(AnswerInfrastructure.LevelTypeParameterName, answer.LevelType),
                base.GetParameter(AnswerInfrastructure.StakeholderIdParameterName, answer.StakeholderId),
                base.GetParameter(AnswerInfrastructure.LevelIdParameterName, answer.LevelId),
                base.GetParameter(AnswerInfrastructure.QuestionaireTemplateIdParameterName, answer.QuestionaireTemplateId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, answer.CurrentUserId)
            };


            await base.ExecuteNonQuery(parameters, AnswerInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            answer.Id = Convert.ToInt32(answerIdParamter.Value);

            return answer.Id;
        }

        /// <summary>
        /// Activate activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public  Task<bool> Activate(Answer answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public  Task<Answer> Get(Answer answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public Task<AllResponse<Answer>> GetAll(AllRequest<Answer> answers)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public Task<List<Answer>> GetList(Answer answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<bool> Update(Answer answer)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(AnswerInfrastructure.IdParameterName, answer.Id),
                base.GetParameter(AnswerInfrastructure.AnswerValueParameterName, answer.AnswerValue),
                base.GetParameter(AnswerInfrastructure.LevelTypeParameterName, answer.LevelType),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, answer.CurrentUserId)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, AnswerInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);

            return true;
        }

        
        /// <summary>
        /// ResponsibleLookUp fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LookUpVM>> LevelTypeLookUpGetList()
        {
            var list = new List<LookUpVM>();
            LookUpVM lookUpVM = null;
            var parameters = new List<DbParameter>
            {

            };

            using (var dataReader = await base.ExecuteReader(parameters, AnswerInfrastructure.LevelTypeLookUpGetListProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        lookUpVM = new LookUpVM
                        {
                            Id = dataReader.GetUnsignedIntegerValue(AnswerInfrastructure.IdColumnName),
                            Title = dataReader.GetStringValue(AnswerInfrastructure.TitleColumnName)
                        };

                        list.Add(lookUpVM);
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return list;
        }


        /// <summary>
        /// GetReportList fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReportResponseVM> GetReportList(StakeholderAnswerRequest request)
        {

            var answers = new ReportResponseVM();
            answers.Report = new List<FourP>();
            FourP item = null;
            ReportQList item1 = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(AnswerInfrastructure.LevelIdParameterName, request.LevelId),
                base.GetParameter(AnswerInfrastructure.QuestionaireTemplateIdParameterName, request.QuestionaireTemplateId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, AnswerInfrastructure.GetReportListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        item = new FourP
                        {
                            FounrPId = dataReader.GetUnsignedIntegerValue("FourP"),
                            Desired = dataReader.GetDecimalValue("Desired"),
                            Current = dataReader.GetDecimalValue("Current"),
                            Max = dataReader.GetUnsignedIntegerValue("Max")
                        };
                        answers.Report.Add(item);
                    }
                    if(dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            item1 = new ReportQList
                            {
                                Title = dataReader.GetStringValue("Title"),
                                Desired = dataReader.GetDecimalValue("Desired"),
                                Current = dataReader.GetDecimalValue("Current"),
                            };
                            answers.ReportQList.Add(item1);
                        }
                    }


                }
            }

            return answers;
        }
        public async Task<List<SPResponseVM>> GetParentReportList(int LevelId)
        {

            var answers = new List<SPResponseVM>();
            SPResponseVM item = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(AnswerInfrastructure.LevelIdParameterName, LevelId),
            };

            using (var dataReader = await base.ExecuteReader(parameters, "SP_GetParentReportList", CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        item = new SPResponseVM
                        {
                            FourPId = dataReader.GetUnsignedIntegerValue("FourP"),
                            LevelId = dataReader.GetUnsignedIntegerValue("LevelId"),
                            ParentId = dataReader.GetUnsignedIntegerValue("ParentId"),
                            Desired = dataReader.GetDecimalValue("Desired"),
                            Current = dataReader.GetDecimalValue("Current"),
                            Max = dataReader.GetUnsignedIntegerValue("Max"),
                            modelTitle = dataReader.GetStringValue("LevelName")
                        };
                        answers.Add(item);
                    }
                }
            }

            return answers;
        }
        /// <summary>
        /// GetListByStakeholderId fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<Answer>> GetListByStakeholderId(StakeholderAnswerRequest request)
        {

            var answers = new List<Answer>();
            Answer item  = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(AnswerInfrastructure.StakeholderIdParameterName, request.StakeholderId),
                base.GetParameter(AnswerInfrastructure.LevelIdParameterName, request.LevelId),
                base.GetParameter(AnswerInfrastructure.QuestionaireTemplateIdParameterName, request.QuestionaireTemplateId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, AnswerInfrastructure.GetListByStakeholderIdStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        item = new Answer
                        {
                            Id = dataReader.GetUnsignedIntegerValue(AnswerInfrastructure.IdColumnName),
                            QuestionId = dataReader.GetUnsignedIntegerValue(AnswerInfrastructure.QuestionIdColumnName),
                            AnswerValue = dataReader.GetStringValue(AnswerInfrastructure.AnswerValueColumnName),
                            LevelType = dataReader.GetUnsignedIntegerValue(AnswerInfrastructure.LevelTypeColumnName),
                            StakeholderId = dataReader.GetUnsignedIntegerValue(AnswerInfrastructure.StakeholderIdColumnName),
                            LevelId = dataReader.GetUnsignedIntegerValue(AnswerInfrastructure.LevelIdIdColumnName),
                            QuestionaireTemplateId = dataReader.GetUnsignedIntegerValue(AnswerInfrastructure.QuestionaireTemplateIdColumnName),
                            Level0 = dataReader.GetStringValue(AnswerInfrastructure.Level0ColumnName),
                            Level1 = dataReader.GetStringValue(AnswerInfrastructure.Level1ColumnName),
                            Level2 = dataReader.GetStringValue(AnswerInfrastructure.Level2ColumnName),
                            Level3 = dataReader.GetStringValue(AnswerInfrastructure.Level3ColumnName),
                            Level4 = dataReader.GetStringValue(AnswerInfrastructure.Level4ColumnName),
                            Element = dataReader.GetStringValue(AnswerInfrastructure.ElementColumnName),
                            AreaLookUpTitle = dataReader.GetStringValue(AnswerInfrastructure.AreaLookUpTitleColumnName)

                        };
                        answers.Add(item);
                    }


                }
            }

            return answers;
        }


        /// <summary>
        /// Add StakeholderQuestionnaireStatus  new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<int> QuestionnaireStatusToSumbit(StakeholderQuestionnaireStatus request)
        {
            var questionnaireStatusIdParamter = base.GetParameterOut(AnswerInfrastructure.IdParameterName, SqlDbType.Int, request.Id);
            var parameters = new List<DbParameter>
            {
                questionnaireStatusIdParamter,
                base.GetParameter(AnswerInfrastructure.QuestionaireTemplateIdParameterName, request.QuestionaireTemplateId),
                base.GetParameter(AnswerInfrastructure.StakeholderIdParameterName, request.StakeholderId),
                base.GetParameter(AnswerInfrastructure.LevelIdParameterName, request.LevelId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, request.CurrentUserId)
            };


            await base.ExecuteNonQuery(parameters, AnswerInfrastructure.StakeholderQuestionnaireStatusAddStoredProcedureName, CommandType.StoredProcedure);

            request.Id = Convert.ToInt32(questionnaireStatusIdParamter.Value);

            return request.Id;
        }



        #endregion

    }
}
