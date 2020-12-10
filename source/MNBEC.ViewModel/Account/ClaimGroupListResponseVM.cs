namespace MNBEC.ViewModel.Account
{
    /// <summary>
    /// ClaimGroupListResponseVM class holds basic properties for ApplicationClaimGroup list object response.
    /// </summary>
    public class ClaimGroupListResponseVM
    {
        #region Properties and Data Members
        public int ClaimGroupId { get; set; }
        public string ClaimGroupLabel { get; set; }
        public string ClaimGroupCode { get; set; }
        #endregion
    }
}