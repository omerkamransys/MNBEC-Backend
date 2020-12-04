using System;
using System.Collections.Generic;
using System.Text;
using MNBEC.ViewModel.Common;

namespace MNBEC.ViewModel.Account
{
   public class UserAllRequestVM : BaseAllRequestVM
    {
        public uint UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public uint UserTypeId { get; set; }
        public uint RoleId { get; set; }
        public bool? ActiveColumn { get; set; }
    }
}
