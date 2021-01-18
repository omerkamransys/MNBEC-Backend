using MNBEC.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.Domain
{
    public class Answer : BaseDomain
    {
        #region Propeties
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AnswerValue { get; set; }
        public int LevelType { get; set; }
        public int StakeholderId { get; set; }
        public string Level0 { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }

        #endregion
    }
}
