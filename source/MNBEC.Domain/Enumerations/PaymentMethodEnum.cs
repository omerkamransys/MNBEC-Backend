using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// PaymentMethodEnum hold values from PaymentMethod Table with Id Mapping.
    /// </summary>
    public enum PaymentMethodEnum
    {
        [Description("CASH")]
        Cash = 1,

        [Description("PO")]
        PayOrder = 2,

        [Description("CHEQUE")]
        Cheque = 3,

        [Description("DD")]
        BankDraft = 4,

        [Description("BT")]
        BankTransfer = 5
    }
}
