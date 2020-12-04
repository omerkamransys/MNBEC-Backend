using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// AppointmentTypeEnum hold values from AppointmentType Table with Id Mapping.
    /// </summary>
    public enum AppointmentTypeEnum
    {
        [Description("APP")]
        AppointmentV1 = 1,

        [Description("REAPP")]
        AppointmentV2 = 2,
       
    }
}
