using System;
using System.Collections.Generic;
using System.Text;

namespace MNBEC.ViewModel.Account
{
   public class RoleAllResponseVM
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string NormalizedRoleName { get; set; }
        public bool Active { get; set; }
    }
}
