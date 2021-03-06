﻿using System.ComponentModel.DataAnnotations;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account
{
    /// <summary>
    /// RoleActivateRequestVM class holds basic request properties.
    /// </summary>
    public class RoleActivateRequestVM : BaseRequestVM
    {
        #region Propeties
        [Required]
        [Range(1, uint.MaxValue)]
        public uint RoleId { get; set; }
        #endregion
    }
}