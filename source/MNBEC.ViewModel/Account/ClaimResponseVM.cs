namespace MNBEC.ViewModel.Account
{
    /// <summary>
    /// ClaimAllResponseVM class holds basic properties for ApplicationClaim object Get all response.
    /// </summary>
    public class ClaimResponseVM
    {
        #region Properties and Data Members
        public int ClaimId { get; set; }
        public int ClaimGroupId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimLabel { get; set; }
        public string ClaimCode { get; set; }
        public bool Active { get; set; }
        #endregion
    }
}