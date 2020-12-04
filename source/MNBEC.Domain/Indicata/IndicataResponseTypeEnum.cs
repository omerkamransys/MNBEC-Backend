using System.ComponentModel;

namespace MNBEC.Domain.Indicata
{
    /// <summary>
    /// IndicataResponseTypeEnum hold values from IndicataResponseType with Id Mapping.
    /// </summary>
    public enum IndicataResponseTypeEnum
    {
        [Description("category")]
        Category = 1,

        [Description("make")]
        Make = 2,

        [Description("model")]
        Model = 3,

        [Description("regmonth")]
        Month = 4,

        [Description("regdate")]
        Year = 5,

        [Description("body")]
        Body = 6,

        [Description("facelift")]
        Facelift = 7,

        [Description("seats")]
        Seat = 8,

        [Description("engine")]
        Engine = 9,

        [Description("wheeldrive")]
        WheelDrive = 10,

        [Description("transmission")]
        Transmission = 11,

        [Description("trim")]
        TrimLevel = 12,

        [Description("")]
        TrimLevelDetail = 13,

        [Description("")]
        Valuation = 14,

        [Description("year not found")]
        YearNotFound = 15,

        [Description("Unidentified")]
        Unidentified = 16,

        [Description("ValuationLimitExceeded")]
        ValuationLimitExceeded = 17,
    }
}
