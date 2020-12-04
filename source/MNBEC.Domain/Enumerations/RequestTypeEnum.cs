using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MNBEC.Domain.Enumerations
{
    public enum RequestTypeEnum
    {
        [Description("Post")]
        Post = 1,

        [Description("Put")]
        Put = 2,

    }
}

