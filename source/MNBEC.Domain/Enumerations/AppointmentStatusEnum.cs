using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// AppointmentStatusEnum hold values from AppointmentStatus Table with Id Mapping.
    /// </summary>
    public enum AppointmentStatusEnum
    {
        [Description("BOOKED")]
        Booked = 1,

        [Description("INPROCESS")]
        InProcess = 2,

        [Description("COMPLETED")]
        Completed = 3,

        [Description("RESCHDULED")]
        Rescheduled = 4,

        [Description("CANCELLED")]
        Cancelled = 5,

        [Description("ASSIGNED")]
        Assigned = 6,

        [Description("REJECTED")]
        Rejected = 7,

        [Description("NOSHOW")]
        NoShow = 8,
    }
}
