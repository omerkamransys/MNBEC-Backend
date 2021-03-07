using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.Infrastructure.Extensions;
using MNBEC.InfrastructureInterface;
using MNBEC.ViewModel.Level;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNBEC.Infrastructure
{
    /// <summary>
    /// LevelInfrastructure inherits from BaseDataAccess and implements ILevelInfrastructure. It performs all required CRUD operations on Level Entity on database.
    /// </summary>
    public class LevelInfrastructure : BaseSQLInfrastructure, ILevelInfrastructure
    {
        #region Constructor
        /// <summary>
        ///  Levelfrastructure initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public LevelInfrastructure(IConfiguration configuration, ILogger<LevelInfrastructure> logger) : base(configuration, logger)
        {
        }

        #endregion

        #region Constants
        private const string AddStoredProcedureName = "LevelAdd";
        private const string ActivateStoredProcedureName = "LevelActivate";
        private const string ReviewerLevelDeleteStoredProcedureName = "ReviewerLevelDelete";
        private const string StakeholderLevelDeleteStoredProcedureName = "StakeholderLevelDelete";
        private const string GetStoredProcedureName = "LevelGet";
        private const string GetPlanReportCommentStoredProcedureName = "GetPlanReportComment";
        private const string GetAllStoredProcedureName = "LevelGetAll";
        private const string GetListStoredProcedureName = "LevelGetList";
        private const string GetListByStakeholderIdStoredProcedureName = "LevelGetListByStakeholderId";
        private const string UpdateStoredProcedureName = "LevelUpdate";
        private const string AddPlanReportCommentStoredProcedureName = "AddPlanReportComment";
        private const string UpdatePlanReportCommentStoredProcedureName = "UpdatePlanReportComment";

        private const string LevelIdColumnName = "LevelId";
        private const string ParentIdColumnName = "ParentId";
        private const string QuestionaireTemplateIdColumnName = "QuestionaireTemplateId";
        private const string LevelNameColumnName = "LevelName";
        private const string UserIdColumnName = "UserId";
        private const string ReviewerLevelIdColumnName = "ReviewerLevelId";
        private const string StakeholderLevelIdColumnName = "StakeholderLevelId";
        private const string DeadlineDateColumnName = "DeadlineDate";
        private const string RenewalDateColumnName = "RenewalDate";
        private const string QuestionaireTemplateNameColumnName = "QuestionaireTemplateName";
        private const string IsSubmitColumnName = "IsSubmit";
        private const string WFColumnName = "WF";
        private const string StrengthsColumnName = "Strengths";
        private const string OFIColumnName = "OFI";


        private const string LevelIdParameterName = "PLevelId";
        private const string IdParameterName = "PId";
        private const string LevelNameParameterName = "PLevelName";
        private const string ParentIdParameterName = "PParentId";
        private const string QuestionaireTemplateIdParameterName = "PQuestionaireTemplateId";
        private const string DeadlineDateParameterName = "PDeadlineDate";
        private const string RenewalDateParameterName = "PRenewalDate";
        private const string WFParameterName = "PWF";
        private const string StakeholderIdParameterName = "PStakeholderId";
        private const string StrengthsParameterName = "PStrengths";
        private const string OFIParameterName = "POFI";







        private const string BulkInsertStakeholderLevelsDynamicForm =

            @"
                
                   Insert Into StakeholderLevel(UserId,LevelId,CreatedById,CreatedDate,Active)
		Values";

        private const string BulkInsertReviewerLevelsDynamicForm =

           @"
                
                   Insert Into ReviewerLevel(UserId,LevelId,CreatedById,CreatedDate,Active)
		Values";





        #endregion

        #region Interface ILevelfrastructure Implementation
        /// <summary>
        /// Add adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<int> Add(Level level)
        {
            var levelIdParamter = base.GetParameterOut(LevelInfrastructure.LevelIdParameterName, SqlDbType.Int, level.LevelId);
            var parameters = new List<DbParameter>
            {
                levelIdParamter,
                base.GetParameter(LevelInfrastructure.LevelNameParameterName, level.LevelName),
                base.GetParameter(LevelInfrastructure.ParentIdParameterName, level.ParentId),
                base.GetParameter(LevelInfrastructure.QuestionaireTemplateIdParameterName, level.QuestionaireTemplateId),
                base.GetParameter(LevelInfrastructure.DeadlineDateParameterName, level.DeadlineDate),
                base.GetParameter(LevelInfrastructure.RenewalDateParameterName, level.RenewalDate),
                base.GetParameter(LevelInfrastructure.WFParameterName, level.WF),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, level.CurrentUserId)
            };
            //TODO: Add other parameters.

            await base.ExecuteNonQuery(parameters, LevelInfrastructure.AddStoredProcedureName, CommandType.StoredProcedure);

            level.LevelId = Convert.ToInt32(levelIdParamter.Value);

            if (level.StakeholderLevels != null && level.StakeholderLevels.Count > 0)
            {
                List<string> BulkStakeholderLevels = new List<string>();
                foreach (var item in level.StakeholderLevels)
                {

                    BulkStakeholderLevels.Add(string.Format("({0},{1},{2},GETUTCDATE(),'1')",
                        item.UserId,
                        level.LevelId,
                        GetCreatedById(level.CurrentUserId)
                       ));
                }
                await base.BulkInsertSQLGeneric(BulkInsertStakeholderLevelsDynamicForm, BulkStakeholderLevels);
            }


            if (level.ReviewerLevels != null && level.ReviewerLevels.Count > 0)
            {
                List<string> BulkReviewerLevels = new List<string>();
                foreach (var item in level.ReviewerLevels)
                {

                    BulkReviewerLevels.Add(string.Format("({0},{1},{2},GETUTCDATE(),'1')",
                        item.UserId,
                        level.LevelId,
                        GetCreatedById(level.CurrentUserId)
                       ));
                }
                await base.BulkInsertSQLGeneric(BulkInsertReviewerLevelsDynamicForm, BulkReviewerLevels);
            }



            return level.LevelId;
        }


        /// <summary>
        /// Activate activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Level level)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(LevelInfrastructure.LevelIdParameterName, level.LevelId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, level.CurrentUserId),
                base.GetParameter(BaseSQLInfrastructure.ActiveParameterName, level.Active)
            };

            var returnValue = await base.ExecuteNonQuery(parameters, LevelInfrastructure.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return true;
        }

        /// <summary>
        /// Get fetch and returns queried item from database.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<Level> Get(Level level)
        {
            Level LevelItem = null;
            ReviewerLevel ReviewerLevelItem = null;
            StakeholderLevel StakeholderLevelItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(LevelInfrastructure.LevelIdParameterName, level.LevelId),
            };

            using (var dataReader = await base.ExecuteReader(parameters, LevelInfrastructure.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        LevelItem = new Level
                        {
                            LevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.LevelIdColumnName),
                            ParentId = dataReader.GetUnsignedIntegerValueNullable(LevelInfrastructure.ParentIdColumnName),
                            QuestionaireTemplateId = dataReader.GetUnsignedIntegerValueNullable(LevelInfrastructure.QuestionaireTemplateIdColumnName),
                            RenewalDate = dataReader.GetDateTimeValueNullable(LevelInfrastructure.RenewalDateColumnName),
                            DeadlineDate = dataReader.GetDateTimeValueNullable(LevelInfrastructure.DeadlineDateColumnName),
                            LevelName = dataReader.GetStringValue(LevelInfrastructure.LevelNameColumnName),
                            WF = dataReader.GetDecimalValueNullable(LevelInfrastructure.WFColumnName),
                            Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName),
                            ReviewerLevels = new List<ReviewerLevel>(),
                            StakeholderLevels = new List<StakeholderLevel>()

                        };
                    }

                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            ReviewerLevelItem = new ReviewerLevel
                            {
                                ReviewerLevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.ReviewerLevelIdColumnName),
                                LevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.LevelIdColumnName),
                                UserId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.UserIdColumnName)
                            };
                            LevelItem.ReviewerLevels.Add(ReviewerLevelItem);
                        }
                    }

                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            StakeholderLevelItem = new StakeholderLevel
                            {
                                StakeholderLevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.StakeholderLevelIdColumnName),
                                LevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.LevelIdColumnName),
                                UserId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.UserIdColumnName)
                            };
                            LevelItem.StakeholderLevels.Add(StakeholderLevelItem);
                        }
                    }

                }
            }

            return LevelItem;
        }

        /// <summary>
        /// GetAll fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task<AllResponse<Level>> GetAll(AllRequest<Level> level)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetList fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<Level>> GetList(Level level)
        {
            var levels = new List<Level>();

            var reviewerLevel = new ReviewerLevel();
            var stakeholderLevel = new StakeholderLevel();

            Level levelItem = null;
            Level CurrentlevelItem = null;
            var parameters = new List<DbParameter>
            {
            };

            using (var dataReader = await base.ExecuteReader(parameters, LevelInfrastructure.GetListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        levelItem = new Level
                        {
                            LevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.LevelIdColumnName),
                            ParentId = dataReader.GetUnsignedIntegerValueNullable(LevelInfrastructure.ParentIdColumnName),
                            QuestionaireTemplateId = dataReader.GetUnsignedIntegerValueNullable(LevelInfrastructure.QuestionaireTemplateIdColumnName),
                            RenewalDate = dataReader.GetDateTimeValueNullable(LevelInfrastructure.RenewalDateColumnName),
                            DeadlineDate = dataReader.GetDateTimeValueNullable(LevelInfrastructure.DeadlineDateColumnName),
                            LevelName = dataReader.GetStringValue(LevelInfrastructure.LevelNameColumnName),
                            WF = dataReader.GetDecimalValueNullable(LevelInfrastructure.WFColumnName),
                            Active = dataReader.GetBooleanValue(BaseSQLInfrastructure.ActiveColumnName),
                            ReviewerLevels = new List<ReviewerLevel>(),
                            StakeholderLevels = new List<StakeholderLevel>()

                        };

                        levels.Add(levelItem);
                    }

                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            reviewerLevel = new ReviewerLevel
                            {
                                ReviewerLevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.ReviewerLevelIdColumnName),
                                LevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.LevelIdColumnName),
                                UserId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.UserIdColumnName)
                            };

                            CurrentlevelItem = levels.FirstOrDefault(c => c.LevelId == reviewerLevel.LevelId);
                            CurrentlevelItem?.ReviewerLevels.Add(reviewerLevel);

                        }
                    }

                    if (dataReader.NextResult())
                    {
                        while (dataReader.Read())
                        {
                            stakeholderLevel = new StakeholderLevel
                            {
                                StakeholderLevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.StakeholderLevelIdColumnName),
                                LevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.LevelIdColumnName),
                                UserId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.UserIdColumnName)
                            };

                            CurrentlevelItem = levels.FirstOrDefault(c => c.LevelId == stakeholderLevel.LevelId);
                            CurrentlevelItem?.StakeholderLevels.Add(stakeholderLevel);
                        }
                    }


                }
            }

            return levels;
        }

        /// <summary>
        /// GetListByStakeholderId fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<StakeholderLevelModel>> GetListByStakeholderId(int stakeholderId)
        {

            var levels = new List<StakeholderLevelModel>();
            StakeholderLevelModel levelItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(LevelInfrastructure.StakeholderIdParameterName, stakeholderId),
            };

            using (var dataReader = await base.ExecuteReader(parameters, LevelInfrastructure.GetListByStakeholderIdStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        levelItem = new StakeholderLevelModel
                        {
                            LevelId = dataReader.GetUnsignedIntegerValue(LevelInfrastructure.LevelIdColumnName),
                            ParentId = dataReader.GetUnsignedIntegerValueNullable(LevelInfrastructure.ParentIdColumnName),
                            QuestionaireTemplateId = dataReader.GetUnsignedIntegerValueNullable(LevelInfrastructure.QuestionaireTemplateIdColumnName),
                            LevelName = dataReader.GetStringValue(LevelInfrastructure.LevelNameColumnName),
                            QuestionaireTemplateName = dataReader.GetStringValue(LevelInfrastructure.QuestionaireTemplateNameColumnName),
                            IsSumbit = dataReader.GetBooleanValue(LevelInfrastructure.IsSubmitColumnName)

                        };
                        levels.Add(levelItem);
                    }

                    
                }
            }

            return levels;
        }

        /// <summary>
        /// Update updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<bool> Update(Level level)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(LevelInfrastructure.LevelIdParameterName, level.LevelId),
                base.GetParameter(LevelInfrastructure.LevelNameParameterName, level.LevelName),
                base.GetParameter(LevelInfrastructure.ParentIdParameterName, level.ParentId),
                base.GetParameter(LevelInfrastructure.QuestionaireTemplateIdParameterName, level.QuestionaireTemplateId),
                base.GetParameter(LevelInfrastructure.DeadlineDateParameterName, level.DeadlineDate),
                base.GetParameter(LevelInfrastructure.RenewalDateParameterName, level.RenewalDate),
                base.GetParameter(LevelInfrastructure.WFParameterName, level.WF),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, level.CurrentUserId)
            };
            //TODO: Add other parameters.

            var returnValue = await base.ExecuteNonQuery(parameters, LevelInfrastructure.UpdateStoredProcedureName, CommandType.StoredProcedure);


            var deleteParameter = new List<DbParameter>
            {
                base.GetParameter(LevelInfrastructure.LevelIdParameterName, level.LevelId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, level.CurrentUserId)
            };

            var returnValue1 = await base.ExecuteNonQuery(deleteParameter, LevelInfrastructure.ReviewerLevelDeleteStoredProcedureName, CommandType.StoredProcedure);
            if (level.ReviewerLevels != null && level.ReviewerLevels.Count > 0)
            {
                List<string> BulkReviewerLevels = new List<string>();
                foreach (var item in level.ReviewerLevels)
                {

                    BulkReviewerLevels.Add(string.Format("({0},{1},{2},GETUTCDATE(),'1')",
                        item.UserId,
                        level.LevelId,
                        GetCreatedById(level.CurrentUserId)
                       ));
                }
                await base.BulkInsertSQLGeneric(BulkInsertReviewerLevelsDynamicForm, BulkReviewerLevels);
            }

            var deleteParameter2 = new List<DbParameter>
            {
                base.GetParameter(LevelInfrastructure.LevelIdParameterName, level.LevelId),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, level.CurrentUserId)
            };


            var returnValue2 = await base.ExecuteNonQuery(deleteParameter2, LevelInfrastructure.StakeholderLevelDeleteStoredProcedureName, CommandType.StoredProcedure);
            if (level.StakeholderLevels != null && level.StakeholderLevels.Count > 0)
            {
                List<string> BulkStakeholderLevels = new List<string>();
                foreach (var item in level.StakeholderLevels)
                {

                    BulkStakeholderLevels.Add(string.Format("({0},{1},{2},GETUTCDATE(),'1')",
                        item.UserId,
                        level.LevelId,
                        GetCreatedById(level.CurrentUserId)
                       ));
                }
                await base.BulkInsertSQLGeneric(BulkInsertStakeholderLevelsDynamicForm, BulkStakeholderLevels);
            }

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

        public async Task<PlanReportComment> GetPlanReportComment(PlanReportComment level)
        {
            PlanReportComment LevelItem = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(LevelInfrastructure.LevelIdParameterName, level.LevelId),
            };

            using (var dataReader = await base.ExecuteReader(parameters, LevelInfrastructure.GetPlanReportCommentStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        LevelItem = new PlanReportComment
                        {
                            Strengths = dataReader.GetStringValue(LevelInfrastructure.StrengthsColumnName),
                            OFI = dataReader.GetStringValue(LevelInfrastructure.OFIColumnName)

                        };
                    }

                   

                }
            }

            return LevelItem;
        }


        public async Task<int> AddPlanReportComment(PlanReportComment entity)
        {
            var levelIdParamter = base.GetParameterOut(LevelInfrastructure.IdParameterName, SqlDbType.Int, entity.Id);
            var parameters = new List<DbParameter>
            {
                levelIdParamter,
                base.GetParameter(LevelInfrastructure.StrengthsParameterName, entity.Strengths),
                base.GetParameter(LevelInfrastructure.LevelIdParameterName, entity.LevelId),
                base.GetParameter(LevelInfrastructure.OFIParameterName, entity.OFI),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, entity.CurrentUserId)
            };
            //TODO: Add other parameters.

            await base.ExecuteNonQuery(parameters, LevelInfrastructure.AddPlanReportCommentStoredProcedureName, CommandType.StoredProcedure);

            
            return entity.Id;
        }

        public async Task<bool> UpdatePlanReportComment(PlanReportComment entity)
        {
            
            var parameters = new List<DbParameter>
            {   base.GetParameter(LevelInfrastructure.IdParameterName,  entity.Id),
                base.GetParameter(LevelInfrastructure.StrengthsParameterName, entity.Strengths),
                base.GetParameter(LevelInfrastructure.LevelIdParameterName, entity.LevelId),
                base.GetParameter(LevelInfrastructure.OFIParameterName, entity.OFI),
                base.GetParameter(BaseSQLInfrastructure.CurrentUserIdParameterName, entity.CurrentUserId)
            };
            //TODO: Add other parameters.

            await base.ExecuteNonQuery(parameters, LevelInfrastructure.UpdatePlanReportCommentStoredProcedureName, CommandType.StoredProcedure);


            return true;
        }
        #endregion
    }
}
