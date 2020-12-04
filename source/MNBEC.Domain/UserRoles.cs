using System;
using System.Collections.Generic;
using System.Text;
using MNBEC.Domain.Common;

namespace MNBEC.Domain
{
    public class UserRoles : BaseDomain
    {
        public uint RoleId { get; set; }
        public uint UserId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameTranslation { get; set; }

    }
}
