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
        public uint? CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public uint? ModifiedById { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
        public string SearchText { get; set; }

        
        #endregion
    }
}
