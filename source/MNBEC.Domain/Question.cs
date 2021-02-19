using MNBEC.Domain.Common;


namespace MNBEC.Domain
{
    /// <summary>
    /// Question inherits BaseDomain and respesents Question object.
    /// </summary>
    public class Question : BaseDomain
    {
        #region Propeties
        public int Id { get; set; }
        public int QuestionaireTemplateId { get; set; }
        public int? Area { get; set; }
        public int? FourP { get; set; }
        public int? Responsible { get; set; }
        public int? Level { get; set; }
        public int? OrderNumber { get; set; }
        public string Level0 { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string QuestionElement { get; set; }
        public int? DesiredLevel { get; set; }

        #endregion
    }
}
