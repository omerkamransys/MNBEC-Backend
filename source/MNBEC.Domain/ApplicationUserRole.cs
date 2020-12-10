using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// applicationuserrole inherits BaseDomain and respesents Vehicle applicationuserrole object.
    /// </summary>
    public class ApplicationUserRole : BaseDomain
    {
        #region Propeties
		public int UserRoleId { get; set; }
		public int UserId { get; set; }
		public int RoleId { get; set; }

        #endregion
    }
}
