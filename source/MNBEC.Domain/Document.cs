using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// Model inherits BaseDomain and respesents Vehicle Centre object.
    /// </summary>
    public class Documents : BaseDomain
    {
        #region Propeties
        public int DocumentId { get; set; }
        public int DocumentTypeId { get; set; }
        public string ClientName { get; set; }
        public string DocumentTypeCode { get; set; }
        public string DocumentName { get; set; }
        public string DisplayName { get; set; }
        public string DocumentExtension { get; set; }
        public string DocumentPath { get; set; }
        public byte[] DocumentByte { get; set; }
        public bool SecureDocument { get; set; }
        public int AuctionId { get; set; }
        public bool PrimaryDocument { get; set; }


        #endregion
    }
}
