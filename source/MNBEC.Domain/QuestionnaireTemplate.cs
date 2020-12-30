using System.Collections.Generic;
using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// QuestionnaireTemplate inherits BaseDomain and respesents QuestionnaireTemplate object.
    /// </summary>
    public class QuestionnaireTemplate : BaseDomain
    {
        #region Propeties
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<Question> QuestionsList { get; set; }

        #endregion
    }
}
