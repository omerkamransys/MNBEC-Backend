using System.Dynamic;

namespace MNBEC.ViewModel.Common
{
    /// <summary>
    /// BaseRequestVM class holds basic request properties.
    /// </summary>
    public class BaseRequestVM
    {
        #region Properties and Data Members
        public bool Active { get; set; }
        public int? CurrentUserId { get; set; }
        #endregion
    }
}