using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MNBEC.Domain.Enumerations
{
    public enum NotificationEnum
    {
        [Description("DealershipRegistration")]
        DealershipRegistration = 1,

        [Description("PartnerRegistration")]
        PartnerRegistration = 2,

        [Description("DealerUserInvite")]
        DealerUserInvite = 3,

        [Description("DealerUserRegistration")]
        DealerUserRegistration = 4,

        [Description("AuctionStart")]
        AuctionStart = 5,

        [Description("DealerBid")]
        DealerBid = 6
    }
}
