using MNBEC.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.Domain
{
    public class Level : BaseDomain
    {
        #region Propeties
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public int? ParentId { get; set; }
        public int? QuestionaireTemplateId { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public decimal? WF { get; set; }

        public List<StakeholderLevel> StakeholderLevels { get; set; }
        public List<ReviewerLevel> ReviewerLevels { get; set; }

        #endregion
    }
}
