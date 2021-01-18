using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.ViewModel.Answer
{
    public class StakeholderAnswerRequest
    {
        public int StakeholderId { get; set; }
        public int LevelId { get; set; }
        public int QuestionaireTemplateId { get; set; }
    }
}
