using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.InfrastructureInterface;

namespace MNBEC.Application
{
    /// <summary>
    /// QuestionnaireTemplateApplication inherits from BaseApplication and implements IClaimGroupApplication. It provides the implementation for QuestionnaireTemplate related operations.
    /// </summary>
    public class QuestionnaireTemplateApplication : BaseApplication, IQuestionnaireTemplateApplication
    {
        #region Constructor
        /// <summary>
        /// QuestionnaireTemplateApplication initailizes object instance.
        /// </summary>
        /// <param name="QuestionnaireTemplateInfrastructure"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public QuestionnaireTemplateApplication(IQuestionnaireTemplateInfrastructure questionnaireTemplateInfrastructure, IConfiguration configuration, ILogger<QuestionnaireTemplateApplication> logger) : base(configuration, logger)
        {
            this.QuestionnaireTemplateInfrastructure = questionnaireTemplateInfrastructure;
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// QuestionnaireTemplateInfrastructure holds the Infrastructure object.
        /// </summary>
        public IQuestionnaireTemplateInfrastructure QuestionnaireTemplateInfrastructure { get; }
        #endregion

        #region Interface IQuestionnaireTemplateApplication Implementation
        /// <summary>
        /// Add calls QuestionnaireTemplateInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="questionnaireTemplate"></param>
        /// <returns></returns>
        public async Task<int> Add(QuestionnaireTemplate questionnaireTemplate)
        {
            return await this.QuestionnaireTemplateInfrastructure.Add(questionnaireTemplate);
        }

        /// <summary>
        /// Activate calls QuestionnaireTemplateInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="questionnaireTemplate"></param>
        /// <returns></returns>
        public async Task<bool> Activate(QuestionnaireTemplate questionnaireTemplate)
        {
            return await this.QuestionnaireTemplateInfrastructure.Activate(questionnaireTemplate);
        }

        /// <summary>
        /// Get calls QuestionnaireTemplateInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="questionnaireTemplate"></param>
        /// <returns></returns>
        public async Task<QuestionnaireTemplate> Get(QuestionnaireTemplate questionnaireTemplate)
        {
            return await this.QuestionnaireTemplateInfrastructure.Get(questionnaireTemplate);
        }

        /// <summary>
        /// GetAll calls QuestionnaireTemplateInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="questionnaireTemplate"></param>
        /// <returns></returns>
        public async Task<AllResponse<QuestionnaireTemplate>> GetAll(AllRequest<QuestionnaireTemplate> questionnaireTemplate)
        {
            return await this.QuestionnaireTemplateInfrastructure.GetAll(questionnaireTemplate);
        }



        /// <summary>
        /// GetAll calls QuestionnaireTemplateInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="questionnaireTemplate"></param>
        /// <returns></returns>
        public async Task<List<QuestionnaireTemplate>> GetList(QuestionnaireTemplate questionnaireTemplate)
        {
            return await this.QuestionnaireTemplateInfrastructure.GetList(questionnaireTemplate);
        }

        /// <summary>
        /// Update calls QuestionnaireTemplateInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="questionnaireTemplate"></param>
        /// <returns></returns>
        public async Task<bool> Update(QuestionnaireTemplate questionnaireTemplate)
        {
            return await this.QuestionnaireTemplateInfrastructure.Update(questionnaireTemplate);
        }
        #endregion
    }
}
