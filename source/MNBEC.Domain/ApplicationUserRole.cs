using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// applicationuserrole inherits BaseDomain and respesents Vehicle applicationuserrole object.
    /// </summary>
    public class ApplicationUserRole : BaseDomain
    {
        #region Propeties
		public uint UserRoleId { get; set; }
		public uint UserId { get; set; }
		public uint RoleId { get; set; }

        #endregion
    }
}
