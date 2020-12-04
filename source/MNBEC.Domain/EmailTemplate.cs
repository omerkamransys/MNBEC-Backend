using System.Collections.Generic;
using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// EmailTemplate inherits BaseDomain and respesents EmailTemplate object.
    /// </summary>
    public class EmailTemplate : BaseDomain
    {
        #region Propeties
        public uint EmailTemplateId { get; set; }
        public string EmailTemplateCode { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string SubjectTranslation { get; set; }
        public string MessageTranslation { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string From { get; set; }
        public List<byte[]> Attachments { get; set; }
        #endregion
    }
}