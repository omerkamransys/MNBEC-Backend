using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// AppointmentSourceEnum hold values from AppointmentSource Table with Id Mapping.
    /// </summary>
    public enum AppointmentSourceEnum
    {
        [Description("CUSTOMER")]
        CustomerPortal = 1,

        [Description("WALKIN")]
        WalkinCustomer = 2,

        [Description("DIALIN")]
        DialinCustomer = 3,

        [Description("INVITATION")]
        CustomerInvitation = 4,

        [Description("OTHER")]
        Other = 5
    }
}
