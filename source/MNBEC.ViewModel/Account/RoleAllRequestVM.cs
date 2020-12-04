using System;
using System.Collections.Generic;
using System.Text;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account
{
   public class RoleAllRequestVM : BaseAllRequestVM
    {
        public uint RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
