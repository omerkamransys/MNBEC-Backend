using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    //TODO: Correct case for Enum data
    /// <summary>
    /// ExternalAuctionStatusEnum hold values from Status Type for External Auctions
    /// </summary>
    public enum ExternalAuctionStatusEnum
    {
        [Description("Sold")]
        Sold = 1,

        [Description("NotSold")]
        NotSold = 2,

        [Description("BuyNow")]
        BuyNow = 3

    }
}
