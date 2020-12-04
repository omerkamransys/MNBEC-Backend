using System.Collections.Generic;
using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// ApplicationClaimGroup inherits BaseDomain and respesents Vehicle ApplicationClaimGroup object.
    /// </summary>
    public class ApplicationClaimGroup : BaseDomain
    {
        #region Propeties
        public uint ClaimGroupId { get; set; }
        public string ClaimGroupLabel { get; set; }
        public string ClaimGroupCode { get; set; }
        public string ClaimGroupLabelTranslation { get; set; }

        public IList<ApplicationClaim> Claims { get; set; }

        #endregion
    }
}
