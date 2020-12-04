using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account

{
    /// <summary>
    /// BaseRequestVM class holds basic request properties.
    /// </summary>
    public class RoleRequestVM : BaseRequestVM
    {
        #region Propeties
        public uint RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<ClaimGroupResponseVM> ClaimGroups { get; set; }
        #endregion
    }
}