using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.ViewModel.Account
{
   public class RoleAllResponseVM
    {
        public uint RoleId { get; set; }
        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }
        public bool Active { get; set; }
    }
}
