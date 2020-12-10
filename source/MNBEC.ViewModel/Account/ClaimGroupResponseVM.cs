using System.Collections.Generic;

namespace MNBEC.ViewModel.Account
{
    /// <summary>
    /// ClaimGroupAllResponseVM class holds basic properties for ApplicationClaimGroup object Get all response.
    /// </summary>
    public class ClaimGroupResponseVM
    {
        #region Properties and Data Members
        public int ClaimGroupId { get; set; }
        public string ClaimGroupLabel { get; set; }
        public string ClaimGroupCode { get; set; }

        public IList<ClaimResponseVM> Claims { get; set; }
        #endregion
    }
}