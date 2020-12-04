using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MNBEC.Domain.Enumerations
{
    public enum UserTypeEnum
    {
        [Description("001"), DisplayName("001")]
        Administrator = 1,

        [Description("002"), DisplayName("002")]
        Inspector = 2,
    }
}
