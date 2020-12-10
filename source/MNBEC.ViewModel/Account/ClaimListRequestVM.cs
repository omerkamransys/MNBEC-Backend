﻿using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account
{
    /// <summary>
    /// ClaimRequestVM class holds basic properties for general ApplicationClaim request.
    /// </summary>
    public class ClaimListRequestVM : BaseRequestVM
    {
        #region Properties and Data Members
        public int ClaimId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimLabel { get; set; }
        public int ClaimGroupId { get; set; }
        #endregion
    }
}