using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// PaymentTypeEnum hold values from PaymentType Table with Id Mapping.
    /// </summary>
    public enum PaymentTypeEnum
    {
        [Description("TOKEN")]
        Token = 1,

        [Description("FINALPAY")]
        FinalPayment = 2,

        [Description("TOKENRTN")]
        TokenReturn = 3,

        [Description("TOKENAMTRTN")]
        TokenAmountReturn = 4
    }
}
