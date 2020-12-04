using System;

namespace MNBEC.ViewModel.Common
{
    /// <summary>
    /// BaseResponseVM class holds basic properties for full object response.
    /// </summary>
    public class BaseResponseVM
    {
        #region Properties and Data Members
        public uint? CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public uint? ModifiedById { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
    
        public bool Active { get; set; }
        #endregion
    }
}