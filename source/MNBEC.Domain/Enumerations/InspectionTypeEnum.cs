using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// InspectionTypeEnum hold values from InspectionType Table with Id Mapping.
    /// </summary>
    public enum InspectionTypeEnum
    {
        [Description("INP")]
        InspectionV1 = 1,

        [Description("REINP")]
        InspectionV2 = 2,

        [Description("QC")]
        InspectionQC = 3
    }
}
