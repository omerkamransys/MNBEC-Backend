using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// Employee inherits BaseDomain and respesents Vehicle Employee object.
    /// </summary>
    public class Employee : BaseDomain
    {
        #region Propeties
		public int EmployeeId { get; set; }
		public int UserTypeId { get; set; }

        #endregion
    }
}
