using System;
using System.Dynamic;

namespace MNBEC.Domain.Common
{
    /// <summary>
    /// BaseDomain is base class with fields in all objects.
    /// </summary>
    public abstract class BaseDomain
    {
        #region Propeties
        public int? CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedById { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
        public string SearchText { get; set; }

        
        #endregion
    }
}
