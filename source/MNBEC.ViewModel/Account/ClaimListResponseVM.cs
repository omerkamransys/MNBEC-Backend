﻿namespace MNBEC.ViewModel.Account
{
    /// <summary>
    /// ClaimListResponseVM class holds basic properties for ApplicationClaim list object response.
    /// </summary>
    public class ClaimListResponseVM
    {
        #region Properties and Data Members
        public uint ClaimId { get; set; }
        //public uint ClaimGroupId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimCode { get; set; }
        public string ClaimLabel { get; set; }
        public string ClaimLabelTranslation { get; set; }
        //public string ClaimLabel { get; set; }
        #endregion
    }
}