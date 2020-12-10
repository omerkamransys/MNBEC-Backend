using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account
{
    /// <summary>
    /// ClaimGroupAllRequestVM class holds basic properties for ApplicationClaimGroup get all request.
    /// </summary>
    public class ClaimGroupAllRequestVM : BaseAllRequestVM
    {
        #region Properties and Data Members
        public int ApplicationClaimGroupId { get; set; }
        public string ApplicationClaimGroupName { get; set; }
        public string ApplicationClaimGroupCode { get; set; }
        #endregion
    }
}