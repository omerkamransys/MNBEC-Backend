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
        public uint UserInterfaceActivityId { get; set; }
        public uint UserId { get; set; }
        public uint MenuId { get; set; }
        public string MenuName { get; set; }
        public string Username { get; set; }
        public uint? Counter { get; set; }
        #endregion
    }
}
