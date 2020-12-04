using System.Collections.Generic;
using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// applicationrole inherits BaseDomain and respesents Vehicle applicationrole object.
    /// </summary>
    public class ApplicationRole : BaseDomain
    {
        public ApplicationRole()
        {
            ApplicationClaims = new List<ApplicationClaim>();
        }
        #region Propeties
		    public uint RoleId { get; set; }
		    public string RoleName { get; set; }
        public string RoleNameCode { get; set; }
        public string RoleNameTranslation { get; set; }
        public string NormalizedRoleName { get; set; }
        public IList<ApplicationClaimGroup> ClaimGroups { get; set; }

        public IList<ApplicationClaim> ApplicationClaims { get; set; }
        #endregion
    }
}
