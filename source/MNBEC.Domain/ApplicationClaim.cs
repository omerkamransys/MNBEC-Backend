using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    /// <summary>
    /// A model for the database table claims
    /// </summary>
    public class ApplicationClaim : BaseDomain
    {
        /// <summary>
        /// An auto-incremented ID that is meant to minify the size of a claim Eg 1, 2, 3, 4 ...
        /// </summary>
        public uint ClaimId { get; set; }

        /// <summary>
        /// A Claim using the format [Controller Name].[Api] Eg Values.Delete
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string ClaimLabel { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public uint ClaimGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClaimCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ClaimLabelTranslation { get; set; }
    }
}
