using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// Email inherits BaseDomain and respesents Email object.
    /// </summary>
    public class Email : BaseDomain
    {
        #region Propeties
        public string RequesterEmail { get; set; }
        public string RequesterName { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string From { get; set; }
        public bool HasAttachment { get; set; }
        #endregion
    }
}