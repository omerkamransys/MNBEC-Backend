using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// UserType inherits BaseDomain and respesents UserType object.
    /// </summary>
    public class UserType : BaseDomain
    {
        #region Propeties
		public int UserTypeId { get; set; }
		public string UserTypeCode { get; set; }
		public string UserTypeName { get; set; }
		public string UserTypeNameTranslation { get; set; }

        #endregion
    }
}
