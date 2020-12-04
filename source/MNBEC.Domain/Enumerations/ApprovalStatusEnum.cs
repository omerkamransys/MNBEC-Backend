using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// ApprovalStatusEnum hold values from ApprovalStatus Table with Id Mapping.
    /// </summary>
    public enum ApprovalStatusEnum
    {
        [Description("APRV")]
        Approved = 1,

        [Description("RJCT")]
        Rejected = 2,

        [Description("REQ")]
        Requested = 3,
    }
}
