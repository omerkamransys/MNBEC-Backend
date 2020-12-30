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
using MNBEC.ViewModel;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// QuestionInfrastructure inherits from BaseDataAccess and implements IQuestionInfrastructure. It performs all required CRUD operations on Question Entity on database.
    /// </summary>
    public class QuestionInfrastructure : BaseSQLInfrastructure, IQuestionInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  QuestionInfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public QuestionInfrastructure(IConfiguration configuration, ILogger<QuestionInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants
        private const string BulkUpdateDynamicForm =

           @"
                   ";

        private const string AddStoredProcedureName = "QuestionAdd";
        private const string GetStoredProcedureName = "QuestionGet";
        private const string UpdateStoredProcedureName = "QuestionUpdate";
        private const string ActivateStoredProcedureName = "QuestionActivate";
        private const string AreaLookUpGetListProcedureName = "AreaLookUpGetList";
        private const string FourPLookUpGetListProcedureName = "FourPLookUpGetList";
        private const string ResponsibleLookUpGetListProcedureName = "ResponsibleLookUpGetList";
        private const string LevelLookUpGetListProcedureName = "LevelLookUpGetList";


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

        
        private const string IdParameterName = "PId";
        private const string QuestionaireTemplateIdParameterName = "PQuestionaireTemplateId";
        private const string AreaParameterName = "PArea";
        private const string FourPParameterName = "PFourP";
        private const string ResponsibleParameterName = "PResponsible";
        private const string LevelParameterName = "PLevel";
        private const string OrderNumberParameterName = "POrderNumber";
        private const string Level0ParameterName = "PLevel0";
        private const string Level1ParameterName = "PLevel1";
        private const string Level2ParameterName = "PLevel2";
        private const string Level3ParameterName = "PLevel3";
        #endregion

        #region Interface IQuestionInfrastructure Implementation

        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<int> Add(Question question)
        {
            var questionIdParamter = base.GetParameterOut(QuestionInfrastructure.IdParameterName, SqlDbType.Int, question.Id);
            var parameters = new List<DbParameter>
            {
                questionIdParamter,
                base.GetParameter(QuestionInfrastructure.QuestionaireTemplateIdParameterName, question.QuestionaireTemplateId),
                base.GetParameter(QuestionInfrastructure.AreaParameterName, question.Area),
                base.GetParameter(QuestionInfrastructure.FourPParameterName, question.FourP),
                base.GetParameter(QuestionInfrastructure.ResponsibleParameterName, question.Responsible),
                base.GetParameter(QuestionInfrastructure.LevelParameterName, question.Level),
                base.GetParameter(QuestionInfrastructure.Level0ParameterName, question.Level0),
                base.GetParameter(QuestionInfrastructure.Level1ParameterName, question.Level1),
                base.GetParameter(QuestionInfrastructure.Level2ParameterName, question.Level2),
                base.GetParameter(QuestionInfrastructure.Level3ParameterName, question.Level3),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, question.CurrentUserId)
            };


            await base.ExecuteNonQuery(parameters, QuestionInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            question.Id = Convert.ToInt32(questionIdParamter.Value);

            return question.Id;
        }

        /// <summary>
        /// Activate activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Question question)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(QuestionInfrastructure.IdParameterName, question.Id),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, question.Active),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, question.CurrentUserId)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, QuestionInfrastructure.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return true;
        }

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<Question> Get(Question question)
        {
            Question questionItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(QuestionInfrastructure.IdParameterName, question.Id)
            };

            using (var dataReader = await base.ExecuteReader(parameters, QuestionInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        questionItem = new Question
                        {
                            Id = dataReader.GetUnsignedIntegerValue(QuestionInfrastructure.IdColumnName),
                            QuestionaireTemplateId = dataReader.GetUnsignedIntegerValue(QuestionInfrastructure.QuestionaireTemplateIdColumnName),
                            Area = dataReader.GetUnsignedIntegerValueNullable(QuestionInfrastructure.AreaColumnName),
                            FourP = dataReader.GetUnsignedIntegerValueNullable(QuestionInfrastructure.FourPColumnName),
                            Responsible = dataReader.GetUnsignedIntegerValueNullable(QuestionInfrastructure.ResponsibleColumnName),
                            Level = dataReader.GetUnsignedIntegerValueNullable(QuestionInfrastructure.LevelColumnName),
                            Level0 = dataReader.GetStringValue(QuestionInfrastructure.Level0ColumnName),
                            Level1 = dataReader.GetStringValue(QuestionInfrastructure.Level1ColumnName),
                            Level2 = dataReader.GetStringValue(QuestionInfrastructure.Level2ColumnName),
                            Level3 = dataReader.GetStringValue(QuestionInfrastructure.Level3ColumnName),
                            OrderNumber = dataReader.GetUnsignedIntegerValueNullable(QuestionInfrastructure.OrderNumberColumnName),
                            Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName)
                        };
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return questionItem;
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public Task<AllResponse<Question>> GetAll(AllRequest<Question> question)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public Task<List<Question>> GetList(Question question)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<bool> Update(Question question)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(QuestionInfrastructure.IdParameterName, question.Id),
                base.GetParameter(QuestionInfrastructure.QuestionaireTemplateIdParameterName, question.QuestionaireTemplateId),
                base.GetParameter(QuestionInfrastructure.AreaParameterName, question.Area),
                base.GetParameter(QuestionInfrastructure.FourPParameterName, question.FourP),
                base.GetParameter(QuestionInfrastructure.ResponsibleParameterName, question.Responsible),
                base.GetParameter(QuestionInfrastructure.LevelParameterName, question.Level),
                base.GetParameter(QuestionInfrastructure.Level0ParameterName, question.Level0),
                base.GetParameter(QuestionInfrastructure.Level1ParameterName, question.Level1),
                base.GetParameter(QuestionInfrastructure.Level2ParameterName, question.Level2),
                base.GetParameter(QuestionInfrastructure.Level3ParameterName, question.Level3),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, question.CurrentUserId),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, question.CurrentUserId)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, QuestionInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);

            return true;
        }

        public async Task<bool> UpdateOrder(List<Question> questions)
        {
            List<string> BulkValues = new List<string>();

            foreach (var item in questions)
            {

                BulkValues.Add(string.Format("UPDATE Question SET OrderNumber = {0} " +
                    "  WHERE Id = {1} ;",
                   item.OrderNumber,
                   item.Id
                  ));
            }

            await base.BulkUpdateSQLGeneric(BulkUpdateDynamicForm, BulkValues);


            return true;
        }

        /// <summary>
        /// AreaLookUpGetList fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LookUpVM>> AreaLookUpGetList()
        {
            var list = new List<LookUpVM>();
            LookUpVM lookUpVM = null;
            var parameters = new List<DbParameter>
            {
                
            };

            using (var dataReader = await base.ExecuteReader(parameters, QuestionInfrastructure.AreaLookUpGetListProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        lookUpVM = new LookUpVM
                        {
                            Id = dataReader.GetUnsignedIntegerValue(QuestionInfrastructure.IdColumnName),
                            Title = dataReader.GetStringValue(QuestionInfrastructure.TitleColumnName)
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
        /// FourPLookUpGetList fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LookUpVM>> FourPLookUpGetList()
        {
            var list = new List<LookUpVM>();
            LookUpVM lookUpVM = null;
            var parameters = new List<DbParameter>
            {

            };

            using (var dataReader = await base.ExecuteReader(parameters, QuestionInfrastructure.FourPLookUpGetListProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        lookUpVM = new LookUpVM
                        {
                            Id = dataReader.GetUnsignedIntegerValue(QuestionInfrastructure.IdColumnName),
                            Title = dataReader.GetStringValue(QuestionInfrastructure.TitleColumnName)
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
        /// ResponsibleLookUp fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LookUpVM>> ResponsibleLookUpGetList()
        {
            var list = new List<LookUpVM>();
            LookUpVM lookUpVM = null;
            var parameters = new List<DbParameter>
            {

            };

            using (var dataReader = await base.ExecuteReader(parameters, QuestionInfrastructure.ResponsibleLookUpGetListProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        lookUpVM = new LookUpVM
                        {
                            Id = dataReader.GetUnsignedIntegerValue(QuestionInfrastructure.IdColumnName),
                            Title = dataReader.GetStringValue(QuestionInfrastructure.TitleColumnName)
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
        /// LevelLookUpGetList fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LookUpVM>> LevelLookUpGetList()
        {
            var list = new List<LookUpVM>();
            LookUpVM lookUpVM = null;
            var parameters = new List<DbParameter>
            {

            };

            using (var dataReader = await base.ExecuteReader(parameters, QuestionInfrastructure.LevelLookUpGetListProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        lookUpVM = new LookUpVM
                        {
                            Id = dataReader.GetUnsignedIntegerValue(QuestionInfrastructure.IdColumnName),
                            Title = dataReader.GetStringValue(QuestionInfrastructure.TitleColumnName)
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

        #endregion
    }
}
