using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MNBEC.ViewModel.Account
{
    public class RoleResponseVM
    {
        public uint RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameTranslation { get; set; }
        public string RoleNameCode { get; set; }
        public IList<ClaimGroupResponseVM> ClaimGroups { get; set; }
        public bool Active { get; set; }
    }
}
