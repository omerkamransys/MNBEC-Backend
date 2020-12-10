using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account
{
    /// <summary>
    /// ClaimGroupRequestVM class holds basic properties for general ApplicationClaimGroup request.
    /// </summary>
    public class ClaimGroupRequestVM : BaseRequestVM
    {
        #region Properties and Data Members
        public int ClaimGroupId { get; set; }
        public string ClaimGroupLabel { get; set; }
        public string ClaimGroupCode { get; set; }
        #endregion
    }
}