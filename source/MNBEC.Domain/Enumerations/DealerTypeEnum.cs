using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// DealerTypeEnum hold values from DealerType Table with Id Mapping.
    /// </summary>
    public enum DealerTypeEnum
    {
        [Description("OWNER")]
        PrimaryOwner = 1,

        [Description("DEALER")]
        Dealer = 2,

        [Description("PARTNER")]
        Partner = 3
    }
}
