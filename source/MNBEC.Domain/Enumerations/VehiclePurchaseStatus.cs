using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// InspectionStatusEnum hold values from InspectionStatus Table with Id Mapping.
    /// </summary>
    public enum VehiclePurchaseStatusEnum : uint
    {
        [Description("PUR")]
        Purchased = 1,

        [Description("AUCREATED")]
        AuctionCreated = 2,

        [Description("TRANSIT")]
        InTransit = 3,

        [Description("STOP")]
        Stop = 3,

        [Description("PUAGRD")]
        PurchaseAgreed = 13,

        [Description("ASSIGNWAREHOUSE")]
        ASSIGNWAREHOUSE = 14,

        [Description("VehicleReadyForCollection")]
        VehicleReadyForCollection = 20



    }
}
