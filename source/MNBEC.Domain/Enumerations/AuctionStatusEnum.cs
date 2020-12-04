using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    //TODO: Correct case for Enum data
    /// <summary>
    /// PaymentTypeEnum hold values from PaymentType Table with Id Mapping.
    /// </summary>
    public enum AuctionStatusEnum
    {
        [Description("Created")]
        CREATED = 1,

        [Description("Scheduled")]
        SCHEDULED = 2,

        [Description("Cancelled")]
        CANNCELLED = 3,

        [Description("Completed")]
        COMPLETED = 4,


        [Description("InTransit")]
        TRANSIT = 5,

        [Description("Live")]
        LIVE = 6,


        [Description("Stop")]
        STOP = 7,

        [Description("Sold")]
        SOLD = 8,

        [Description("BuyNow")]
        BUYNOW = 9,

        [Description("AuctionWon")]
        AUCTIONWON = 11,

        [Description("AuctionLost")]
        AUCTIONLOST = 12

    }
}
