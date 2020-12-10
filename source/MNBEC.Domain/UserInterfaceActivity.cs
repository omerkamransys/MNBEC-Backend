using System.Collections.Generic;
using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// Make inherits BaseDomain and respesents Vehicle Make object.
    /// </summary>
    public class UserInterfaceActivity : BaseDomain
    {
        #region Propeties
        public int UserInterfaceActivityId { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Username { get; set; }
        public int? Counter { get; set; }
        #endregion
    }
}
