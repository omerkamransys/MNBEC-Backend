using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// PaymentTypeEnum hold values from PaymentType Table with Id Mapping.
    /// </summary>
    public enum DocumentTypeEnum
    {
        [Description("VIMG")]
        VehicleImages = 1,

        [Description("CHKPOINT"),DisplayName("CheckpointItem")]
        CHKPOINT = 26,

        [Description("SL_SIGN")]
        SideLetter = 42,

        [Description("AUCTION"), DisplayName("Auction")]
        AUCTION = 100,

        [Description("ACF")]
        AcceptanceForm = 60,

        [Description("DAPL")]
        DeliveryAndPurchaseLetter = 61


    }
}
