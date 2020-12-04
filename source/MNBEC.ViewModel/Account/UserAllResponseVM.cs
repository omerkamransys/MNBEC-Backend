using System;
using System.Collections.Generic;
using System.Text;
using MNBEC.Domain;

namespace MNBEC.ViewModel.Account
{
   public class UserAllResponseVM
    {
        public uint UserId { get; set; }
        public uint UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public uint UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public bool Active { get; set; }
        public string RoleName { get; set; }
        public uint RoleId { get; set; }
        public IList<UserRoles> UserRoles { get; set; }
    }
}
