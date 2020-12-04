using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// SMS inherits BaseDomain and respesents SMS object.
    /// </summary>
    public class SMS : BaseDomain
    {
        #region Propeties
        public uint SMSTemplateId { get; set; }
        public string SMSCode { get; set; }
        public string Message { get; set; }
        public string MessageTranslation { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        #endregion
    }
}