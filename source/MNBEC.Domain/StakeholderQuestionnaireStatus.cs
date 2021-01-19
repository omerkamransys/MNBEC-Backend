using MNBEC.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.Domain
{
    public class StakeholderQuestionnaireStatus : BaseDomain
    {
        #region Propeties
        public int Id { get; set; }
        public int QuestionaireTemplateId { get; set; }
        public bool IsSubmit { get; set; }
        public int StakeholderId { get; set; }
        public int LevelId { get; set; }

        #endregion
    }
}
