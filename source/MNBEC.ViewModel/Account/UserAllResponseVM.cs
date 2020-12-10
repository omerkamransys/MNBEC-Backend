using System;
using System.Collections.Generic;
using System.Text;
using MNBEC.Domain;

namespace MNBEC.ViewModel.Account
{
   public class UserAllResponseVM
    {
        public int UserId { get; set; }
        public int UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public bool Active { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public IList<UserRoles> UserRoles { get; set; }
    }
}
