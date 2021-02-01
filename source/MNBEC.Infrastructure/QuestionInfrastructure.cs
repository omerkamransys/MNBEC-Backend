﻿using Microsoft.Extensions.Configuration;
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
using MNBEC.ViewModel.LookUp;

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

        private const string AddAreaLookUpStoredProcedureName = "AreaLookUpAdd";
        private const string UpdateAreaLookUpStoredProcedureName = "AreaLookUpUpdateUpdate";
        private const string ActivateAreaLookUpStoredProcedureName = "AreaLookUpActivate";
        private const string GetAreaLookUpStoredProcedureName = "AreaLookUpGet";


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
        private const string Level4ParameterName = "PLevel4";
        private const string TitleParameterName = "PTitle";
        private const string ElementParameterName = "PElement";
        #endregion

        private const string BulkInsertQuestions =

            @"
                
                   Insert Into Question
				(QuestionaireTemplateId, Area, FourP, Responsible, Level, Level0, Level1, Level2, Level3, Level4, CreatedById,  CreatedDate,  Active, Element)
		Values";

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
                base.GetParameter(QuestionInfrastructure.Level4ParameterName, question.Level4),
                base.GetParameter(QuestionInfrastructure.ElementParameterName, question.QuestionElement),
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
                            Level4 = dataReader.GetStringValue(QuestionInfrastructure.Level4ColumnName),
                            QuestionElement = dataReader.GetStringValue(QuestionInfrastructure.ElementColumnName),
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
                base.GetParameter(QuestionInfrastructure.Level4ParameterName, question.Level4),
                base.GetParameter(QuestionInfrastructure.ElementParameterName, question.QuestionElement),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, question.CurrentUserId),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, question.Active)
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
        /// Add Area adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> AreaLookUpAdd(LookUpRequestVM request)
        {
            var areaLookIdParamter = base.GetParameterOut(QuestionInfrastructure.IdParameterName, SqlDbType.Int, request.Id);
            var parameters = new List<DbParameter>
            {
                areaLookIdParamter,
                base.GetParameter(QuestionInfrastructure.TitleParameterName, request.Title),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, request.CurrentUserId)
            };


            await base.ExecuteNonQuery(parameters, QuestionInfrastructure.AddAreaLookUpStoredProcedureName, CommandType.StoredProcedure);

            request.Id = Convert.ToInt32(areaLookIdParamter.Value);

            return request.Id;
        }

        /// <summary>
        /// Update AreaLookUp updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AreaLookUpUpdate(LookUpRequestVM request)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(QuestionInfrastructure.IdParameterName, request.Id),
                base.GetParameter(QuestionInfrastructure.TitleParameterName, request.Title),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, request.CurrentUserId),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, request.Active)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, QuestionInfrastructure.UpdateAreaLookUpStoredProcedureName, CommandType.StoredProcedure);

            return true;
        }

        /// <summary>
        /// Activate AreaLookUp activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AreaLookUpActivate(LookUpRequestVM request)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(QuestionInfrastructure.IdParameterName, request.Id),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, request.Active),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, request.CurrentUserId)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, QuestionInfrastructure.ActivateAreaLookUpStoredProcedureName, CommandType.StoredProcedure);

            return true;
        }

        /// <summary>
        /// Get AreaLookUp fetch and returns queried item from database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LookUpRequestVM> AreaLookUpGet(LookUpVM request)
        {
            LookUpRequestVM areaLookUpItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(QuestionInfrastructure.IdParameterName, request.Id)
            };

            using (var dataReader = await base.ExecuteReader(parameters, QuestionInfrastructure.GetAreaLookUpStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        areaLookUpItem = new LookUpRequestVM
                        {
                            Id = dataReader.GetUnsignedIntegerValue(QuestionInfrastructure.IdColumnName),
                            Title = dataReader.GetStringValue(QuestionInfrastructure.TitleColumnName),
                            Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName)
                        };
                    }
                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return areaLookUpItem;
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

        public async Task<bool> AddBulk(List<Question> questions)
        {
            List<string> BulkQuestions = new List<string>();
            foreach (var item in questions)
            {
                BulkQuestions.Add(string.Format("({0},{1},{2},{3},{4},'{5}','{6}','{7}','{8}','{9}',{10},GETUTCDATE(),'1',{11})",
                    item.QuestionaireTemplateId,
                    GetCreatedById(item.Area),
                    GetCreatedById(item.FourP),
                    GetCreatedById(item.Responsible),
                    GetCreatedById(item.Level),
                    item.Level0,
                    item.Level1,
                    item.Level2,
                    item.Level3,
                    item.Level4,
                    GetCreatedById(item.CurrentUserId),
                    item.QuestionElement
                   ));
            }
            await base.BulkInsertSQLGeneric(BulkInsertQuestions, BulkQuestions);

            return true;
        }

        private static string GetCreatedById(int? id)
        {
            string CreatedById = null;

            if (id == null)
            {
                CreatedById = "null";
            }
            else
            {
                CreatedById = id.ToString();
            }

            return CreatedById;
        }

        #endregion
    }
}
