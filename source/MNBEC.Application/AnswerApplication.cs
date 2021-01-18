using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.InfrastructureInterface;
using MNBEC.ViewModel;
using MNBEC.ViewModel.Answer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MNBEC.Application
{
    public class AnswerApplication : BaseApplication, IAnswerApplication
    {
        #region Constructor
        /// <summary>
        /// AnswerApplication initailizes object instance.
        /// </summary>
        /// <param name="answerInfrastructure"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public AnswerApplication(IAnswerInfrastructure answerInfrastructure, IConfiguration configuration, ILogger<Answer> logger) : base(configuration, logger)
        {
            this.AnswerInfrastructure = answerInfrastructure;
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// AnswerInfrastructure holds the Infrastructure object.
        /// </summary>
        public IAnswerInfrastructure AnswerInfrastructure { get; }
        #endregion

        #region Interface AnswerInfrastructure Implementation
        /// <summary>
        /// Add calls AnswerInfrastructure to adds new object in database and returns provided ObjectId.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<int> Add(Answer answer)
        {
            return await this.AnswerInfrastructure.Add(answer);
        }

        /// <summary>
        /// Activate calls AnswerInfrastructure to activate/deactivate provided record and returns true if action was successfull.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<bool> Activate(Answer answer)
        {
            return await this.AnswerInfrastructure.Activate(answer);
        }

        /// <summary>
        /// Get calls AnswerInfrastructure to fetch and returns queried item from database.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<Answer> Get(Answer answer)
        {
            return await this.AnswerInfrastructure.Get(answer);
        }

        /// <summary>
        /// GetAll calls AnswerInfrastructure to fetch and returns queried list of items from database.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<AllResponse<Answer>> GetAll(AllRequest<Answer> answer)
        {
            return await this.AnswerInfrastructure.GetAll(answer);
        }



        /// <summary>
        /// GetAll calls AnswerInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<List<Answer>> GetList(Answer answer)
        {
            return await this.AnswerInfrastructure.GetList(answer);
        }

        /// <summary>
        /// Update calls AnswerInfrastructure to updates existing object in database and returns true if action was successfull.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<bool> Update(Answer answer)
        {
            return await this.AnswerInfrastructure.Update(answer);
        }


        /// <summary>
        /// LevelTypeLookUpGetList calls AnswerInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<LookUpVM>> LevelTypeLookUpGetList()
        {
            return await this.AnswerInfrastructure.LevelTypeLookUpGetList();
        }

        /// <summary>
        /// GetListByStakeholderId calls AnswerInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<List<Answer>> GetListByStakeholderId(StakeholderAnswerRequest request)
        {
            return await this.AnswerInfrastructure.GetListByStakeholderId(request);
        }
        

        #endregion
    }
}
