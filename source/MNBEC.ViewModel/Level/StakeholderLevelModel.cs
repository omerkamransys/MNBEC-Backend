using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.ViewModel.Level
{
    public class StakeholderLevelModel
    {
        #region Propeties
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public int? ParentId { get; set; }
        public int? QuestionaireTemplateId { get; set; }
        public string QuestionaireTemplateName { get; set; }
        public bool IsSumbit { get; set; }
        
        #endregion
    }
}
