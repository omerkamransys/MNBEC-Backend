using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// InspectionStatusEnum hold values from InspectionStatus Table with Id Mapping.
    /// </summary>
    public enum WarehouseVehiclePurchaseStatusEnum : uint
    {
        
        [Description("TRANSIT")]
        InTransit = 1,

        [Description("CHECKIN")]
        CHECKIN = 2



    }
}
