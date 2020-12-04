using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// Employee inherits BaseDomain and respesents Vehicle Employee object.
    /// </summary>
    public class Employee : BaseDomain
    {
        #region Propeties
		public uint EmployeeId { get; set; }
		public uint UserTypeId { get; set; }

        #endregion
    }
}
