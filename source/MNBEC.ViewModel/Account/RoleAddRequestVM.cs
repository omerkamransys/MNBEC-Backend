using System.ComponentModel.DataAnnotations;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account

{
    /// <summary>
    /// BaseRequestVM class holds basic request properties.
    /// </summary>
    public class RoleAddRequestVM : BaseRequestVM
    {
        #region Propeties
        public uint RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleNameCode { get; set; }
        public string RoleNameTranslation { get; set; }
        #endregion
    }
}