using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.InfrastructureInterface;
using MNBEC.ViewModel;
using MNBEC.ViewModel.LookUp;

namespace MNBEC.Application
{
    /// <summary>
    /// QuestionApplication inherits from BaseApplication and implements IClaimGroupApplication. It provides the implementation for Question related operations.
    /// </summary>
    public class QuestionApplication : BaseApplication, IQuestionApplication
    {
        #region Constructor
        /// <summary>
        /// QuestionApplication initailizes object instance.
        /// </summary>
        /// <param name="QuestionInfrastructure"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public QuestionApplication(IQuestionInfrastructure questionInfrastructure, IConfiguration configuration, ILogger<Question> logger) : base(configuration, logger)
        {
            this.QuestionInfrastructure = questionInfrastructure;
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// QuestionInfrastructure holds the Infrastructure object.
        /// </summary>
        public IQuestionInfrastructure QuestionInfrastructure { get; }
        #endregion

        #region Interface IQuestionApplication Implementation
        /// <summary>
        /// Add calls QuestionInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<int> Add(Question question)
        {
            return await this.QuestionInfrastructure.Add(question);
        }

        /// <summary>
        /// Activate calls QuestionInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Question question)
        {
            return await this.QuestionInfrastructure.Activate(question);
        }

        /// <summary>
        /// Get calls QuestionInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<Question> Get(Question question)
        {
            return await this.QuestionInfrastructure.Get(question);
        }

        /// <summary>
        /// GetAll calls QuestionInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<AllResponse<Question>> GetAll(AllRequest<Question> question)
        {
            return await this.QuestionInfrastructure.GetAll(question);
        }



        /// <summary>
        /// GetAll calls QuestionInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<List<Question>> GetList(Question question)
        {
            return await this.QuestionInfrastructure.GetList(question);
        }

        /// <summary>
        /// Update calls QuestionInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="Question question"></param>
        /// <returns></returns>
        public async Task<bool> Update(Question question)
        {
            return await this.QuestionInfrastructure.Update(question);
        }

        /// <summary>
        /// Update calls QuestionInfrastructure to updates Order Number existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="Question question"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOrder(List<Question> questions)
        {
            return await this.QuestionInfrastructure.UpdateOrder(questions);
        }

        /// <summary>
        /// AreaLookUpGetList calls QuestionInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<List<LookUpVM>> AreaLookUpGetList()
        {
            return await this.QuestionInfrastructure.AreaLookUpGetList();
        }

        /// <summary>
        /// AreaLookUpAdd calls QuestionInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> AreaLookUpAdd(LookUpRequestVM request)
        {
            return await this.QuestionInfrastructure.AreaLookUpAdd(request);
        }

        /// <summary>
        /// AreaLookUpUpdate calls QuestionInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AreaLookUpUpdate(LookUpRequestVM request)
        {
            return await this.QuestionInfrastructure.AreaLookUpUpdate(request);
        }

        public async Task<bool> DesiredLevelChange(Question question)
        {
            return await this.QuestionInfrastructure.DesiredLevelChange(question);
        }

        

        /// <summary>
        /// AreaLookUpActivate calls QuestionInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AreaLookUpActivate(LookUpRequestVM request)
        {
            return await this.QuestionInfrastructure.AreaLookUpActivate(request);
        }

        /// <summary>
        /// Get AreaLookUp fetch and returns queried item from database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LookUpRequestVM> AreaLookUpGet(LookUpVM request)
        {
            return await this.QuestionInfrastructure.AreaLookUpGet(request);
        }


        /// <summary>
        /// FourPLookUpGetList calls QuestionInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<List<LookUpVM>> FourPLookUpGetList()
        {
            return await this.QuestionInfrastructure.FourPLookUpGetList();
        }

        /// <summary>
        /// ResponsibleLookUpGetList calls QuestionInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<List<LookUpVM>> ResponsibleLookUpGetList()
        {
            return await this.QuestionInfrastructure.ResponsibleLookUpGetList();
        }

        /// <summary>
        /// LevelLookUpGetList calls QuestionInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<List<LookUpVM>> LevelLookUpGetList()
        {
            return await this.QuestionInfrastructure.LevelLookUpGetList();
        }

        /// <summary>
        /// Add calls QuestionInfrastructure to adds bulk new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        public async Task<bool> AddBulk(List<Question> questions)
        {
            return await this.QuestionInfrastructure.AddBulk(questions);
        }
        #endregion
    }
}
