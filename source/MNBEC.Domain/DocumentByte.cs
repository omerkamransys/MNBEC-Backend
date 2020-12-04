using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// Model inherits BaseDomain and respesents Vehicle Centre object.
    /// </summary>
    public class DocumentByte : BaseDomain
    {
        #region Propeties
        public uint DocumentId { get; set; }
        
        public byte[] DocumentsByte { get; set; }

        #endregion
    }
}
