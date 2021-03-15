using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.Domain.Common;
using MNBEC.InfrastructureInterface;
using MNBEC.ViewModel;
using MNBEC.ViewModel.Answer;
using MNBEC.ViewModel.ReportResponseVM;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// GetReportList calls AnswerInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<ReportResponseVM> GetReportList(StakeholderAnswerRequest request)
        {
            return await this.AnswerInfrastructure.GetReportList(request);
        }
        /// <summary>
        /// QuestionnaireStatusToSumbit calls AnswerInfrastructure to fetch and returns queried list of items with specific fields from database.
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<int> QuestionnaireStatusToSumbit(StakeholderQuestionnaireStatus request)
        {
            return await this.AnswerInfrastructure.QuestionnaireStatusToSumbit(request);
        }
        List<SPResponseVM> result;
        public async Task<ParentReportResponseVM> GetParentReportList(int LevelId)
        {
            result = await this.AnswerInfrastructure.GetParentReportList(LevelId);

            ParentReportResponseVM modeledData = getModeledData(LevelId,"",0);
            return modeledData;
        }

        private ParentReportResponseVM getModeledData(int levelId,string modelName,decimal wf)
        {
            var imidiateChild = result.Where(x => x.ParentId == levelId);
            if(imidiateChild != null && imidiateChild.Count() > 0)
            {
                ParentReportResponseVM obj = new ParentReportResponseVM();
                obj.ChilderenList = new List<ParentReportResponseVM>();
                obj.LevelId = levelId;
                obj.modelTitle = modelName;
                obj.wf = wf;
                obj.TotalFourPReport.FounrPId = 0;
                obj.TotalFourPReport.Max = 4;
                List<int> processedChild = new List<int>();
                int divider = 0;
                foreach (var child in imidiateChild)
                {
                    if(!processedChild.Contains(child.LevelId))
                    {
                        var childres = getModeledData(child.LevelId,child.modelTitle,child.wf);
                        childres.ParentId = levelId;
                        obj.ChilderenList.Add(childres);
                        processedChild.Add(child.LevelId);
                        obj.TotalFourPReport.Desired += childres.TotalFourPReport.Desired;
                        obj.TotalFourPReport.Current += childres.TotalFourPReport.Current;
                        divider++;
                    }
                    
                }
                if(divider>0 )
                {
                    obj.TotalFourPReport.Desired = obj.TotalFourPReport.Desired > 0 ? (obj.TotalFourPReport.Desired/divider) : 0;
                    obj.TotalFourPReport.Current = obj.TotalFourPReport.Current > 0 ? (obj.TotalFourPReport.Current/ divider) : 0;
                }
                return obj;
            } 
            else
            {
                ParentReportResponseVM obj = new ParentReportResponseVM();
                obj.LevelId = levelId;
                obj.modelTitle = modelName;
                obj.wf = wf;
                var FPList = result.Where(x => x.LevelId == levelId && x.FourPId > 0);
                obj.TotalFourPReport.FounrPId = 0;
                obj.TotalFourPReport.Max = 4;
                foreach (var child in FPList)
                {
                    if(child.FourPId > 0)
                    {
                        FourP fp = new FourP();
                        fp.FounrPId = child.FourPId;
                        fp.Max = 4;
                        fp.Desired = child.Desired;
                        fp.Current = child.Current;
                        obj.FourPReport.Add(fp);

                        obj.TotalFourPReport.Desired += fp.Desired;
                        obj.TotalFourPReport.Current += fp.Current;
                    }
                    
                }
                if(FPList !=null && FPList.Count()>0)
                {

                    obj.TotalFourPReport.Desired = obj.TotalFourPReport.Desired > 0 ? (obj.TotalFourPReport.Desired / FPList.Count()) : 0;
                    obj.TotalFourPReport.Current = obj.TotalFourPReport.Current > 0 ? (obj.TotalFourPReport.Current / FPList.Count()) : 0;
                }
                
                return obj;
            }

        }




        #endregion
    }
}
