using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account

{
    /// <summary>
    /// RoleUpdateRequestVM class holds basic request properties.
    /// </summary>
    public class RoleUpdateRequestVM : BaseRequestVM
    {
        #region Propeties
        [Required]
        [Range(1, uint.MaxValue)]
        public uint RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleNameTranslation { get; set; }
        public string RoleNameCode { get; set; }
        public IList<ClaimGroupResponseVM> ClaimGroups { get; set; }
        #endregion
    }
}