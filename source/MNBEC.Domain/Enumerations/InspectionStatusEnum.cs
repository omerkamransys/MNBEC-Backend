using System.ComponentModel;

namespace MNBEC.Domain.Enumerations
{
    /// <summary>
    /// InspectionStatusEnum hold values from InspectionStatus Table with Id Mapping.
    /// </summary>
    public enum InspectionStatusEnum : int
    {
        [Description("RFINSP")]
        ReadyForInspection = 1,

        [Description("INPRG")]
        InspectionInProgress = 2,

        [Description("INSCOMPLET")]
        InspectionComplete = 3,
        
        [Description("PQR")]
        PriceQuoteRequired = 4,

        [Description("PQS")]
        PriceQuoteSent = 5,

        [Description("PRCREQ")]
        PriceUpdateRequested = 6,

        [Description("PRCSENT")]
        PriceUpdateSent = 7,

        [Description("PRCREJ")]
        PriceUpdateRejected = 8,


        [Description("RFP")]
        ReadyForPurchase = 9,

        [Description("RFV")]
        ReadyForVerification = 10,

        [Description("IVS")]
        InspectionVerificationScheduled = 11,

        [Description("IVS")]
        InspectionVerificationInProgress = 12,

        [Description("IV")]
        Verified = 13,

        [Description("PR")]
        Purchased = 14,

        [Description("INSCANCEL")]
        InspectionCancelled = 15,

        [Description("QCINSCOMPL")]
        QCInspectionComplete = 19,


        [Description("NOTPURCHASED")]
        NotPurchased = 25,

        [Description("AGRESIGND")]
        AggrementSigned = 26,

        [Description("AGRESIGNDNOT")]
        AggrementNotSigned = 27,

        [Description("SLSIGNED")]
        SideLetterSigned = 28,

        [Description("V2ABORT")]
        V2Aborted = 29,

        [Description("SLSIGNEDV2")]
        SideLetterSignedV2 = 30,

        [Description("RFQC")]
        ReadyForQC = 31,

        [Description("QCABORT")]
        QCAborted = 32,

        [Description("QCRESCHLD")]
        QCRescheduled = 33,

        [Description("PLSIGN")]
        PLSIGN = 34


    }
}
